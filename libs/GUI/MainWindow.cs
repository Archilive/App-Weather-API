using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using CSharp_Meteo.API;

namespace CSharp_Meteo.GUI
{
    class MainWindow : Window
    {
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);
            new MainApplication(builder);
            new City(builder);
            new Prevision(builder);
            new Parameter(builder);

            DeleteEvent += Window_DeleteEvent;            
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}
