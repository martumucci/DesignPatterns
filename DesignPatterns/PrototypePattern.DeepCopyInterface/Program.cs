using System;
using System.Net.Sockets;
using static System.Console;

namespace PrototypePattern.DeepCopyInterface
{
    //  This approach works but is very tedious if you have many different classes involved.

    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Address : IPrototype<Address>
    {
        public string StreetAddress, City, Country;

        public Address(string streetAddress, string city, string country)
        {
            StreetAddress = streetAddress ?? throw new ArgumentNullException(paramName: nameof(streetAddress));
            City = city ?? throw new ArgumentNullException(paramName: nameof(city));
            Country = country ?? throw new ArgumentNullException(paramName: nameof(country));
        }

        public Address(Address other)
        {
            StreetAddress = other.StreetAddress;
            City = other.City;
            Country = other.Country;
        }

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
        }

        public Address DeepCopy()
        {
            return new Address(StreetAddress, City, Country);
        }
    }

    public class Person : IPrototype<Person>
    {
        public string Name;
        public Address Address;

        public Person(string name, Address address)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public Person(Person other)
        {
            Name = other.Name;
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }

        public Person DeepCopy()
        {
            return new Person(Name, Address.DeepCopy());
        }
    }

    public class CopyConstructors
    {
        static void Main(string[] args)
        {
            var john = new Person("John", new Address("123 London Road", "London", "UK"));

            //var chris = john;
            var chris = john.DeepCopy();

            chris.Name = "Chris";
            WriteLine(john);
            WriteLine(chris);


        }
    }
}