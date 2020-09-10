using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Proxy.ProxyViewModel
{
    //  MVVM

    // Model
    public class Person
    {
        public string FirstName, LastName; 
    }

    // View = ui


    // View Model
    public class PersonViewModel : INotifyPropertyChanged // proxy over the person
    {
        private readonly Person person;
        public PersonViewModel(Person person)
        {
            this.person = person;
        }

        public string FirstName
        {
            get => person.FirstName;
            set
            {
                if (person.FirstName == value) return;
                person.FirstName = value;
                OnProperyChanged();
                OnProperyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get => person.LastName;
            set
            {
                if (person.LastName == value) return;
                person.LastName = value;
                OnProperyChanged();
                OnProperyChanged(nameof(LastName));
            }
        }

        public string FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                if(value == null)
                {
                    FirstName = LastName = null;
                    return;
                }
                var items = value.Split();
                if (items.Length > 0) FirstName = items[0];
                if (items.Length > 1) LastName = items[1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnProperyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
