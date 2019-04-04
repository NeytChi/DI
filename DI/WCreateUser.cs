using System;
using Common.NDatabase;

namespace DI
{
    public partial class WCreateUser : Gtk.Window
    {
        public WCreateUser() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }

        protected void OnButton1Pressed(object sender, EventArgs e)
        {
            string userName = entry1.Text;
            string password = entry2.Text;
            ClientConnection.client.CreateUser(userName, password);
            Hide();
            Dispose();
        }
    }
}
