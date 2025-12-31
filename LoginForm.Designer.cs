// ============================================================================
// BLVDE LOGIN MODULE
// ============================================================================
// Secure entry point for the Command OS.
// Theme: Hacker / Terminal
// ============================================================================

namespace BLVDEContentStudio
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Constants
            System.Drawing.Color colBlack = System.Drawing.Color.FromArgb(5, 5, 5);
            System.Drawing.Color colGreen = System.Drawing.Color.FromArgb(0, 255, 0);
            System.Drawing.Font fontTerminal = new System.Drawing.Font("Consolas", 12F);

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            
            this.SuspendLayout();

            // 
            // LOGIN FORM SETUP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = colBlack;
            this.ForeColor = colGreen;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // No borders for pure hacker look
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "LoginForm";
            this.Text = "LOGIN";

            // Border Paint
            this.Paint += (s, e) => {
                System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, colGreen, System.Windows.Forms.ButtonBorderStyle.Solid);
            };

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(130, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "AUTHENTICATE_USER";

            // User Label
            this.lblUser.AutoSize = true;
            this.lblUser.Font = fontTerminal;
            this.lblUser.Location = new System.Drawing.Point(50, 90);
            this.lblUser.Name = "lblUser";
            this.lblUser.Text = "USERNAME_:";

            // User Box
            this.txtUser.BackColor = colBlack;
            this.txtUser.ForeColor = colGreen;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = fontTerminal;
            this.txtUser.Location = new System.Drawing.Point(180, 88);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(250, 31);
            this.txtUser.TabIndex = 0;

            // Pass Label
            this.lblPass.AutoSize = true;
            this.lblPass.Font = fontTerminal;
            this.lblPass.Location = new System.Drawing.Point(50, 140);
            this.lblPass.Name = "lblPass";
            this.lblPass.Text = "PASSWORD_:";

            // Pass Box
            this.txtPass.BackColor = colBlack;
            this.txtPass.ForeColor = colGreen;
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Font = fontTerminal;
            this.txtPass.Location = new System.Drawing.Point(180, 138);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(250, 31);
            this.txtPass.TabIndex = 1;

            // Button: Login
            this.btnLogin.BackColor = colBlack;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = fontTerminal;
            this.btnLogin.ForeColor = colGreen;
            this.btnLogin.Location = new System.Drawing.Point(180, 200);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(120, 40);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "[ ENTER ]";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += (s, e) => { btnLogin.BackColor = colGreen; btnLogin.ForeColor = colBlack; };
            this.btnLogin.MouseLeave += (s, e) => { btnLogin.BackColor = colBlack; btnLogin.ForeColor = colGreen; };

            // Button: Exit
            this.btnExit.BackColor = colBlack;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = fontTerminal;
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(310, 200);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 40);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "[ ABORT ]";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += (s, e) => { System.Windows.Forms.Application.Exit(); };
            this.btnExit.MouseEnter += (s, e) => { btnExit.BackColor = System.Drawing.Color.Red; btnExit.ForeColor = colBlack; };
            this.btnExit.MouseLeave += (s, e) => { btnExit.BackColor = colBlack; btnExit.ForeColor = System.Drawing.Color.Red; };

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
