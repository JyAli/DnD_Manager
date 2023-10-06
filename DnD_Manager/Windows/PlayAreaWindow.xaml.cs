using DnD_Manager.Classes;
using System;
using System.Collections;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace DnD_Manager.Windows
{
    /// <summary>
    /// Interaction logic for PlayAreaWindow.xaml
    /// </summary>
    public partial class PlayAreaWindow : Window
    {
        private MediaPlayer EntrancePlayer;
        private MediaPlayer LoopPlayer;
        private bool IsFullscreen = false;

        private Image selectedImage;

        public PlayAreaWindow()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            Closed += delegate { try { ((MainWindow)Application.Current.MainWindow).ClearMainPagesFrame(); } catch { } };
            Closing += delegate { EntrancePlayer?.Stop(); LoopPlayer?.Stop(); EntrancePlayer?.Close(); LoopPlayer?.Close(); };
            EntrancePlayer = new MediaPlayer();
            LoopPlayer = new MediaPlayer();
        }

        public void UpdateDisplayedImages(IList selectedItems)
        {
            EntrancePlayer.Stop();
            LoopPlayer.Stop();
            DisplayedImagesPanel.Children.Clear();
            CharactersCanvas.Children.Clear();
            foreach (Scene scene in selectedItems)
            { 
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                image.UriSource = new Uri(scene.ImagePath); ;
                image.EndInit();

                Image newImage = new Image() {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = scene.DisplayState,
                    Source = image
                };
                RenderOptions.SetBitmapScalingMode(newImage, BitmapScalingMode.HighQuality);
                DisplayedImagesPanel.Children.Add(newImage);
            }

            if (selectedItems.Count > 0) PlaySounds((Scene)selectedItems[0]);

            if(selectedItems.Count == 1)
            {
                foreach (Character caracter in ((Scene)selectedItems[0]).Characters)
                {
                     AddCharacter(caracter);
                }
            }
        }

        public void PlaySounds(Scene scene)
        {
            if (scene.EntranceSoundPath is not null)
            {
                EntrancePlayer.Open(new Uri(scene.EntranceSoundPath));
                EntrancePlayer.Play();
            }

            if (scene.LoopSoundPath is not null)
            {
                LoopPlayer.MediaEnded += delegate { LoopPlayer.Position = TimeSpan.Zero; LoopPlayer.Play(); };
                LoopPlayer.Open(new Uri(scene.LoopSoundPath));
                LoopPlayer.Play();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11)
            {
                if (IsFullscreen)
                {
                    IsFullscreen = false;
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    WindowState = WindowState.Maximized;
                    ResizeMode = ResizeMode.CanResize;
                }
                else
                {
                    IsFullscreen = true;
                    ResizeMode = ResizeMode.NoResize;
                    WindowStyle = WindowStyle.None;

                    //The window state is set twice due to a weird bug. It MUST be set to Normal before being set to Maximized
                    WindowState = WindowState.Normal;
                    WindowState = WindowState.Maximized;
                }
            }
        }

        public void AddCharacter(Character character)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            image.UriSource = new Uri(character.ImagePath); ;
            image.EndInit();

            Image characterImage = new Image();
            characterImage.Width = 150;
            characterImage.Margin = new Thickness(character.Left, character.Top, 0, 0);
            characterImage.Source = image;
            characterImage.Tag = character;
            RenderOptions.SetBitmapScalingMode(characterImage, BitmapScalingMode.HighQuality);

            CharactersCanvas.Children.Add(characterImage);
        }

        public void removeCharacter(string imagePath)
        {
            Image selectedImage = null;
            foreach (Image character in CharactersCanvas.Children)
            {
                if(imagePath.Equals(((Character)character.Tag).ImagePath))
                {
                    selectedImage = character;
                    continue;
                }
            }

            if (selectedImage is not null) CharactersCanvas.Children.Remove(selectedImage);
        }


        private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = e.Source as Image;

            if (image != null && CharactersCanvas.CaptureMouse())
            {
                selectedImage = image;
                Panel.SetZIndex(selectedImage, 1); // in case of multiple images
            }
        }

        private void CanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedImage != null)
            {
                CharactersCanvas.ReleaseMouseCapture();
                Panel.SetZIndex(selectedImage, 0);
                selectedImage = null;
            }
        }

        private void CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (selectedImage != null)
            {
                var position = e.GetPosition(CharactersCanvas);
                double left = position.X - selectedImage.ActualWidth / 2;
                double top = position.Y - selectedImage.ActualHeight / 2;
                selectedImage.Margin = new Thickness(left, top, 0, 0);
                Character character = (Character)selectedImage.Tag;
                character.Left = left;
                character.Top = top;
            }
        }

        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }
    }
}
