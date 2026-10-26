using Microsoft.Extensions.Configuration;using MusikApp;

// Läser inställningar från appsettings.json (samma sätt som i hero_databas).
// Databasens sökväg ligger där, så du slipper skriva den i koden.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

// Börja här. Den här raden visar att inställningarna lästes in, ta bort den när du vill.
Console.WriteLine($"Musiklistappen: börja med att få menyn att fungera!  (databas: {connectionString})");

// Tips på ordning (se UPPGIFT.md):
//   1. Rita upp huvudmenyn (alternativ numrerade 1-N) och läs in ett val med Console.ReadLine().
//      Skriv ut "Här kommer en lista senare" tills databasen är kopplad.
//   2. Skapa ett DbHandler-objekt: new DbHandler(connectionString)
//   3. Anropa DbHandlers metoder från menyn. Här ska ALDRIG SQL stå.
//   4. Avsluta snyggt när användaren väljer "Avsluta".
bool on = true;
var db = new DbHandler(connectionString);

while (on)
{
    Console.WriteLine("=== MUSIKLISTAPPEN feat. (J cole) ===");
    Console.WriteLine("1. Visar alla låtar/album");
    Console.WriteLine("2. Sök på titel eller artist");
    Console.WriteLine("3. Visa detaljer för en specifik post");
    Console.WriteLine("4. Avsluta");
    Console.Write("Välj ett alternativ: ");

// String? för att inte det ska kunna bli null
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine();
            dash();
            info();
            dash();
            
            List<Song> songs = db.GetAllSongs();
            
            foreach (var song in songs)
            {
                Console.WriteLine($"{song.Id}, {song.Name}, {song.Album}, {song.Artist}");
            }

            dash();
            Console.WriteLine("Klicka på [Enter] för att gå tillbaka till huvudmenyn");
            Console.ReadLine();
            break;

        case "2":
            Console.Write("Sök på låt eller artist: ");
            string searchText = Console.ReadLine() ?? "";

            List<Song> searchSongs = db.SearchSongs(searchText);
            
            Console.WriteLine();
            dash();
            info();
            dash();
            foreach (var song in searchSongs)
            {
                Console.WriteLine($"{song.Id}, {song.Name}, {song.Album}, {song.Artist}");
            }
            
            // Resultatet kommer tillbaka utan att hitta några låtar
            if (searchSongs.Count == 0)
            {
                Console.WriteLine("Ingen låt eller artist hittades.");
            }

            dash();
            Console.WriteLine("Klicka på [Enter] för att gå tillbaka till huvudmenyn");
            Console.ReadLine();
            break;

        case "3":
            Console.WriteLine("Visa detaljer för en specifik post");
            break;

        case "4":
            Console.WriteLine("Programmet avslutas.");
            on = false;
            break;

        default:
            Console.WriteLine("Ogiltigt val.");
            Console.WriteLine();
            break;
    }
}

void dash() {
Console.WriteLine("--------------------------------------------");
}

void info() {
    Console.WriteLine("Song ID, Song name, Album name, Artist name");
}