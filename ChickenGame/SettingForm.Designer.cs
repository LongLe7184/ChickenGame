namespace ChickenGame
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.baudRateComboBox = new System.Windows.Forms.ComboBox();
            this.startGame = new System.Windows.Forms.Button();
            this.displayBox = new System.Windows.Forms.TextBox();
            this.bgmCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // portComboBox
            // 
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(296, 226);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(121, 24);
            this.portComboBox.TabIndex = 0;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(448, 227);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(76, 23);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(41)))), ((int)(((byte)(79)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(157, 230);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(121, 15);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Select Serial Port :";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(41)))), ((int)(((byte)(79)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox2.Location = new System.Drawing.Point(195, 273);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(80, 15);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Baud Rate :";
            // 
            // baudRateComboBox
            // 
            this.baudRateComboBox.FormattingEnabled = true;
            this.baudRateComboBox.Location = new System.Drawing.Point(296, 268);
            this.baudRateComboBox.Name = "baudRateComboBox";
            this.baudRateComboBox.Size = new System.Drawing.Size(121, 24);
            this.baudRateComboBox.TabIndex = 4;
            // 
            // startGame
            // 
            this.startGame.BackColor = System.Drawing.Color.White;
            this.startGame.Location = new System.Drawing.Point(249, 362);
            this.startGame.Name = "startGame";
            this.startGame.Size = new System.Drawing.Size(98, 34);
            this.startGame.TabIndex = 5;
            this.startGame.Text = "Start Game";
            this.startGame.UseVisualStyleBackColor = false;
            // 
            // displayBox
            // 
            this.displayBox.Location = new System.Drawing.Point(195, 326);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(222, 22);
            this.displayBox.TabIndex = 6;
            this.displayBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bgmCheckBox
            // 
            this.bgmCheckBox.AutoSize = true;
            this.bgmCheckBox.Location = new System.Drawing.Point(494, 421);
            this.bgmCheckBox.Name = "bgmCheckBox";
            this.bgmCheckBox.Size = new System.Drawing.Size(78, 20);
            this.bgmCheckBox.TabIndex = 7;
            this.bgmCheckBox.Text = "BGM Off";
            this.bgmCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(582, 453);
            this.Controls.Add(this.bgmCheckBox);
            this.Controls.Add(this.displayBox);
            this.Controls.Add(this.startGame);
            this.Controls.Add(this.baudRateComboBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.portComboBox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(600, 500);
            this.Name = "SettingForm";
            this.Text = "ChickenGame - Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox baudRateComboBox;
        private System.Windows.Forms.Button startGame;
        private System.Windows.Forms.TextBox displayBox;
        private System.Windows.Forms.CheckBox bgmCheckBox;
    }
}