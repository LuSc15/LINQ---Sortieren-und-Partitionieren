using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace LINQ___Sortieren_und_Partitionieren
{
    internal class Program
    {
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
            var absteigend = numbers.OrderByDescending(x => x);
            var geradeAufsteigend = numbers.OrderBy(x => x).Where(x => x % 2 == 0);
            Console.WriteLine("aufsteigend:");
            foreach(var v in aufsteigend) Console.WriteLine(v);

            Console.WriteLine("Absteigend:");
            foreach(var v in absteigend)  Console.WriteLine(v);

            Console.WriteLine("gerade aufsteigend:");
            foreach(var v in geradeAufsteigend) Console.WriteLine(v);


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
            var laengeAufsteigendDannAlphabetisch = numbersString.OrderBy(x => x.Length).ThenBy(x => x);
            var drehen = numbersString.Reverse();
            Console.WriteLine("Nach Länge aufsteigend:");
            foreach(var v in laengeAufsteigend) Console.WriteLine(v);
            Console.WriteLine("Länge aufsteigend dann alphabetisch:");
            foreach(var v in laengeAufsteigendDannAlphabetisch) Console.WriteLine(v);
            Console.WriteLine("umgekehrte Reihenfolge:");
            foreach(var v in drehen) Console.WriteLine(v);

            Console.WriteLine("Dateien aus C:Windows nach Namen sortiert:");
            DirectoryInfo d = new DirectoryInfo(@"C:\Windows\");
            var dateienNachName = d.GetFiles().OrderBy(x => x.Name);
            foreach (var v in dateienNachName) Console.WriteLine(v.Name);

            Console.WriteLine("Datei nach größe");
            var dateienNachGroesse = d.GetFiles().OrderBy(x => x.Length);
            foreach(var v in dateienNachGroesse) Console.WriteLine(v.Name+" "+v.Length);
            Console.WriteLine("Dateien nach letztem Zugriff:");
            var letzterZugriff = d.GetFiles().OrderBy(x => x.LastAccessTime.Ticks);
            foreach(var v in letzterZugriff) Console.WriteLine(v.Name+" - "+v.LastAccessTime.ToString());


            int[] numbers3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 22, 12, 16, 18, 11, 19, 13 };
            var ersten5 = numbers3.Take(5);
            foreach(var v in ersten5) Console.WriteLine(v);
            var letzten5 = numbers.TakeLast(5);
            foreach(var v in letzten5) Console.WriteLine(v);
            var ohneErsteUndLetzten3 = numbers.Skip(3).SkipLast(3);
            foreach(var v in ohneErsteUndLetzten3) Console.WriteLine(v);
            var groesser0 = numbers.Where(x => x > 0);
            foreach(var v in groesser0) Console.WriteLine(v);
            var nach12 = numbers.Skip(Array.IndexOf(numbers, 12)+1).Select(x => x);
            Console.WriteLine("Skip bis 12:");
            foreach (var v in nach12) Console.WriteLine(v);

            DirectoryInfo DI = new DirectoryInfo(@"C:\Windows\");
            var neusten5 = DI.GetFiles().OrderByDescending(x=>x.LastWriteTime).Take(5);
            Console.WriteLine("letzte 5 Dateien aus Windows:");
            foreach(var v in neusten5) Console.WriteLine(v+" - "+v.LastWriteTime);

            var gruppiert = Directory.EnumerateFiles(@"C:\Windows\").Select((datei, autoIndex) => new { File = datei, Index = autoIndex }).GroupBy(x => x.Index / 5).Select(g => g.Select(x => x.File).ToList()); //der 2. Parameter von Select definiert den Indexer
            Console.WriteLine("Gruppiert in 5er-Blöcken:");
            int i = 0;
            foreach(var v in gruppiert)
            {
                Console.WriteLine("Seite "+i);
                foreach(var vi in v)
                {
                    Console.WriteLine(vi);
                }
                i++;
                Console.WriteLine("----------------");
            }



        }
        //public static void Ausgabe(List<T> sammlung,string name)
        //{
        //    Console.WriteLine(name);
        //    foreach(T t in sammlung)
        //    {
        //        Console.WriteLine(t);
        //    }
        //}
    }
}
