# Musiklistappen — Raman Tajerian

## Vad appen gör

Appen är ett konsolprogram i C# som använder en SQLite-databas med bandet Kent. Databasen har fyra av deras album och 45 låtar. I menyn kan man visa alla låtar, söka på titel, visa låtar tillsammans med vilket album de tillhör, och ta bort en låt.

## Så kör du den

```
git clone https://github.com/raman-tajerian/raman-clo26-musiklistappen
cd raman-clo26-musiklistappen/MusikApp
dotnet run
```

Databasen `musik.db` ska ligga i mappen `MusikApp/`. Om `appsettings.json` saknas kopierar man `appsettings.example.json` och döper om kopian till `appsettings.json`.

## Så ser huvudmenyn ut

![Huvudmenyn](meny.png)

## Mina designval (VG)

**1. Hur designade du din databas? Varför valde du just den strukturen?**

Jag har fem tabeller: artists, albums, songs, genres och song_genres. Jag delade upp informationen så att samma sak inte behöver skrivas flera gånger. Kents namn finns bara i artists, och albums har artist_id som pekar på rätt artist. Låtarna har på samma sätt album_id. Genrerna har en egen tabell, och song_genres kopplar ihop låtar och genrer eftersom en låt kan ha flera genrer och en genre kan höra till många låtar (many-to-many). Jag hade kunnat skriva genrerna direkt i songs, men då hade flera värden hamnat i samma cell, vilket gör det svårare att söka.

**2. Vilket SQL-krav valde du (JOIN eller subquery) och varför passade det din app?**

Jag valde JOIN. I songs finns bara album_id, alltså en siffra, men en användare vill se albumets namn. I menyval 3 kopplar jag därför ihop songs och albums med songs.album_id = albums.id, så att varje låt visas tillsammans med sitt album. Det passade appen eftersom datan redan låg i två tabeller.

**3. Vad skulle du gjort annorlunda om du byggde om den från scratch?**

Jag hade byggt och testat appen efter varje metod i stället för att skriva mycket på en gång. Jag hade också lagt till fler menyval, till exempel att lägga till en låt, och visat längden som minuter och sekunder i stället för sekunder, eftersom det är så det visas på Spotify och jag föredrar det formatet.
