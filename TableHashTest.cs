using System;
using System.Collections.Generic;
namespace SM.Algorithms
{
    class TableHashDemo
    {
       public static void TestTableHashOverwirteEmployees()
        {
            TableHash<string, string> employees = new TableHash<string, string>();
            employees.Add("640-62-2255", "Sailaja");
            employees.Add("707-20-7986", "Ranga");
            employees.Add("435-90-8764", "Abhiram");

            // Access a particular key and update value
            if (employees.ContainsKey("640-62-2255"))
            {
                string empName = employees["640-62-2255"];
                employees["640-62-2255"] = "newsailaja";
                Console.WriteLine("Employee 640-62-2255's new name is: " + employees["640-62-2255"]);
            }
            else
            {
                Console.WriteLine("Employee 640-62-2255 is not in the hash table...");
            }
        }

        public static void TestTableHashDuplicateOpenWith()
        {
            TableHash<string, string> openWith = new TableHash<string, string>();
            // Add some elements to the dictionary. There are no
            // duplicate keys, but some of the values are duplicates.
            openWith.Add("txt", "notepad.exe");
            openWith.Add("bmp", "paint.exe");
            openWith.Add("dib", "paint.exe");
            openWith.Add("rtf", "wordpad.exe");

            // The Add method throws an exception if the new key is
            // already in the dictionary.
            try
            {
                openWith.Add("txt", "winword.exe");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("An element with Key = \"txt\" already exists.");
            }
        }
    }
}
