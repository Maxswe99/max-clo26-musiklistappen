using Microsoft.Extensions.Configuration;using MusikApp;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

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
            Dash();
            Info();
            Dash();
            
            List<Song> songs = db.GetAllSongs();
            
            foreach (var song in songs)
            {
                Console.WriteLine($"{song.Id}, {song.Name}, {song.Album}, {song.Artist}");
            }

            Dash();
            PressEnter();
            Console.ReadLine();
            break;

        case "2":
            Console.Write("Sök på låt eller artist: ");
            string searchText = Console.ReadLine() ?? "";

            List<Song> searchSongs = db.SearchSongs(searchText);
            
            Console.WriteLine();
            Dash();
            Info();
            Dash();
            foreach (var song in searchSongs)
            {
                Console.WriteLine($"{song.Id}, {song.Name}, {song.Album}, {song.Artist}");
            }
            
            // Resultatet kommer tillbaka utan att hitta några låtar
            if (searchSongs.Count == 0)
            {
                Console.WriteLine("Ingen låt eller artist hittades.");
            }

            Dash();
            PressEnter();
            Console.ReadLine();
            break;

        case "3":
            Console.Write("Sök på låt/album/artist: ");
            string? input = Console.ReadLine();

            List<Song> details = db.ShowDetails(input);
            Console.WriteLine();
            Dash();

            //fick en del hjälp av ai med att sortera artist och låtar för sig själv
            if (details != null && details.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("--- Artist Info ---");
                Console.WriteLine();

                // En lista där vi sparar namnen på alla artister vi redan skrivit ut
                List<string> printedArtists = new List<string>();

                foreach (var artist in details)
                {
                    // Kolla om vi INTE redan har skrivit ut denna artist
                    if (!printedArtists.Contains(artist.StageName))
                    {
                        Console.WriteLine($"Artist first name: {artist.FirstName}");
                        Console.WriteLine($"Artist last name: {artist.LastName}");
                        Console.WriteLine($"Artist stage name: {artist.StageName}");
                        Console.WriteLine($"Artists origin: {artist.Origin}");
                        Console.WriteLine($"Active since: {artist.ActiveSince}");
                        Console.WriteLine();

                        // Lägg till artisten i listan så vi vet att hen är färdigutskriven!
                        printedArtists.Add(artist.StageName);
                    }
                }
                
                Console.WriteLine("--- Songs ---");
                Console.WriteLine();

                foreach (var song in details)
                {
                    Console.WriteLine($"Song ID: {song.Id}");
                    Console.WriteLine($"Song name: {song.Name}");
                    Console.WriteLine($"Song Duration (seconds): {song.DurationSeconds}");
                    Console.WriteLine($"Single release?: {song.IsSingle}");
                    Console.WriteLine($"Album Name: {song.Album}");
                    Console.WriteLine($"Release date: {song.ReleaseDate}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Inga träffar hittades.");
            }
            
            Dash();
            PressEnter();
            Console.ReadLine();
            break;

        case "4":
            Console.WriteLine("Programmet avslutas.");
            Console.WriteLine("Tack för att du använde MUSIKLISTAPPEN feat. (J cole)!");
            Console.WriteLine("Du kan ge feedback på google classroom med skalan 1-3 :)");
            on = false;
            break;

        default:
            Console.WriteLine("Ogiltigt val.");
            Console.WriteLine();
            break;
    }
}

void Dash() {
Console.WriteLine("--------------------------------------------");
}

void Info() {
    Console.WriteLine("Song ID, Song name, Album name, Artist name");
}

void PressEnter() {
    Console.WriteLine("Klicka på [Enter] för att gå tillbaka till huvudmenyn");
}