using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ShotLab
{
	/// <summary>
	/// форма она же View она представление игры, честно тут по большому счёту творится цыганская магия, но если будут вопросы - пиши попробую объяснит
	/// все фокусы
	/// </summary>
    class ShotLabForm : Form
    {
		private Image backGround;
		private readonly Image winGameImage = Properties.Resources.Win;
		private readonly Image gameOverImage = Properties.Resources.Died;
		private readonly Image mapImage = Properties.Resources.LabBack;
		private readonly Image wallImage = Properties.Resources.LabWall;
		private readonly Image exitImage = Properties.Resources.Exit;
		private readonly MenuForm menuForm;
		public readonly Walker Walker = new Walker();
		private readonly Size screenResolution = Screen.PrimaryScreen.Bounds.Size;
		private int cellLength;


		public new SizeF Size => new SizeF(CurrentPlayGround.Laboratory.GetLength(0), CurrentPlayGround.Laboratory.GetLength(1));
		public Size LevelSize => new Size(CurrentPlayGround.Laboratory.GetLength(0), CurrentPlayGround.Laboratory.GetLength(1));

		public readonly PlayGround CurrentPlayGround;


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			DoubleBuffered = true;
			WindowState = FormWindowState.Maximized;

		}

        protected override void OnPaint(PaintEventArgs e)
        {
			e.Graphics.DrawImage(backGround, 0, 0);
			update(e);
		}

		private void Mover(object sender, KeyEventArgs e)
        {
			Walker.Move(CurrentPlayGround.Gamer, CurrentPlayGround, e);
			Walker.Rotate(CurrentPlayGround.Gamer, e);

			Walker.MakeShot(CurrentPlayGround.Gamer, CurrentPlayGround, e);
			Walker.ChangeWeapon(CurrentPlayGround.Gamer, e);
			Walker.Pause(CurrentPlayGround, e);
        }

		private void update(PaintEventArgs e)
        {
			DrawChecks(e);
			DrawPlayer(e);
			DrawBoxes(e);
			DrawKillers(e);
			if(CurrentPlayGround.GameIsFinished)
				DrawFinishScreen(e, winGameImage);
			if(CurrentPlayGround.GameIsOver())
				DrawFinishScreen(e, gameOverImage);

		}
        public ShotLabForm(string level, MenuForm menu)
		{
			menuForm = menu;
			DoubleBuffered = false;
			CurrentPlayGround = LoadLevels(level);
			CreateMap();
			Invalidate();
			var timer = new Timer { Interval = 110};
			KeyDown += (sender, args) =>
			{
				Mover(sender, args);
				if (!CurrentPlayGround.GameIsPaused)
					timer.Start();
			};
			
			timer.Tick += (sender, args) =>
			{
				MoverKillers();
				Invalidate();
				if (CurrentPlayGround.GameIsPaused || CurrentPlayGround.GameIsFinished || CurrentPlayGround.GameIsOver())
				{
					timer.Stop();
					menuForm.Show();
				}
			};					
            timer.Start();
		}

		private static PlayGround LoadLevels(string level)
		{
			return  PlayGround.FromText(level);			
		}

		/// <summary>
		/// отрисовка карты
		/// </summary>
		public void CreateMap()
		{
			cellLength = Math.Min(screenResolution.Width / LevelSize.Width, screenResolution.Height / LevelSize.Height);
			backGround = new Bitmap(LevelSize.Width * cellLength, LevelSize.Height * cellLength);
			var graphics = Graphics.FromImage(backGround);
			for (var x = 0; x < CurrentPlayGround.Laboratory.GetLength(0); x++)
			{
				for (var y = 0; y < CurrentPlayGround.Laboratory.GetLength(1); y++)
				{
					if (CurrentPlayGround.Laboratory[x, y] == MapCell.Wall)
						graphics.DrawImage(wallImage, new Rectangle(x * cellLength, y * cellLength, cellLength, cellLength));
					else if (CurrentPlayGround.Laboratory[x, y] == MapCell.Empty)
						graphics.DrawImage(mapImage, new Rectangle(x * cellLength, y * cellLength, cellLength, cellLength));
				}
			}
			graphics.DrawImage(exitImage, new Rectangle(CurrentPlayGround.Exit.X * cellLength, CurrentPlayGround.Exit.Y * cellLength, cellLength, cellLength));			
		}

		private void DrawPlayer(PaintEventArgs e)
        {
			var graphics = e.Graphics;
			var player = CurrentPlayGround.Gamer;
			var angle = player.CurrentWeapon.Angle;
			graphics.TranslateTransform(player.Position.X * cellLength + cellLength / 2, player.Position.Y * cellLength + cellLength / 2);
			graphics.RotateTransform(angle);
			graphics.TranslateTransform(-player.Position.X * cellLength - cellLength / 2, -player.Position.Y * cellLength - cellLength / 2);
			graphics.DrawImage(player.Sprite, 
				new Rectangle(player.Position.X *cellLength , player.Position.Y *cellLength , cellLength, cellLength));
			graphics.ResetTransform();
		}

		private void DrawKillers(PaintEventArgs e)
        {
			var graphics = e.Graphics;
			foreach (var killer in CurrentPlayGround.Killers)
			{
				var angle = killer.CurrentWeapon.Angle;
				graphics.TranslateTransform(killer.Position.X * cellLength + cellLength / 2, killer.Position.Y * cellLength + cellLength / 2);
				graphics.RotateTransform(angle);
				graphics.TranslateTransform(-killer.Position.X * cellLength - cellLength / 2, -killer.Position.Y * cellLength - cellLength / 2);
				graphics.DrawImage(killer.Sprite, new Rectangle(killer.Position.X * cellLength, killer.Position.Y * cellLength, cellLength, cellLength));
				graphics.ResetTransform();
			}
		}

		private void DrawBoxes(PaintEventArgs e)
        {
			var graphics = e.Graphics;
			foreach (var box in CurrentPlayGround.Boxes)
				graphics.DrawImage(box.Sprite, new Rectangle(box.Position.X * cellLength, box.Position.Y * cellLength, cellLength, cellLength));			
		}

		private void DrawChecks(PaintEventArgs e)
		{
			var graphics = e.Graphics;
			foreach (var checkPoint in CurrentPlayGround.CheckPoints)
				graphics.DrawImage(checkPoint.Sprite, new Rectangle(checkPoint.Position.X * cellLength, checkPoint.Position.Y * cellLength, cellLength, cellLength));
		}

		private void MoverKillers()
        {
			foreach(var killer in CurrentPlayGround.Killers)
				killer.Intelect.MoveKiller(killer, CurrentPlayGround);
        }

		private void DrawFinishScreen(PaintEventArgs e, Image image)
        {
			var graphics = e.Graphics;
			graphics.DrawImage(image, 0 ,0, Size.Width * cellLength, Size.Height * cellLength);
		}

		public PlayGround ShowCurrentPlayGround() => CurrentPlayGround;
    }
}
