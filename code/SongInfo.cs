using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kiru;
public class SongInfo
{
    [JsonPropertyName("_version")]
    public string Version { get; set; }
    [JsonPropertyName("_songName")]
    public string SongName { get; set; }
    [JsonPropertyName("_songSubName")]
    public string SongSubName { get; set; }
    [JsonPropertyName("_songAuthorName")]
    public string SongAuthorName { get; set; }
    [JsonPropertyName("_levelAuthorName")]
    public string LevelAuthorName { get; set; }
    [JsonPropertyName("_beatsPerMinute")]
    public float BPM { get; set; }
    [JsonPropertyName("_songTimeOffset")]
    public float SongTimeOffset { get; set; }
    [JsonPropertyName("_shuffle")]
    public float Shuffle { get; set; }
    [JsonPropertyName("_shufflePeriod")]
    public float ShufflePeriod { get; set; }
    [JsonPropertyName("_previewStartTime")]
    public float PreviewStartTime { get; set; }
    [JsonPropertyName("_previewDuration")]
    public float PreviewDuration { get; set; }
    [JsonPropertyName("_songFilename")]
    public string SongFilename { get; set; }
    [JsonPropertyName("_coverImageFilename")]
    public string CoverImageFilename { get; set; }
    [JsonPropertyName("_environmentName")]
    public string EnvironmentName { get; set; }
    [JsonPropertyName("_allDirectionsEnvironmentName")]
    public string AllDirectionsEnvironmentName { get; set; }
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
        return JsonSerializer.Deserialize<SongInfo>(jsonAsString);
    }
}
