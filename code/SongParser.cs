using System.IO;
using Sandbox;
using Sandbox.Audio;

public sealed class SongParser : Component
{
	[Property] float ScrollSpeed { get; set; } = 300;
	[Property] GameObject NotePrefab { get; set; }
	[Property] SoundFile SongFile { get; set; }
	[Property] float SpawnDistance { get; set; } = 1024;
	[Property] float StartLine { get; set; } = 64;
	Song songData { get; set; }
	SongInfo songInfo { get; set; }
	TimeSince timeSinceStart { get; set; }
	Vector3 SpawnPosition { get; set; }
	int BPM { get; set; }
	int noteCount { get; set; } = 0;
	float timeToReach { get; set; }
	protected override void OnStart()
	{
		timeSinceStart = 0;
		SpawnPosition = new Vector3( SpawnDistance, 48, 32 );

		//parse data
		songData = Song.Read();
		songInfo = SongInfo.Read();
		BPM = songInfo._beatsPerMinute;

		//create sound event and play song
		SoundEvent songAudio = new( SongFile.ResourcePath );
		songAudio.UI = true;
		Sound.Play( songAudio );
	}
	protected override void OnFixedUpdate()
	{
		var currentBeat = timeSinceStart.Relative * BPM / 60;
		Note currentNote = songData._notes[noteCount];
		timeToReach = Vector3.DistanceBetween( SpawnPosition, Vector3.Zero.WithY( StartLine ) ) / ScrollSpeed * BPM / 60;
		if( currentBeat >= currentNote._time - timeToReach )
		{
			//Spawn note prefab, set position, and pass note data
			var no = NotePrefab.Clone( SpawnPosition + new Vector3( 0, currentNote._lineIndex * -32, currentNote._lineLayer * 32 ) );
			NoteComponent co = no.Components.GetOrCreate<NoteComponent>();
			co.noteData = currentNote;
			co.NoteSpeed = ScrollSpeed;
			
			noteCount++;
		}
	}
}

//Song data
public class Song
{
	public string _version { get; set; }
	public List<float> _BPMChanges { get; set; }
	public List<Event> _events { get; set; }
	public List<Note> _notes { get; set; }
	public List<Obstacle> _obstacles { get; set; }
	public static Song Read()
	{
		return FileSystem.Mounted.ReadJson<Song>("songs/ExpertPlus.json");
	}
}

public class Event
{
	public float _time { get; set; }
	public int _type { get; set; }
	public int _value { get; set; }
}

public class Note
{
	public float _time { get; set; }
	public int _type { get; set; }
	public int _value { get; set; }
	public int _lineIndex { get; set; }
	public int _lineLayer { get; set; }
	public int _cutDirection { get; set; }
}

public class Obstacle
{
	public float _time { get; set; }
	public float _duration { get; set; }
	public int _lineIndex { get; set; }
	public int _type { get; set; }
	public int _width { get; set; }
}

//Song Info
public class SongInfo
{
	public string _songName { get; set; }
	public int _beatsPerMinute { get; set; }

	public static SongInfo Read()
	{
		return FileSystem.Mounted.ReadJson<SongInfo>("songs/info.json");
	}
}