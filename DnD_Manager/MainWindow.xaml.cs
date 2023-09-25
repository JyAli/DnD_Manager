using DnD_Manager.Classes;
using DnD_Manager.Pages;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            Closing += delegate { FilesHandler.Save(); };
            CreateNecessaryFilesAndDirectories();

            //The timer that is responable for saving every 10 seconds
            Timer saveTimer = new Timer()
            {
                Interval = 10000,
                AutoReset = true,
                Enabled = true,
            };
            saveTimer.Elapsed += delegate { FilesHandler.Save(); };
            saveTimer.Start();
        }

        public static void CreateNecessaryFilesAndDirectories()
        {
            Directory.CreateDirectory(DndDirectories.ScenesFolder);
            Directory.CreateDirectory(DndDirectories.SoundsFolder);
            Directory.CreateDirectory(DndDirectories.MusicFolder);
            Directory.CreateDirectory(DndDirectories.CharactersFolder);
            Directory.CreateDirectory(DndDirectories.DataFolder);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MainPagesFrame.Content = new ControlsPage();
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
