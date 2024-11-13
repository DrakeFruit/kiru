using System.Text.Json;

namespace Kiru;
public class SongInfo
{
    public string _version { get; set; }
    public string _songName { get; set; }
    public string _songSubName { get; set; }
    public string _songAuthorName { get; set; }
    public string _levelAuthorName { get; set; }
    public float _beatsPerMinute { get; set; }
    public float _songTimeOffset { get; set; }
    public float _shuffle { get; set; }
    public float _shufflePeriod { get; set; }
    public float _previewStartTime { get; set; }
    public float _previewDuration { get; set; }
    public string _songFilename { get; set; }
    public string _coverImageFilename { get; set; }
    public string _environmentName { get; set; }
    public string _allDirectionsEnvironmentName { get; set; }
    public List<DifficultyBeatmapSet> _difficultyBeatmapSets { get; set; }
    public class DifficultyBeatmapSet
    {
        public string _beatmapCharacteristicName { get; set; }
        public List<DifficultyBeatmap> _difficultyBeatmaps { get; set; }
        public class DifficultyBeatmap
        {
            public string _difficulty { get; set; }
            public int _difficultyRank { get; set; }
            public string _beatmapFilename { get; set; }
            public float _noteJumpMovementSpeed { get; set; }
            public float _noteJumpStartBeatOffset { get; set; }
            public int _beatmapColorSchemeIdx { get; set; }
            public int _environmentNameIdx { get; set; }
        }
    }

    public static SongInfo Read( string JsonAsString )
    {
    	return JsonSerializer.Deserialize<SongInfo>( JsonAsString );
    }
}
