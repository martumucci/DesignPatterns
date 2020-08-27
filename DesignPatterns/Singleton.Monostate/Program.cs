using System;
using static System.Console;

namespace Singleton.Monostate
{
    //  The Monostate Pattern is a variation of the Singleton Pattern.
    //  All the objects created share the same data

    public class CEO
    {
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Age
        {
            get => age;
            set => age = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ceo = new CEO();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new CEO();

            WriteLine(ceo2.Name);
            WriteLine(ceo2.Age);
        }
    }
}
