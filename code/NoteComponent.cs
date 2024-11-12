using System;
using Sandbox;

public sealed class NoteComponent : Component
{
	[Property] public Color LeftNoteColor { get; set; }
	[Property] public Color RightNoteColor { get; set; }
	[Property]public ModelRenderer Model { get; set; }
	public Note noteData { get; set; }
	public float NoteSpeed { get; set; } = 300f;
	
	protected override void OnStart()
	{
		//Apply note color
		if( noteData._type == 0 ) Model.Tint = LeftNoteColor;
		else Model.Tint = RightNoteColor;
		//Apply note rotation
		int rot = ToRotation(noteData._cutDirection);
		LocalRotation = new Angles( 0, 0, rot );
	}
	protected override void OnFixedUpdate()
	{
		LocalPosition += Vector3.Backward * NoteSpeed * Time.Delta;
	}

	private static int ToRotation(float cutDirection) => cutDirection switch
	{
		0 => 0,
		1 => 180,
		2 => -90,
		3 => 90,
		4 => -45,
		5 => 45,
		6 => -135, // was -145
		7 => 135,  // was 145
		_ => throw new ArgumentOutOfRangeException(nameof(cutDirection), cutDirection, null),
	};
}
