using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using Core;

namespace Projektas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            string[] fileName = null;
            string path = @"c:\Users\MSI\Desktop";
            //string filter = "Image files|*.bmp;*.jpg;*.png";
            string filter2 = "*.png";

            fileName = Core.Class1.OpenFolder(path, filter2);

            
            //Image.Source = Core.Class1.OpenImage(fileName);
        }
    }
}
