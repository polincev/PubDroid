namespace PubDroid;

public static class Constants
{
    public static class Description
    {
        public const string Bundle = "Relative or absolute path of the app bundle.";
        public const string PackageName = "Package name from Google Play Console";
        public const string JsonKey = "Relative or absolute path of the service account JSON Key";
        public const string Track = "Play Console testing track to upload the bundle to.";
        public const string Status = "Release status to assign to the bundle upon upload.";
    }
    
    public static class Status
    {
        public const string Draft = "draft";
        public const string Active = "active";
    }
    
    public static class Track
    {
        public const string Internal = "internal";
        public const string Closed = "closed";
        public const string Open = "open";
    }
}