using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQ___Sortieren_und_Partitionieren
{
    internal class Program
    {
        enum options
        {
            lastAccess,
            size
        }
        static void Main(string[] args)
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 22, 12, 16, 18, 11, 19, 13 };
            /*1.
Geben Sie das obige Array aufsteigend sortiert aus
2.
Geben Sie das obige Array absteigend sortiert aus
3.
Geben Sie aus dem obigen Array alle graden Zahlen aufsteigend sortiert aus*/

            var aufsteigend = numbers.OrderBy(x => x);
            Ausgabe(aufsteigend, "Aufsteigend:");
            var absteigend = numbers.OrderByDescending(x => x);
            Ausgabe(absteigend, "Absteigend:");
            var geradeAufsteigend = numbers.Where(x => x % 2 == 0).OrderBy(x => x);
            Ausgabe(geradeAufsteigend, "nur gerade Zahlen, aufsteigend:");



            string[] numbersString = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen" };

            /*1.
Geben Sie das obige Array nach der Länge der Worte aufsteigend sortiert aus
2.
Geben Sie das obige Array nach der Länge der Worte aufsteigend sortiert aus, bei gleicher Länge soll alphabetisch absteigend sortiert werden
3.
Drehen Sie die Reihenfolge der Elemente im Array um

Erstellen Sie ein DirectoryInfo-Objekt für ein Verzeichnis Ihrer Wahl.
4.
Listen Sie alle Dateien in dem Verzeichnis, absteigend nach Namen sortiert auf
5.
Listen Sie alle Dateien in dem Verzeichnis, nach Größe aufsteigend sortiert auf
6.
Listen Sie alle Dateien in dem Verzeichnis, nach dem Datum des letzten Zugriffs auf, jüngste Dateien zuerst*/

            var laengeAufsteigend = numbersString.OrderBy(x => x.Length);
            Ausgabe(laengeAufsteigend, "Nach Länge aufsteigend:");

            var laengeAufsteigendDannAlphabetisch = numbersString.OrderBy(x => x.Length).ThenByDescending(x => x);
            Ausgabe(laengeAufsteigendDannAlphabetisch, "Nach Länge aufsteigend dann alphabetisch:");

            var drehen = numbersString.Reverse();
            Ausgabe(drehen, "Umgekehrte Reihenfolge:");

            DirectoryInfo d = new DirectoryInfo(@"C:\Windows\");
            var dateienNachName = d.GetFiles().OrderByDescending(x => x.Name);

            var dateienNachNameQuery = from file in d.EnumerateFiles() orderby file.Name descending select file;
            Ausgabe(dateienNachName, "Dateien nach Name sortiert:");



            var dateienNachGroesse = d.GetFiles().OrderBy(f => f.Length).Select(file => new { file.Name, file.Length });
            Console.WriteLine("Dateien nach Größe:");
            foreach(var v in dateienNachGroesse)
            {
                Console.WriteLine(v);
            }
            // Ausgabe(dateienNachGroesse, "Dateien nach Größe:", "size");





            var letzterZugriff = d.GetFiles().OrderBy(x => x.LastAccessTime.Ticks);
            Ausgabe(letzterZugriff, "Dateien nach letztem Zugriff:", "last access");

            int[] numbers3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 22, 12, 16, 18, 11, 19, 13 };
            var ersten5 = numbers3.Take(5);
            Ausgabe(ersten5, "Die ersten 5 Werte:");

            var letzten5 = numbers.TakeLast(5);
            Ausgabe(letzten5, "Die letzten 5 Werte:");

            var ohneErsteUndLetzten3 = numbers.Skip(3).SkipLast(3);
            Ausgabe(ohneErsteUndLetzten3, "Ohne ersten 3 und letzten 3 Werte:");

            var groesser0 = numbers.Where(x => x > 0);
            Ausgabe(groesser0, "Werte größer 0:");

            var nach12 = numbers.Skip(Array.IndexOf(numbers, 12)+1).Select(x => x);
            var nach12alternativ = numbers.SkipWhile(x => x != 12).Skip(1);
            Ausgabe(nach12, "Alle Werte die nach 12 folgen:");

            DirectoryInfo DI = new DirectoryInfo(@"C:\Windows\");
            var neusten5 = DI.GetFiles().OrderByDescending(x=>x.LastWriteTime).Take(5);
            Ausgabe(neusten5, @"5 Daten die zuletzt geändert wurden aus C:\Windows\:","last access");

            // var gruppiert = Directory.EnumerateFiles(@"C:\Windows\").Select((datei, autoIndex) => new { File = datei, Index = autoIndex }).GroupBy(x => x.Index / 5).Select(g => g.Select(x => x.File).ToList()); //der 2. Parameter von Select definiert den Indexer
            var gruppiertChunk = DI.GetFiles().Chunk(5).Select(x => x);
            Console.WriteLine("Gruppiert in 5er-Blöcken (Chunk):");

            int i = 0;
            AusgabeChunk(gruppiertChunk, "Gruppiert in 5er-Blöcken (Chunk):");
            foreach(var v in gruppiertChunk)
            {
                Console.WriteLine("Seite "+i);
                foreach(var vi in v)
                {
                    Console.WriteLine(vi);
                }
                i++;
                Console.WriteLine("----------------");
            }
            Console.WriteLine("Alternative Ausgabe:");
            Console.WriteLine(string.Join("\n\n", DI.GetFiles().Chunk(5).Select(einChunk => string.Join("\n", einChunk.Select(datei => datei.Name)))));

        }



        public static void AusgabeChunk(IEnumerable<IEnumerable<FileInfo>> collection,string name)
        {
            int i = 0;
            foreach (var innereCollection in collection)
            {
                Console.WriteLine("Seite " + i);
                foreach (var eintrag in innereCollection)
                {
                    Console.WriteLine(eintrag);
                }
                i++;
                Console.WriteLine("----------------");
            }
        }
    
        public static void Ausgabe<T>(IEnumerable<T> sammlung, string name)
        {
            Console.WriteLine(name);
            foreach (T t in sammlung)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine();
        }

        public static void Ausgabe(IEnumerable<FileInfo> sammlung, string name,params string[] options)
        {
            Console.WriteLine(name);
            foreach (FileInfo t in sammlung)
            {
                Console.Write(t+" - ");
                if(options.Contains("size"))
                {
                    Console.Write(t.Length + " Byte\n");
                }
                if (options.Contains("last access"))
                {
                    Console.Write(t.LastAccessTime+"\n");
                }
            }
            Console.WriteLine();
        }
    }
}
