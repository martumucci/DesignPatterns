using System;
using System.Collections.Generic;
using static System.Console;


namespace FactoryMethod
{
    //  A Factory is a separate component which knows how to initialize types in a particular way

    //  You get to have an overload with the same sets of arguments, but with different descriptive names
    // and the names of the factory methods are also unique.

    public class Demo
    {
        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        public static class PointFactory
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

        }
        public class Point
        {
            private double x, y;

            public Point(double a, double b)
            {
                x = a;
                y = b;

            }


        }
        static void Main(string[] args)
        {
            var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
            WriteLine(point);
        }
    }
}