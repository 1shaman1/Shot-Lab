using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ShotLab
{
    /// <summary>
    /// класс клеткаи чекпоинта
    /// </summary>
    public class CheckPoint
    {
        /// <summary>
        /// бокс отрисовки(он же единчиный жлемент карты)
        /// </summary>
        public Bitmap Sprite;
        /// <summary>
        /// позиция на карте
        /// </summary>
        public Point Position;
        /// <summary>
        /// флаг посещённости
        /// </summary>
        public bool Visited { get; set; }

        public CheckPoint(Point position)
        {
            Position = position;
            Visited = false;
            Sprite = Properties.Resources.BlueCheck;
        }
        public void VisitCheckPoint()
        {
            Visited = true;
            Sprite = Properties.Resources.RedCheck;
        }
    }
}
