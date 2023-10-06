using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace DnD_Manager.Classes
{
    [Serializable]
    public class Scene : INotifyPropertyChanged
    {
        private string _ImagePath;
        public string ImagePath { get { return _ImagePath; } private set { _ImagePath = value; OnPropertyChanged(); } }




        private string _EntranceSoundPath;
        public string EntranceSoundPath { get { return _EntranceSoundPath; } set { _EntranceSoundPath = value; OnPropertyChanged(); } }




        private string _LoopSoundPath;
        public string LoopSoundPath { get { return _LoopSoundPath; } set { _LoopSoundPath = value; OnPropertyChanged(); } }




        public Stretch DisplayState { get; set; }




        public List<Character> Characters { get; private set; }




        public event PropertyChangedEventHandler? PropertyChanged;

        public Scene(string imagePath)
        {
            ImagePath = imagePath;
            DisplayState = Stretch.UniformToFill;
            Characters = new List<Character>();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
