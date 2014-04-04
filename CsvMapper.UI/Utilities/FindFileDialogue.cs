using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvMapper.UI.Utilities
{
    public class FindFileDialogue
    {

        public  string LocationOfFile { get; set; }

        public FindFileDialogue(string filter)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            switch (filter)
            {
                case "csv":
                    dlg.DefaultExt = ".csv";
                    dlg.Filter = "CSV Files (*.csv)|*.csv|All Files |*.*";
                    break;
            }

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                LocationOfFile = filename;
            }
        }
    }
}
