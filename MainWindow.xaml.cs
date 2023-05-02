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
using Core;
using System.IO;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Projektas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MyFolder>? folders = new ObservableCollection<MyFolder>();
        public ObservableCollection<MyFileInfo>? allFiles = new ObservableCollection<MyFileInfo>();

        //public string Folders = @"C:\Users\MSI\Desktop\Folders.xml";
        //public string Files = @"C:\Users\MSI\Desktop\Files.xml";
        public string Files = @"C:\Users\MSI\Desktop\Files.JSON";
        public string Folders = @"C:\Users\MSI\Desktop\Folders.JSON";

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += HandleWindowLoaded;
            this.Closing += HandleWindowClosed;
        }

        public void HandleWindowClosed(object? sender, EventArgs e)
        {           
            //XmlSerialize.ConvertListOfObjectsToXml(allFiles, Files, true);
            //XmlSerialize.ConvertListOfObjectsToXml(folders, Folders, true);

            JSONSerialize.SerializeToJSON(allFiles, Files);
            JSONSerialize.SerializeToJSON(folders, Folders);
        }

        public void HandleWindowLoaded(object sender, RoutedEventArgs e)
        {
            //allFiles = XmlSerialize.ConvertListOfObjectsFromXml(allFiles, Files, true);
            //folders = XmlSerialize.ConvertListOfObjectsFromXml(folders, Folders, true);

            allFiles = JSONSerialize.DeserializeFromJson<MyFileInfo>(Files);
            folders = JSONSerialize.DeserializeFromJson<MyFolder>(Folders);

            filesListBox.ItemsSource = allFiles;

            foldersListBox.ItemsSource = folders;
        }

        private void AddFolderButton_Click(object sender, RoutedEventArgs e) 
        {
            Image.Source = null;
            
            string path = "";

            List<MyFileInfo> files = Core.Methods.OpenFolder(ref path);

            files.ToList().ForEach(allFiles.Add);

            folders.Add(new MyFolder(path));            
            
            filesListBox.ItemsSource = allFiles;
                       
            foldersListBox.ItemsSource = folders;
        }

        private void filesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            BitmapImage image = new BitmapImage();
            MyFileInfo fileInfo = (MyFileInfo)filesListBox.SelectedItem;

            string path = "";

            if (fileInfo != null)
            {
                path = fileInfo.Path;                
            }
            else 
            {
                Image.Source = null;
            }

            if (Path.Exists(path))
            {
                image = Core.Methods.OpenImage(path);
                Image.Source = image;
            }
        }  

        private void RemoveFolderButton_Click(object sender, RoutedEventArgs e)
        {
            string? selected = null;
            MyFolder folderInfo = (MyFolder)foldersListBox.SelectedItem;

            if (foldersListBox.SelectedItem != null) 
            {
                selected = foldersListBox.SelectedItem.ToString();
            }
            
            if (selected != null) 
            {                               
                folders.Remove(folderInfo);
                for (int i = allFiles.Count-1; i >=0; i--)
                {
                    if (allFiles[i].Path.Contains(folderInfo.Path))
                    {                       
                        allFiles.RemoveAt(i);
                    }
                }
            }
        }
    }
}
