using Sandbox;

public sealed class Grid : Component
{
    [Property] List<GameObject> ColumnOne = new();
    [Property] List<GameObject> ColumnTwo = new();
    [Property] List<GameObject> ColumnThree = new();
    [Property] List<GameObject> ColumnFour = new();
    public List<List<GameObject>> Positions { get; set; }
    protected override void OnStart()
    {
	    Positions = new List<List<GameObject>>();
	    Positions.Add(ColumnOne);
	    Positions.Add(ColumnTwo);
	    Positions.Add(ColumnThree);
	    Positions.Add(ColumnFour);
    }
}
