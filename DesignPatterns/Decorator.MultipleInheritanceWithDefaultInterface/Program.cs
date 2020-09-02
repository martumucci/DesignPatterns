using System;
using static System.Console;

namespace Decorator.MultipleInheritanceWithDefaultInterface
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if (Age >= 10)
            {
                WriteLine("I am flying");
            }
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if (Age >= 10)
            {
                WriteLine("I am crawling");
            }
        }
    }

    public class Organism { }

    public class Dragon : Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }

    // inheritance
    // SmallDragon(Dragon)
    // extension method 
    // C#8 default interface methods

    class Program
    {
        static void Main(string[] args)
        {
            Dragon d = new Dragon { Age = 5 };

            if (d is IBird bird)
            {
                bird.Fly();
            }

            if (d is ILizard lizard)
            {
                lizard.Crawl();
            }
        }
    }
}
