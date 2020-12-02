using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace Service_Center
{
    class Export
    {
        //Экспорт в MS Word
        public void Export_Word()
        {
           

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Documents (*.doc|*.doc)";
            //saveFileDialog.FileName = (DBConnection.Name_File);
            saveFileDialog.ShowDialog();
        }

}
}
