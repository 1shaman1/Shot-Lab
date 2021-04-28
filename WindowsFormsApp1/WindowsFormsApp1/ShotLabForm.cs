using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShotLab
{
    class ShotLabForm : Form
    {

		private Bitmap mapImage = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\LabBack.png");
		private Bitmap backImage = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\LaboratoryBack.png");

		private Bitmap wallImage = new Bitmap(@"C:\progs_for_visualStudio\ShotLab\WindowsFormsApp1\ShotLabImages\LabWall.jpg");
		public SizeF Size => new SizeF(curentPlayGround.Laboratory.GetLength(0), curentPlayGround.Laboratory.GetLength(1));
		public Size LevelSize => new Size(curentPlayGround.Laboratory.GetLength(0), curentPlayGround.Laboratory.GetLength(1));

		private PlayGround curentPlayGround;

		//private readonly PainterLaboratory painter;
		private readonly Timer timer;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			DoubleBuffered = true;
			WindowState = FormWindowState.Maximized;
		}

        protected override void OnPaint(PaintEventArgs e)
        {
			CreateMap(e);
			DrawLevel(e.Graphics);
        }

        public ShotLabForm()
		{			
			var level = LoadLevels();

			curentPlayGround = level;
			
			//var menuPanel = new FlowLayoutPanel
			//{
			//	FlowDirection = FlowDirection.LeftToRight,
			//	Dock = DockStyle.Left,
			//	Width = 200,
			//	BackColor = Color.Black,
			//	Padding = new Padding(20),
			//	Font = new Font(SystemFonts.DefaultFont.FontFamily, 16)
			//};
			

			//var link = new LinkLabel
			//{
			//	Text = "Level",
			//	ActiveLinkColor = Color.LimeGreen,
			//	TextAlign = ContentAlignment.MiddleCenter,
			//	AutoSize = true,
			//	Margin = new Padding(32, 8, 8, 8),
			//	Tag = level
			//};
			////link.LinkClicked += (sender, args) =>
			////{
			////	ChangeLevel(level);
			////	UpdateLinksColors(level, linkLabels);
			////};
			//menuPanel.Controls.Add(link);
			//Controls.Add(menuPanel);

            //timer = new Timer { Interval = 50 };
            ////timer.Tick += TimerTick;
            //timer.Start();
        }

		private static PlayGround LoadLevels()
		{
			return  PlayGround.FromText("#########\n#     # #\n#     #  #\n####    #\n##  #   #\n#    ####\n#########");			
		}

		public void DrawLevel(Graphics graphics)
		{
			graphics.DrawImage(backImage, new Rectangle(0, 0, LevelSize.Width, LevelSize.Height));
			foreach (var wall in curentPlayGround.Walls)
				graphics.DrawImage(wallImage, new Rectangle(wall.X, wall.Y, 1, 1));
		}

		public void CreateMap(PaintEventArgs e)
		{
			var cellWidth = wallImage.Width;
			var cellHeight = wallImage.Height;
			mapImage = new Bitmap(LevelSize.Width * cellWidth, LevelSize.Height * cellHeight);
			for (var x = 0; x < curentPlayGround.Laboratory.GetLength(0); x++)
			{
				for (var y = 0; y < curentPlayGround.Laboratory.GetLength(1); y++)
				{
					var graphics = e.Graphics;
					if (curentPlayGround.Laboratory[x, y] == MapCell.Wall)
						graphics.DrawImage(wallImage, new Rectangle(x * 25, y * 25, 25, 25));
					else if (curentPlayGround.Laboratory[x, y] == MapCell.Empty)
						graphics.DrawImage(mapImage, new Rectangle(x * 25, y * 25, 25, 25));
				}
			}
		}


		//private void DrawLevelSwitch(Map[] levels, FlowLayoutPanel menuPanel)
		//{
		//	menuPanel.Controls.Add(new Label
		//	{
		//		Text = "Choose level:",
		//		ForeColor = Color.White,
		//		AutoSize = true,
		//		Margin = new Padding(8)

		//	});
		//	var linkLabels = new List<LinkLabel>();
		//	for (var i = 0; i < levels.Length; i++)
		//	{
		//		var level = levels[i];
		//		var link = new LinkLabel
		//		{
		//			Text = $"Level {i + 1}",
		//			ActiveLinkColor = Color.LimeGreen,
		//			TextAlign = ContentAlignment.MiddleCenter,
		//			AutoSize = true,
		//			Margin = new Padding(32, 8, 8, 8),
		//			Tag = level
		//		};
		//		link.LinkClicked += (sender, args) =>
		//		{
		//			ChangeLevel(level);
		//			UpdateLinksColors(level, linkLabels);
		//		};
		//		menuPanel.Controls.Add(link);
		//		linkLabels.Add(link);
		//	}
		//	UpdateLinksColors(levels[0], linkLabels);
		//}

		//private void UpdateLinksColors(Map level, List<LinkLabel> linkLabels)
		//{
		//	foreach (var linkLabel in linkLabels)
		//	{
		//		linkLabel.LinkColor = linkLabel.Tag == level ? Color.LimeGreen : Color.White;
		//	}

		//}

		//private void ChangeLevel(Map newMap)
		//{
		//	scenePainter.ChangeLevel(newMap);
		//	timer.Start();
		//	scaledViewPanel.Invalidate();
		//}

		//private void TimerTick(object sender, EventArgs e)
		//{
		//	painter.Update();
		//}
		private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ShotLabForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ShotLabForm";
            this.Load += new System.EventHandler(this.ShotLabForm_Load);
            this.ResumeLayout(false);

        }

        private void ShotLabForm_Load(object sender, EventArgs e)
        {

        }
    }
}
