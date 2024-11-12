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
		float rot = 0;
		switch( noteData._cutDirection )
		{
			case 0:
				rot = 0;
				break;
			case 1:
				rot = 180;
				break;
			case 2:
				rot = -90;
				break;
			case 3:
				rot = 90;
				break;
			case 4:
				rot = -45;
				break;
			case 5:
				rot = 45;
				break;
			case 6:
				rot = -145;
				break;
			case 7:
				rot = 145;
				break;
		}
		LocalRotation = new Angles( 0, 0, rot );
	}
	protected override void OnFixedUpdate()
	{
		LocalPosition += Vector3.Backward * NoteSpeed * Time.Delta;
	}
}
