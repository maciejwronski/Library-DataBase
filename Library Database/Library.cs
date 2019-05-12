using System;
using System.Collections.Generic;

namespace Library_Database
{
    class Library
    {
        public const int maxPeople = 100000;
        List<Person> menList;
        List<Person> womenList;
        List<Book> bookList;
        PeselGenerator peselGenerator;
        Random random;
        string libraryName;

        public string LibraryName { get => libraryName; set => libraryName = value; }
        internal List<Book> BookList { get => bookList; set => bookList = value; }
        internal List<Person> WomenList { get => womenList; set => womenList = value; }
        internal List<Person> MenList { get => menList; set => menList = value; }

        public Library(string libName)
        {
            menList = new List<Person>();
            womenList = new List<Person>();
            bookList = new List<Book>();
            peselGenerator = new PeselGenerator();
            random = new Random();

            LibraryName = libName;
        }
        private int ReturnAmountOfWomens()
        {
            Random random = new Random();
            return random.Next(maxPeople);
        }
        public void GenerateBookData(string[] BookNames, int nextPortion, int maxCount)
        {
            int start = (BookNames.Length * nextPortion / maxCount);
            int end = (BookNames.Length * (nextPortion+1) / maxCount);
            int minimumPageCount = 100;
            int maximumPageCount = 2000;
            int minimumYear = 1901;
            int maximumYear = 2019;
            for (int i = start; i < end; i++)
            {
                int authorsNum = random.Next(1, 3);
                List<Person> authors = new List<Person>();
                Console.Write("Book name: " + BookNames[i] + " "  );
                for (int author = 0; author < authorsNum; author++)
                {
                    if (random.Next(0, 2) == 0) // Generate Man
                        authors.Add(GetRandomMan());
                    else // Generate Woman
                        authors.Add(GetRandomWoman());
                }
                bookList.Add(new Book(BookNames[i], authors, random.Next(minimumPageCount, maximumPageCount), 
                    random.Next(minimumYear, maximumYear)));
            }
            Console.WriteLine(nextPortion + " Lib generated");
        }
        public void GeneratePeopleData()
        {
            int NumberOfWomen = ReturnAmountOfWomens();
            int NumberOfMen = maxPeople - NumberOfWomen;
            while (menList.Count < NumberOfMen)
            {
                menList.Add(GetRandomMan());
                Console.WriteLine("Loading mans: " + MenList.Count);
            }
            while (womenList.Count < NumberOfWomen)
            {
                womenList.Add(GetRandomWoman());
                Console.WriteLine("Loading womans: " + WomenList.Count);
            }
            Console.WriteLine("People Data Generated! Total: " + womenList.Count + MenList.Count);
        }
        private Person GetRandomMan()
        {
            return new Person(MainWindowForm.menNames[random.Next(100)], MainWindowForm.menSurnames[random.Next(100)], peselGenerator.Generate(true), 'M');
        }
        private Person GetRandomWoman()
        {
           return new Person(MainWindowForm.WomenNames[random.Next(100)], MainWindowForm.womenSurnames[random.Next(100)], peselGenerator.Generate(false), 'W');
        }
    }

}
