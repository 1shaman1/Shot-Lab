﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ShotLab
{
    public class Box : Prop
    {
        public Box(int health, Point position)
        {
            Health = health;
            Position = position;
            Sprite = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\Box.png");
        }
    }
}
