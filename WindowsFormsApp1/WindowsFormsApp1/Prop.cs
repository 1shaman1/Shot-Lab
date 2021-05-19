using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShotLab
{
    public class Prop
    {
        public int Health { get; set; }
        public Point Position { get; set; }
        public bool IsDead() =>
            Health <= 0;

        public Bitmap Sprite { get; set; }
    }
}
