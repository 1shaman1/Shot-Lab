using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Windows.Forms;

namespace ShotLab
{
    /// <summary>
    /// контроллер меню игры
    /// </summary>
    class MenuController
    {
        /// <summary>
        /// функция снятия с паузы игры
        /// </summary>
        /// <param name="playGround">игровая площадка</param>
        /// <param name="e">событие нажатия на кнопку клавиатуры</param>
        public void StopGame(PlayGround playGround, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape && playGround.GameIsPaused)
                playGround.GameIsPaused = false;
        }        
    }
}
