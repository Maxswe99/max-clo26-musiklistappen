# Reflektion — Musiklistappen

**Namn:** Max Svensson
**Datum:** 2026-10-27

---

## Vad var svårast?

jag gjorde db hero uppgiften med labbarna. det resulterade att jag inte tyckte dbhandler var så svår.
song.cs är ju väldigt simpel. copy pastade nästan mest från hero och fick lite hjälp av ai när jag ville
göra det på mitt sätt. svårtaste var nog att komma igång samt att hur jag skriver i program.cs tex. List<Song> songs = db.GetAllSongs();
för att programmet ska veta och sedan att jag skulle loopa igenom. tror man har glömt lite struktur och tänkande.
sen uppskattar jag alla todo och kommentarar som projektet skickade med.

---

## Vad lärde du dig?

att alla fält/egenskaper jag skickar in i song.cs behövs vid metoderna annars så funkar det inte.
jag behöver inte skriva ut dem i program.cs men det måste finnas i koden annars startar det inte.
IsDBNull.

---

## Hur använde du ADO.NET i praktiken — vad kändes naturligt och vad var förvirrande?

Tänk på de fem stegen: öppna anslutning, skapa kommando, skriv SQL, skicka in värden, kör.

eftersom jag hade gjort hero så var det ju rätt gött i ryggsäcken att ha gjort ett liknande projekt innan
vet inte vad som var naturligt jag bara körde. tog väl några 7-9 timmar att göra hela projektet. höll inte räkning :D
när jag fastnade så frågade jag ai vad jag hade gjort fel eller så. ringde även upp några i klassen och kollade läget.

---

## Varför ska man använda parameteriserade queries?

Vad kan hända om man klistrar ihop SQL med strängar i stället? (Tänk på Bobby Tables.)

för att skydda mot SQL injection.
om du är smart och har kunskap kan du förstöra en databas.
vi gillar inte Strängkonkatenering, det är osäkert :)

---

## Om du hade mer tid — vad hade du lagt till eller gjort annorlunda?

satsat på VG. jag är nöjd med G för denna uppgift. om inte LMGMIVG - lite mer G men inte VG
