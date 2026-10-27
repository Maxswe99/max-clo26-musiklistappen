using Microsoft.Data.Sqlite;

namespace MusikApp;

public class DbHandler
{
    private string _connectionString;

    public DbHandler(string connectionString)
    {
        _connectionString = connectionString;
    }
    
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
    
    /// <summary>Visar utökad information där titeln eller artisten innehåller söktexten.</summary>
    public List<Song> ShowDetails(string details)
    {
        var songs = new List<Song>();
        
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        
        //fick hjälp av ai med "CAST" då man inte kunde söka på tex "2014" från albumet som heter "2014 Forest Hills Drive"
        using var command = conn.CreateCommand();
        command.CommandText = @"
            SELECT 
            songs.song_id, 
            songs.song_name, 
            songs.duration_seconds, 
            songs.is_single, 
            albums.album_name, 
            albums.release_date, 
            artist.first_name, 
            artist.last_name, 
            artist.stage_name, 
            artist.origin, 
            artist.active_since
            FROM songs
            LEFT JOIN albums ON songs.album_id = albums.album_id
            LEFT JOIN song_artists ON songs.song_id = song_artists.song_id
            LEFT JOIN artist ON song_artists.artist_id = artist.artist_id
            WHERE songs.song_name LIKE @search
            OR albums.album_name LIKE @search
            OR CAST(albums.release_date AS TEXT) LIKE @search
            OR artist.stage_name LIKE @search;
            ";
        command.Parameters.AddWithValue("@search", $"%{details}%");
        
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            int songId = reader.GetInt32(0);
            string songName = reader.GetString(1);
            int durationSeconds = reader.GetInt32(2);
            bool isSingle = reader.GetBoolean(3);
            string albumName = reader.GetString(4);
            int releaseDate = reader.GetInt32(5);
            string firstName = reader.GetString(6);
            string lastName = reader.GetString(7);
            string stageName = reader.GetString(8);
            string origin = reader.GetString(9);
            int activeSince = reader.GetInt32(10);

            songs.Add(new Song(songId, songName, durationSeconds, isSingle, albumName, releaseDate, firstName, lastName, stageName, origin, activeSince));
        }

        return songs;
    }
}
