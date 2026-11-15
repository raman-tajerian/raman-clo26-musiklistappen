# Reflektion — Musiklistappen

**Namn:** Raman Tajerian

**Datum:** 2026-11-15

## Vad var svårast?

Uppgiften var inte supersvår tycker jag, men något jag frågade om förklaringar för att få djupare förståelse var hur C# och databasen hänger ihop. Databasen har tabeller med rader, men i C# jobbar man med objekt och listor. Man måste själv läsa varje rad och göra ett Song-objekt av den. I början ville jag till exempel förstå varför Song.cs behövdes, men det blev tydligt när jag såg att den bara beskriver hur en låt ser ut i koden.

Det var också svårt att hålla ordning på tre filer som beror på varandra. Ett litet fel i en fil kan göra att hela projektet slutar kompilera. Jag fick till exempel felet CS1513: } expected för att en klammerparentes saknades i slutet av DbHandler.cs, och då visade Visual Studio röda fel nästan överallt. Det var svårt att se att det egentligen var ett enda ställe som var fel.

## Vad lärde du dig?

Jag lärde mig att dela upp programmet så att varje fil gör det jobb som den ska, och varför det är nödvändigt att göra på det sättet. Program.cs sköter menyn, DbHandler.cs sköter databasen och Song.cs beskriver datan. Jag lärde mig också de fem stegen i ADO.NET: öppna anslutning, skapa kommando, skriv SQL, skicka in värden och kör. När en metod fungerade kunde jag använda samma upplägg i de andra.

Jag fick göra flera fel som jag lärde mig av. Lärarens mall låg kvar längst ner i DbHandler.cs, så jag hade två konstruktorer med samma namn. Jag lärde mig att läsa felmeddelandena noga, för de visar både fil och rad, och att köra koden ofta.

Jag fick hjälp av Claude med att förstå strukturen och hitta fel i koden, och med att förklara hur ADO.NET fungerar. Syntaxen för connection och command, till exempel connection.Open() och connection.CreateCommand(), var svår i början. Claude hjälpte mig att förstå den, och sedan kunde jag återanvända det mönstret i metoderna.

## Hur använde du ADO.NET i praktiken — vad kändes naturligt och vad var förvirrande?

Jag använde samma fem steg i alla metoder i DbHandler.cs. Först öppnar man en anslutning med new SqliteConnection och connection.Open(), sedan skapar man ett kommando med connection.CreateCommand() och skriver SQL i command.CommandText. Syntaxen för connection och command var svår i början, och Claude hjälpte mig att förstå den. Eftersom den är likadan i alla metoder kunde jag sedan återanvända den och bara ändra SQL-frågan.

Värden skickar jag in med command.Parameters.AddWithValue, till exempel @sok, och det kändes naturligt när jag förstod att det är en tom plats som fylls i separat. Sist kör man kommandot. ExecuteReader() använder man för SELECT och läser raderna med while (reader.Read()), och ExecuteNonQuery() använder man för DELETE. Det mest förvirrande var som nämnt tidigare de fem stegen, jag försökte lägga mycket tid på att förstå syftena och funktionen bakom vart och ett av stegen.

## Varför ska man använda parameteriserade queries?

Om man klistrar ihop SQL med text från användaren blir texten en del av SQL-koden. Då kan någon skriva SQL i sökfältet och förstöra databasen. Det är vad SQL-injektion är, och det är det Bobby Tables handlar om. Med en parameter som @sok skickas texten in separat och behandlas bara som data. Jag använde parametrar i SearchSongs och DeleteSong.

## Om du hade mer tid — vad hade du lagt till eller gjort annorlunda?

Jag hade lagt till AddSong så man kan lägga till nya låtar, och GetSongById. Jag hade också gjort en subquery som visar låtar som är längre än medellängden, och en JOIN över tre tabeller som visar artist, album och låt. Längden visas nu i sekunder, och jag hade velat visa den som minuter och sekunder. Nästa gång skulle jag testa efter varje metod i stället för att skriva mycket på en gång.

## (VG) Varför delade du upp koden som du gjorde?

Jag delade upp koden i tre filer eftersom de har olika jobb. Song.cs beskriver en låt, DbHandler.cs har all SQL och kontakten med databasen, och Program.cs visar menyn. På så sätt blandas inte SQL med Console.WriteLine.

Om allt hade legat i en fil hade det varit svårare att hitta rätt ställe när jag ville ändra något. Nu går jag till Program.cs för menyn och DbHandler.cs för databasen. GetAllSongs används både när jag visar alla låtar och när jag ska välja vilken som ska tas bort, och PrintSongs används på flera ställen. Eftersom all SQL ligger på ett ställe är det också lätt att kolla att alla frågor använder parametrar.