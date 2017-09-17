using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class UIElement
    {
        public Tuple<Texture2D, int, int> Background { get; set;  } 
        public List<Tuple<Texture2D, int, int>> ElementImages { get; set; } //RELATIVE to background position
        public List<Tuple<string, int, int>> ElementText { get; set; } //RELATIVE to background position

        public UIElement(Texture2D Texture, int x, int y)
        {
            this.Background = new Tuple<Texture2D, int, int>(Texture, x, y);
            this.ElementImages = new List<Tuple<Texture2D, int, int>>();
            this.ElementText = new List<Tuple<string, int, int>>();
        }
        
        public UIElement(Texture2D Texture, int x, int y, List<Tuple<Texture2D, int, int>> ElementImages)
        {
            this.Background = new Tuple<Texture2D, int, int>(Texture, x, y);
            this.ElementImages = ElementImages;
            this.ElementText = new List<Tuple<string, int, int>>();
        }

        public UIElement(Texture2D Texture, int x, int y, List<Tuple<Texture2D,int,int>> ElementImages, List<Tuple<string,int,int>> ElementText)
        {
            this.Background = new Tuple<Texture2D, int, int>(Texture, x, y);
            this.ElementImages = ElementImages;
            this.ElementText = ElementText;
        }
    }
}
