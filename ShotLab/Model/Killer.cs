using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShotLab
{
    /// <summary>
    /// класс противника наследуется от класса объекта карты
    /// </summary>
    public class Killer : Prop
    {
        public Weapon CurrentWeapon;
        public II Intelect = new II();
        public Killer(int health, Point position)
        {
            Health = health;
            Position = position;
            Sprite = Properties.Resources.enemy1;
            CurrentWeapon = new Weapon(2, 100, WeaponType.Rifle);
        }
    }
}
