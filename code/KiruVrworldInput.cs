using System.Numerics;

namespace Sandbox;

[Title( "Kiru VR World Input" ), Category( "VR" ), Icon( "flip_camera_android" )]
public sealed class KiruVRWorldInput : Component
{
	UI.WorldInput worldInput = new();

	/// <summary>
	/// Represents a controller to use
	/// </summary>
	public enum HandSources
	{
		/// <summary>
		/// The left controller
		/// </summary>
		Left,

		/// <summary>
		/// The right controller
		/// </summary>
		Right
	}

	/// <summary>
	/// Which hand should we use?
	/// </summary>
	[Property]
	public HandSources HandSource { get; set; } = HandSources.Left;

	/// <summary>
	/// How much should we dampen movement when the trigger is pressed?
	/// </summary>
	[Property, Range( 0.0f, 10.0f )]
	public float DampingFactor { get; set; } = 0.0f;

	protected override void OnEnabled()
	{
		worldInput.Enabled = true;
	}

	protected override void OnDisabled()
	{
		worldInput.Enabled = false;
	}

	private float GetTriggerValue()
	{
		return HandSource switch
		{
			HandSources.Left => Input.VR.LeftHand.Trigger,
			HandSources.Right => Input.VR.RightHand.Trigger,

			_ => 0.0f
		};
	}

	private Vector2 GetScrollValue()
	{
		return HandSource switch
		{
			HandSources.Left => Input.VR.LeftHand.Joystick,
			HandSources.Right => Input.VR.RightHand.Joystick,

			_ => Vector2.Zero
		};
	}

	private Ray TargetRay;

	protected override void OnUpdate()
	{
		if ( !Game.IsRunningInVR )
			return;

		//
		var triggerValue = GetTriggerValue();
		var triggerPressed = triggerValue > 0.5f;

		//
		var scrollValue = GetScrollValue();

		//
		var currentRay = new Ray( WorldPosition, WorldRotation.Forward );
		var delta = triggerValue.Remap( 1.0f, 0.5f );
		delta = delta.Clamp( 0.1f * DampingFactor, 1.0f );

		TargetRay.Position = TargetRay.Position.LerpTo( currentRay.Position, delta );
		TargetRay.Forward = TargetRay.Forward.LerpTo( currentRay.Forward, delta );
		//SceneTraceResult tr = Scene.Trace.Ray( currentRay, 1024f ).IgnoreGameObjectHierarchy( GameObject.Parent ).Run();

		worldInput.Ray = TargetRay;
		worldInput.MouseLeftPressed = triggerPressed;
		worldInput.MouseRightPressed = false;
		worldInput.MouseWheel = scrollValue;
		
		if ( worldInput.Hovered == null ) Gizmo.Draw.Line( WorldPosition, WorldRotation.Forward * 1024f );
		if ( !worldInput.Hovered.IsValid() ) return;
		var panel = worldInput.Hovered.FindRootPanel() as UI.WorldPanel;
		if ( panel.IsValid() && panel.RayToLocalPosition( TargetRay, out var position, out var distance ) )
		{
			Plane plane = new( panel.Position, panel.Rotation.Forward );
			if ( plane.TryTrace( TargetRay, out Vector3 hitPoint, true ) )
			{
				Gizmo.Draw.Line( WorldPosition, hitPoint );
			}
		}
	}
}
