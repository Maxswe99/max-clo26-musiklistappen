namespace MusikApp;

// Din data-klass: ett Song-objekt = en rad i din låt-tabell.
//
// TODO: lägg till en egenskap per kolumn du vill visa i appen,
// t.ex. Id, Titel och Längd. Namn och typer ska passa din databas.
//
// Behöver du fler klasser (Album, Artist) för din app? Skapa dem i egna filer.
public class Song
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Album { get; set; }
    public string Artist { get; set; }

    public Song(int id, string name, string album, string artist)
    {
        Id = id;
        Name = name;
        Album = album;
        Artist = artist;
    }
}