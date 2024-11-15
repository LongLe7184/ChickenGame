using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text;

namespace ChickenGame
{
    public partial class MainForm : Form
    {
        //Generic var
        private System.Windows.Forms.Timer timer;
        private Random random;
        private Button tryAgainButton;
        private SerialPort serialPort;

        //Specific var
        private int bgSpeed;        //Speed of the game
        private int tick;           //Count interval elapsed times
        private int phasePeriod;    //Time period till next chicken appear
        private int levelDuration;  //Duration till go to next difficulty level (Easy -> Hard)
        private int score;          //Scoreboard
        private bool gameOver;      //GameOver flag

        //Joystick var
        private int xVal;           //Horizontal control val
        private int yVal;           //Vertical control val
        private bool pVal;          //Try again button
        private StringBuilder dataBuffer;   //Store data from serial port

        //Chickens
        private List<Tuple<Point, Image>> chickenObjects;
        private Size chickenSize;
        private List<Image> chickenImages;
        
        //Player
        private Image playerImage;
        private Size playerSize;
        private Point playerPosition;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;                     //reduced flickering

            //Initial "Try Again" button
            tryAgainButton = new Button();
            tryAgainButton.Text = "Try Again";
            tryAgainButton.Size = new Size(100, 50);
            tryAgainButton.Location = new Point((this.ClientSize.Width - tryAgainButton.Width) / 2, (this.ClientSize.Height - tryAgainButton.Height) / 2);
            tryAgainButton.Visible = false;
            tryAgainButton.Click += TryAgainButton_Click;
            this.Controls.Add(tryAgainButton);

            //Initial UI timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 80;                            //in millisecond
            timer.Tick += BgTimer_Tick;                     //occurs when above timer interval elapsed
            timer.Start();


            //Initial specific & generic var
            bgSpeed = 1;
            tick = 0;
            score = 0;
            phasePeriod = 100;
            levelDuration = 0;
            gameOver = false;
            random = new Random();

            //Initial Chicken objects
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

            //Initial Player object
            playerImage = Properties.Resources.player;
            playerSize = new Size(50, 50);
            playerPosition = new Point((this.ClientSize.Width - playerSize.Width) / 2, (this.ClientSize.Height - 100));

            //Initial serialPort (communicate with Arduino)
            dataBuffer = new StringBuilder();
            serialPort = new SerialPort("COM7", 9600);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort.Open();

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

            // Update position of player
            switch (xVal)
            {
                case 1: //Player go RIGHT
                    if (playerPosition.X <= this.ClientSize.Width - playerSize.Width)
                    {
                        playerPosition = new Point(playerPosition.X + 2, playerPosition.Y);
                    }
                    break;
                case -1: //Player go LEFT
                    if (playerPosition.X >= 2)
                    {
                        playerPosition = new Point(playerPosition.X - 2, playerPosition.Y);
                    }
                   break;
                default:
                    break;
            }
            switch (yVal)
            {
                case 1: //Player go UP
                    if (playerPosition.Y >= 2)
                    {
                        playerPosition = new Point(playerPosition.X, playerPosition.Y - 2);
                    }
                    break;
                case -1: //Player go DOWN
                    if (playerPosition.Y <= this.ClientSize.Height - playerSize.Height)
                    {
                        playerPosition = new Point(playerPosition.X, playerPosition.Y + 2);
                    }
                   break;
                default:
                    break;
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
                    MessageBox.Show("Game Over!\nYour score: " + score, "Notification");
                    //WaitForTryAgain();
                    return;
                }
            }

            // Remove chickens that have moved out of the window
            chickenObjects.RemoveAll(bgObj => bgObj.Item1.Y > this.ClientSize.Height);

            // Redraw the form
            this.Invalidate();

        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    string readSerial = serialPort.ReadExisting();
                    dataBuffer.Append(readSerial); // Append data to buffer
                    ProcessBufferedData();
                }));
            }
        }

        private void ProcessBufferedData()
        {
            while (dataBuffer.Length > 0)
            {
                int newLineIndex = dataBuffer.ToString().IndexOf('\n');
                if (newLineIndex == -1)
                {
                    break; // No complete line in buffer
                }
                string line = dataBuffer.ToString(0, newLineIndex + 1);
                dataBuffer.Remove(0, newLineIndex + 1);
                ProcessJoystickData(line.Trim());
            }
        }

        //readData from serial form: "X:1 Y:-1"
        private void ProcessJoystickData(string readData)
        {
            string[] strings = readData.Trim().Split(' ');
            if (strings.Length == 2)
            {
                if (int.TryParse(strings[0].Trim(), out int newXVal) && int.TryParse(strings[1].Trim(), out int newYVal))
                {
                    if (Math.Abs(newXVal - xVal) >= 1 || Math.Abs(newYVal - yVal) >= 1)
                    {
                        xVal = newXVal;
                        yVal = newYVal;
                    }
                }
            }
        }

        // Try Again by clicking on screen button
        private void TryAgainButton_Click(object sender, EventArgs e)
        {
            TryAgain();
        }

        private void TryAgain()
        {
            chickenObjects.Clear();
            gameOver = false;
            tryAgainButton.Visible = false;
            tick = 0;
            score = 0;
            phasePeriod = 100;
            levelDuration = 0;
            bgSpeed = 1;
            playerPosition = new Point((this.ClientSize.Width - playerSize.Width) / 2, (this.ClientSize.Height - 100));
            timer.Start();
        }

        /* Using button on DE10 board to replace using mouse to click 'Try Again' on-screen button */
        //private void WaitForTryAgain()
        //{
        //    while (gameOver)
        //    {
        //        if (!pVal)  //JoyStick's button is pushed
        //        {
        //            TryAgain();
        //            break;
        //        }
        //    }
        //}
        
        // Draw out object on the screen
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw each Chicken
            foreach (var bgObj in chickenObjects)
            {
                e.Graphics.DrawImage(bgObj.Item2, new Rectangle(bgObj.Item1, chickenSize));
            }

            // Draw Player
            e.Graphics.DrawImage(playerImage, new Rectangle(playerPosition, playerSize));

            //Draw the scoreboard
            e.Graphics.DrawString($"Score: {score}", this.Font, Brushes.White, new PointF(10, this.ClientSize.Height - 25));
        }

        // Closing SerialPort when Form close
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
}
