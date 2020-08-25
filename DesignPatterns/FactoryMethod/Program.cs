using System;
using System.Collections.Generic;
using static System.Console;


namespace FactoryMethod
{
    //  You get to have an overload with the same sets of arguments, but with different descriptive names
    // and the names of the factory methods are also unique.

    public class Demo
    {
        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }
        public class Point
        {
            //Factory method
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }

            private double x, y;

            private Point(double a, double b)
            {
                x = a;
                y = b;

            }


        }
        static void Main(string[] args)
        {
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            WriteLine(point);
        }
    }
}