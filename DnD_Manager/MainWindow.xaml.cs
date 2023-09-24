using DnD_Manager.Classes;
using DnD_Manager.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DnD_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            CreateNecessaryFilesAndDirectories();
        }

        public static void CreateNecessaryFilesAndDirectories()
        {
            Directory.CreateDirectory(DndDirectories.ScenesFolder);
            Directory.CreateDirectory(DndDirectories.SoundsFolder);
            Directory.CreateDirectory(DndDirectories.MusicFolder);
            Directory.CreateDirectory(DndDirectories.CharactersFolder);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ScenesButton_Click(object sender, RoutedEventArgs e)
        {
            MainPagesFrame.Content = new ScenesPage();
        }

        private void AssetsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPagesFrame.Content = new AssetsPage();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ClearMainPagesFrame()
        {
            MainPagesFrame.Content = null;
        }
    }
}
