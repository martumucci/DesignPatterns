using System;
using static System.Console;

namespace Proxy.Proxy
{
    //  An interface for accessing a particular resource
    //  A class that functions as an interface to a particular resource. 
    // That resource may be remote, expensive to construct, or may require 
    // logging or some other added functionality.

    //  A proxy has the same interface as the underlying object. 
    //  To create a proxy, simply replicate the existing interface of an object.
    //  Add relevant functionality to the redefined member functions.


    //  Protection Proxy

    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            WriteLine("Car is being driven");
        }
    }

    public class Driver
    {
        public int Age { get; set; }

        public Driver(int age)
        {
            Age = age;
        }
    }

    public class CarProxy : ICar
    {
        private Driver driver;
        private Car car = new Car();
        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }
        public void Drive()
        {
            if (driver.Age >= 16)
            {
                car.Drive();
            }
            else
            {
                WriteLine("too young");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICar car = new CarProxy(new Driver(12));
            car.Drive();
        }
    }
}
