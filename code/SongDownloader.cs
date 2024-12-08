using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Kiru;
public sealed class SongDownloader : Component
{
	public SongParser Parser { get; set; }
	public WebSongInfo.SearchResults Results { get; set; }
	public WebSongInfo WebInfo { get; set; }
	public SongChart Chart { get; set; }
	public SongInfo Info { get; set; }
	public List<string> InstalledSongs { get; set; } = new List<string>();
	public string SongFolder;
	public string AudioPath;
	protected override void OnStart()
	{
		Parser = Components.GetInChildrenOrSelf<SongParser>();
		foreach ( var i in FileSystem.Data.FindDirectory( "songs" ) )
		{
			InstalledSongs.Add( i );
		}
	}
	public async void GetSearchResults( string Query )
	{
		string page = "0";
		string SortOrder = "Relevance";
		string Url = $"https://api.beatsaver.com/search/text/{page}?leaderboard=All&q={Query}&sortOrder={SortOrder}";
		string response = await Http.RequestStringAsync( Url );
		Results = WebSongInfo.ReadList( response );
		// I have no fucking clue how pages work, just check the next page if 0 is empty?
		if ( Results.InfoList.Count == 0 )
		{
			page = "1";
			Url = $"https://api.beatsaver.com/search/text/{page}?leaderboard=All&q={Query}&sortOrder=Rating";
			response = await Http.RequestStringAsync( Url );
			Results = WebSongInfo.ReadList( response );
		}
	}
	public async Task DownloadSong()
	{
		SongFolder = $"songs/{WebInfo.Name}";
		if ( !FileSystem.Data.DirectoryExists( SongFolder ) )
		{
			FileSystem.Data.CreateDirectory( SongFolder );
			
			// Download the Zip from the url and extract the data
			string songUrl = WebInfo.Versions.First().DownloadURL;

			byte[] songFileData = await Http.RequestBytesAsync( songUrl );
			try
			{
				ZipArchive zip = new(new MemoryStream(songFileData));
				foreach (var e in zip.Entries)
				{
					string name = e.FullName;
					if( e.FullName.EndsWith(".egg") ) name = e.FullName.Replace(".egg", ".ogg");
					if( e.FullName.EndsWith(".dat") ) name = e.FullName.Replace(".dat", ".json");
					string path = SongFolder + "/" + name;
					Log.Info("Saving file " + path);
					Stream zipStream = e.Open();
					Stream fileStream = FileSystem.Data.OpenWrite(path);
					await zipStream.CopyToAsync(fileStream);
					fileStream.Close();
					zipStream.Close();
				}
			}
			catch (Exception e)
			{
				Log.Error("Error downloading song " + e.Message);
			}
		}
		
		var diffSelection = WebInfo.Versions.First().Difficulties.Last();
        Info = SongInfo.Read( FileSystem.Data.ReadAllText( SongFolder + "/Info.json" ) );
        InstalledSongs.Add(Info.SongName);
        string songFilePath = SongFolder + "/" + diffSelection.Name;
        
        // There has to be a better way to do this, don't do this
        try { Chart = SongChart.Read( FileSystem.Data.ReadAllText( $"{songFilePath}.json" ) ); }
        catch
        {
	        try
	        {
		        songFilePath += diffSelection.Characteristic;
		        Chart = SongChart.Read( FileSystem.Data.ReadAllText( $"{songFilePath}.json" ) );
		        if ( Chart.NotesNew.Any() ) Chart.Notes = Chart.NotesNew;
	        }
	        catch {Log.Warning("Chart does not exist");}
        }

        if ( Chart.Notes == null )
        {
	        Log.Warning("Chart uses custom song data, not yet supported");
	        return;
        }
        AudioPath =  SongFolder + "/" +  FileSystem.Data.FindFile( SongFolder ).First( x => x.EndsWith( ".ogg" ) );
	}
}
