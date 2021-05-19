using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Windows.Forms;

namespace ShotLab
{
    public class Weapon
    {
        public readonly WeaponType NextType;
        public readonly double Range;
        
        public Vector2 Angle;
        public readonly int Damage;

        

        public Weapon()
        {
            Range = 30;
            Damage = 100;
            NextType = WeaponType.Rifle;
        }
        public Weapon(double range, int damage, WeaponType nextType)
        {
            Range = range;
            Damage = damage;
            NextType = nextType;
        }
        public void Rotate(Prop carrier, MouseEventArgs mouse)
        {
            var mousePos = mouse.Location;
            Angle = Vector2.Normalize(new Vector2(mousePos.X, mousePos.Y) - new Vector2(carrier.Position.X, carrier.Position.Y));
        }

        public void Shoot(Prop carrier, PlayGround playground)
        {
            var shootVector = new Vector2((float)(Angle.X * Range), (float)(Angle.Y * Range));

            
        }
    }
}
