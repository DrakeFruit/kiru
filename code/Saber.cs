using Kiru;
using Sandbox;

public sealed class Saber : Component
{
	[Property] MeshComponent Mesh { get; set; }
	[Property] private bool LeftHand { get; set; }
	private SongParser Parser { get; set; }

	protected override void OnStart()
	{
		Parser = Scene.GetComponentInChildren<SongParser>();
		Mesh.Color = LeftHand ? Parser.LeftNoteColor : Parser.RightNoteColor;
	}
	protected override void OnFixedUpdate()
	{
		foreach ( var i in Mesh.Touching )
		{
			if ( i.Tags.Has( "note" ) )
			{
				var note = i.GetComponent<NoteComponent>();
				switch ( note.noteData?.Type )
				{
					case 0 when LeftHand:
						i.DestroyGameObject();
						return;
					case 1 when !LeftHand:
						i.DestroyGameObject();
						return;
				}
			}
		}
	}
}
