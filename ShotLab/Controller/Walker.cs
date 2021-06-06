using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Windows.Forms;

namespace ShotLab
{
    class Walker
    {
        private readonly Dictionary<Keys, Size> moves = new Dictionary<Keys, Size>()
        {
            { Keys.W , new Size (0, -1)},
            { Keys.S , new Size (0, 1)},
            { Keys.A , new Size (-1, 0)},
            { Keys.D , new Size (1, 0)}
        };

        private readonly Dictionary<Keys, Size> shoots = new Dictionary<Keys, Size>()
        {
            { Keys.Up , new Size (0, -1)},
            { Keys.Down , new Size (0, 1)},
            { Keys.Left , new Size (-1, 0)},
            { Keys.Right , new Size (1, 0)}
        };
        public void Move(Player player, PlayGround playGround, KeyEventArgs e)
        {
            var currentPosition = player.Position;
            var key = e.KeyCode;
            if (!moves.ContainsKey(key))
                return;
            player.Position = TryMove(currentPosition, playGround, moves[key]);
        }

        private bool CanMove(PlayGround playGround, Point nextPosition) =>
            playGround.InBounds(nextPosition) && playGround.PointIsEmpty(nextPosition) 
            && !playGround.IsBox(nextPosition) && !playGround.IsKiller(nextPosition);

        private Point TryMove(Point currentPosition, PlayGround playGround, Size smooth)
        {
            while (true)
            {
                var nextPosition = currentPosition + smooth;
                if (CanMove(playGround, nextPosition))
                {
                    if (nextPosition == playGround.Exit)
                        playGround.TryToFinish();
                    var currentCheck = playGround.CheckPoints.FirstOrDefault(checkPoint => checkPoint.Position == nextPosition);
                    if (currentCheck != default)
                        currentCheck.VisitCheckPoint();
                    currentPosition = nextPosition;
                }
                else
                    break;
            }
            return currentPosition;
        }

        public void Rotate(Player player, KeyEventArgs e)
        { 
            var key = e.KeyCode;
            if (!shoots.ContainsKey(key))
                return;
            player.CurrentWeapon.Rotate(shoots[key]);
        }

        public void MakeShot(Player player, PlayGround playGround, KeyEventArgs e)
        {
            var key = e.KeyCode;
            if (key == Keys.Space)
                player.CurrentWeapon.Shoot(player, playGround);
        }

        public void ChangeWeapon(Player player, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                player.ChangeWeapon();
        }
    }
}
