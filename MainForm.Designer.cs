// ============================================================================
// BLVDE COMMAND OS - FORM DESIGNER // HACKER EDITION
// ============================================================================
// Style: Anonymous / Matrix / Terminal
// Colors: Black (0,0,0) and Lime Green (0, 255, 0)
// Font: Consolas (Monospace)
// ============================================================================

namespace BLVDEContentStudio
{
    public partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Components
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblSystemStatus;

        private System.Windows.Forms.GroupBox grpSystemControl;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnSettings;

        private System.Windows.Forms.GroupBox grpAutomation;
        private System.Windows.Forms.Button btnAutoPost;
        private System.Windows.Forms.Button btnGenerateContent;
        private System.Windows.Forms.Label lblLastAction;

        private System.Windows.Forms.GroupBox grpAgentConsole;
        private System.Windows.Forms.TextBox txtAgentInput;
        private System.Windows.Forms.Button btnExecuteAgent;
        private System.Windows.Forms.Label lblAgentPrompt;
        
        // Console Log (Upgraded to RichTextBox for colored status)
        private System.Windows.Forms.RichTextBox txtConsoleLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // STYLE CONSTANTS
            System.Drawing.Color colorBg = System.Drawing.Color.FromArgb(10, 10, 15);
            System.Drawing.Color colorAccent = System.Drawing.Color.FromArgb(0, 210, 255);
            System.Drawing.Color colorViolet = System.Drawing.Color.FromArgb(180, 0, 255); 
            System.Drawing.Color colorSuccess = System.Drawing.Color.FromArgb(0, 255, 127);
            System.Drawing.Color colorError = System.Drawing.Color.FromArgb(255, 45, 85);
            System.Drawing.Color colorBlack = System.Drawing.Color.FromArgb(5, 5, 5);
            
