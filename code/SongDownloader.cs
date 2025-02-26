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
		try
		{
			var page = "0";
			var SortOrder = "Relevance";
			var Url = $"https://api.beatsaver.com/search/text/{page}?leaderboard=All&q={Query}&sortOrder={SortOrder}";
			var response = await Http.RequestStringAsync( Url );
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
		catch (Exception e)
		{
			Log.Warning( "Search Error " + e.Message );
		}
	}
	public async Task DownloadSong()
	{
		WebInfo.Name = WebInfo.Name.Replace( "/", "" );
		WebInfo.Name = WebInfo.Name.Replace( @"\", "" );
		WebInfo.Name = WebInfo.Name.Replace( "*", "" );
		WebInfo.Name = WebInfo.Name.Replace( "?", "" );
		WebInfo.Name = WebInfo.Name.Replace( ":", "" );
		WebInfo.Name = WebInfo.Name.Replace( "\"", "" );
		WebInfo.Name = WebInfo.Name.Replace( "<", "" );
		WebInfo.Name = WebInfo.Name.Replace( ">", "" );
		WebInfo.Name = WebInfo.Name.Replace( "|", "" );
		SongFolder = $"songs/{WebInfo.Name + " - " + WebInfo.Id}";
		if ( !FileSystem.Data.DirectoryExists( SongFolder ) )
		{
			FileSystem.Data.CreateDirectory( SongFolder );
			
			// Download the Zip from the url and extract the data
			var songUrl = WebInfo.Versions.First().DownloadURL;

			var songFileData = await Http.RequestBytesAsync( songUrl );
			try
			{
				ZipArchive zip = new(new MemoryStream(songFileData));
				foreach (var e in zip.Entries)
				{
					var name = e.FullName;
					if( e.FullName.EndsWith(".egg") ) name = e.FullName.Replace(".egg", ".ogg");
					if( e.FullName.EndsWith(".dat") ) name = e.FullName.Replace(".dat", ".json");
					var path = SongFolder + "/" + name;
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
				Log.Error( "Error downloading song " + e.Message  );
				return;
			}
		}
		
		var diffSelection = WebInfo.Versions.First().Difficulties.Last();
		var songInfoPath = FileSystem.Data.FindFile( SongFolder ).First( i => i.ToLower().Contains( "info" ) );

		try
		{
			Info = SongInfo.Read( await FileSystem.Data.ReadAllTextAsync( SongFolder + $"/{songInfoPath}" ) );
		}
		catch
		{
			Log.Warning( "Song Info not found" );
			return;
		}

		try
		{
			InstalledSongs.Add( Info.SongName );
		}
		catch
		{
			Game.IsPaused = true;
			Log.Warning( "Couldn't add song to install list" );
			return;
		}
		
        var songFilePath = FileSystem.Data.FindFile(SongFolder).First( i => i.ToLower().Contains(diffSelection.Name.ToLower()) );
        songFilePath = SongFolder + $"/{songFilePath}";
        
        try
        {
	        Chart = SongChart.Read( await FileSystem.Data.ReadAllTextAsync( songFilePath ) );
        }
        catch
        { 
	        Log.Warning( "Chart does not exist" );
	        return;
        }
        if ( Chart?.Notes == null )
        {
	        Log.Warning( "No notes found" );
	        return;
        }
        if ( Info.BPM <= 0 )
        {
	        Log.Warning( "Info Error" );
	        return;
        }
        
        AudioPath = SongFolder + "/" + FileSystem.Data.FindFile( SongFolder ).First( x => x.EndsWith( ".ogg" ) );
	}
}
