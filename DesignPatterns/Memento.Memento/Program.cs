using System;
using static System.Console;

namespace Memento.Memento
{
    //  Keep a memento of an object's state to return to that state
    //  A token/handle representing the system state. It lets us
    // roll back to the state when the token was generated. May or may not 
    // directly expose state information

    //  Mementos are used to roll back states arbitrarily.
    //  A Memento is simply a token/handle class with (typically) no functions of is own.
    //  A memento is not required to expose directly the stae(s) to which it reverts the system.
    //  It can be used to implement undo and redo operations.

    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            Balance = balance;
        }
    }

    public class BankAccount
    {
        public int balance;
        public BankAccount(int balance)
        {
            this.balance = balance;
        }

        public Memento Deposit(int amount)
        {
            balance += amount;
            return new Memento(balance);
        }

        public void Restore(Memento m)
        {
            balance = m.Balance;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            WriteLine(ba);

            ba.Restore(m1);
            WriteLine(ba);

            ba.Restore(m2);
            WriteLine(ba);

        }
    }
}
