using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DnD_Manager.Classes
{
    public class Scene : INotifyPropertyChanged
    {
        private string _ImagePath;
        public string ImagePath { get { return _ImagePath; } private set { _ImagePath = value; OnPropertyChanged(); } }


        private string _EntranceSoundPath;
        public string EntranceSoundPath { get { return _EntranceSoundPath; } set { _EntranceSoundPath = value; OnPropertyChanged(); } }


        private string _LoopSoundPath;
        public string LoopSoundPath { get { return _LoopSoundPath; } set { _LoopSoundPath = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Scene(string imagePath)
        {
            ImagePath = imagePath;
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
