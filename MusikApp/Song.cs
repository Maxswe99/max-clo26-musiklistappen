namespace MusikApp;

public class Song
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Album { get; set; }
    public string Artist { get; set; }
    public int DurationSeconds { get; set; }
    public bool IsSingle { get; set; }
    public int ReleaseDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string StageName { get; set; }
    public string Origin { get; set; }
    public int ActiveSince { get; set; }

    public Song(int id, string name, string album, string artist)
    {
        Id = id;
        Name = name;
        Album = album;
        Artist = artist;
    }
    
    public Song(int id, string name, int durationseconds, bool issingle, string album, int releasedate, string firstname, string lastname, string stagename, string origin, int activesince)
    {
        Id = id;
        Name = name;
        DurationSeconds = durationseconds;
        IsSingle = issingle;
        Album = album;
        ReleaseDate = releasedate;
        FirstName = firstname;
        LastName = lastname;
        StageName = stagename;
        Origin = origin;
        ActiveSince = activesince;
    }
}