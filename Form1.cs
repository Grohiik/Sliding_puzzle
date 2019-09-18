using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sliding_puzzle
{
    public partial class Form1 : Form
    {
        bool GameOn = false;
        int GameTime = 0;
        Padding pad = new Padding();
        PictureBox[][] positions = new PictureBox[][]
        {
            new PictureBox[4],
            new PictureBox[4],
            new PictureBox[4],
            new PictureBox[4]
        };
        PictureBox[][] positionfacit;

        public Form1()
        {
            InitializeComponent();
            fillPositions();
            positionfacit = new PictureBox[][] {
            new PictureBox[] {positions[0][0], positions[0][1], positions[0][2], positions[0][3] },
            new PictureBox[] {positions[1][0], positions[1][1], positions[1][2], positions[1][3] },
            new PictureBox[] {positions[2][0], positions[2][1], positions[2][2], positions[2][3] },
            new PictureBox[] {positions[3][0], positions[3][1], positions[3][2], positions[3][3] },
            };
        }




        private void TickTock_Tick(object sender, EventArgs e)
        {
            GameTime++;
            lblTimer.Text = GameTime / 60 + ":" + GameTime % 60;

            
        }

        private void MovingBoxTimer_Tick(object sender, EventArgs e)
        {
            //Moving Boxes
            for (int i = 0; i <= 3; i++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    positions[k][i].Location = new Point(k * 212 + 20, i * 212 + 50);
                }
            }
        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            if (!GameOn)
            {
                pictureBox16.Visible = false;
                GameOn = true;
                Scramble();

                //Setting a border so you see what you are doing
                pad.All = 1;
                for (int i = 0; i <= 3; i++)
                {
                    for (int k = 0; k <= 3; k++)
                    {
                        positions[k][i].Padding = pad;
                    }
                }
            }

            if (TickTock.Enabled)
            {
                btnStartStop.Text = "Start";
                TickTock.Enabled = false;

                //Disabeling all boxes so you can't move them while on pause
                for (int i = 0; i <= 3; i++)
                {
                    for (int k = 0; k <= 3; k++)
                    {
                        positions[k][i].Enabled = false;
                    }
                }
            }
            else
            {
                btnStartStop.Text = "Pause";
                TickTock.Enabled = true;

                //Enabeling all boxes so you can move them
                for (int i = 0; i <= 3; i++)
                {
                    for (int k = 0; k <= 3; k++)
                    {
                        positions[k][i].Enabled = true;
                    }
                }
            }

        }

        private void BtnReSet_Click(object sender, EventArgs e)
        {
            btnStartStop.Text = "Start";
            GameOn = false;
            TickTock.Enabled = false;
            GameTime = 0;
            lblTimer.Text = "0:0";
            pictureBox16.Visible = true;
            Unscramble();

            pad.All = 0;
            for (int i = 0; i <= 3; i++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    positions[k][i].Padding = pad;
                }
            }
        }

        private void Unscramble()
        {
            
            fillPositions();

        }

        private void Scramble()
        {
            Random Random = new Random();
            string BoxName = "pictureBox";

            for(int i = 1; i <= 100000; i++)
            {
                movebox(positions[findposistion(BoxName + Random.Next(1, 15))[0]][findposistion(BoxName + Random.Next(1, 15))[1]]);
            }
        }

        private void HaveIWon()
        {
            for(int i = 0; i <=3; i++)
            {
                for(int k = 0; k <=3; k++)
                {
                    if (positions[i][k] != positionfacit[i][k])
                    {
                        return;
                    }
                }
            }

            TickTock.Enabled = false;
            System.Windows.Forms.MessageBox.Show("You Won with the time " + GameTime / 60 + ":" + GameTime % 60);
            BtnReSet_Click(pictureBox1, new EventArgs());
            return;
        }

        private int[] findposistion (string boxName)
        {
            
            for(int i = 0; i <= 3; i++)
            {
                for(int j = 0; j <= 3; j++)
                {
                    if(boxName == positions[j][i].Name)
                    {
                        return new int[] { j, i };
                    }
                }
            }

            //should never be called, but visual studios complains at me if it is not there.
            return new int[] { 1, 3 };
        }

        private void fillPositions()
        {
            positions[0][0] = pictureBox1;
            positions[1][0] = pictureBox2;
            positions[2][0] = pictureBox3;
            positions[3][0] = pictureBox4;
            positions[0][1] = pictureBox5;
            positions[1][1] = pictureBox6;
            positions[2][1] = pictureBox7;
            positions[3][1] = pictureBox8;
            positions[0][2] = pictureBox9;
            positions[1][2] = pictureBox10;
            positions[2][2] = pictureBox11;
            positions[3][2] = pictureBox12;
            positions[0][3] = pictureBox13;
            positions[1][3] = pictureBox14;
            positions[2][3] = pictureBox15;
            positions[3][3] = pictureBox16;
        }

        private void movebox(PictureBox box)
        {
            int[] ClickedBoxPosition = findposistion(box.Name);
            int[] EmptyBoxPosition = findposistion(pictureBox16.Name);
            if (((ClickedBoxPosition[0] == EmptyBoxPosition[0] - 1 || ClickedBoxPosition[0] == EmptyBoxPosition[0] + 1) && (ClickedBoxPosition[1] == EmptyBoxPosition[1])) || ((ClickedBoxPosition[1] == EmptyBoxPosition[1] - 1 || ClickedBoxPosition[1] == EmptyBoxPosition[1] + 1) && (ClickedBoxPosition[0] == EmptyBoxPosition[0])))
            {

                PictureBox tempbox = positions[ClickedBoxPosition[0]][ClickedBoxPosition[1]];
                positions[ClickedBoxPosition[0]][ClickedBoxPosition[1]] = positions[EmptyBoxPosition[0]][EmptyBoxPosition[1]];
                positions[EmptyBoxPosition[0]][EmptyBoxPosition[1]] = tempbox;

                
            }
        }



        private void PictureBox1_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox10_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox11_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox12_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox13_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox14_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void PictureBox15_Click(object sender, EventArgs e)
        {
            movebox((PictureBox)sender);
            HaveIWon();
        }

        private void BtnOpenPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select a picture",
                Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF",
                Title = "Open your picture"
            };

            openFileDialog1.ShowDialog();

            ShowPicture();
        }

        private void ShowPicture()
        {
            int xByx = 4;
            var imgarray = new Image[16];
            var img = Image.FromFile(openFileDialog1.FileName);
            for (int i = 0; i < xByx; i++)
            {
                for (int j = 0; j < xByx; j++)
                {
                    var index = i * xByx + j;
                    imgarray[index] = new Bitmap(img.Width / xByx, img.Height / xByx);
                    var graphics = Graphics.FromImage(imgarray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, img.Width / xByx, img.Height / xByx), new Rectangle(j * img.Width / xByx, i * img.Height / xByx, img.Width / xByx, img.Height / xByx), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }
            }
            pictureBox1.Image = imgarray[0];
            pictureBox2.Image = imgarray[1];
            pictureBox3.Image = imgarray[2];
            pictureBox4.Image = imgarray[3];
            pictureBox5.Image = imgarray[4];
            pictureBox6.Image = imgarray[5];
            pictureBox7.Image = imgarray[6];
            pictureBox8.Image = imgarray[7];
            pictureBox9.Image = imgarray[8];
            pictureBox10.Image = imgarray[9];
            pictureBox11.Image = imgarray[10];
            pictureBox12.Image = imgarray[11];
            pictureBox13.Image = imgarray[12];
            pictureBox14.Image = imgarray[13];
            pictureBox15.Image = imgarray[14];
            pictureBox16.Image = imgarray[15];
        }
    }
}
