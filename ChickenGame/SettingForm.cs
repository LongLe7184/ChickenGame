using System;
using System.Drawing;
using System.IO.Ports;
using System.Media;
using System.Windows.Forms;

namespace ChickenGame
{
    public partial class SettingForm : Form
    {
        public static String selectedPort;
        public static int selectedBaudRate;
        private SoundPlayer bgmPlayer;
        
        public SettingForm()
        {
            InitializeComponent();
            InitializeBGM();
            bgmPlayer.PlayLooping();

            this.Controls.Add(portComboBox);
            this.Controls.Add(baudRateComboBox);
            this.Controls.Add(refreshButton);

            // Refresh Serial Port list
            refreshButton.Click += RefreshButton_Click;

            // Load ports on startup
            LoadPorts();
            LoadBaudrate();

            // Get selected Serial Port and Baud Rate
            portComboBox.SelectedIndexChanged += PortComboBox_SelectedIndexChanged;
            baudRateComboBox.SelectedIndexChanged += BaudRateComboBox_SelectedIndexChanged;

            // Start Game
            startGame.BackColor = ColorTranslator.FromHtml("#FF5E5E");
            startGame.Enabled = false;
            startGame.Click += StartGame_Click;

        }

        private void InitializeBGM()
        { 
            bgmPlayer = new SoundPlayer("Uppercut.wav");
            bgmCheckBox.CheckedChanged += BgmCheckBox_CheckedChanged;
            Controls.Add(bgmCheckBox);
        }

        private void BgmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bgmCheckBox.Checked)
            {
                bgmPlayer.Stop();
            }
            else
            {
                bgmPlayer.PlayLooping();
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadPorts();
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.ShowDialog();
            this.Close();
        }

        private void LoadPorts()
        {
            portComboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                portComboBox.Items.Add(port);
            }

            if (portComboBox.Items.Count > 0)
            {
                portComboBox.SelectedIndex = 0;
            }
        }

        private void LoadBaudrate()
        {
            baudRateComboBox.Items.Clear();

            baudRateComboBox.Items.Add("9600");
            baudRateComboBox.Items.Add("14400");
            baudRateComboBox.Items.Add("19200");
            baudRateComboBox.Items.Add("115200");

            baudRateComboBox.SelectedIndex = 0;
        }

        private void PortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPort = portComboBox.SelectedItem.ToString();
            String temp = "Port: " + selectedPort + ", BaudRate: " + selectedBaudRate.ToString();
            displayBox.Text = temp;
            if (selectedPort != "" && selectedBaudRate != 0) 
            {
                startGame.BackColor = ColorTranslator.FromHtml("#86FFAA");
                startGame.Enabled = true;
            }
        }


        private void BaudRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedBaudRate = int.Parse(baudRateComboBox.SelectedItem.ToString());
            String temp = "Port: " + selectedPort + ", BaudRate: " + selectedBaudRate.ToString();
            displayBox.Text = temp;
            if (selectedPort != "" && selectedBaudRate != 0)
            {
                startGame.BackColor = ColorTranslator.FromHtml("#86FFAA");
                startGame.Enabled = true;
            }
        }
    }
}
