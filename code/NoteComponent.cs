using Sandbox;

public sealed class NoteComponent : Component
{
	[Property] public Color LeftNoteColor { get; set; }
	[Property] public Color RightNoteColor { get; set; }
	[Property]public ModelRenderer model { get; set; }
	public Note noteData { get; set; }
	public float NoteSpeed { get; set; } = 300f;
	
	protected override void OnStart()
	{
		//Tint Note
		if( noteData._type == 0 ) model.Tint = LeftNoteColor;
		else model.Tint = RightNoteColor;
	}
	protected override void OnFixedUpdate()
	{
		LocalPosition += Vector3.Backward * NoteSpeed * Time.Delta;
	}
}
