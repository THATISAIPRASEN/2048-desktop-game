using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace game_2048
{
    class Game
    {
        private int[][] block;
        private int[][] pastData;
        private int pastScore, pastBestScore;
        private int score = 0, bestscore = 0;
        private int []scores;
        private List<Button> button = new List<Button>();
        private List<System.Drawing.Bitmap> bitmap = new List<System.Drawing.Bitmap>();
        private System.Drawing.Font font10 = new System.Drawing.Font("Clear Sans", 10, System.Drawing.FontStyle.Bold);
        private System.Drawing.Font font12 = new System.Drawing.Font("Clear Sans", 12, System.Drawing.FontStyle.Bold);
        private System.Drawing.Font font22 = new System.Drawing.Font("Clear Sans", 22, System.Drawing.FontStyle.Bold);
        private System.Drawing.SizeF stringSize = new System.Drawing.SizeF();
        private int blocksToCreate = 2;
        private Random random = new Random();
        private Boolean gameOver = false;
        private System.Drawing.Rectangle Rect;
        public Boolean isTop, isRight, isBottom, isLeft;
        public Boolean flag = true;
        private Boolean is2048 = false;
        public enum Direction{Top,Right,Bottom,Left,};
        public Game()
        {
            this.scores = new int[6];
            this.block = new int[4][];
            for (int i = 0; i < 4; i++)
            block[i] = new int[4];
            this.pastData = new int[4][];
            for (int i = 0; i < 4; i++)
            pastData[i] = new int[4];
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) pastData[i][j] = 0;
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._1));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._2));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._3));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._4));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._5));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._6));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._7));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._8));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._9));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._10));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._11));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._12));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._13));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._14));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._15));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._16));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._17));
            bitmap.Add(new System.Drawing.Bitmap(Properties.Resources._18));
            button.Add(new Button(400, 40, 100, 66, 1, false)); 
            button.Add(new Button(400, 156, 100, 66, 1, false)); 
            Rect = new System.Drawing.Rectangle(50,50,300,300);
            updateHighScores();
        }
        public void Update()
        {
            while (!gameOver && blocksToCreate > 0)
            {
                int nX = random.Next(0, 4), nY = random.Next(0, 4);

                if (block[nX][nY] == 0)
                {
                    block[nX][nY] = random.Next(0, 20) == 0 ? random.Next(0, 15) == 0 ? 8 : 4 : 2;
                   
                    --blocksToCreate;
                }
            }
        }

        public void Draw(System.Drawing.Graphics g)
        {
            DrawGame(g);
            if (gameOver) GameOverDraw(g);
            flag = false;
        }
        public void DrawGame(System.Drawing.Graphics g)
        {
            for (int i = 0; i < button.Count; i++)
            {
                button[i].Draw(g, bitmap[button[i].getIMGID()]);
            }
            printText(g, "SCORE", font12, new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.SolidBrush(System.Drawing.Color.Violet), 450, 50);
            printText(g, score.ToString(), font12, new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.SolidBrush(System.Drawing.Color.White), 450, 80);
            g.DrawImage(bitmap[2], new System.Drawing.Point(400, 340));
            printText(g, "BEST", font12, new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.SolidBrush(System.Drawing.Color.Violet), 450, 166);
            printText(g, bestscore.ToString(), font12, new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.SolidBrush(System.Drawing.Color.White), 450, 200);
            g.DrawImage(bitmap[2], new System.Drawing.Point(400, 272));
            printText(g, "MAIN MENU", font10, new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.SolidBrush(System.Drawing.Color.Red), 450, 290);
            printText(g, "UNDO", font12, new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.SolidBrush(System.Drawing.Color.Yellow), 450, 360);
            
            g.DrawImage(bitmap[3], new System.Drawing.Point(20, 30));

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    g.DrawImage(bitmap[ getBitmapID(block[i][j])], new System.Drawing.Point(32 + 87 * i, 42 + 87 * j));
                    if (block[i][j] > 0)
                    {
                        printText(g, block[i][j].ToString(), font22, new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.SolidBrush(System.Drawing.Color.White), 72 + 87 * i, 82 + 87 * j);
                    }
                }
            }
        }
        public void updateHighScores()
        {
            if(!System.IO.File.Exists("HighScores.txt"))
            System.IO.File.Create("HighScores.txt").Close();
            System.IO.StreamReader sr = new System.IO.StreamReader("HighScores.txt");
            for(int i=0;i<5;i++) 
             scores[i]  = Convert.ToInt32(sr.ReadLine());
            sr.Close();
            scores[5] = score;
            Array.Sort(scores);bestscore = scores[5];
            System.IO.StreamWriter sw = new System.IO.StreamWriter("HighScores.txt");
            for (int i = 5; i >0; i--)
            sw.WriteLine(scores[i]);
            sw.Close();
        }
        public void GameOverDraw(System.Drawing.Graphics g)
        {
            updateHighScores();
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Gold), Rect);

            printText(g, "GAME OVER", font22, new System.Drawing.SolidBrush(System.Drawing.Color.Blue), new System.Drawing.SolidBrush(System.Drawing.Color.Black), 200, 110);
            printText(g, "SCORE: " + score.ToString(), font22, new System.Drawing.SolidBrush(System.Drawing.Color.Orange), new System.Drawing.SolidBrush(System.Drawing.Color.Red), 200, 190);
        }

        public void printText(System.Drawing.Graphics g, String sText, System.Drawing.Font nFont, System.Drawing.SolidBrush nSolidBrush, System.Drawing.SolidBrush nSolidBrush2, int X, int Y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new System.Drawing.PointF(X - stringSize.Width / 2 + 1, Y - stringSize.Height / 2 + 1));
            g.DrawString(sText, nFont, nSolidBrush2, new System.Drawing.PointF(X - stringSize.Width / 2, Y - stringSize.Height / 2));
        }
         public void moveBoard(Direction nDirection)
        {
            Boolean bAdd = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    pastData[i][j] = block[i][j];
                }
            }
            pastBestScore = bestscore;
            pastScore = score;
            switch (nDirection)
            {
                case Direction.Top:
                    for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                            for (int k = j + 1; k < 4; k++)
                            {
                                if (block[i][k] == 0) continue;
                                else if (block[i][k] == block[i][j])
                                {
                                    block[i][j] *= 2; 
                                    score += block[i][j];
                                    block[i][k] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (block[i][j] == 0 && block[i][k] != 0)
                                    {
                                        block[i][j] = block[i][k];
                                        block[i][k] = 0;
                                        j--;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (block[i][j] != 0) break;
                                }
                            }
                    break;
                case Direction.Right:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 3; i >= 0; i--)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (block[k][j] == 0)
                                {
                                    continue;
                                }
                                else if (block[k][j] == block[i][j])
                                {
                                    block[i][j] *= 2; 
                                    score += block[i][j];
                                    block[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (block[i][j] == 0 && block[k][j] != 0)
                                    {
                                        block[i][j] = block[k][j];
                                        block[k][j] = 0;
                                        i++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (block[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.Bottom:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 3; j >= 0; j--)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (block[i][k] == 0)
                                {
                                    continue;
                                }
                                else if (block[i][k] == block[i][j])
                                {
                                    block[i][j] *= 2; 
                                    score += block[i][j];
                                    block[i][k] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (block[i][j] == 0 && block[i][k] != 0)
                                    {
                                        block[i][j] = block[i][k];
                                        block[i][k] = 0;
                                        j++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (block[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.Left:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            for (int k = i + 1; k < 4; k++)
                            {
                                if (block[k][j] == 0)
                                {
                                    continue;
                                }
                                else if (block[k][j] == block[i][j])
                                {
                                    block[i][j] *= 2; 
                                    score += block[i][j];
                                    block[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (block[i][j] == 0 && block[k][j] != 0)
                                    {
                                        block[i][j] = block[k][j];
                                        block[k][j] = 0;
                                        i--;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (block[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            if (score > bestscore)
            {
                bestscore = score;
            }

            if (bAdd)
            {
                ++blocksToCreate;
            }
            checkGameOver();
            for(int i=0;i<4;i++)
            {
                for(int j=0;j<4;j++)
                {
                    if (block[i][j] == 2048 && is2048 == false) { MessageBox.Show("Congrats... you have reached 2048"); is2048 = true; }
                }
            }
            flag = true;
        }

        public void checkGameOver()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (block[i - 1][j] == block[i][j])
                        {
                            return;
                        }
                    }

                    if (i + 1 < 4)
                    {
                        if (block[i + 1][j] == block[i][j])
                        {
                            return;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (block[i][j - 1] == block[i][j])
                        {
                            return;
                        }
                    }

                    if (j + 1 < 4)
                    {
                        if (block[i][j + 1] == block[i][j])
                        {
                            return;
                        }
                    }

                    if (block[i][j] == 0)
                    {
                        return;
                    }
                }
            }

            gameOver = true;
        }
        public int getBitmapID(int iNum)
        {
            switch (iNum)
            {
                case 0:
                    return 4;
                case 2:
                    return 5;
                case 4:
                    return 6;
                case 8:
                    return 7;
                case 16:
                    return 8;
                case 32:
                    return 9;
                case 64:
                    return 10;
                case 128:
                    return 11;
                case 256:
                    return 12;
                case 512:
                    return 13;
                case 1024:
                    return 14;
                case 2048:
                    return 15;
                case 4096:
                case 8192:
                case 16384:
                    return 16;
            }

            return 4;
        }

        
       
        public void undoGameData(System.Drawing.Graphics g)
        {
            int f = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (pastData[i][j] != 0) f = 1;
                }
            }
           if(f==1)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        block[i][j] = pastData[i][j];
                    }
                }
                bestscore = pastBestScore;
                score = pastScore;
                DrawGame(g);
            }
            flag = true;
        }
    }
}
