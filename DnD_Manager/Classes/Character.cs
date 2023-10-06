using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Manager.Classes
{
    [Serializable]
    public class Character
    {
        public string ImagePath { get; private set; }
        public double Left { get; set; }
        public double Top { get; set; }

        public double Scale { get; set; }

        public Character(string imagePath)
        {
            ImagePath = imagePath;
            Left = 0;
            Top = 0;
            Scale = 1;
        }
    }
}
