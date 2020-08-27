using System;
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace PrototypePattern.CopyThroughSerialization
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }

        public class Address
        {
            public string StreetAddress, City, Country;

            public Address()
            {

            }

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
        }

        public class Person
        {
            public string Name;
            public Address Address;

            public Person()
            {

            }

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
        }

        public class CopyConstructors
        {
            static void Main(string[] args)
            {
                var john = new Person("John", new Address("123 London Road", "London", "UK"));

                var chris = john.DeepCopyXml();

                chris.Name = "Chris";
                WriteLine(john);
                WriteLine(chris);


            }
        }
    }
}