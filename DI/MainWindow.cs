using DI;
using Gtk;
using System;
using Common;
using Common.NDatabase;

public partial class MainWindow : Gtk.Window
{

    public MainWindow() : base(WindowType.Toplevel)
    {
        Logger.WriteLog("Init window.", LogLevel.Usual);
        Build();
        Show();
    }
    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Logger.WriteLog("Quit from application.", LogLevel.Usual);
        Application.Quit();
        a.RetVal = true;
    }
    protected void OnButton6Pressed(object sender, EventArgs e)
    {
        if (ClientConnection.Authorization(entry2.Text, entry1.Text))
        {
            WindowTable windowTable = new WindowTable();
            Hide();
            Logger.WriteLog("Authorization user with user_name->" + entry2.Text, LogLevel.Usual);
            Dispose();
        }
        else
        {
            label1.Text = "False authorization user";
            Logger.WriteLog("False authorization user", LogLevel.Warning);
        }
    }
}
