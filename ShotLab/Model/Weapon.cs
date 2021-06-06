using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace ShotLab
{
    public class Weapon
    {
        private readonly Dictionary<Size, float> angles = new Dictionary<Size, float>()
        {
            { new Size(1, 0), 0},
            { new Size(-1, 0), 180},
            { new Size(0, 1), 90},
            { new Size(0, -1), 270},
            { new Size(0, 0), 0}
        };
        public readonly WeaponType NextType;
        public readonly int Range;

        public float Angle;
                
        public Size ShootWay = new Size(1, 0);
        public readonly int Damage;        

        public Weapon()
        {
            Range = 30;
            Damage = 100;
            NextType = WeaponType.Rifle;
        }
        public Weapon(int range, int damage, WeaponType nextType)
        {
            Range = range;
            Damage = damage;
            NextType = nextType;
        }

        public void Rotate(Size shootWay)
        {
            ShootWay = shootWay;
            Angle = angles[shootWay];
        }

        public void Shoot(Prop carrier, PlayGround playground)
        {
            var shootRay = new Size();
            while (shootRay.Width  <= Range && shootRay.Height <= Range)
            {
                shootRay += ShootWay;
                var target = carrier.Position + shootRay;
                if (!playground.InBounds(target) || !playground.PointIsEmpty(target))
                    break;
                if (playground.Gamer.Position == target)
                {
                    Hit(playground.Gamer, playground);
                    break;                    
                }
                var boxTarget = playground.Boxes.FirstOrDefault(box => box.Position == target);
                if (boxTarget != default)
                {
                    Hit(boxTarget, playground);
                    break;
                }
                var killerTarget = playground.Killers.FirstOrDefault(box => box.Position == target);
                if (killerTarget != default)
                {
                    Hit(killerTarget, playground);
                    break;
                }                
            }            
        }

        private void Hit(Prop prop, PlayGround playground)
        {
            prop.Health -= Damage;
            if (prop.IsDead())
                playground.PropsDeath(prop);
        }
    }
}
