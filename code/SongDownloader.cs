using System;
using System.IO;
using System.IO.Compression;

namespace Kiru;
public sealed class SongDownloader : Component
{
    [Property] string mapId { get; set; }
	SongParser parser { get; set; }
	public string songFolder;
	public string Url;
	protected override async void OnStart()
	{
		parser = Components.GetInChildrenOrSelf<SongParser>();
		Url = $"https://api.beatsaver.com/maps/id/{mapId}";

		// Get song info, write to a file if it doesn't exist
		string response = await Http.RequestStringAsync( Url );
		WebSongInfo webSongInfo = WebSongInfo.Read( response );
		songFolder = $"songs/{webSongInfo.name}";
		if( !FileSystem.Data.DirectoryExists( songFolder ) ) FileSystem.Data.CreateDirectory( songFolder );

		// Download the Zip from the url and extract the data
		string songUrl = webSongInfo.versions.First().downloadURL;

		byte[] songFileData = await Http.RequestBytesAsync( songUrl );
        try
        {
            ZipArchive zip = new(new MemoryStream(songFileData));
            foreach (var e in zip.Entries)
            {
                string name = e.FullName;
                if( e.FullName.EndsWith(".egg") ) name = e.FullName.Replace(".egg", ".ogg");
                if( e.FullName.EndsWith(".dat") ) name = e.FullName.Replace(".dat", ".json");
                string path = songFolder + "/" + name;
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

        SongInfo songInfo = SongInfo.Read( songFolder + "/Info.json" );
        //Song song = Song.Read( songFolder + $"/{webSongInfo.versions.First().diffs.First()}.json" );
        //SoundFile audio = SoundFile.Load(FileSystem.Data.FindFile(songFolder).Where(x => x.EndsWith(".ogg")).First());

        //parser.PlaySong(song, songInfo, audio);
	}
}
