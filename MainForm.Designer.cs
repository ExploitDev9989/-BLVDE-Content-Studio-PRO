// ============================================================================
// BLVDE Content Studio - Modern UI Design
// ============================================================================
// Clean, professional design with intuitive layout
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
        private System.Windows.Forms.TextBox txtCaption; // NEW: Caption input

        private System.Windows.Forms.GroupBox grpAgentConsole;
        private System.Windows.Forms.TextBox txtAgentInput;
        private System.Windows.Forms.Button btnExecuteAgent;
        private System.Windows.Forms.Label lblAgentPrompt;
        
        // Console Log
        private System.Windows.Forms.RichTextBox txtConsoleLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // MODERN COLOR PALETTE
            System.Drawing.Color colorBg = System.Drawing.Color.FromArgb(245, 247, 250);     // Light gray-blue background
            System.Drawing.Color colorCard = System.Drawing.Color.White;                      // White cards
            System.Drawing.Color colorPrimary = System.Drawing.Color.FromArgb(99, 102, 241); // Modern indigo
            System.Drawing.Color colorSuccess = System.Drawing.Color.FromArgb(34, 197, 94);  // Green
            System.Drawing.Color colorWarning = System.Drawing.Color.FromArgb(251, 146, 60); // Orange
            System.Drawing.Color colorDanger = System.Drawing.Color.FromArgb(239, 68, 68);   // Red
            System.Drawing.Color colorText = System.Drawing.Color.FromArgb(30, 41, 59);      // Dark slate
            System.Drawing.Color colorTextLight = System.Drawing.Color.FromArgb(100, 116, 139); // Gray
            System.Drawing.Color colorBorder = System.Drawing.Color.FromArgb(226, 232, 240); // Light border
            
            System.Drawing.Font fontMain = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontButton = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fontHeader = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fontLog = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular);

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
            this.txtCaption = new System.Windows.Forms.TextBox(); // NEW
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
            this.ForeColor = colorText;
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Name = "MainForm";
            this.Text = "BLVDE Content Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font = fontMain;

            // LOGO & HEADER
            this.pictureBoxLogo.Location = new System.Drawing.Point(40, 30);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(150, 60);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            try { this.pictureBoxLogo.Image = System.Drawing.Image.FromFile("Resources/BlvdeLogo.png"); } catch { }

            this.lblSystemStatus.AutoSize = true;
            this.lblSystemStatus.Font = fontHeader;
            this.lblSystemStatus.ForeColor = colorText;
            this.lblSystemStatus.Location = new System.Drawing.Point(210, 45);
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Text = "Content Studio";

            // Modern Button Styler
            void StyleBtn(System.Windows.Forms.Button btn, System.Drawing.Color themeColor, bool isPrimary = false) {
                if (isPrimary) {
                    btn.BackColor = themeColor;
                    btn.ForeColor = System.Drawing.Color.White;
                } else {
                    btn.BackColor = colorCard;
                    btn.ForeColor = themeColor;
                }
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = isPrimary ? themeColor : colorBorder;
                btn.FlatAppearance.BorderSize = 1;
                btn.Font = fontButton;
                btn.Cursor = System.Windows.Forms.Cursors.Hand;
                
                var origBg = btn.BackColor;
                var origFg = btn.ForeColor;
                
                btn.MouseEnter += (s, e) => {
                    if (isPrimary) {
                        btn.BackColor = System.Drawing.Color.FromArgb(
                            Math.Max(0, themeColor.R - 20),
                            Math.Max(0, themeColor.G - 20),
                            Math.Max(0, themeColor.B - 20)
                        );
                    } else {
                        btn.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
                    }
                };
                btn.MouseLeave += (s, e) => {
                    btn.BackColor = origBg;
                };
            }

            // GROUP: SYSTEM CONTROL (Left sidebar)
            this.grpSystemControl.Location = new System.Drawing.Point(40, 130);
            this.grpSystemControl.Size = new System.Drawing.Size(280, 720);
            this.grpSystemControl.Text = "System Controls";
            this.grpSystemControl.BackColor = colorCard;
            this.grpSystemControl.ForeColor = colorText;
            this.grpSystemControl.Font = fontButton;

            this.btnSettings.Location = new System.Drawing.Point(20, 40);
            this.btnSettings.Size = new System.Drawing.Size(240, 50);
            this.btnSettings.Text = "⚙️ Settings";
            StyleBtn(this.btnSettings, colorPrimary);
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            this.btnLock.Location = new System.Drawing.Point(20, 120);
            this.btnLock.Size = new System.Drawing.Size(240, 50);
            this.btnLock.Text = "🔒 Lock Session";
            StyleBtn(this.btnLock, colorTextLight);

            this.btnRestart.Location = new System.Drawing.Point(20, 200);
            this.btnRestart.Size = new System.Drawing.Size(240, 50);
            this.btnRestart.Text = "🔄 Restart System";
            StyleBtn(this.btnRestart, colorWarning);
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);

            this.btnShutdown.Location = new System.Drawing.Point(20, 280);
            this.btnShutdown.Size = new System.Drawing.Size(240, 50);
            this.btnShutdown.Text = "⚠️ Shutdown";
            StyleBtn(this.btnShutdown, colorDanger);
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);

            this.grpSystemControl.Controls.AddRange(new Control[] { btnSettings, btnLock, btnRestart, btnShutdown });

            // GROUP: AUTOMATION (Main content area)
            this.grpAutomation.Location = new System.Drawing.Point(340, 130);
            this.grpAutomation.Size = new System.Drawing.Size(1020, 400);
            this.grpAutomation.Text = "Upload Content";
            this.grpAutomation.BackColor = colorCard;
            this.grpAutomation.ForeColor = colorText;
            this.grpAutomation.Font = fontButton;

            // CAPTION INPUT (NEW!)
            this.txtCaption.Location = new System.Drawing.Point(30, 40);
            this.txtCaption.Size = new System.Drawing.Size(960, 120);
            this.txtCaption.Multiline = true;
            this.txtCaption.BackColor = colorCard;
            this.txtCaption.ForeColor = colorText;
            this.txtCaption.Font = fontMain;
            this.txtCaption.BorderStyle = BorderStyle.FixedSingle;
            this.txtCaption.Text = "Enter your video caption here...";
            this.txtCaption.ScrollBars = ScrollBars.Vertical;
            
            // Placeholder behavior
            this.txtCaption.GotFocus += (s, e) => {
                if (this.txtCaption.Text == "Enter your video caption here...") {
                    this.txtCaption.Text = "";
                    this.txtCaption.ForeColor = colorText;
                }
            };
            this.txtCaption.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(this.txtCaption.Text)) {
                    this.txtCaption.Text = "Enter your video caption here...";
                    this.txtCaption.ForeColor = colorTextLight;
                }
            };
            this.txtCaption.ForeColor = colorTextLight;

            this.lblLastAction.AutoSize = true;
            this.lblLastAction.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.lblLastAction.ForeColor = colorTextLight;
            this.lblLastAction.Location = new System.Drawing.Point(30, 180);
            this.lblLastAction.Text = "Status: Ready";

            this.btnAutoPost.Location = new System.Drawing.Point(30, 230);
            this.btnAutoPost.Size = new System.Drawing.Size(460, 130);
            this.btnAutoPost.Text = "🚀 Upload to All Platforms";
            StyleBtn(this.btnAutoPost, colorPrimary, true);
            this.btnAutoPost.Click += new System.EventHandler(this.btnAutoPost_Click);

            this.btnGenerateContent.Location = new System.Drawing.Point(530, 230);
            this.btnGenerateContent.Size = new System.Drawing.Size(470, 140);
            this.btnGenerateContent.Text = "✨ Generate Test Content";
            StyleBtn(this.btnGenerateContent, colorSuccess);

            this.grpAutomation.Controls.AddRange(new Control[] { txtCaption, lblLastAction, btnAutoPost, btnGenerateContent });

            // GROUP: CONSOLE (Bottom)
            this.grpAgentConsole.Location = new System.Drawing.Point(340, 550);
            this.grpAgentConsole.Size = new System.Drawing.Size(1020, 300);
            this.grpAgentConsole.Text = "Activity Log";
            this.grpAgentConsole.BackColor = colorCard;
            this.grpAgentConsole.ForeColor = colorText;
            this.grpAgentConsole.Font = fontButton;

            this.txtConsoleLog.BackColor = System.Drawing.Color.FromArgb(15, 23, 42); // Dark slate
            this.txtConsoleLog.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); // Light slate
            this.txtConsoleLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConsoleLog.Font = fontLog;
            this.txtConsoleLog.Location = new System.Drawing.Point(20, 35);
            this.txtConsoleLog.Size = new System.Drawing.Size(700, 240);
            this.txtConsoleLog.ReadOnly = true;
            this.txtConsoleLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtConsoleLog.Text = "> BLVDE Content Studio v12\n> Ready to upload content.\n";

            this.lblAgentPrompt.Text = "Command:";
            this.lblAgentPrompt.Location = new System.Drawing.Point(740, 35);
            this.lblAgentPrompt.AutoSize = true;
            this.lblAgentPrompt.ForeColor = colorTextLight;

            this.txtAgentInput.Location = new System.Drawing.Point(740, 60);
            this.txtAgentInput.Size = new System.Drawing.Size(260, 150);
            this.txtAgentInput.Multiline = true;
            this.txtAgentInput.BackColor = colorCard;
            this.txtAgentInput.ForeColor = colorText;
            this.txtAgentInput.BorderStyle = BorderStyle.FixedSingle;
            this.txtAgentInput.Font = fontMain;

            this.btnExecuteAgent.Location = new System.Drawing.Point(740, 225);
            this.btnExecuteAgent.Size = new System.Drawing.Size(260, 50);
            this.btnExecuteAgent.Text = "Execute";
            StyleBtn(this.btnExecuteAgent, colorPrimary);

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
