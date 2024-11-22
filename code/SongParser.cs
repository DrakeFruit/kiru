using Sandbox.Audio;

namespace Kiru;
public sealed class SongParser : Component
{
	[Property] public float ScrollSpeed { get; set; } = 1500;
	[Property] GameObject NotePrefab { get; set; }
	[Property] public float SpawnDistance { get; set; } = 1024;
	[Property] private GameObject StartLine { get; set; }
	[Property] public Color LeftNoteColor { get; set; } = Color.Red;
	[Property] public Color RightNoteColor { get; set; } = Color.Cyan;
	[RequireComponent] SongBrowser browser { get; set; }
	SongChart SongChartData { get; set; }
	SongInfo songInfo { get; set; }
	MusicPlayer musicPlayer { get; set; }
	TimeSince timeSinceStart { get; set; }
	Vector3 SpawnPosition { get; set; }
	bool IsSongPlaying { get; set; }
	int noteCount { get; set; } = 0;
	public float BPM { get; set; }
	public float timeToReach { get; set; } = 0;
	protected override void OnStart()
	{
		SpawnPosition = new Vector3( SpawnDistance, 22, 40 );
	}
	protected override void OnFixedUpdate()
	{
		if( IsSongPlaying )
		{
			var currentBeat = timeSinceStart.Relative * BPM / 60;
			var currentNote = SongChartData._notes[noteCount];
			timeToReach = Vector3.DistanceBetween( SpawnPosition, Vector3.Zero.WithX( StartLine.WorldPosition.x ) ) / ScrollSpeed * BPM / 60;
			if( currentBeat >= currentNote._time - timeToReach )
			{
				//Spawn note prefab, set position, and pass note data
				GameObject no = NotePrefab.Clone( SpawnPosition + new Vector3( 0, currentNote._lineIndex * -16, currentNote._lineLayer * 16 ) );
				NoteComponent co = no.Components.GetOrCreate<NoteComponent>();
				co.noteData = currentNote;
				co.NoteSpeed = ScrollSpeed;

				noteCount++;
			}
		}
	}
	public void PlaySong(SongChart data, SongInfo info, string audio)
	{
		browser.Enabled = false;
		SongChartData = data;
		songInfo = info;
		timeSinceStart = 0;
		BPM = songInfo._beatsPerMinute;

		// Play song using this shit music player
		musicPlayer = MusicPlayer.Play( FileSystem.Data, audio );
		musicPlayer.ListenLocal = true;

		IsSongPlaying = true;
	}
}
