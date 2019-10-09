using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilePatcher
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileInfo toPatch;
        FileInfo patch;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                toPatch = new FileInfo(ofd.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "FP files (*.fp)|*.fp|All files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                patch = new FileInfo(ofd.FileName);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(toPatch == null)
            {
                MessageBox.Show("Please select a file to patch!");
                return;
            }
            if (patch == null)
            {
                MessageBox.Show("Please select a patch!");
                return;
            }
            //get offset from file
            string data = File.ReadLines(patch.FullName).ToList()[0];
            string offset = Tokens.getData(data, "Offset");
            progBar.Value += 1;

            //get patch data from file
            data = File.ReadLines(patch.FullName).ToList()[1];
            string patchedData = Tokens.getData(data, "Patch");
            string[] patchedDataArr = patchedData.Split(' ');
            List<byte> patchBytes = new List<byte>();
            foreach(string byteStr in patchedDataArr)
            {
                patchBytes.Add(Convert.ToByte(byteStr, 16));
            }
            progBar.Value += 1;

            using (Stream stream = File.Open(toPatch.FullName, FileMode.Open))
            {
                stream.Position = Convert.ToInt64(offset, 16);
                stream.Write(patchBytes.ToArray(), 0, patchBytes.Count);
                progBar.Value += 1;
            }

            MessageBox.Show("Patch complete!");
        }
    }
}
