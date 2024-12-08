using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kiru;
public class SongChart
{
	// "New" extension is for a weird hack with the multiple song chart types ; "_version" vs "version" in the json file
	[JsonPropertyName("_version")]
	public string Version { get; set; }
	[JsonPropertyName("version")]
	public string VersionNew { set { Version = value; } }
	
	[JsonPropertyName("_BPMChanges")]
	public List<float> BPMChanges { get; set; }
	[JsonPropertyName("bpmEvents")]
	public List<float> BPMChangesNew { set { BPMChanges = value; } }
	
	[JsonPropertyName("_events")]
	public List<Event> Events { get; set; }
	[JsonPropertyName("rotationEvents")]
	public List<Event> EventsNew { set { Events = value; } }
	
	[JsonPropertyName("_notes")]
	public List<Note> Notes { get; set; }
	[JsonPropertyName("colorNotes")]
	public List<Note> NotesNew { get; set; }
	
	[JsonPropertyName("_obstacles")]
	public List<Obstacle> Obstacles { get; set; }
	[JsonPropertyName("obstacles")]
	public List<Obstacle> ObstaclesNew { set { Obstacles = value; } }
	
	public class Event
	{
		[JsonPropertyName("_time")]
		public float Time { get; set; }
		
		[JsonPropertyName("_type")]
		public int Type { get; set; }
		
		[JsonPropertyName("_value")]
		public int Value { get; set; }
	}
	public class Note
	{
		[JsonPropertyName("_time")]
		public float Time { get; set; }
		[JsonPropertyName("b")]
		public float TimeNew { set { Time = value; } }
		
		// [JsonPropertyName("_value")]
		// public int Value { get; set; }
		
		[JsonPropertyName("_lineIndex")]
		public int LineIndex { get; set; }
		[JsonPropertyName("x")]
		public int LineIndexNew { set { LineIndex = value; } }
		
		[JsonPropertyName("_lineLayer")]
		public int LineLayer { get; set; }
		[JsonPropertyName("y")]
		public int LineLayerNew { set { LineLayer = value; } }
		
		[JsonPropertyName("_type")]
		public int Type { get; set; }
		[JsonPropertyName("c")]
		public int TypeNew { set { Type = value; } }
		
		[JsonPropertyName("_cutDirection")]
		public int CutDirection { get; set; }
		[JsonPropertyName("d")]
		public int CutDirectionNew { set { CutDirection = value; } }
	}
	public class Obstacle
	{
		[JsonPropertyName("_time")]
		public float Time { get; set; }
		[JsonPropertyName("_duration")]
		public float Duration { get; set; }
		[JsonPropertyName("_lineIndex")]
		public int LineIndex { get; set; }
		[JsonPropertyName("_type")]
		public int Type { get; set; }
		[JsonPropertyName("_width")]
		public int Width { get; set; }
	}
	public static SongChart Read(string JsonAsString)
	{
		return JsonSerializer.Deserialize<SongChart>(JsonAsString);
	}
}
