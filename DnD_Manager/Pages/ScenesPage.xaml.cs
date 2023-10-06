using DnD_Manager.Classes;
using DnD_Manager.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DnD_Manager.Pages
{
    /// <summary>
    /// Interaction logic for ScenesPage.xaml
    /// </summary>
    public partial class ScenesPage : Page
    {
        public ScenesPage()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            ScenesListBox.ItemsSource = Global.Scenes;
            ScenesListBox.Items.Refresh();
        }

        private void UpdateScenesListBox()
        {
            string[] imagesPaths = FilesHandler.GetImagesInFolder(DndDirectories.ScenesFolder);
            Global.Scenes.Clear();
            foreach (string imagePath in imagesPaths) Global.Scenes.Add(new Scene(imagePath));
            ScenesListBox.Items.Refresh();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateScenesListBox();
        }

        private void ScenesFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", DndDirectories.ScenesFolder);
        }

        private void ScenesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Uri ImageUri = new Uri(((Scene)ScenesListBox.SelectedItem).ImagePath);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            image.UriSource = ImageUri;
            image.EndInit();
            
            DisplayedImage.Source = image;
        }

        private void SoundsButton_Click(object sender, RoutedEventArgs e)
        {
            Button soundsButton = (Button)sender;
            Scene clickedScene = (Scene)soundsButton.CommandParameter;
            new SoundsWindow(clickedScene).ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ClearMainPagesFrame();
        }
    }
}
