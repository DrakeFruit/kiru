namespace Kiru;
public sealed class SongParser : Component
{
	[Property] float ScrollSpeed { get; set; } = 1500;
	[Property] GameObject NotePrefab { get; set; }
	[Property] float SpawnDistance { get; set; } = 1024;
	[Property] float StartLine { get; set; } = 64;
	SongChart SongChartData { get; set; }
	SongInfo songInfo { get; set; }
	TimeSince timeSinceStart { get; set; }
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
			timeToReach = Vector3.DistanceBetween( SpawnPosition, Vector3.Zero.WithY( StartLine ) ) / ScrollSpeed * BPM / 60;
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
		timeSinceStart = 0;
		BPM = songInfo._beatsPerMinute;

		// Play song using this shit music player
		MusicPlayer.Play( FileSystem.Data, audio );

		IsSongPlaying = true;
	}
}
