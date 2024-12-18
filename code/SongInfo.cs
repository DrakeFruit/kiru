using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kiru;
public class SongInfo
{
    [JsonPropertyName("_version")]
    public string Version { get; set; }
    [JsonPropertyName("version")]
    public string VersionNew { set { Version = value; } }
    [JsonPropertyName("_songName")]
    public string SongName { get; set; }
    [JsonPropertyName("song")]
    public TitleList Title { get; set; }
    public class TitleList
    {
	    [JsonPropertyName( "title" )]
	    public string TitleNew { get; set; }
    }
    [JsonPropertyName("title")]
    public string SongNameNew { set { SongName = value; } }
    [JsonPropertyName("_songSubName")]
    public string SongSubName { get; set; }
    [JsonPropertyName("subtitle")]
    public string SongSubNameNew { set { SongSubName = value; } }
    [JsonPropertyName("_songAuthorName")]
    public string SongAuthorName { get; set; }
    [JsonPropertyName("author")]
    public string SongAuthorNameNew { set { SongAuthorName = value; } }
    [JsonPropertyName("_levelAuthorName")]
    public string LevelAuthorName { get; set; }
    // [JsonPropertyName("author")]
    // public string LevelAuthorNameNew { set { LevelAuthorName = value; } }
    [JsonPropertyName("audio")]
    public AudioInfo Audio { get; set; }
    public class AudioInfo
    {
	    [JsonPropertyName( "bpm" )]
	    public float BPMNew { get; set; }
    }
    [JsonPropertyName("_beatsPerMinute")]
    public float BPM { get; set; }
    [JsonPropertyName("_songTimeOffset")]
    public float SongTimeOffset { get; set; }
    // [JsonPropertyName("_songTimeOffset")]
    // public float SongTimeOffsetNew { set { SongTimeOffset = value; } }
    [JsonPropertyName("_shuffle")]
    public float Shuffle { get; set; }
    // [JsonPropertyName("_shuffle")]
    // public float ShuffleNew { set { Shuffle = value; } }
    [JsonPropertyName("_shufflePeriod")]
    public float ShufflePeriod { get; set; }
    // [JsonPropertyName("_shufflePeriod")]
    // public float ShufflePeriodNew { set { ShufflePeriod = value; } }
    [JsonPropertyName("_previewStartTime")]
    public float PreviewStartTime { get; set; }
    // [JsonPropertyName("_previewStartTime")]
    // public float PreviewStartTimeNew { set { PreviewStartTime = value; } }
    [JsonPropertyName("_previewDuration")]
    public float PreviewDuration { get; set; }
    // [JsonPropertyName("_previewDuration")]
    // public float PreviewDurationNew { set { PreviewDuration = value; } }
    [JsonPropertyName("_songFilename")]
    public string SongFilename { get; set; }
    // [JsonPropertyName("_songFilename")]
    // public string SongFilenameNew { set { SongFilename = value; } }
    [JsonPropertyName("_coverImageFilename")]
    public string CoverImageFilename { get; set; }
    // [JsonPropertyName("_coverImageFilename")]
    // public string CoverImageFilenameNew { set { CoverImageFilename = value; } }
    [JsonPropertyName("_environmentName")]
    public string EnvironmentName { get; set; }
    // [JsonPropertyName("_environmentName")]
    // public string EnvironmentNameNew { set { EnvironmentName = value; } }
    [JsonPropertyName("_allDirectionsEnvironmentName")]
    public string AllDirectionsEnvironmentName { get; set; }
    // [JsonPropertyName("_allDirectionsEnvironmentName")]
    // public string AllDirectionsEnvironmentNameNew { set { AllDirectionsEnvironmentName = value; } }
    [JsonPropertyName("_difficultyBeatmapSets")]
    public List<DifficultyBeatmapSet> DifficultyBeatmapSets { get; set; }
    public class DifficultyBeatmapSet
    {
        [JsonPropertyName("_beatmapCharacteristicName")]
        public string BeatmapCharacteristicName { get; set; }
        [JsonPropertyName("_difficultyBeatmaps")]
        public List<DifficultyBeatmap> DifficultyBeatmaps { get; set; }
        public class DifficultyBeatmap
        {
            [JsonPropertyName("_difficulty")]
            public string Difficulty { get; set; }
            [JsonPropertyName("_difficultyRank")]
            public int DifficultyRank { get; set; }
            [JsonPropertyName("_beatmapFilename")]
            public string BeatmapFilename { get; set; }
            [JsonPropertyName("_noteJumpMovementSpeed")]
            public float NoteJumpMovementSpeed { get; set; }
            [JsonPropertyName("_noteJumpStartBeatOffset")]
            public float NoteJumpStartBeatOffset { get; set; }
            [JsonPropertyName("_beatmapColorSchemeIdx")]
            public int BeatmapColorSchemeIdx { get; set; }
            [JsonPropertyName("_environmentNameIdx")]
            public int EnvironmentNameIdx { get; set; }
        }
    }
    public static SongInfo Read(string jsonAsString)
    {
	    var parsedInfo = JsonSerializer.Deserialize<SongInfo>(jsonAsString);
	    if ( parsedInfo.BPM <= 0 ) parsedInfo.BPM = parsedInfo.Audio.BPMNew;
	    if ( parsedInfo.SongName == null ) parsedInfo.SongName = parsedInfo.Title.TitleNew;
	    return parsedInfo;
    }
}
