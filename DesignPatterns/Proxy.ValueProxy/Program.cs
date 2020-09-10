using System;
using System.Diagnostics;

namespace Proxy.ValueProxy
{
    //  constructed over a primitive type

    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage
    {
        private readonly float value;

        internal Percentage(float value)
        {
            this.value = value;
        }

        public static float operator *(float f, Percentage p)
        {
            return f * p.value;
        }

        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a.value + b.value);
        }

        public override string ToString()
        {
            return $"{value * 100}%";
        }
    }

    public static class PercentageExtensions
    {
        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100.0f);
        }
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.0f);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(10f * 5.Percent());
            Console.WriteLine(2.Percent() + 3.Percent());
        }
    }
}
