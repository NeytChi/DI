using System;
using Common.NDatabase;

namespace DI
{
    public partial class WindowCreateDatabase : Gtk.Window
    {
        public WindowTable windowTable;
        public WindowCreateDatabase() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
        protected void OnButton4Pressed(object sender, EventArgs e)
        {
            string databaseName = entry1.Text;
            if (databaseName == null)
            {
                return;
            }
            ClientConnection.client.CreateDatabase(databaseName);
            if (windowTable != null)
            {
                windowTable.InitTreeView();
            }
            Hide();
            Dispose();
        }
    }
}
