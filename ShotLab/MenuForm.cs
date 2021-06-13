using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ShotLab
{
    public partial class MenuForm : Form
    {
        private Button startButton;
        private Button tutorialButton;
        private Button exitButton;
        private ShotLabForm shotLabForm;
        private readonly MenuController controller = new MenuController();

        public MenuForm()
        {
            Size = new Size(1000, 600);
            MaximumSize = Size;
            MinimumSize = Size;
            InitializeComponent();
            CenterToScreen();            
            BackgroundImage = Properties.Resources.BackMenu;

            startButton.Click += (sender, args) =>
                CreateNewForm(Properties.Resources.Level1);

            tutorialButton.Click += (sender, args) =>
                CreateNewForm(Properties.Resources.Tutorial);

            exitButton.Click += (sender, eventArgs) => Close();
            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show("Действительно закрыть?", "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
        }

        private void InitializeComponent()
        {
            startButton = new Button();
            tutorialButton = new Button();
            exitButton = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(Size.Width / 2 - 100, 50);
            startButton.Name = "startButton";
            startButton.Size = new Size(200, 100);
            startButton.TabIndex = 0;
            startButton.Text = "Start Game";
            startButton.UseVisualStyleBackColor = true;
            // 
            // tutorialButton
            // 
            tutorialButton.Location = new Point(Size.Width / 2 - 100, Size.Height / 3 + 50);
            tutorialButton.Name = "tutorialButton";
            tutorialButton.Size = new Size(200, 100);
            tutorialButton.TabIndex = 1;
            tutorialButton.Text = "Tutorial";
            tutorialButton.UseVisualStyleBackColor = true;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(Size.Width / 2 - 100, Size.Height / 3 * 2 + 50);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(200, 100);
            exitButton.TabIndex = 2;
            
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            // 
            // MenuForm
            // 
            Controls.Add(exitButton);
            Controls.Add(tutorialButton);
            Controls.Add(startButton);
            Name = "MenuForm";
            ResumeLayout(false);
        }
        private void CreateNewForm(string level)
        {
            if (shotLabForm != null)
                shotLabForm.Close();
            shotLabForm = new ShotLabForm(level, this);
            shotLabForm.Show();
            Hide();
        }
    }
}
