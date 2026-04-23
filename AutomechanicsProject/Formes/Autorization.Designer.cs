using AutomechanicsProject.Properties;

namespace AutomechanicsProject
{
    partial class Autorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Autorization));
            this.labelEnter = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.panelauto = new System.Windows.Forms.Panel();
            this.panelautorization = new System.Windows.Forms.Panel();
            this.buttonRegest = new System.Windows.Forms.Button();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.panelautorization.SuspendLayout();
            this.SuspendLayout();
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            // 
            // labelEnter
            // 
            this.labelEnter.AutoSize = true;
            this.labelEnter.Font = new System.Drawing.Font("Jost", 22.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEnter.Location = new System.Drawing.Point(38, 36);
            this.labelEnter.Name = "labelEnter";
            this.labelEnter.Size = new System.Drawing.Size(201, 85);
            this.labelEnter.TabIndex = 0;
            this.labelEnter.Text = Resources.Auth_LabelEnter_Text;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLogin.Location = new System.Drawing.Point(38, 140);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(104, 46);
            this.labelLogin.TabIndex = 1;
            this.labelLogin.Text = Resources.Auth_LabelLogin_Text;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPassword.Location = new System.Drawing.Point(38, 255);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(127, 46);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = Resources.Auth_LabelPassword_Text;
            // 
            // panelauto
            // 
            this.panelauto.AutoScroll = true;
            this.panelauto.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelauto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelauto.BackgroundImage")));
            this.panelauto.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelauto.Location = new System.Drawing.Point(641, 0);
            this.panelauto.Name = "panelauto";
            this.panelauto.Size = new System.Drawing.Size(535, 801);
            this.panelauto.TabIndex = 3;
            this.panelauto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            // 
            // panelautorization
            // 
            this.panelautorization.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelautorization.Controls.Add(this.buttonRegest);
            this.panelautorization.Controls.Add(this.buttonEnter);
            this.panelautorization.Controls.Add(this.textBoxPassword);
            this.panelautorization.Controls.Add(this.textBoxLogin);
            this.panelautorization.Controls.Add(this.labelEnter);
            this.panelautorization.Controls.Add(this.labelPassword);
            this.panelautorization.Controls.Add(this.labelLogin);
            this.panelautorization.Font = new System.Drawing.Font("Jost", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelautorization.Location = new System.Drawing.Point(0, 0);
            this.panelautorization.Name = "panelautorization";
            this.panelautorization.Size = new System.Drawing.Size(1176, 801);
            this.panelautorization.TabIndex = 4;
            this.panelautorization.Dock = System.Windows.Forms.DockStyle.Fill; 
            this.panelautorization.AutoSize = true;
            this.panelautorization.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            // 
            // buttonRegest
            // 
            this.buttonRegest.FlatAppearance.BorderSize = 0;
            this.buttonRegest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegest.Location = new System.Drawing.Point(38, 661);
            this.buttonRegest.Name = "buttonRegest";
            this.buttonRegest.Size = new System.Drawing.Size(561, 42);
            this.buttonRegest.TabIndex = 6;
            this.buttonRegest.Text = Resources.Auth_ButtonRegister_Text;
            this.buttonRegest.UseVisualStyleBackColor = true;
            this.buttonRegest.Click += new System.EventHandler(this.linkRegisterClick);
            // 
            // buttonEnter
            // 
            this.buttonEnter.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonEnter.Font = new System.Drawing.Font("Jost", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEnter.Location = new System.Drawing.Point(38, 525);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(561, 130);
            this.buttonEnter.TabIndex = 5;
            this.buttonEnter.Text = Resources.Auth_ButtonEnter_Text;
            this.buttonEnter.UseVisualStyleBackColor = false;
            this.buttonEnter.Click += new System.EventHandler(this.BtnLoginClick);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassword.Location = new System.Drawing.Point(38, 313);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(561, 54);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.Text = Resources.AuthPasswordWatermark;
            this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLogin.Font = new System.Drawing.Font("Jost", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogin.Location = new System.Drawing.Point(38, 196);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(561, 54);
            this.textBoxLogin.TabIndex = 3;
            this.textBoxLogin.Text = Resources.AuthLoginWatermark;
            this.textBoxLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // Autorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 801);
            this.Controls.Add(this.panelauto);
            this.Controls.Add(this.panelautorization);
            this.Name = "Autorization";
            this.Text = Resources.Auth_Form_Title;
            this.panelautorization.ResumeLayout(false);
            this.panelautorization.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelEnter;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Panel panelauto;
        private System.Windows.Forms.Panel panelautorization;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonRegest;
    }
}