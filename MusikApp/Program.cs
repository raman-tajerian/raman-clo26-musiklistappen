using Microsoft.Extensions.Configuration;
using MusikApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

// Skapar objektet som pratar med databasen
var db = new DbHandler(connectionString);

// Så länge running är true visas meny om igen
bool running = true;

while (running)
{
    //Skriv ut huvudmenyn
    Console.WriteLine();
    Console.WriteLine("--- Musiklistan ---");
    Console.WriteLine("1. Visa alla låtar");
    Console.WriteLine("2. Sök på titel");
    Console.WriteLine("3. Visa låtar med album");
    Console.WriteLine("4. Ta bort en låt");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    // Läser in vad användaren skrev

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            // Hämta alla låtar och skriv ut dem
            var allSongs = db.GetAllSongs();
            PrintSongs(allSongs);
            break;

        case "2":
            // Fråga efter söktext
            Console.Write("Sök på titel: ");
            string? searchText = Console.ReadLine();
            var found = db.SearchSongs(searchText ?? "");
            PrintSongs(found);
            break;

        case "3":
            // JOIN, låt tillsammans med album
            var withAlbum = db.GetSongsWithAlbum();
            foreach (var item in withAlbum)
            {
                Console.WriteLine($"{item.SongTitle} ({item.AlbumName})");
            }
            break;

        case "4":
            // Visa först alla låtar så användaren vet vilket id som ska bort
            PrintSongs(db.GetAllSongs());
            Console.Write("Skriv id på låten du vill ta bort: ");

            // TryParse gör texten till ett heltal
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Är du säker? j/n: ");
                string? answer = Console.ReadLine();

                if (answer == "j")
                {
                    bool deleted = db.DeleteSong(id);
                    Console.WriteLine(deleted ? "Låten togs bort." : "Hittade ingen låt med det id:t");

                }
                else
                {
                    Console.WriteLine("Avbrutet, ingenting togs bort.");
                }
            }
            else
            {
                Console.WriteLine("Det där var inget giltig id.");
            }
            break;

        case "5":
            // Detta gör att loopen slutar
            running = false;
            Console.WriteLine("Hej då");
            break;

        default:
            // Allt annat än 1-5
            Console.WriteLine("Ogiltigt val, skriv en siffra mellan 1 och 5.");
            break;
    }
}

// Hjälpmetod som skriver ut en lista med låtar (så slipper vi skriva samma loop två gånger)

void PrintSongs(List<Song> songs)
{
    if (songs.Count == 0)
    {
        Console.WriteLine("Inga låtar hittades.");
        return;
    }

    foreach (var song in songs)
    {
        Console.WriteLine($"{song.Id}: {song.Title} ({song.LengthSeconds} sek)");

    }
}
