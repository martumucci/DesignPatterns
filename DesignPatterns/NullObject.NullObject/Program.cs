using Autofac;
using JetBrains.Annotations;
using System;
using static System.Console;

namespace NullObject.NullObject
{
    //  Behavioral design pattern with no behaviors.
    //  An object that conforms to the required interface, satisfying 
    // a dependency requirement of some other object, but does nothing at all.

    //  To build a Null Object:
    // 1. Implement the required interface
    // 2. Rewrite the methods with empty bodies
    //  2.a. if the method is non-void, return default(T)
    // 3. Supply an instance of NullObject in place of actual object
    // 4. Dynamic construction possible.

    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            WriteLine(msg);
        }

        public void Warn(string msg)
        {
            WriteLine("WARNING ! ! !  " + msg);
        }
    }
    public class BankAccount
    {
        private ILog log;
        private int balance;
        public BankAccount(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(paramName: nameof(log));
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log.Info($"Deposited {amount}, balance is now {balance}");
        }
    }

    public class NullLog : ILog // it isn't always safe
    {
        public void Info(string msg)
        {

        }

        public void Warn(string msg)
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var log = new ConsoleLog();
            //var ba = new BankAccount(log);
            //ba.Deposit(100);

            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();

            using (var c = cb.Build())
            {
                var ba = c.Resolve<BankAccount>();
                ba.Deposit(100);
            }

        }
    }
}
