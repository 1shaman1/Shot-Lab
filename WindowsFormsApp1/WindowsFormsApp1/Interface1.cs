using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ShotLab
{
    interface IWeapon
    {
        public void ChangeWeapon();

        public Vector2 Shoot();

        public void Rotate();
    }
}
