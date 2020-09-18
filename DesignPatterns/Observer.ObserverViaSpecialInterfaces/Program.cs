using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Observer.ObserverViaSpecialInterfaces
{
    //  IObserver, IObservable

    public class Event
    {

    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }

    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            subscriptions.Add(subscription);
            return subscription;
        }

        public void FallIll()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent { Address = "123 London Rd" });
            }
        }

        private class Subscription : IDisposable
        {
            private readonly Person person;
            public readonly IObserver<Event> Observer;
            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }

            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }

    class Program : IObserver<Event>
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var person = new Person();
            //IDisposable sub = person.Subscribe(this); // option 1

            person.OfType<FallsIllEvent>().Subscribe(args => Console.WriteLine($"A doctor is required at {args.Address}")); // option 2

            person.FallIll();

        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
            {
                Console.WriteLine($"A doctor is required at {args.Address}");
            }
        }
    }
}
