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
    Console.WriteLine("=== MUSIKLISTAPPEN ===");
    Console.WriteLine("1. Visa alla låtar");
    Console.WriteLine("2. Lägg till en låt");
    Console.WriteLine("3. Ta bort en låt");
    Console.WriteLine("4. Avsluta");
    Console.Write("Välj ett alternativ: ");

// string? för att inte det ska kunna bli null
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine();
            dash();
            
            var songs = db.GetAllSongs();

            foreach (var song in songs)
            {
                Console.WriteLine(song.Name);
            }

            dash();
            Console.WriteLine("Klicka på [Enter] för att gå tillbaka till huvudmenyn");
            Console.ReadLine();
            break;

        case "2":
            Console.WriteLine("Lägg till en låt senare.");
            break;

        case "3":
            Console.WriteLine("Ta bort en låt senare.");
            break;

        case "4":
            Console.WriteLine("Programmet avslutas.");
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