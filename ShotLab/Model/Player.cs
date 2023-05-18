using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace ShotLab
{
    /// <summary>
    /// Игрок, да-да - он тоже по сути объект карты и потому он наследуется от клсса Prop
    /// </summary>
    public class Player : Prop
    {
        private readonly Dictionary<WeaponType, Weapon> weapons = new Dictionary<WeaponType, Weapon>()
        {
            { WeaponType.Rifle, new Weapon(500, 30, WeaponType.Knife)},
            { WeaponType.Knife, new Weapon(2, 100, WeaponType.Rifle)}
        };

        public Weapon CurrentWeapon;
        public Player(int health, Point startPosition)
        {
            Sprite = Properties.Resources.Player;
            Health = health;
            Position = startPosition;
            CurrentWeapon = new Weapon(500, 30, WeaponType.Knife);
        }


        public void ChangeWeapon()
        {
            var shootWay = CurrentWeapon.ShootWay;
            CurrentWeapon = weapons[CurrentWeapon.NextType];
            CurrentWeapon.Rotate(shootWay);            
        }
    }
}
