using System;
using Sandbox;

namespace Kiru;
public sealed class NoteComponent : Component
{
	[Property] public ModelRenderer Model { get; set; }
	public SongParser Parser { get; set; }
	public SongChart.Note noteData { get; set; }
	public int Angle { get; set; }
	public float NoteSpeed { get; set; } = 300f;
	protected override void OnStart()
	{
		Parser = Scene.GetComponentInChildren<SongParser>();
		// Apply note color
		if( noteData.Type == 0 ) Model.Tint = Parser.LeftNoteColor;
		else Model.Tint = Parser.RightNoteColor;
		// Apply note rotation
		if ( noteData.CutDirection != 8 )
		{
			Angle = ToRotation( noteData.CutDirection );
		}
		else Angle = 0;
		LocalRotation = new Angles( 0, 0, Angle );
	}
	protected override void OnFixedUpdate()
	{
		if ( SongParser.IsSongPlaying )
		{
			LocalPosition += Vector3.Backward * NoteSpeed * Time.Delta;
			if ( LocalPosition.x <= -64 )
			{
				Sound.Play( SongParser.MissSoundEvent );
				GameObject.Destroy();
			}
		}
	}

	private static int ToRotation(float cutDirection) => cutDirection switch
	{
		0 => 0,
		1 => 180,
		2 => -90,
		3 => 90,
		4 => -45,
		5 => 45,
		6 => -145,
		7 => 145,
		_ => throw new ArgumentOutOfRangeException(nameof(cutDirection), cutDirection, null),
	};
}
