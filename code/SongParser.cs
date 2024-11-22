namespace Kiru;
public sealed class SongParser : Component
{
	[Property] float ScrollSpeed { get; set; } = 1500;
	[Property] GameObject NotePrefab { get; set; }
	[Property] float SpawnDistance { get; set; } = 1024;
	[Property] private GameObject StartLine { get; set; }
	SongChart SongChartData { get; set; }
	SongInfo songInfo { get; set; }
	MusicPlayer musicPlayer { get; set; }
	TimeSince timeSinceStart { get; set; }
	private float timeSince { get; set; } = 0;
	Vector3 SpawnPosition { get; set; }
	bool IsSongPlaying { get; set; }
	int noteCount { get; set; } = 0;
	float BPM { get; set; }
	float timeToReach { get; set; }
	protected override void OnStart()
	{
		SpawnPosition = new Vector3( SpawnDistance, 48, 32 );
	}
	protected override void OnFixedUpdate()
	{
		if( IsSongPlaying )
		{
			float currentBeat = timeSinceStart.Relative * BPM / 60;
			SongChart.Note currentNote = SongChartData._notes[noteCount];
			timeToReach = Vector3.DistanceBetween( SpawnPosition, Vector3.Zero.WithX( StartLine.LocalPosition.x ) ) / ScrollSpeed * BPM / 60;
			if( currentBeat >= currentNote._time - timeToReach )
			{
				//Spawn note prefab, set position, and pass note data
				GameObject no = NotePrefab.Clone( SpawnPosition + new Vector3( 0, currentNote._lineIndex * -32, currentNote._lineLayer * 32 ) );
				NoteComponent co = no.Components.GetOrCreate<NoteComponent>();
				co.noteData = currentNote;
				co.NoteSpeed = ScrollSpeed;

				noteCount++;
			}
		}
	}
	public void PlaySong(SongChart data, SongInfo info, string audio)
	{
		SongChartData = data;
		songInfo = info;
		timeSinceStart = timeSince;
		BPM = songInfo._beatsPerMinute;

		// Play song using this shit music player
		musicPlayer = MusicPlayer.Play( FileSystem.Data, audio );

		IsSongPlaying = true;
	}

	public void Stop()
	{
		timeSince = timeSinceStart;
		musicPlayer.Stop();
		IsSongPlaying = false;
	}

	public void Pause()
	{
		timeSince = timeSinceStart;
		musicPlayer.Paused = true;
		IsSongPlaying = false;
	}

	public void Unpause()
	{
		timeSinceStart = timeSince;
		musicPlayer.Paused = false;
		IsSongPlaying = true;
	}
}
