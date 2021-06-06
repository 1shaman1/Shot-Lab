using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ShotLab
{
    class ShotLabForm : Form
    {
		private Image backGround;
		private readonly Image winGameImage = Properties.Resources.Win;
		private readonly Image gameOverImage = Properties.Resources.Died;
		private readonly Image mapImage = Properties.Resources.LabBack;
		private readonly Image wallImage = Properties.Resources.LabWall;
		private readonly Image exitImage = Properties.Resources.Exit;

		public new SizeF Size => new SizeF(currentPlayGround.Laboratory.GetLength(0), currentPlayGround.Laboratory.GetLength(1));
		public Size LevelSize => new Size(currentPlayGround.Laboratory.GetLength(0), currentPlayGround.Laboratory.GetLength(1));

		private readonly PlayGround currentPlayGround;


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
			var walker = new Walker();
			walker.Move(currentPlayGround.Gamer, currentPlayGround, e);
			walker.Rotate(currentPlayGround.Gamer, e);
			walker.MakeShot(currentPlayGround.Gamer, currentPlayGround, e);
			walker.ChangeWeapon(currentPlayGround.Gamer, e);
        }



		private void update(PaintEventArgs e)
        {
			DrawChecks(e);
			DrawPlayer(e);
			DrawBoxes(e);
			DrawKillers(e);
			if(currentPlayGround.GameIsFinished)
				DrawFinishScreen(e, winGameImage);
			if(currentPlayGround.GameIsOver())
				DrawFinishScreen(e, gameOverImage);

		}
        public ShotLabForm()
		{
			DoubleBuffered = false;
			var level = LoadLevels();

			currentPlayGround = level;
			CreateMap();
			Invalidate();
			var timer = new Timer { Interval = 150};
			this.KeyDown += new KeyEventHandler(Mover);
			timer.Tick += (sender, args) =>
			{
				MoverKillers();
				Invalidate();
				if(currentPlayGround.GameIsFinished)
					timer.Stop();
				if (currentPlayGround.GameIsOver())
					timer.Stop();
			};
					
            timer.Start();
		}

		private static PlayGround LoadLevels()
		{
			return  PlayGround.FromText(Properties.Resources.Level1);			
		}

		public void CreateMap()
		{
			backGround = new Bitmap(LevelSize.Width * 25, LevelSize.Height * 25);
			var graphics = Graphics.FromImage(backGround);
			for (var x = 0; x < currentPlayGround.Laboratory.GetLength(0); x++)
			{
				for (var y = 0; y < currentPlayGround.Laboratory.GetLength(1); y++)
				{
					if (currentPlayGround.Laboratory[x, y] == MapCell.Wall)
						graphics.DrawImage(wallImage, new Rectangle(x * 25, y * 25, 25, 25));
					else if (currentPlayGround.Laboratory[x, y] == MapCell.Empty)
						graphics.DrawImage(mapImage, new Rectangle(x * 25, y * 25, 25, 25));
				}
			}
			graphics.DrawImage(exitImage, new Rectangle(currentPlayGround.Exit.X * 25, currentPlayGround.Exit.Y * 25, 25, 25));			
		}

		private void DrawPlayer(PaintEventArgs e)
        {
			var graphics = e.Graphics;
			var player = currentPlayGround.Gamer;
			var angle = player.CurrentWeapon.Angle;
			graphics.TranslateTransform(player.Position.X * 25 + 25 / 2, player.Position.Y * 25 + 25 / 2);
			graphics.RotateTransform(angle);
			graphics.TranslateTransform(-player.Position.X * 25 - 25 / 2, -player.Position.Y * 25 - 25 / 2);
			graphics.DrawImage(player.Sprite, 
				new Rectangle(player.Position.X *25 , player.Position.Y *25 , 25, 25));
			graphics.ResetTransform();
		}

		private void DrawKillers(PaintEventArgs e)
        {
			var graphics = e.Graphics;
			foreach (var killer in currentPlayGround.Killers)
			{
				var angle = killer.CurrentWeapon.Angle;
				graphics.TranslateTransform(killer.Position.X * 25 + 25 / 2, killer.Position.Y * 25 + 25 / 2);
				graphics.RotateTransform(angle);
				graphics.TranslateTransform(-killer.Position.X * 25 - 25 / 2, -killer.Position.Y * 25 - 25 / 2);
				graphics.DrawImage(killer.Sprite, new Rectangle(killer.Position.X * 25, killer.Position.Y * 25, 25, 25));
				graphics.ResetTransform();
			}
		}
		private void DrawBoxes(PaintEventArgs e)
        {

			var graphics = e.Graphics;
			foreach (var box in currentPlayGround.Boxes)
				graphics.DrawImage(box.Sprite, new Rectangle(box.Position.X * 25, box.Position.Y * 25, 25, 25));			
		}

		private void DrawChecks(PaintEventArgs e)
		{
			var graphics = e.Graphics;
			foreach (var checkPoint in currentPlayGround.CheckPoints)
				graphics.DrawImage(checkPoint.Sprite, new Rectangle(checkPoint.Position.X * 25, checkPoint.Position.Y * 25, 25, 25));
		}

		private void MoverKillers()
        {
			foreach(var killer in currentPlayGround.Killers)
				killer.Intelect.MoveKiller(killer, currentPlayGround);
        }

		private void DrawFinishScreen(PaintEventArgs e, Image image)
        {
			var graphics = e.Graphics;
			graphics.DrawImage(image, 0 ,0, image.Width, image.Height);
		}
    }
}
