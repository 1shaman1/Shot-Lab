using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Windows.Forms;

namespace ShotLab
{ 
    /// <summary>
    /// класс контроллера управления пользователя
    /// </summary>
    class Walker
    {
        /// <summary>
        /// попытка прикрутить звук - не работает
        /// </summary>
        System.Media.SoundPlayer snd = new System.Media.SoundPlayer(Properties.Resources.shoot);

        /// <summary>
        /// привязка клавиш управления к соответствующим боксам движения(условный вектор сдвига в размеченном пространстве)
        /// </summary>
        private readonly Dictionary<Keys, Size> moves = new Dictionary<Keys, Size>()
        {
            { Keys.W , new Size (0, -1)},
            { Keys.S , new Size (0, 1)},
            { Keys.A , new Size (-1, 0)},
            { Keys.D , new Size (1, 0)}
        };

        /// <summary>
        /// привязка клавиш клавиатуры к направлению поворотам
        /// </summary>
        private readonly Dictionary<Keys, Size> shoots = new Dictionary<Keys, Size>()
        {
            { Keys.Up , new Size (0, -1)},
            { Keys.Down , new Size (0, 1)},
            { Keys.Left , new Size (-1, 0)},
            { Keys.Right , new Size (1, 0)}
        };

        /// <summary>
        /// функция движения игрока по полю
        /// </summary>
        /// <param name="player">игрок</param>
        /// <param name="playGround">игровое поле</param>
        /// <param name="e">событие нажития клавиши на клавиатуре</param>
        public void Move(Player player, PlayGround playGround, KeyEventArgs e)
        {
            var currentPosition = player.Position;
            var key = e.KeyCode;
            if (!moves.ContainsKey(key))
                return;
            player.Position = TryMove(currentPosition, playGround, moves[key]);
        }

        /// <summary>
        /// проверка возможности движения
        /// </summary>
        /// <param name="playGround">игровая площадка</param>
        /// <param name="nextPosition">позиция, куда происходит попытка движения</param>
        /// <returns>флаг возможен сдвиг/невозможен</returns>
        private bool CanMove(PlayGround playGround, Point nextPosition) =>
            playGround.InBounds(nextPosition) && playGround.PointIsEmpty(nextPosition) 
            && !playGround.IsBox(nextPosition) && !playGround.IsKiller(nextPosition);

        /// <summary>
        /// попытка совершить движение
        /// </summary>
        /// <param name="currentPosition">текущая позиция элемента</param>
        /// <param name="playGround">игровое поле</param>
        /// <param name="smooth">итоговый сдвиг</param>
        /// <returns>конечная позиция</returns>
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

        /// <summary>
        /// Функция вращения игрока по нажатию клавиши на клавиатуре
        /// </summary>
        /// <param name="player">экземпляр класса игрока</param>
        /// <param name="e">события нажатия на кнопку</param>
        public void Rotate(Player player, KeyEventArgs e)
        { 
            var key = e.KeyCode;
            if (!shoots.ContainsKey(key))
                return;
            player.CurrentWeapon.Rotate(shoots[key]);
        }
        
        /// <summary>
        /// производство выстрела
        /// </summary>
        /// <param name="player">игрок</param>
        /// <param name="playGround">игровая площадка</param>
        /// <param name="e">событие нажатия на клавишу клавиатуры</param>
        public void MakeShot(Player player, PlayGround playGround, KeyEventArgs e)
        {
            var key = e.KeyCode;

            if (key == Keys.Space)
            {                
                snd.Play();
                player.CurrentWeapon.Shoot(player, playGround);
            }
        }
        
        /// <summary>
        /// смена оружия
        /// </summary>
        /// <param name="player">игрок</param>
        /// <param name="e">событие нажатия на клавишу клавиатуры</param>
        public void ChangeWeapon(Player player, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                player.ChangeWeapon();
        }
        
        /// <summary>
        /// функция постановки игры на паузу
        /// </summary>
        /// <param name="playGround">игровая площадка</param>
        /// <param name="e">событие нажатия на кнопку клавы</param>
        public void Pause(PlayGround playGround, KeyEventArgs e)
        {
            var key = e.KeyCode;
            if (key == Keys.Escape && !playGround.GameIsPaused)
                playGround.GameIsPaused = true;
            else if (key == Keys.Escape && playGround.GameIsPaused)
                playGround.GameIsPaused = false;
        }
    }
}