            System.Drawing.Font fontMain = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontTerminal = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontTerminalBig = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fontHeader = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);

            // Initialize Controls
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblSystemStatus = new System.Windows.Forms.Label();
            this.grpSystemControl = new System.Windows.Forms.GroupBox();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.grpAutomation = new System.Windows.Forms.GroupBox();
            this.btnAutoPost = new System.Windows.Forms.Button();
            this.btnGenerateContent = new System.Windows.Forms.Button();
            this.lblLastAction = new System.Windows.Forms.Label();
            this.grpAgentConsole = new System.Windows.Forms.GroupBox();
            this.txtAgentInput = new System.Windows.Forms.TextBox();
            this.btnExecuteAgent = new System.Windows.Forms.Button();
            this.lblAgentPrompt = new System.Windows.Forms.Label();
            this.txtConsoleLog = new System.Windows.Forms.RichTextBox();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.grpSystemControl.SuspendLayout();
            this.grpAutomation.SuspendLayout();
            this.grpAgentConsole.SuspendLayout();
            this.SuspendLayout();

            // MAIN FORM
            this.BackColor = colorBg;
            this.ForeColor = colorAccent;
            this.ClientSize = new System.Drawing.Size(2160, 1080);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Name = "MainForm";
            this.Text = "BLVDE_CONTENT_STUDIO // PRO_EDITION";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // LOGO & STATUS
            this.pictureBoxLogo.Location = new System.Drawing.Point(60, 45);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(180, 80);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            try { this.pictureBoxLogo.Image = System.Drawing.Image.FromFile("Resources/BlvdeLogo.png"); } catch { }

            this.lblSystemStatus.AutoSize = true;
            this.lblSystemStatus.Font = fontHeader;
            this.lblSystemStatus.ForeColor = colorAccent;
            this.lblSystemStatus.Location = new System.Drawing.Point(260, 60);
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Text = "/// BLVDE_CONTENT_ORCHESTRATOR [UPLINK_PRO]";

            // Button Styler
            void StyleBtn(System.Windows.Forms.Button btn, Color themeColor) {
                btn.BackColor = colorBlack;
                btn.ForeColor = themeColor;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = themeColor;
                btn.FlatAppearance.BorderSize = 1;
                btn.Font = fontTerminal;
                btn.Cursor = System.Windows.Forms.Cursors.Hand;
                btn.MouseEnter += (s, e) => { btn.BackColor = themeColor; btn.ForeColor = colorBlack; };
                btn.MouseLeave += (s, e) => { btn.BackColor = colorBlack; btn.ForeColor = themeColor; };
            }

            // GROUP: SYSTEM NODES (LEFT)
            this.grpSystemControl.Location = new System.Drawing.Point(60, 180);
            this.grpSystemControl.Size = new System.Drawing.Size(500, 450);
            this.grpSystemControl.Text = " [ SYSTEM_OPS ] ";
            this.grpSystemControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpSystemControl.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, this.grpSystemControl.ClientRectangle, colorAccent, ButtonBorderStyle.Solid); };

            this.btnShutdown.Location = new System.Drawing.Point(40, 60);
            this.btnShutdown.Size = new System.Drawing.Size(420, 70);
            this.btnShutdown.Text = "/// KILL_SYSTEM";
            StyleBtn(this.btnShutdown, colorError);
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);

            this.btnRestart.Location = new System.Drawing.Point(40, 150);
            this.btnRestart.Size = new System.Drawing.Size(420, 70);
            this.btnRestart.Text = "/// REBOOT_NODE";
            StyleBtn(this.btnRestart, colorAccent);
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);

            this.btnLock.Location = new System.Drawing.Point(40, 240);
            this.btnLock.Size = new System.Drawing.Size(420, 70);
            this.btnLock.Text = "/// SECURE_SESSION";
            StyleBtn(this.btnLock, colorSuccess);

            this.btnSettings.Location = new System.Drawing.Point(40, 330);
            this.btnSettings.Size = new System.Drawing.Size(420, 70);
            this.btnSettings.Text = ">> CONFIG_API_NODES";
            StyleBtn(this.btnSettings, colorViolet);
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            this.grpSystemControl.Controls.AddRange(new Control[] { btnShutdown, btnRestart, btnLock, btnSettings });

            // GROUP: TRANSMISSION (RIGHT)
            this.grpAutomation.Location = new System.Drawing.Point(600, 180);
            this.grpAutomation.Size = new System.Drawing.Size(1500, 450);
            this.grpAutomation.Text = " [ TRANSMISSION_SUITE ] ";
            this.grpAutomation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpAutomation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.grpAutomation.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, this.grpAutomation.ClientRectangle, colorViolet, ButtonBorderStyle.Solid); };

            this.btnAutoPost.Location = new System.Drawing.Point(50, 60);
            this.btnAutoPost.Size = new System.Drawing.Size(350, 350);
            this.btnAutoPost.Text = ">> INITIATE_UPLOAD_SEQUENCE";
            StyleBtn(this.btnAutoPost, colorViolet);
            this.btnAutoPost.Click += new System.EventHandler(this.btnAutoPost_Click);

            this.btnGenerateContent.Location = new System.Drawing.Point(440, 60);
            this.btnGenerateContent.Size = new System.Drawing.Size(350, 350);
            this.btnGenerateContent.Text = ">> COMPILE_SYNTHETIC_MEDIA";
            StyleBtn(this.btnGenerateContent, colorAccent);

            this.lblLastAction.AutoSize = true;
            this.lblLastAction.Font = fontHeader;
            this.lblLastAction.ForeColor = colorSuccess;
            this.lblLastAction.Location = new System.Drawing.Point(830, 60);
            this.lblLastAction.Text = "NETWORK_STATUS: STANDBY_";

            this.grpAutomation.Controls.AddRange(new Control[] { btnAutoPost, btnGenerateContent, lblLastAction });

            this.grpAgentConsole.Location = new System.Drawing.Point(60, 660);
            this.grpAgentConsole.Size = new System.Drawing.Size(2040, 350); // Filling the bottom
            this.grpAgentConsole.Text = " [ HYPER_TERMINAL ] ";
            this.grpAgentConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpAgentConsole.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.grpAgentConsole.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, this.grpAgentConsole.ClientRectangle, colorSuccess, ButtonBorderStyle.Solid); };

            this.txtConsoleLog.BackColor = colorBlack;
            this.txtConsoleLog.ForeColor = colorSuccess;
            this.txtConsoleLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConsoleLog.Font = fontTerminal;
            this.txtConsoleLog.Location = new System.Drawing.Point(30, 40);
            this.txtConsoleLog.Size = new System.Drawing.Size(1400, 240); // Leave room for Agent Prompt below if needed
            this.txtConsoleLog.ReadOnly = true;
            this.txtConsoleLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtConsoleLog.Text = "> INITIALIZING_WATCHDOG...\n> UPLINK_PRO_V11_READY.\n";

            this.lblAgentPrompt.Text = "admin@blvde:~$";
            this.lblAgentPrompt.Location = new System.Drawing.Point(1450, 40);
            this.lblAgentPrompt.AutoSize = true;

            this.txtAgentInput.Location = new System.Drawing.Point(1450, 80);
            this.txtAgentInput.Size = new System.Drawing.Size(560, 180);
            this.txtAgentInput.Multiline = true;
            this.txtAgentInput.BackColor = colorBlack;
            this.txtAgentInput.ForeColor = colorAccent;
            this.txtAgentInput.BorderStyle = BorderStyle.FixedSingle;

            this.btnExecuteAgent.Location = new System.Drawing.Point(1450, 270);
            this.btnExecuteAgent.Size = new System.Drawing.Size(560, 60);
            this.btnExecuteAgent.Text = "[ EXECUTE_COMMAND_OVERRIDE ]";
            StyleBtn(this.btnExecuteAgent, colorAccent);

            this.grpAgentConsole.Controls.AddRange(new Control[] { txtConsoleLog, lblAgentPrompt, txtAgentInput, btnExecuteAgent });

            // FINALIZE FORM
            this.Controls.AddRange(new Control[] { pictureBoxLogo, lblSystemStatus, grpSystemControl, grpAutomation, grpAgentConsole });
            
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.grpSystemControl.ResumeLayout(false);
            this.grpAutomation.ResumeLayout(false);
            this.grpAgentConsole.ResumeLayout(false);
            this.grpAgentConsole.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
