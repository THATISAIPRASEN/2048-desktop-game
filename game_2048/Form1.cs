using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_2048
{
    public partial class Form1 : Form
    {
        private Game Game;

        private Graphics gGraphics, gG;
        private Bitmap bBackground;
        public Form1()
        {
            InitializeComponent();
            bBackground = new Bitmap(600, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            gGraphics = this.CreateGraphics();
            gG = Graphics.FromImage(bBackground);
            Game = new Game();

        }
        public void UpdateGame()
        {
            Game.Update();
        }
        private void goToMainMenu()
        {
            if (MessageBox.Show("This will close the game and takes you to main menu Confirm?", "Close Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
        }
        public void Draw(Graphics g)
        {
            g.Clear(Color.FromArgb(251, 248, 239));

            Game.Draw(g);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Game.isTop && !Game.isRight && !Game.isBottom && (e.KeyCode == Keys.A || e.KeyCode == Keys.Left))
            {
                Game.isLeft = true;
                Game.moveBoard(Game.Direction.Left);
            }
            else if (!Game.isLeft && !Game.isRight && !Game.isBottom && (e.KeyCode == Keys.W || e.KeyCode == Keys.Up))
            {
                Game.isTop = true;
                Game.moveBoard(Game.Direction.Top);
            }
            else if (!Game.isTop && !Game.isLeft && !Game.isBottom && (e.KeyCode == Keys.D || e.KeyCode == Keys.Right))
            {
                Game.isRight = true;
                Game.moveBoard(Game.Direction.Right);
            }
            else if (!Game.isTop && !Game.isRight && !Game.isLeft && (e.KeyCode == Keys.S || e.KeyCode == Keys.Down))
            {
                Game.isBottom = true;
                Game.moveBoard(Game.Direction.Bottom);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (Game.isLeft && (e.KeyCode == Keys.A || e.KeyCode == Keys.Left))
            {
                Game.isLeft = false;
            }

            if (Game.isTop && (e.KeyCode == Keys.W || e.KeyCode == Keys.Up))
            {
                Game.isTop = false;
            }

            if (Game.isRight && (e.KeyCode == Keys.D || e.KeyCode == Keys.Right))
            {
                Game.isRight = false;
            }

            if (Game.isBottom && (e.KeyCode == Keys.S || e.KeyCode == Keys.Down))
            {
                Game.isBottom = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            if (Game.flag)
            {
                Draw(gG);

                gGraphics.DrawImage(bBackground, new Point(0, 0));
            }
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X >= 400 && e.X <= 500 && e.Y >= 272 && e.Y <= 310)
            { Game.updateHighScores(); goToMainMenu(); } 
            else if (e.X >= 400 && e.X <= 500 && e.Y >= 340 && e.Y <= 378)Game.undoGameData(gGraphics);
        }
    }
}
