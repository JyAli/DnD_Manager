using DnD_Manager.Classes;
using DnD_Manager.Windows;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DnD_Manager.Pages
{
    /// <summary>
    /// Interaction logic for ControlsPage.xaml
    /// </summary>
    public partial class ControlsPage : Page
    {
        private PlayAreaWindow PlayArea;
        public MediaPlayer MusicPlayer;
        private Timer TimeSyncTimer;
        private bool IsSliderBeingDragged = false;
        public ControlsPage()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            ScenesListBox.ItemsSource = Global.Scenes;
            ScenesListBox.Items.Refresh();

            CharactersListBox.ItemsSource = Global.Characters;
            CharactersListBox.Items.Refresh();

            MusicListBox.ItemsSource = Global.Music;
            MusicListBox.Items.Refresh();

            PlayArea = new PlayAreaWindow();
            PlayArea.Show();

            MusicPlayer = new MediaPlayer();
            MusicPlayer.MediaOpened += delegate
            {
                ProgressSlider.Maximum = MusicPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                ProgressSlider.Value = MusicPlayer.Position.TotalMilliseconds;
            };

            TimeSyncTimer = new Timer(_ => {
                if (!IsSliderBeingDragged)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            ProgressSlider.Value = MusicPlayer.Position.TotalMilliseconds;
                        }
                        catch (Exception) { }

                    });
                }
            }, null, 0, 200);
        }

        private void ScenesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayStateComboBox.IsEnabled = ScenesListBox.SelectedItems.Count == 1;

            if (ScenesListBox.SelectedItems.Count == 1)
            {
                switch (((Scene)ScenesListBox.SelectedItems[0]).DisplayState)
                {
                    case Stretch.Uniform:
                        DisplayStateComboBox.SelectedIndex = 0;
                        break;

                    case Stretch.UniformToFill:
                        DisplayStateComboBox.SelectedIndex = 1;
                        break;

                    case Stretch.Fill:
                        DisplayStateComboBox.SelectedIndex = 2;
                        break;
                }
            }

            PlayArea.UpdateDisplayedImages(ScenesListBox.SelectedItems);
        }

        private void Music_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MusicListBox.SelectedItem is null) return;
            string clickedMusic = (string)MusicListBox.SelectedItem;
            MusicPlayer.Open(new Uri(clickedMusic));
            MusicPlayer.Play();

            PlayButton.Visibility = Visibility.Hidden;
            PauseButton.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Close();
            ((MainWindow)Application.Current.MainWindow).ClearMainPagesFrame();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Play();
            PlayButton.Visibility = Visibility.Hidden;
            PauseButton.Visibility = Visibility.Visible;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Pause();
            PlayButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Hidden;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MusicPlayer.Stop();
            MusicPlayer.Position = TimeSpan.Zero;

            PlayButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Hidden;
        }

        private void ControlElement_DragStarted(object sender, DragStartedEventArgs e)
        {
            IsSliderBeingDragged = true;
        }

        private void ControlElement_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            MusicPlayer.Position = TimeSpan.FromMilliseconds(ProgressSlider.Value);
            IsSliderBeingDragged = false;
        }

        private void EndSessionButton_Click(object sender, RoutedEventArgs e)
        {
            PlayArea.Close();
        }

        private void Character_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CharactersListBox.SelectedItem is null || ScenesListBox.SelectedItems.Count == 0 || PlayArea.DisplayedImagesPanel.Children.Count != 1) return;
            string clickedCharacteImagePath = (string)CharactersListBox.SelectedItem;
            Scene selectedScene = (Scene)ScenesListBox.SelectedItems[0];

            if(selectedScene.Characters.Any(x => x.ImagePath == clickedCharacteImagePath))
            {
                selectedScene.Characters.RemoveAt(selectedScene.Characters.FindIndex(x => x.ImagePath == clickedCharacteImagePath));
                PlayArea.removeCharacter(clickedCharacteImagePath);
            }
            else
            {
                Character character = new Character(clickedCharacteImagePath);
                selectedScene.Characters.Add(character);
                PlayArea.AddCharacter(character);
            }
        }

        private void DisplayStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ScenesListBox.SelectedItems.Count != 1) return;

            Stretch state = Stretch.None;
            switch (DisplayStateComboBox.SelectedIndex)
            {
                case 0: state = Stretch.Uniform; break;
                case 1: state = Stretch.UniformToFill; break;
                case 2: state = Stretch.Fill; break;
            }

            ((Scene)ScenesListBox.SelectedItems[0]).DisplayState = state;
            PlayArea.UpdateDisplayedImages(ScenesListBox.SelectedItems);
        }
    }
}
