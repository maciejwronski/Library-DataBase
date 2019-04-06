using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Database
{
    class Book
    {
        string name;
        List<Person> authors = new List<Person>();
        int pageCount;
        int year;
        public Book(string n, List<Person> auth, int pagec, int y)
        {
            name = n;
            authors = auth;
            pageCount = pagec;
            year = y;
        }
        public string Name { get => name; set => name = value; }
        public int PageCount { get => pageCount; set => pageCount = value; }
        public int Year { get => year; set => year = value; }
        internal List<Person> Authors { get => authors; set => authors = value; }
    }
}
