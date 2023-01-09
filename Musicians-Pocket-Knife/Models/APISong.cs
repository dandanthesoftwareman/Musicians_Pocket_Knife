namespace Musicians_Pocket_Knife.Models
{

    public class APISong
    {
        public Song song { get; set; }
    }

    public class Song
    {
        public string id { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
        public Artist artist { get; set; }
        public string tempo { get; set; }
        public string time_sig { get; set; }
        public string key_of { get; set; }
        public string open_key { get; set; }
    }

    public class Artist
    {
        public string id { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public string img { get; set; }
        public string[] genres { get; set; }
        public string from { get; set; }
        public string mbid { get; set; }
    }

}
