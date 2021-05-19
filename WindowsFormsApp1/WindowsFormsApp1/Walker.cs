using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
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
                    currentPosition = nextPosition;
                else
                    break;
            }
            return currentPosition;
        }
    }
}
