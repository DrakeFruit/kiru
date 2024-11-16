using System.Text.Json;

namespace Kiru;
public class WebSongInfo
{
    public string id { get; set; }
	public string name { get; set; }
    public string description { get; set; }
    public Uploader uploader { get; set; }
	public MetaData metadata { get; set; }
    public Stats stats { get; set; }
    public string uploaded { get; set; }
    public bool automapper { get; set; }
    public bool ranked { get; set; }
    public bool qualified { get; set; }
    public List<Versions> versions { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
    public string lastPublishedAt { get; set; }
    public List<string> tags { get; set; }
    public bool bookMarked { get; set; }
    public string declaredAi { get; set; }
    public bool blRanked { get; set; }
    public bool blQualified { get; set; }

    public class Uploader
    {
        public int id { get; set; }
        public string name { get; set; }
        public string hash { get; set; }
        public string avatar { get; set; }
        public string type { get; set; }
        public bool admin { get; set; }
        public bool curator { get; set; }
        public bool seniorCurator { get; set; }
        public string playlistUrl { get; set; }
    }
    public class MetaData
    {
    	public float bpm { get; set; }
    	public int duration { get; set; }
    	public string songName { get; set; }
    	public string songSubName { get; set; }
    	public string songAuthorName { get; set; }
    	public string levelAuthorName { get; set; }
    }
    public class Stats
    {
        public int plays { get; set; }
        public int downloads { get; set; }
        public int upvotes { get; set; }
        public int downvotes { get; set; }
        public float score { get; set; }
        public int reviews { get; set; }
    }
    public class Versions
    {
        public string hash { get; set; }
        public string key { get; set; }
        public string state { get; set; }
        public string createdAt { get; set; }
        public int sageScore { get; set; }
        public List<Difficulty> diffs { get; set; }
        public string downloadURL { get; set; }
        public string coverURL { get; set; }
        public string previewURL { get; set; }
        public class Difficulty
        {
            public float njs { get; set; }
            public float offset { get; set; }
            public int notes { get; set; }
            public int bombs { get; set; }
            public int obstacles { get; set; }
            public float nps { get; set; }
            public float length { get; set; }
            public string characteristic { get; set; }
            public string difficulty { get; set; }
            public int events { get; set; }
            public bool chroma { get; set; }
            public bool me { get; set; }
            public bool ne { get; set; }
            public bool cinema { get; set; }
            public float seconds { get; set; }
            public ParitySummary paritySummary { get; set; }
            public float stars { get; set; }
            public int maxScore { get; set; }
            public class ParitySummary
            {
                public int errors { get; set; }
                public int warns { get; set; }
                public int resets { get; set; }
            }
        }
    }
    public class SearchResults
    {
	    public List<WebSongInfo> docs { get; set; }
    }
    
    public static SearchResults ReadList( string JsonAsString )
    {
	    return JsonSerializer.Deserialize<SearchResults>( JsonAsString );
    }
    public static WebSongInfo Read( string JsonAsString )
	{
		return JsonSerializer.Deserialize<WebSongInfo>( JsonAsString );
	}
}
