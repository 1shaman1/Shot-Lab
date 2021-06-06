using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace ShotLab
{
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
            Sprite = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\Player.png");
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
