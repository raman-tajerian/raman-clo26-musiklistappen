using Microsoft.Data.Sqlite;

namespace MusikApp;

public class DbHandler
{
    private readonly string _connectionString;
    public DbHandler(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Metod 1: Hämta alla låtar

    public List<Song> GetAllSongs()
    {
        var songs = new List<Song>();

        // Öppnar en anslutning till databasen

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Skapar ett kommando 

        var command = connection.CreateCommand();

        command.CommandText = "SELECT id, title, length_seconds, album_id FROM Songs ORDER BY title";

        // Skriver SQL

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            songs.Add(new Song
            {
                Id = reader.GetInt32(0),                // Kolumn 0 = id
                Title = reader.GetString(1),            // Kolmn 1 = title
                LengthSeconds = reader.GetInt32(2),     // Kolumn 2 = length_seconds
                AlbumId = reader.GetInt32(3)            // Kolumn 3 = album_id
            });

        }


        return songs;

    }

    // Metod 2: Hämta specifik låt

    public Song? GetSongById(int id)

    {
        throw new NotImplementedException();

    }


    // Metod 3: Sök låtar på titel

    public List<Song> SearchSongs(string searchText)
    {
        var songs = new List<Song>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = "SELECT id, title, length_seconds, album_id FROM songs WHERE title LIKE @sok ORDER BY title";

        command.Parameters.AddWithValue("@sok", "%" + searchText + "%");

        using var reader = command.ExecuteReader();

        while (reader.Read())

        {
            songs.Add(new Song
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                LengthSeconds = reader.GetInt32(2),
                AlbumId = reader.GetInt32(3)
            });

        }

        return songs;
    }


    // Metod 4: Lägg till en låt

    public void AddSong(Song song)
    {
        throw new NotImplementedException();

    }

    // Metod 5: Ta bort en låt och returnerar true om en låt faktiskt togs bort

    public bool DeleteSong(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var deleteLinks = connection.CreateCommand();
        deleteLinks.CommandText = "DELETE FROM song_genres WHERE song_id = @id";
        deleteLinks.Parameters.AddWithValue("@id", id);
        deleteLinks.ExecuteNonQuery();

        // Ta bort själva låten

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM songs WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        // Använder ExecuteNonQuery, returnerar hur många rader som togs bort

        int antalRader = command.ExecuteNonQuery();

        // True om minst en rad togs bort, annars false

        return antalRader > 0;


    }


    // Metod 6: Låtar tillsammans med albumets namn. Använder en JOIN

    public List<(string SongTitle, string AlbumName)> GetSongsWithAlbum()
    {
        var result = new List<(string SongTitle, string AlbumName)>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        //JOIN

        command.CommandText = @"SELECT songs.title, albums.title
                              FROM songs
                              JOIN albums ON songs.album_id = albums.id
                              ORDER BY albums.title, songs.title";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add((reader.GetString(0), reader.GetString(1)));

        }

        return result;

    }

    // Metod 7

    public List<Song> GetSongsLongerThanAverage()
    {
        throw new NotImplementedException();

    }
}

