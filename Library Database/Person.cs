using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Database
{
    class Person
    {
        string name;
        string surname;
        string pesel;
        char gender;
        public Person(string n, string s, string p, char gend)
        {
            name = n;
            surname = s;
            pesel = p;
            gender = gend;
        }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Pesel { get => pesel; set => pesel = value; }
        public char Gender { get => gender; set => gender = value; }
    }
}
