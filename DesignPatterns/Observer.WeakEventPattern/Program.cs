using System;
using System.Windows;
using static System.Console;

namespace Observer.WeakEventPattern
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            button.Clicked += ButtonOnClicked;
            //WeakEventManager<Button, EventArgs>.AddHandler(button, "Clicled", ButtonOnClicked); // WindowsBase not avaiable in .NET Core
        }

        private void ButtonOnClicked(object sender, EventArgs e)
        {
            WriteLine("Button clicked (Window handler)");

        }

        ~Window()
        {
            WriteLine("Window finalized");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var btn = new Button();
            var window = new Window(btn);
            var windowRef = new WeakReference(window);
            btn.Fire();

            WriteLine("Setting window to null");
            window = null;

            fireGC();
            WriteLine($"is the window alive after GC? {windowRef.IsAlive}");
        }

        private static void fireGC()
        {
            WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            WriteLine("GC is done");

        }
    }
}
