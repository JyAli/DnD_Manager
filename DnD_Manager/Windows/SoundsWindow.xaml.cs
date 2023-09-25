using DnD_Manager.Classes;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DnD_Manager.Windows
{
    /// <summary>
    /// Interaction logic for SoundsWindow.xaml
    /// </summary>
    public partial class SoundsWindow : Window
    {
        private Scene Scene;
        private MediaPlayer Player;
        public SoundsWindow(Scene scene)
        {
            Scene = scene;
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            Player = new MediaPlayer();
            SoundsListBox.ItemsSource = Global.Sounds;
            string entranceText = Scene.EntranceSoundPath is not null ? Path.GetFileNameWithoutExtension(Scene.EntranceSoundPath) : string.Empty;
            string loopText = Scene.LoopSoundPath is not null ? Path.GetFileNameWithoutExtension(Scene.LoopSoundPath) : string.Empty;
            EntranceTextBlock.Text = $"Entrance Sound: {entranceText}";
            LoopTextBlock.Text = $"Loop Sound: {loopText}";
            UpdateSoundsListBox();
        }

        private void UpdateSoundsListBox()
        {
            string[] audioFilesPaths = FilesHandler.GetAudioFilesInFolder(DndDirectories.SoundsFolder);
            Global.Sounds.Clear();
            foreach (string audioFilePath in audioFilesPaths) Global.Sounds.Add(audioFilePath);

            SoundsListBox.Items.Refresh();
        }

        private void SoundEffectsFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", DndDirectories.SoundsFolder);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSoundsListBox();
        }

        private void PlaySoundButton_Click(object sender, RoutedEventArgs e)
        {
            Button playButton = (Button)sender;
            string clickedSound = (string)playButton.CommandParameter;
            Player.Open(new Uri(clickedSound));
            Player.Play();
        }

        private void EntranceButton_Click(object sender, RoutedEventArgs e)
        {
            if (SoundsListBox.SelectedItem is null) return;
            string soundPath = (string)SoundsListBox.SelectedItem;
            Scene.EntranceSoundPath = soundPath;
            EntranceTextBlock.Text = $"Entrance Sound: {Path.GetFileNameWithoutExtension(soundPath)}";
        }

        private void LoopButton_Click(object sender, RoutedEventArgs e)
        {
            if (SoundsListBox.SelectedItem is null) return;
            string soundPath = (string)SoundsListBox.SelectedItem;
            Scene.LoopSoundPath = soundPath;
            LoopTextBlock.Text = $"Loop Sound: {Path.GetFileNameWithoutExtension(soundPath)}";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Scene.EntranceSoundPath = null;
            Scene.LoopSoundPath = null;
            EntranceTextBlock.Text = "Entrance Sound: ";
            LoopTextBlock.Text = "Loop Sound: ";
        }
    }
}
