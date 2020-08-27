using System;
using System.Collections.Generic;
using static System.Console;


namespace FactoryMethod
{
    //  A Factory method is a static method that creates objects. It can be external or reside inside the object as an inner class

    //  A Factory is a separate component which knows how to initialize types in a particular way

    //  A Factory Method is a static method that creates objects
    
    //  Hierarchies of factories can be used to create related objects

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
            private double x, y;

            private Point(double a, double b)
            {
                x = a;
                y = b;
            }

            public static class Factory
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

        }

        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            WriteLine(point);
        }
    }
}