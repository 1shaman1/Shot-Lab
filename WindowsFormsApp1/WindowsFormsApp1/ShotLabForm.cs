using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShotLab
{
    class ShotLabForm : Form
    {		
		private readonly Bitmap mapImage = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\LabBack.png");
		private readonly Bitmap backImage = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\LaboratoryBack.png");

		private readonly Bitmap wallImage = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\LabWall.jpg");

        public new SizeF Size => new SizeF(currentPlayGround.Laboratory.GetLength(0), currentPlayGround.Laboratory.GetLength(1));
		public Size LevelSize => new Size(currentPlayGround.Laboratory.GetLength(0), currentPlayGround.Laboratory.GetLength(1));

		private PlayGround currentPlayGround;


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			DoubleBuffered = true;
			WindowState = FormWindowState.Maximized;

		}

        protected override void OnPaint(PaintEventArgs e)
        {
			CreateMap(e);
			update(e.Graphics);
		}

		private void Mover(object sender, KeyEventArgs e)
        {
			var walker = new Walker();
			walker.Move(currentPlayGround.Gamer, currentPlayGround, e);
        }



		private void update(Graphics graphics)
        {
			DrawPlayer(graphics);
			DrawBoxes(graphics);
        }
        public ShotLabForm()
		{
			DoubleBuffered = true;
			var level = LoadLevels();

			currentPlayGround = level;
			

			var timer = new Timer { Interval = 30 };
			this.KeyDown += new KeyEventHandler(Mover);
			timer.Tick += (sender, args) =>
			{
				Invalidate();	
			};
					
            timer.Start();			
		}

		private static PlayGround LoadLevels()
		{
			return  PlayGround.FromText("#########\n#P   B# #\n#    E#  #\n####    #\n##  #   #\n#    ####\n#########");			
		}

		private void DrawLevel(Graphics graphics)
		{
			graphics.DrawImage(backImage, new Rectangle(0, 0, LevelSize.Width, LevelSize.Height));
			foreach (var wall in currentPlayGround.Walls)
				graphics.DrawImage(wallImage, new Rectangle(wall.X, wall.Y, 1, 1));
		}

		public void CreateMap(PaintEventArgs e)
		{
			for (var x = 0; x < currentPlayGround.Laboratory.GetLength(0); x++)
			{
				for (var y = 0; y < currentPlayGround.Laboratory.GetLength(1); y++)
				{
					var graphics = e.Graphics;
					if (currentPlayGround.Laboratory[x, y] == MapCell.Wall)
						graphics.DrawImage(wallImage, new Rectangle(x * 25, y * 25, 25, 25));
					else if (currentPlayGround.Laboratory[x, y] == MapCell.Empty)
						graphics.DrawImage(mapImage, new Rectangle(x * 25, y * 25, 25, 25));
				}
			}
		}

		private void DrawPlayer(Graphics graphics)
        {
			graphics.DrawImage(currentPlayGround.Gamer.Sprite, 
				new Rectangle(currentPlayGround.Gamer.Position.X * 25, currentPlayGround.Gamer.Position.Y * 25, 25, 25));
        }

		private void DrawBoxes(Graphics graphics)
        {
			foreach(var box in currentPlayGround.Boxes)
			graphics.DrawImage(box.Sprite, new Rectangle(box.Position.X * 25, box.Position.Y * 25, 25, 25));
		}
			

        private void ShotLabForm_Load(object sender, EventArgs e)
        {

        }


    }
}
