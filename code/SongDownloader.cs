namespace Kiru;
public sealed class SongDownloader : Component
{
	SongParser parser { get; set; }
	public string mapId = "1db6";
	public string Url;
	protected override async void OnStart()
	{
		parser = Components.GetInChildrenOrSelf<SongParser>();
		Url = $"https://api.beatsaver.com/maps/id/{mapId}";

		string response = await Http.RequestStringAsync( Url );
		if( !FileSystem.Data.DirectoryExists( "songs" ) ) FileSystem.Data.CreateDirectory( "songs" );
		if( !FileSystem.Data.FileExists( "songs/info.json" ) ) FileSystem.Data.WriteAllText( "songs/info.json", response );
		SongInfo songInfo = SongInfo.Read( response );
		Log.Info(songInfo);
	}
}
