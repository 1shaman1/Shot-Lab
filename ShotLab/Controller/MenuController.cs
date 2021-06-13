using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Windows.Forms;

namespace ShotLab
{
    class MenuController
    {
        public void StopGame(PlayGround playGround, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape && playGround.GameIsPaused)
                playGround.GameIsPaused = false;
        }        
    }
}
