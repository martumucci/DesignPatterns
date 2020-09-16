using Autofac;
using ImpromptuInterface;
using System;
using System.Dynamic;
using static System.Console;

namespace NullObject.DynamicNullObject
{
    // better for testing, but not so good for production code

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

    public class Null<TInterface> : DynamicObject where TInterface : class
    {
        public static TInterface Instace => new Null<TInterface>().ActLike<TInterface>();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var log = Null<ILog>.Instace;
            log.Info("whatever");
            var ba = new BankAccount(log);
            ba.Deposit(100);

        }
    }
}
