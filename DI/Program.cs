using Gtk;
using System;

namespace DI
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            Application.Run();
        }
    }
}
