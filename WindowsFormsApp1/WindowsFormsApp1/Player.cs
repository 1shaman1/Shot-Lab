using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShotLab
{
    class Player
    {
        public int Health { get; set; }
        public readonly Point Position;
        
        public Player(Point startPos)
        {
            Health = 100;
            Position = startPos;
        }
        public bool IsDead() =>
            Health < 0;

        public void Move()
        {

        }

        public void Shoot()
        {

        }
    }
}
