using System;
using Common.NDatabase;
namespace DI
{
    public partial class WindowAddUser : Gtk.Window
    {
        public WindowAddUser() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
        protected void OnButton3Pressed(object sender, EventArgs e)
        {
            string userName = entry1.Text;
            string password = entry2.Text;
            ClientConnection.client.CreateUser(userName, password);
            Hide();
            Dispose();
        }
    }
}
