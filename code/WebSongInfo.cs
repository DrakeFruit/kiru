using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kiru;
public class WebSongInfo
{
	[JsonPropertyName("id")] public string Id { get; set; }
	[JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("uploader")]
    public Uploader UploaderInfo { get; set; }
    [JsonPropertyName("metadata")]
    public MetaData Metadata { get; set; }
    [JsonPropertyName("stats")]
    public Statistics Stats { get; set; }
    [JsonPropertyName("uploaded")]
    public string Uploaded { get; set; }
    [JsonPropertyName("automapper")]
    public bool Automapper { get; set; }
    [JsonPropertyName("ranked")]
    public bool Ranked { get; set; }
    [JsonPropertyName("qualified")]
    public bool Qualified { get; set; }
    [JsonPropertyName("versions")]
    public List<Version> Versions { get; set; }
    [JsonPropertyName("createdAt")]
    public string CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")]
    public string UpdatedAt { get; set; }
    [JsonPropertyName("lastPublishedAt")]
    public string LastPublishedAt { get; set; }
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }
    [JsonPropertyName("bookMarked")]
    public bool Bookmarked { get; set; }
    [JsonPropertyName("declaredAi")]
    public string DeclaredAi { get; set; }
    [JsonPropertyName("blRanked")]
    public bool BlRanked { get; set; }
    [JsonPropertyName("blQualified")]
    public bool BlQualified { get; set; }
    public class Uploader
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("admin")]
        public bool Admin { get; set; }
        [JsonPropertyName("curator")]
        public bool Curator { get; set; }
        [JsonPropertyName("seniorCurator")]
        public bool SeniorCurator { get; set; }
        [JsonPropertyName("playlistUrl")]
        public string PlaylistUrl { get; set; }
    }
    public class MetaData
    {
        [JsonPropertyName("bpm")]
        public float Bpm { get; set; }
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
        [JsonPropertyName("songName")]
        public string SongName { get; set; }
        [JsonPropertyName("songSubName")]
        public string SongSubName { get; set; }
        [JsonPropertyName("songAuthorName")]
        public string SongAuthorName { get; set; }
        [JsonPropertyName("levelAuthorName")]
        public string LevelAuthorName { get; set; }
    }
    public class Statistics
    {
        [JsonPropertyName("plays")]
        public int Plays { get; set; }
        [JsonPropertyName("downloads")]
        public int Downloads { get; set; }
        [JsonPropertyName("upvotes")]
        public int Upvotes { get; set; }
        [JsonPropertyName("downvotes")]
        public int Downvotes { get; set; }
        [JsonPropertyName("score")]
        public float Score { get; set; }
        [JsonPropertyName("reviews")]
        public int Reviews { get; set; }
    }
    public class Version
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("sageScore")]
        public int SageScore { get; set; }
        [JsonPropertyName("diffs")]
        public List<Difficulty> Difficulties { get; set; }
        [JsonPropertyName("downloadURL")]
        public string DownloadURL { get; set; }
        [JsonPropertyName("coverURL")]
        public string CoverURL { get; set; }
        [JsonPropertyName("previewURL")]
        public string PreviewURL { get; set; }
        public class Difficulty
        {
            [JsonPropertyName("njs")]
            public float Njs { get; set; }
            [JsonPropertyName("offset")]
            public float Offset { get; set; }
            [JsonPropertyName("notes")]
            public int Notes { get; set; }
            [JsonPropertyName("bombs")]
            public int Bombs { get; set; }
            [JsonPropertyName("obstacles")]
            public int Obstacles { get; set; }
            [JsonPropertyName("nps")]
            public float Nps { get; set; }
            [JsonPropertyName("length")]
            public float Length { get; set; }
            [JsonPropertyName("characteristic")]
            public string Characteristic { get; set; }
            [JsonPropertyName("difficulty")]
            public string Name { get; set; }
            [JsonPropertyName("events")]
            public int Events { get; set; }
            [JsonPropertyName("chroma")]
            public bool Chroma { get; set; }
            [JsonPropertyName("me")]
            public bool Me { get; set; }
            [JsonPropertyName("ne")]
            public bool Ne { get; set; }
            [JsonPropertyName("cinema")]
            public bool Cinema { get; set; }
            [JsonPropertyName("seconds")]
            public float Seconds { get; set; }
            [JsonPropertyName("paritySummary")]
            public ParitySummary Parity { get; set; }
            [JsonPropertyName("stars")]
            public float Stars { get; set; }
            [JsonPropertyName("maxScore")]
            public int MaxScore { get; set; }
            public class ParitySummary
            {
                [JsonPropertyName("errors")]
                public int Errors { get; set; }
                [JsonPropertyName("warns")]
                public int Warns { get; set; }
                [JsonPropertyName("resets")]
                public int Resets { get; set; }
            }
        }
    }
    public class SearchResults
    {
        [JsonPropertyName("docs")]
        public List<WebSongInfo> InfoList { get; set; }
    }
    public static SearchResults ReadList(string jsonAsString)
    {
        return JsonSerializer.Deserialize<SearchResults>(jsonAsString);
    }
    public static WebSongInfo Read(string jsonAsString)
    {
        return JsonSerializer.Deserialize<WebSongInfo>(jsonAsString);
    }
}
