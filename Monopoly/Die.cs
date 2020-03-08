using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Monopoly
{
    public class Die
    {
        public static Texture2D Tex;

        public Rectangle Position = new Rectangle(0,0,32,32);

        public static Random rnd = new Random();
        Rectangle SourceRect = new Rectangle(0, 0, 32, 32);

        public int Value = 0;
        
        public Die(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
       
        public int Roll()
        {
            Value = rnd.Next(6);
            SourceRect.X = Value * 32;
            Value++;
            return Value;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, Position,SourceRect, Color.White);
        }
    }
}
