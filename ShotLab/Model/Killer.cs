using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShotLab
{
    public class Killer : Prop
    {
        public Weapon CurrentWeapon;
        public II Intelect = new II();
        public Killer(int health, Point position)
        {
            Health = health;
            Position = position;
            Sprite = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\enemy1.png");
            CurrentWeapon = new Weapon(2, 100, WeaponType.Rifle);
        }
    }
}
