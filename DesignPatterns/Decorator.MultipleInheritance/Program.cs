using System;
using static System.Console;

namespace Decorator.MultipleInheritance
{
    //  It works but it is very inconvinient

    class Program
    {
        public class Bird : IBird
        {
            public int Weight { get; set; }
            public void Fly()
            {
                WriteLine($"Soaring in the sky with weight {Weight}");
            }
        }

        public class Lizard : ILizard
        {
            public int Weight { get; set; }

            public void Crawl()
            {
                WriteLine($"Crawling in the dirt with weight {Weight}");
            }
        }

        public class Dragon : IBird, ILizard // Bird, Lizard
        {
            public int weight { get; set; }
            private Bird bird = new Bird();
            private Lizard lizard = new Lizard();

            public void Crawl()
            {
                lizard.Crawl();
            }
            public void Fly()
            {
                bird.Fly();
            }

            public int Weight
            {
                get { return weight; }
                set
                {
                    weight = value;
                    bird.Weight = value;
                    lizard.Weight = value;
                }
            }
            
        }

        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Weight = 3;
            d.Fly();
            d.Crawl();
        }
    }
}
