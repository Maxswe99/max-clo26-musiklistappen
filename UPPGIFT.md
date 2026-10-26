# Musiklistappen — C# och SQLite

🟢

**Kurs:** CLO26 — Cloud Developer
**Deadline:** Måndag

---

## Intro

Du har redan byggt musiklistan i SQL (diskografin). Nu tar du med dig databasen in i C# och bygger ett enkelt konsolprogram som låter dig navigera, lägga till och ta bort data via menyer.

Det finns inga konstiga krav och inga långa checklistor. Du vet vad du ska göra — du har gjort det i SQL. Nu handlar det om att koppla ihop det med C#.

**Målet:** visa att du förstår tanken — ett program som pratar med en databas — och att du kan använda **ADO.NET** (`SqliteConnection`, `SqliteCommand` och `SqliteDataReader`) för att läsa och ändra data. Mer än så behövs inte.

---

## Vad du ska bygga

En konsolapp som hanterar din favoritartists musik. Appen ska ha en tydlig meny och låta användaren göra grundläggande saker med datan.

Du använder **SQLite** och din databas från diskografi-inlämningen. Du får använda tabellerna som de är, du behöver inte ändra något i schemat.

> Har du ingen `.db`-fil kvar? Importera `exempel/musik_exempel.sql` i DB Browser for SQLite (*Fil → Importera → Databas från SQL-fil*), så får du en liten färdig databas att öva med.

---

## Det här får du i repot

| Fil | Vad det är |
|-----|-----------|
| `MusikApp/DbHandler.cs` | Ett **skelett**: metoderna och kommentarer finns, koden skriver du själv |
| `MusikApp/Song.cs` | Din data-klass (tom, du lägger till egenskaperna) |
| `MusikApp/Program.cs` | Här bygger du menyn. Den läser redan in `appsettings.json` åt dig |
| `MusikApp/appsettings.json` | Inställningar: sökvägen till din databas (`Data Source=musik.db`) |
| `MusikApp/appsettings.example.json` | Exempel som visar vilka nycklar som behövs (samma som i hero_databas) |
| `README.md` | Mall för din egen README, som du fyller i |
| `reflektion.md` | Reflektionen, som du fyller i |
| `INLAMNING.md` | Steg för steg hur du lämnar in |

---

## Krav (G)

### Appen

- Startar och visar en **huvudmeny** med alternativ numrerade 1–N
- Användaren navigerar genom att skriva ett nummer och trycka Enter
- Appen avslutar snyggt när användaren väljer "Avsluta"

### Vad menyn ska kunna göra

Minst tre av följande (välj själv vilka):

- **Visa alla** låtar/album (lista i konsolen) ✅
- **Lägg till** en ny låt eller ett nytt album
- **Ta bort** en post (med bekräftelse: "Är du säker? j/n")
- **Sök** på titel eller artist ✅
- **Visa detaljer** för en specifik post

### Kod

- Minst en klass för din data, t.ex. `Song`, `Album` eller `Artist`
- Databasens logik i klassen `DbHandler`, inte allt i `Program.cs`
- Parameteriserade queries — inte strängkonkatenering

### Inlämning

Se `INLAMNING.md`. Du lämnar in:

- din kod (hela mappen)
- en kort `README.md` med: vad appen gör, hur man kör den (klona, bygg, kör) och en screenshot på huvudmenyn
- ifylld `reflektion.md`

---

## VG

VG kräver **båda** delar:

### Del 1 — Avancerade SQL-frågor i appen

Din app ska använda minst **ett** av följande i sin databaskod:

**A) JOIN över flera tabeller**
Appen har minst två tabeller (t.ex. `Album` och `Låtar`) och hämtar data från båda i samma query:

```sql
SELECT Låtar.Titel, Album.Namn
FROM Låtar
JOIN Album ON Låtar.AlbumId = Album.Id
ORDER BY Album.Namn, Låtar.Titel;
```

**B) Subquery**
Appen använder en underfråga — t.ex. för att visa låtar som är längre än medellängden:

```sql
SELECT Titel, Längd
FROM Låtar
WHERE Längd > (SELECT AVG(Längd) FROM Låtar);
```

### Del 2 — Reflektion i README

Lägg till ett avsnitt **"Mina designval"** i din `README.md` där du svarar på:

1. Hur designade du din databas? Varför valde du just den strukturen?
2. Vilket SQL-krav valde du (JOIN eller subquery) och varför passade det din app?
3. Vad skulle du gjort annorlunda om du byggde om den från scratch?

---

## Kodexempel — så kopplar du till SQLite

```csharp
// ADO.NET mot SQLite. NuGet: Microsoft.Data.Sqlite (redan installerat i projektet)
using Microsoft.Data.Sqlite;

using var connection = new SqliteConnection("Data Source=musik.db");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = "SELECT Id, Titel FROM Låtar ORDER BY Titel";

using var reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader["Id"]}. {reader["Titel"]}");
}
```

Använd alltid parameteriserade queries när du tar emot input:

```csharp
command.CommandText = "INSERT INTO Låtar (Titel, Längd) VALUES (@titel, @langd)";
command.Parameters.AddWithValue("@titel", titel);
command.Parameters.AddWithValue("@langd", langd);
command.ExecuteNonQuery();
```

---

## Tips

- Börja med att få menyn att fungera — utan databas. Skriv ut "Här kommer en lista senare" tills anslutningen är klar.
- Lägg din `.db`-fil i mappen `MusikApp/` och döp den till `musik.db`. Heter din fil något annat ändrar du i `appsettings.json`, inte i koden.
- `appsettings.json` följer med i mallen så att appen fungerar direkt, och den innehåller här bara databasens filnamn, inga hemligheter. Normalt checkar man *inte* in den (se hero_databas, labb 1). Lägger du någonsin ett lösenord i en sådan fil: kör `git rm --cached MusikApp/appsettings.json` först, så att git slutar följa den. Det som ska finnas i repot är `appsettings.example.json`.
- En metod = en sak. `VisaAllaLåtar()`, `LäggTillLåt()`, `TaBortLåt()` — håll dem korta.
- Du behöver inte skriva om eller förbättra SQL-schemat. Det du har fungerar.
- Fastnar du: börja med `GetAllSongs()` i `DbHandler.cs`. När den fungerar är resten samma mönster.

---

*Du har redan gjort det svåra. Nu är det bara att paketera det i C#.*
