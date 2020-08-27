using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using NUnit.Framework;
using static System.Console;

namespace Singleton.Implementation
{
    //  It's a Creational Design Pattern which purpose is to guarantee the existence 
    // of a single instance of a class, and the access to this class needs to be global.

    // Keys to creating this pattern:
    // 1. Private Constructor
    // 2. Static Private Field 
    // 3. Public Static Property

    // Downsides of using Singleton:
    // 1. It hides the dependencies
    // 2. It violates the Single Responsibility Principle (controlling th elife cicle and doing the job it has been designed for)
    // 3. If the class has a state, it is global and is present during the entire life cicle of the application

    //  But in cases where a dependency is used in many parts of the application it could be a good idea to declare it as a Singleton.

    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        static Dictionary<string, int> capitals;
        private static int instanceCount; //0
        public static int Count => instanceCount;
        private SingletonDatabase() // private constructor
        {
            instanceCount++;
            WriteLine("Initializing Database");
            capitals = File.ReadAllLines(
                                Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location)
                                .DirectoryName, "Capitals.txt")
                            )
                           .Batch(2).ToDictionary(
                                        list => list.ElementAt(0).Trim(),
                                        list => int.Parse(list.ElementAt(1))
                                        );
        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase()); // private static field
        public static SingletonDatabase Instance => instance.Value; // public static property
    }

    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        public OrdinaryDatabase()
        {
            WriteLine("Initializing Database");
            capitals = File.ReadAllLines(
                                Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location)
                                .DirectoryName, "Capitals.txt")
                            )
                           .Batch(2).ToDictionary(
                                        list => list.ElementAt(0).Trim(),
                                        list => int.Parse(list.ElementAt(1))
                                        );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "﻿Tokyo";
            WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
    }
}
