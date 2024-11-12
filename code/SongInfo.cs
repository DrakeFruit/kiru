using System.Text.Json;

namespace Kiru;
public class SongInfo
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
    public class Uploader
    {
        int id { get; set; }
        string name { get; set; }
        string hash { get; set; }
        string avatar { get; set; }
        string type { get; set; }
        bool admin { get; set; }
        bool curator { get; set; }
        bool seniorCurator { get; set; }
        string playlistUrl { get; set; }
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
            float njs { get; set; }
            float offset { get; set; }
            int notes { get; set; }
            int bombs { get; set; }
            int obstacles { get; set; }
            float nps { get; set; }
            float length { get; set; }
            string characteristic { get; set; }
            string difficulty { get; set; }
            int events { get; set; }
            bool chroma { get; set; }
            bool me { get; set; }
            bool ne { get; set; }
            bool cinema { get; set; }
            float seconds { get; set; }
            ParitySummary paritySummary { get; set; }
            float stars { get; set; }
            int maxScore { get; set; }
            public class ParitySummary
            {
                int errors { get; set; }
                int warns { get; set; }
                int resets { get; set; }
            }
        }
    }

    public static SongInfo Read( string JsonAsString )
	{
		return JsonSerializer.Deserialize<SongInfo>( JsonAsString );
	}
}

