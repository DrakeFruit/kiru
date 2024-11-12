using System.Text.Json;

namespace Kiru;
public class Song
{
	public string _version { get; set; }
	public List<float> _BPMChanges { get; set; }
	public List<Event> _events { get; set; }
	public List<Note> _notes { get; set; }
	public List<Obstacle> _obstacles { get; set; }
    public class Event
    {
    	public float _time { get; set; }
    	public int _type { get; set; }
    	public int _value { get; set; }
    }

    public class Note
    {
    	public float _time { get; set; }
    	public int _type { get; set; }
    	public int _value { get; set; }
    	public int _lineIndex { get; set; }
    	public int _lineLayer { get; set; }
    	public int _cutDirection { get; set; }
    }

    public class Obstacle
    {
    	public float _time { get; set; }
    	public float _duration { get; set; }
    	public int _lineIndex { get; set; }
    	public int _type { get; set; }
    	public int _width { get; set; }
    }

    public static Song Read( string JsonAsString )
	{
		return JsonSerializer.Deserialize<Song>( JsonAsString );
	}
}