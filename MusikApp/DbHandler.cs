using Microsoft.Data.Sqlite;

namespace MusikApp;

/// <summary>
/// ALL databaskod bor här. Program.cs ska aldrig innehålla SQL eller SqliteConnection.
///
/// Det här är ett skelett: metoderna finns, kommentarerna visar vad varje metod ska göra,
/// men koden är din. Alla metoder kastar NotImplementedException tills du har skrivit dem.
///
/// ADO.NET i fem steg (samma i varje metod som pratar med databasen):
///   1. Öppna en anslutning        -> new SqliteConnection(...) och connection.Open()
///   2. Skapa ett kommando         -> connection.CreateCommand()
///   3. Skriv SQL:en               -> command.CommandText = "..."
///   4. Skicka in värden säkert    -> command.Parameters.AddWithValue("@namn", värde)
///   5. Kör kommandot              -> ExecuteReader() för SELECT, ExecuteNonQuery() för INSERT/DELETE
///
/// Använd "using var" på anslutning och kommando så stängs de av sig själva.
/// Bygg SQL:en med parametrar (@titel), aldrig genom att klistra ihop strängar.
/// </summary>
public class DbHandler
{
    private string _connectionString;

    public DbHandler(string connectionString)
    {
        _connectionString = connectionString;
    }

    // ---------------------------------------------------------------
    // G-nivå: du behöver minst tre av de fem här (välj själv vilka)
    // ---------------------------------------------------------------

    /// <summary>Hämtar alla låtar, sorterade på titel.</summary>
    public List<Song> GetAllSongs()
    {
        //skapar objektet (listan)
        var songs = new List<Song>();

        //öppnar en anslutning
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        //vi pratar med databasen
        using var command = conn.CreateCommand();
        
        // tabell.kolumn annars kan databasen ibland inte söka på grund av ambiguous.
        command.CommandText = @"
            SELECT songs.song_id, songs.song_name, albums.album_name, artist.stage_name
            FROM songs
            JOIN albums ON songs.album_id = albums.album_id
            JOIN artist ON albums.artist_id = artist.artist_id
            GROUP BY songs.song_name;
            ";
        
        //executeReader för det handlar om select. Annars ExecuteNonQuery.
        using var reader = command.ExecuteReader();
        
        //while för att det kan finnas flera låtar m.m
        while (reader.Read())
        {
            int songId = reader.GetInt32(0);
            string songName = reader.GetString(1);
            string albumName = reader.GetString(2);
            string stageName = reader.GetString(3);
            
            songs.Add(new Song(songId, songName, albumName, stageName));
        }

        return songs;
    }

    /// <summary>Hämtar en specifik låt. Returnerar null om den inte finns.</summary>
    public Song? GetSongById(int id)
    {
        // TODO: SELECT ... WHERE Id = @id (parameter!).
        // TODO: if (reader.Read()) { ... } annars returnera null.
         throw new NotImplementedException();
    }

    /// <summary>Söker låtar där titeln innehåller söktexten.</summary>
    public List<Song> SearchSongs(string searchText)
    {
        var songs = new List<Song>();
        
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        
        using var command = conn.CreateCommand();
        command.CommandText = @"
            SELECT songs.song_id, songs.song_name, albums.album_name, artist.stage_name
            FROM songs
            JOIN albums ON songs.album_id = albums.album_id
            JOIN artist ON albums.artist_id = artist.artist_id
            WHERE songs.song_name LIKE @search
            OR artist.stage_name LIKE @search
            ";
        
        //strängkonkatenering
        command.Parameters.AddWithValue("@search", $"%{searchText}%");
        
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            int songId = reader.GetInt32(0);
            string songName = reader.GetString(1);
            string albumName = reader.GetString(2);
            string artistName = reader.GetString(3);

            songs.Add(new Song(songId, songName, albumName, artistName));
        }

        return songs;
    }

    /// <summary>Lägger till en ny låt.</summary>
    public void AddSong(Song song)
    {
        // TODO: INSERT INTO ... VALUES (@titel, @langd, ...) med parametrar.
        // Tips: INSERT ger ingen data tillbaka, så använd ExecuteNonQuery().
        throw new NotImplementedException();
    }

    /// <summary>Tar bort en låt. Returnerar true om en rad faktiskt togs bort.</summary>
    public bool DeleteSong(int id)
    {
        // TODO: DELETE FROM ... WHERE Id = @id.
        // Tips: ExecuteNonQuery() returnerar antalet påverkade rader.
        // Menyn i Program.cs ska fråga "Är du säker? j/n" innan den här metoden anropas.
        throw new NotImplementedException();
    }

    // ---------------------------------------------------------------
    // VG-nivå: du behöver minst EN av dessa två (och reflektionen i README)
    // ---------------------------------------------------------------

    /// <summary>(VG, alternativ A) Låtar tillsammans med albumets namn. Kräver en JOIN.</summary>
    public List<(string SongTitle, string AlbumName)> GetSongsWithAlbum()
    {
        // TODO: SELECT Låtar.Titel, Album.Namn FROM Låtar JOIN Album ON ... ORDER BY ...
        throw new NotImplementedException();
    }

    /// <summary>(VG, alternativ B) Låtar som är längre än medellängden. Kräver en subquery.</summary>
    public List<Song> GetSongsLongerThanAverage()
    {
        // TODO: SELECT ... WHERE Längd > (SELECT AVG(Längd) FROM Låtar).
        throw new NotImplementedException();
    }
}
