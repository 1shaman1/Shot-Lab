using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ShotLab
{
    public class CheckPoint
    {
        public Bitmap Sprite;
        public Point Position;
        public bool Visited { get; set; }

        public CheckPoint(Point position)
        {
            Position = position;
            Visited = false;
            Sprite = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\BlueCheck.jpg");
        }
        public void VisitCheckPoint()
        {
            Visited = true;
            Sprite = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\RedCheck.jpeg");
        }
    }
}
