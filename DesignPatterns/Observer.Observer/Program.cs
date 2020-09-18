using System;
using static System.Console;

namespace Observer.Observer
{
    //  We need to be notified when certain things happen in our system,
    // so we want to listen to events and be notified when they occur.

    //  An observer is an object that wishes to be informed about events 
    // happening in the system. The entity generating the events is an observable.

    //  It is an intrusive approach: an observer must provide an event to subscribe to
    //  Spetial care must be taken to prevent issues in multithreaded scenarios
    //  .NET comes with observable collections
    //  IObserver<T> and IOBservable<T> are used in stream processing (Reactive Extensions)

    public class FallsIllEventArgs
    {
        public string Address;
    }

    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs { Address = "123 London Road" });
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();

            person.FallsIll += CallDoctor;
            person.CatchACold();
        }

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            WriteLine($"A doctor has been called to {e.Address}");
        }
    }
}
