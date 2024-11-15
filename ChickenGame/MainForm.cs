﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChickenGame
{
    public partial class MainForm : Form
    {
        //Generic var
        private System.Windows.Forms.Timer timer;
        private Random random;
        private Button tryAgainButton;

        //Specific var
        private int bgSpeed;        //Speed of the game
        private int tick;           //Count interval elapsed times
        private int phasePeriod;    //Time period till next chicken appear
        private int levelDuration;  //Duration till go to next difficulty level (Easy -> Hard)
        private int score;          //Scoreboard
        private bool gameOver;      //GameOver flag    

        //Chickens
        private List<Tuple<Point, Image>> chickenObjects;
        private List<Image> chickenImages;
        private Size chickenSize;

        //Player
        private Image playerImage;
        private Size playerSize;
        private Point playerPosition;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;                          //reduced flickering

            bgSpeed = 1;
            tick = 0;
            score = 0;
            phasePeriod = 100;
            levelDuration = 0;
            gameOver = false;
            random = new Random();

            chickenObjects = new List<Tuple<Point, Image>>();    //contains chicken-object
            chickenSize = new Size(50, 50);
            chickenImages = new List<Image>
            {
                Properties.Resources.chicken1,
                Properties.Resources.chicken2,
                Properties.Resources.chicken3,
                Properties.Resources.chicken4,
                Properties.Resources.chicken5,
                Properties.Resources.chicken6
            };

            playerImage = Properties.Resources.player;
            playerSize = new Size(50, 50);
            playerPosition = new Point((this.ClientSize.Width - playerSize.Width) / 2, (this.ClientSize.Height - 100));
           
            tryAgainButton = new Button();
            tryAgainButton.Text = "Try Again";
            tryAgainButton.Size = new Size(100, 50);
            tryAgainButton.Location = new Point((this.ClientSize.Width - tryAgainButton.Width) / 2, (this.ClientSize.Height - tryAgainButton.Height) / 2);
            tryAgainButton.Visible = false;
            tryAgainButton.Click += TryAgainButton_Click;
            this.Controls.Add(tryAgainButton);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;                            //millisecond
            timer.Tick += BgTimer_Tick;                     //occurs when above timer interval elapsed
            timer.Start();
        }

        private void BgTimer_Tick(object sender, EventArgs e)
        {
            /*
             * NOTE: consider CAREFULLY when abjust position of object! (Must base on the chickenSize and Application's Window size)
             * Currently base on objSize 50x50
             * The .Point is locate at the top-left of an object NOT the center
            */

            tick++;

            if (gameOver)
            {
                return;
            }

            // Spawn chickens
            if (tick == phasePeriod)
            {
                score += 5;
                tick = 0;
                levelDuration--;
                // Increase difficulty by decrease phasePeriod (More chicken will be spawn at a time) and increase speed of the game
                if (levelDuration == 5)
                {
                    levelDuration = 0;
                    phasePeriod -= 10;
                    bgSpeed += 1;
                }

                var randomImage = chickenImages[random.Next(chickenImages.Count)];
                chickenObjects.Add(new Tuple<Point, Image>(new Point(random.Next(0, this.ClientSize.Width - 50 + 1), -50), randomImage));
            }

            // Update positions of chickens
            for (int i = 0; i < chickenObjects.Count; i++)
            {
                var bgObj = chickenObjects[i];
                chickenObjects[i] = new Tuple<Point, Image>(new Point(bgObj.Item1.X, bgObj.Item1.Y + bgSpeed), bgObj.Item2); // Move bgObject down by bgSpeed pixels
            }

            // Check for collision between chicken and player
            foreach (var chickenObject in chickenObjects)
            {
                Rectangle chickenRect = new Rectangle(chickenObject.Item1, chickenSize);
                Rectangle playerRect = new Rectangle(new Point(playerPosition.X + 15, playerPosition.Y + 15), new Size(20, 20));
                if(chickenRect.IntersectsWith(playerRect))
                {
                    gameOver = true;
                    timer.Stop();
                    tryAgainButton.Visible = true;
                    MessageBox.Show("Game Over!");
                    return;
                }
            }

            // Remove chickens that have moved out of the window
            chickenObjects.RemoveAll(bgObj => bgObj.Item1.Y > this.ClientSize.Height);

            // Redraw the form
            this.Invalidate();
        }

        private void TryAgainButton_Click(object sender, EventArgs e)
        {
            chickenObjects.Clear();
            gameOver = false;
            tryAgainButton.Visible = false;
            tick = 0;
            score = 0;
            phasePeriod = 100;
            levelDuration = 0;
            bgSpeed = 1;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw each Obj
            foreach (var bgObj in chickenObjects)
            {
                e.Graphics.DrawImage(bgObj.Item2, new Rectangle(bgObj.Item1, chickenSize));
            }

            // Draw 'Try Again' button
            e.Graphics.DrawImage(playerImage, new Rectangle(playerPosition, playerSize));

            //Draw the scoreboard
            e.Graphics.DrawString($"Score: {score}", this.Font, Brushes.White, new PointF(10, this.ClientSize.Height - 25));
        }
    }
}
