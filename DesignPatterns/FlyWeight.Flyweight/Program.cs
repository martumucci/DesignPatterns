using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace FlyWeight.Flyweight
{
    //  Its goal is to achieve better space optimization
    //  It is a space optimization technique that lets us use less memory 
    // by storing externally the data associated with similar objects.

    //  The idea is to store the common data externally, define the idea of ranges on 
    // homogeneous collections and store data related to those ranges.

    //  (.NET string intering.)
    

    public class User
    {
        private string fullName;
        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", names.Select(i => strings[i]));
    }

    [TestFixture]
    class Program
    {
        static void Main(string[] args)
        {
        }

        [Test]
        public void TestUser() // 1655033 
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(m => { WriteLine(m.SizeInBytes); });
        }

        [Test]
        public void TestUser2() //  1296991 - less memory usage
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User2>();

            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User2($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(m => { WriteLine(m.SizeInBytes); });
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 100)
                .Select(i => (char)('a' + rand.Next(26)))
                .ToArray()
                );
        }
    }
}
