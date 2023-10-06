using DnD_Manager.Classes;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DnD_Manager.Pages
{
    /// <summary>
    /// Interaction logic for AssetsPage.xaml
    /// </summary>
    public partial class AssetsPage : Page
    {
        private MediaPlayer Player;
        private Timer TimeSyncTimer;
        private bool IsSliderBeingDragged = false;
        public AssetsPage()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            Player = new MediaPlayer();
            Player.MediaOpened += delegate
            {
                ProgressSlider.Maximum = Player.NaturalDuration.TimeSpan.TotalMilliseconds;
                ProgressSlider.Value = Player.Position.TotalMilliseconds;
            };

            MusicListBox.ItemsSource = Global.Music;
            MusicListBox.Items.Refresh();

            CharactersListBox.ItemsSource= Global.Characters;
            CharactersListBox.Items.Refresh();

            TimeSyncTimer = new Timer(_ => {
                if (!IsSliderBeingDragged)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            ProgressSlider.Value = Player.Position.TotalMilliseconds;
                        } catch (Exception) { }
                        
                    });
                }
            }, null, 0, 200);
        }

        private void UpdateMusicListBox()
        {
            string[] musicFilesPaths = FilesHandler.GetAudioFilesInFolder(DndDirectories.MusicFolder);
            Global.Music.Clear();
            foreach (string musicFilePath in musicFilesPaths) Global.Music.Add(musicFilePath);

            MusicListBox.Items.Refresh();
        }

        private void UpdateCharactersListBox()
        {
            string[] imagesFilesPaths = FilesHandler.GetImagesInFolder(DndDirectories.CharactersFolder);
            Global.Characters.Clear();
            foreach (string imageFilePath in imagesFilesPaths) Global.Characters.Add(imageFilePath);

            CharactersListBox.Items.Refresh();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MusicListBox.SelectedItem is null) return;
            string clickedMusic = (string)MusicListBox.SelectedItem;
            Player.Open(new Uri(clickedMusic));
            Player.Play();

            PlayButton.Visibility = Visibility.Hidden;
            PauseButton.Visibility = Visibility.Visible;
        }

        private void MusicFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", DndDirectories.MusicFolder);
        }

        private void CharactersFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", DndDirectories.CharactersFolder);
        }

        private void MusicRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateMusicListBox();
        }

        private void RefreshCharactersButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCharactersListBox();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Close();
            ((MainWindow)Application.Current.MainWindow).ClearMainPagesFrame();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Play();
            PlayButton.Visibility = Visibility.Hidden;
            PauseButton.Visibility = Visibility.Visible;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
            PlayButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Hidden;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
            Player.Position = TimeSpan.Zero;

            PlayButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Hidden;
        }

        private void ControlElement_DragStarted(object sender, DragStartedEventArgs e)
        {
            IsSliderBeingDragged = true;
        }

        private void ControlElement_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Player.Position = TimeSpan.FromMilliseconds(ProgressSlider.Value);
            IsSliderBeingDragged = false;
        }

        private void CharactersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Uri ImageUri = new Uri((string)CharactersListBox.SelectedItem);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            image.UriSource = ImageUri;
            image.EndInit();

            DisplayedCharacter.Source = image;
        }
    }
}
