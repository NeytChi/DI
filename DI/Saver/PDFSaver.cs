using System;
using DI.NDatabase;

namespace DI.Saver
{
    public class PDFSaver : ISaverDatabase
    {
        ClientSQL clientSQL;

        public PDFSaver(ClientSQL clientSQL)
        {
            this.clientSQL = clientSQL;
        }
        public void SaveDatabase(string database)
        {

        }
    }
}
