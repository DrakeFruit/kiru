using Sandbox;
using Sandbox.Rendering;

public sealed class DesktopCrosshair : Component
{
	protected override void OnUpdate()
	{
		Scene.Camera.Hud.DrawCircle( Screen.Size / 2, Vector3.One * 5f , Color.White );
	}
}
