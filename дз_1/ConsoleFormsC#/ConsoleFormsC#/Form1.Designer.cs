namespace ConsoleFormsC_
{
    partial class это_точно_cmd
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.upperTextBox = new System.Windows.Forms.TextBox();
            this.exit = new System.Windows.Forms.Button();
            this.lblupperTextBox = new System.Windows.Forms.Label();
            this.lowerTextBox = new System.Windows.Forms.TextBox();
            this.imgItsOk = new System.Windows.Forms.PictureBox();
            this.upperTextBoxChecker = new System.Windows.Forms.Timer(this.components);
            this.lblLowerTextBox = new System.Windows.Forms.Label();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblCurDir = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgItsOk)).BeginInit();
            this.SuspendLayout();
            // 
            // upperTextBox
            // 
            this.upperTextBox.BackColor = System.Drawing.Color.Black;
            this.upperTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.upperTextBox.Location = new System.Drawing.Point(12, 49);
            this.upperTextBox.Multiline = true;
            this.upperTextBox.Name = "upperTextBox";
            this.upperTextBox.Size = new System.Drawing.Size(314, 56);
            this.upperTextBox.TabIndex = 0;
            this.upperTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.upperTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checking);
            // 
            // exit
            // 
            this.exit.ForeColor = System.Drawing.Color.Black;
            this.exit.Location = new System.Drawing.Point(724, 12);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(64, 30);
            this.exit.TabIndex = 2;
            this.exit.Text = "exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // lblupperTextBox
            // 
            this.lblupperTextBox.AutoSize = true;
            this.lblupperTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.lblupperTextBox.Location = new System.Drawing.Point(9, 33);
            this.lblupperTextBox.Name = "lblupperTextBox";
            this.lblupperTextBox.Size = new System.Drawing.Size(113, 13);
            this.lblupperTextBox.TabIndex = 3;
            this.lblupperTextBox.Text = "Место ввода команд";
            this.lblupperTextBox.Click += new System.EventHandler(this.output_Click);
            // 
            // lowerTextBox
            // 
            this.lowerTextBox.BackColor = System.Drawing.Color.Black;
            this.lowerTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.lowerTextBox.Location = new System.Drawing.Point(12, 124);
            this.lowerTextBox.Multiline = true;
            this.lowerTextBox.Name = "lowerTextBox";
            this.lowerTextBox.ReadOnly = true;
            this.lowerTextBox.Size = new System.Drawing.Size(314, 198);
            this.lowerTextBox.TabIndex = 4;
            // 
            // imgItsOk
            // 
            this.imgItsOk.Location = new System.Drawing.Point(414, 49);
            this.imgItsOk.Name = "imgItsOk";
            this.imgItsOk.Size = new System.Drawing.Size(374, 198);
            this.imgItsOk.TabIndex = 5;
            this.imgItsOk.TabStop = false;
            this.imgItsOk.Click += new System.EventHandler(this.imgItsOk_Click);
            // 
            // upperTextBoxChecker
            // 
            this.upperTextBoxChecker.Interval = 300;
            this.upperTextBoxChecker.Tick += new System.EventHandler(this.upperTextBoxChecker_Tick);
            // 
            // lblLowerTextBox
            // 
            this.lblLowerTextBox.AutoSize = true;
            this.lblLowerTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.lblLowerTextBox.Location = new System.Drawing.Point(12, 108);
            this.lblLowerTextBox.Name = "lblLowerTextBox";
            this.lblLowerTextBox.Size = new System.Drawing.Size(106, 13);
            this.lblLowerTextBox.TabIndex = 7;
            this.lblLowerTextBox.Text = "Резульатат команд";
            this.lblLowerTextBox.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.BackColor = System.Drawing.Color.Black;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDir.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDir.Location = new System.Drawing.Point(9, 12);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(140, 13);
            this.lblDir.TabIndex = 8;
            this.lblDir.Text = "Текущая директория: ";
            this.lblDir.Click += new System.EventHandler(this.lblDir_Click);
            // 
            // lblCurDir
            // 
            this.lblCurDir.AutoSize = true;
            this.lblCurDir.BackColor = System.Drawing.Color.Black;
            this.lblCurDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurDir.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCurDir.Location = new System.Drawing.Point(155, 12);
            this.lblCurDir.Name = "lblCurDir";
            this.lblCurDir.Size = new System.Drawing.Size(0, 13);
            this.lblCurDir.TabIndex = 9;
            // 
            // это_точно_cmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.lblCurDir);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.lblLowerTextBox);
            this.Controls.Add(this.imgItsOk);
            this.Controls.Add(this.lowerTextBox);
            this.Controls.Add(this.lblupperTextBox);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.upperTextBox);
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "это_точно_cmd";
            this.ShowIcon = false;
            this.Text = "это_точно_cmd";
            this.Load += new System.EventHandler(this.это_точно_cmd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgItsOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox upperTextBox;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Label lblupperTextBox;
        private System.Windows.Forms.TextBox lowerTextBox;
        private System.Windows.Forms.PictureBox imgItsOk;
        private System.Windows.Forms.Timer upperTextBoxChecker;
        private System.Windows.Forms.Label lblLowerTextBox;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.Label lblCurDir;
    }
}

