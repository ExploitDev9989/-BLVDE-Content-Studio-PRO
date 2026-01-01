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
        private System.Windows.Forms.Button btnLoadVideo;  // Load Video button
        private System.Windows.Forms.Label lblLastAction;
        private System.Windows.Forms.TextBox txtCaption;
        private System.Windows.Forms.TextBox txtNotes;     // Notes textbox
        private System.Windows.Forms.TextBox txtPrompts;   // Prompts textbox

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

        // ====================================================================
        // TAB CONTROL - Main navigation tabs
        // ====================================================================
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabLoadMedia;   // NEW: Load Media tab
        private System.Windows.Forms.TabPage tabUpload;
        private System.Windows.Forms.TabPage tabNotes;       // NEW: Notes tab
        private System.Windows.Forms.TabPage tabPrompts;     // NEW: Prompts tab
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TabPage tabSystem;
        private System.Windows.Forms.TabPage tabConsole;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // ====================================================================
            // COLOR PALETTE - Modify these RGB values to change the theme colors
            // ====================================================================
            System.Drawing.Color colorBg = System.Drawing.Color.FromArgb(245, 247, 250);     // Light gray-blue background
            System.Drawing.Color colorCard = System.Drawing.Color.White;                      // White cards
            System.Drawing.Color colorPrimary = System.Drawing.Color.FromArgb(99, 102, 241); // Modern indigo
            System.Drawing.Color colorSuccess = System.Drawing.Color.FromArgb(34, 197, 94);  // Green
            System.Drawing.Color colorWarning = System.Drawing.Color.FromArgb(251, 146, 60); // Orange
            System.Drawing.Color colorDanger = System.Drawing.Color.FromArgb(239, 68, 68);   // Red
            System.Drawing.Color colorText = System.Drawing.Color.FromArgb(30, 41, 59);      // Dark slate
            System.Drawing.Color colorTextLight = System.Drawing.Color.FromArgb(100, 116, 139); // Gray
            System.Drawing.Color colorBorder = System.Drawing.Color.FromArgb(226, 232, 240); // Light border
            
            // ====================================================================
            // FONT DEFINITIONS - Change these to modify fonts throughout the app
            // ====================================================================
            System.Drawing.Font fontMain = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontButton = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fontHeader = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fontLog = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular);
            System.Drawing.Font fontTabHeader = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);

            // Initialize Tab Controls
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabLoadMedia = new System.Windows.Forms.TabPage();
            this.tabUpload = new System.Windows.Forms.TabPage();
            this.tabNotes = new System.Windows.Forms.TabPage();
            this.tabPrompts = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tabSystem = new System.Windows.Forms.TabPage();
            this.tabConsole = new System.Windows.Forms.TabPage();

            // Initialize Other Controls
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
            this.btnLoadVideo = new System.Windows.Forms.Button();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtPrompts = new System.Windows.Forms.TextBox();
            this.lblLastAction = new System.Windows.Forms.Label();
            this.grpAgentConsole = new System.Windows.Forms.GroupBox();
            this.txtAgentInput = new System.Windows.Forms.TextBox();
            this.btnExecuteAgent = new System.Windows.Forms.Button();
            this.lblAgentPrompt = new System.Windows.Forms.Label();
            this.txtConsoleLog = new System.Windows.Forms.RichTextBox();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.tabLoadMedia.SuspendLayout();
            this.tabUpload.SuspendLayout();
            this.tabNotes.SuspendLayout();
            this.tabPrompts.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabSystem.SuspendLayout();
            this.tabConsole.SuspendLayout();
            this.SuspendLayout();

            // ====================================================================
            // MAIN FORM SETTINGS - Window size, colors, title
            // Change ClientSize to modify window dimensions (Width, Height)
            // ====================================================================
            this.BackColor = colorBg;
            this.ForeColor = colorText;
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Name = "MainForm";
            this.Text = "BLVDE Content Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font = fontMain;

            // ====================================================================
            // LOGO & HEADER - Top left branding area
            // Position: X=40, Y=30 | Size: 150x60
            // ====================================================================
            this.pictureBoxLogo.Location = new System.Drawing.Point(40, 30);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(150, 60);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            try { this.pictureBoxLogo.Image = System.Drawing.Image.FromFile("Resources/BlvdeLogo.png"); } catch { }

            // HEADER LABEL - "Content Studio" text next to logo
            // Position: X=210, Y=45
            this.lblSystemStatus.AutoSize = true;
            this.lblSystemStatus.Font = fontHeader;
            this.lblSystemStatus.ForeColor = colorText;
            this.lblSystemStatus.Location = new System.Drawing.Point(210, 45);
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Text = "Content Studio";

            // ====================================================================
            // BUTTON STYLING FUNCTION - Applies consistent styling to all buttons
            // ====================================================================
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
                
                // Hover effect - darkens primary buttons, lightens secondary buttons
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

            // ====================================================================
            // TAB CONTROL - Main navigation container
            // Position: X=40, Y=130 | Size: Width=1320px, Height=720px
            // ====================================================================
            this.mainTabControl.Location = new System.Drawing.Point(40, 130);
            this.mainTabControl.Size = new System.Drawing.Size(1320, 720);
            this.mainTabControl.Font = fontTabHeader;
            this.mainTabControl.SelectedIndex = 0;  // Default to first tab (Load Media)
            
            // ====================================================================
            // TAB 0: LOAD MEDIA - Load video files or link files
            // ====================================================================
            this.tabLoadMedia.Text = "📂 Load Media";
            this.tabLoadMedia.BackColor = colorCard;
            this.tabLoadMedia.Padding = new Padding(20);
            
            // LOAD VIDEO BUTTON - Big prominent button to load media files
            // Position: X=100, Y=100 | Size: Width=1100px, Height=400px
            this.btnLoadVideo = new System.Windows.Forms.Button();
            this.btnLoadVideo.Location = new System.Drawing.Point(100, 100);
            this.btnLoadVideo.Size = new System.Drawing.Size(1100, 400);
            this.btnLoadVideo.Text = "🛰️ LOAD MEDIA PACKET\n\nClick to select video file (.mp4, .mov) or link file (.txt)";
            this.btnLoadVideo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.btnLoadVideo.BackColor = System.Drawing.Color.FromArgb(5, 5, 5);
            this.btnLoadVideo.ForeColor = System.Drawing.Color.FromArgb(0, 210, 255); // Cyan
            this.btnLoadVideo.FlatStyle = FlatStyle.Flat;
            this.btnLoadVideo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 210, 255);
            this.btnLoadVideo.FlatAppearance.BorderSize = 2;
            this.btnLoadVideo.Cursor = Cursors.Hand;
            
            // Hover effects for Load Video button
            this.btnLoadVideo.MouseEnter += (s, e) => { 
                this.btnLoadVideo.BackColor = System.Drawing.Color.FromArgb(0, 210, 255); 
                this.btnLoadVideo.ForeColor = Color.Black; 
            };
            this.btnLoadVideo.MouseLeave += (s, e) => { 
                this.btnLoadVideo.BackColor = System.Drawing.Color.FromArgb(5, 5, 5); 
                this.btnLoadVideo.ForeColor = System.Drawing.Color.FromArgb(0, 210, 255); 
            };
            
            // Info label
            Label lblLoadInfo = new Label();
            lblLoadInfo.Location = new System.Drawing.Point(100, 520);
            lblLoadInfo.Size = new System.Drawing.Size(800, 100);
            lblLoadInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblLoadInfo.ForeColor = colorTextLight;
            lblLoadInfo.Text = "Supported formats:\n" +
                               "• Video files: .mp4, .mov\n" +
                               "• Link files: .txt containing a public video URL (Google Drive, Dropbox, direct links)";
            
            // OPEN GOOGLE DRIVE BUTTON - Quick access to Google Drive
            // Position: X=930, Y=520 | Size: Width=270px, Height=100px
            Button btnOpenGDrive = new Button();
            btnOpenGDrive.Location = new System.Drawing.Point(930, 520);
            btnOpenGDrive.Size = new System.Drawing.Size(270, 100);
            btnOpenGDrive.Text = "📁 Open\nGoogle Drive";
            btnOpenGDrive.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            btnOpenGDrive.BackColor = colorPrimary;
            btnOpenGDrive.ForeColor = Color.White;
            btnOpenGDrive.FlatStyle = FlatStyle.Flat;
            btnOpenGDrive.FlatAppearance.BorderSize = 0;
            btnOpenGDrive.Cursor = Cursors.Hand;
            
            // Hover effect for Google Drive button
            btnOpenGDrive.MouseEnter += (s, e) => {
                btnOpenGDrive.BackColor = Color.FromArgb(
                    Math.Max(0, colorPrimary.R - 20),
                    Math.Max(0, colorPrimary.G - 20),
                    Math.Max(0, colorPrimary.B - 20)
                );
            };
            btnOpenGDrive.MouseLeave += (s, e) => {
                btnOpenGDrive.BackColor = colorPrimary;
            };
            
            // Open Google Drive in browser
            btnOpenGDrive.Click += (s, e) => {
                try {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
                        FileName = "https://drive.google.com/drive/my-drive",
                        UseShellExecute = true
                    });
                } catch { }
            };
            
            this.tabLoadMedia.Controls.AddRange(new Control[] { btnLoadVideo, lblLoadInfo, btnOpenGDrive });
            
            // ====================================================================
            // TAB 1: UPLOAD - Content upload and posting
            // ====================================================================
            this.tabUpload.Text = "📤 Upload";
            this.tabUpload.BackColor = colorCard;
            this.tabUpload.Padding = new Padding(20);
            
            // CAPTION INPUT BOX - Where users enter video description
            // Position: X=30, Y=30 | Size: Width=1240px, Height=140px
            this.txtCaption.Location = new System.Drawing.Point(30, 30);
            this.txtCaption.Size = new System.Drawing.Size(1240, 140);
            this.txtCaption.Multiline = true;
            this.txtCaption.BackColor = colorCard;
            this.txtCaption.ForeColor = colorText;
            this.txtCaption.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCaption.BorderStyle = BorderStyle.FixedSingle;
            this.txtCaption.Text = "Enter your video caption here...";
            this.txtCaption.ScrollBars = ScrollBars.Vertical;
            
            // Placeholder behavior for caption box
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

            // STATUS LABEL - Shows current operation status
            // Position: X=30, Y=190
            this.lblLastAction.AutoSize = true;
            this.lblLastAction.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.lblLastAction.ForeColor = colorTextLight;
            this.lblLastAction.Location = new System.Drawing.Point(30, 190);
            this.lblLastAction.Text = "Status: Ready";

            // UPLOAD BUTTON - Main action button to post content
            // Position: X=30, Y=240 | Size: Width=600px, Height=180px
            this.btnAutoPost.Location = new System.Drawing.Point(30, 240);
            this.btnAutoPost.Size = new System.Drawing.Size(600, 180);
            this.btnAutoPost.Text = "� Upload to All Platforms";
            this.btnAutoPost.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            StyleBtn(this.btnAutoPost, colorPrimary, true);
            this.btnAutoPost.Click += new System.EventHandler(this.btnAutoPost_Click);

            // GENERATE TEST BUTTON - Creates dummy content for testing
            // Position: X=670, Y=240 | Size: Width=600px, Height=180px
            this.btnGenerateContent.Location = new System.Drawing.Point(670, 240);
            this.btnGenerateContent.Size = new System.Drawing.Size(600, 180);
            this.btnGenerateContent.Text = "✨ Generate Test Content";
            this.btnGenerateContent.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            StyleBtn(this.btnGenerateContent, colorSuccess);
            this.btnGenerateContent.Click += new System.EventHandler(this.btnGenerateContent_Click);

            this.tabUpload.Controls.AddRange(new Control[] { txtCaption, lblLastAction, btnAutoPost, btnGenerateContent });

            // ====================================================================
            // TAB 2: NOTES - Notepad for general notes
            // ====================================================================
            this.tabNotes.Text = "📝 Notes";
            this.tabNotes.BackColor = colorCard;
            this.tabNotes.Padding = new Padding(20);
            
            // NOTES TEXTBOX - Large area for taking notes
            // Position: X=30, Y=30 | Size: Width=1240px, Height=620px
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtNotes.Location = new System.Drawing.Point(30, 30);
            this.txtNotes.Size = new System.Drawing.Size(1240, 620);
            this.txtNotes.Multiline = true;
            this.txtNotes.BackColor = colorCard;
            this.txtNotes.ForeColor = colorText;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNotes.BorderStyle = BorderStyle.FixedSingle;
            this.txtNotes.ScrollBars = ScrollBars.Vertical;
            this.txtNotes.Text = "Your notes here...\n\n";
            
            this.tabNotes.Controls.Add(txtNotes);

            // ====================================================================
            // TAB 3: PROMPTS - Storage for video prompts and templates
            // ====================================================================
            this.tabPrompts.Text = "💡 Prompts";
            this.tabPrompts.BackColor = colorCard;
            this.tabPrompts.Padding = new Padding(20);
            
            // PROMPTS TEXTBOX - Large area for storing prompts
            // Position: X=30, Y=30 | Size: Width=1240px, Height=620px
            this.txtPrompts = new System.Windows.Forms.TextBox();
            this.txtPrompts.Location = new System.Drawing.Point(30, 30);
            this.txtPrompts.Size = new System.Drawing.Size(1240, 620);
            this.txtPrompts.Multiline = true;
            this.txtPrompts.BackColor = colorCard;
            this.txtPrompts.ForeColor = colorText;
            this.txtPrompts.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtPrompts.BorderStyle = BorderStyle.FixedSingle;
            this.txtPrompts.ScrollBars = ScrollBars.Vertical;
            this.txtPrompts.Text = "Your video prompts and templates...\n\n" +
                                   "Example:\n" +
                                   "- Liminal space POV walking\n" +
                                   "- Dark aesthetic cityscape\n" +
                                   "- Cinematic tension builder\n";
            
            this.tabPrompts.Controls.Add(txtPrompts);

            // ====================================================================
            // TAB 4: SETTINGS - API configuration
            // ====================================================================
            this.tabSettings.Text = "⚙️ Settings";
            this.tabSettings.BackColor = colorCard;
            this.tabSettings.Padding = new Padding(20);
            
            // SETTINGS BUTTON - Opens configuration dialog
            // Position: X=30, Y=30 | Size: Width=400px, Height=80px
            this.btnSettings.Location = new System.Drawing.Point(30, 30);
            this.btnSettings.Size = new System.Drawing.Size(400, 80);
            this.btnSettings.Text = "⚙️ Configure API Keys & Account IDs";
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            StyleBtn(this.btnSettings, colorPrimary, true);
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            
            
            // Settings info label
            Label lblSettingsInfo = new Label();
            lblSettingsInfo.Location = new System.Drawing.Point(30, 140);
            lblSettingsInfo.Size = new System.Drawing.Size(1200, 120);
            lblSettingsInfo.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblSettingsInfo.ForeColor = colorTextLight;
            lblSettingsInfo.Text = "Configure your Blotato API credentials and platform account IDs here.\n\n" +
                                   "You'll need:\n" +
                                   "• Blotato API Key\n" +
                                   "• TikTok Account ID (numeric)\n" +
                                   "• Instagram Account ID (numeric)\n" +
                                   "• YouTube Account ID (numeric)";
            
            // PRIVACY SECTION - For streaming protection
            Label lblPrivacyHeader = new Label();
            lblPrivacyHeader.Location = new System.Drawing.Point(30, 290);
            lblPrivacyHeader.Size = new System.Drawing.Size(600, 30);
            lblPrivacyHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            lblPrivacyHeader.ForeColor = colorText;
            lblPrivacyHeader.Text = "🔒 Privacy & Streaming Protection";
            
            // HIDE API KEYS CHECKBOX - Masks credentials when checked
            // Position: X=30, Y=340 | Size: Auto
            CheckBox chkHideApiKeys = new CheckBox();
            chkHideApiKeys.Name = "chkHideApiKeys";
            chkHideApiKeys.Location = new System.Drawing.Point(30, 340);
            chkHideApiKeys.Size = new System.Drawing.Size(600, 30);
            chkHideApiKeys.Font = new System.Drawing.Font("Segoe UI", 12F);
            chkHideApiKeys.ForeColor = colorText;
            chkHideApiKeys.Text = "Hide API Keys (Streaming Mode)";
            chkHideApiKeys.Checked = false;
            
            
            // Privacy info
            Label lblPrivacyInfo = new Label();
            lblPrivacyInfo.Location = new System.Drawing.Point(50, 380);
            lblPrivacyInfo.Size = new System.Drawing.Size(1200, 60);
            lblPrivacyInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblPrivacyInfo.ForeColor = colorTextLight;
            lblPrivacyInfo.Text = "When enabled, your API keys and account IDs will be masked with ••••• in the configuration dialog.\n" +
                                   "This prevents accidentally showing sensitive credentials during screen sharing or streaming.";
            
            // APPEARANCE SECTION - Theme customization
            Label lblAppearanceHeader = new Label();
            lblAppearanceHeader.Location = new System.Drawing.Point(30, 470);
            lblAppearanceHeader.Size = new System.Drawing.Size(600, 30);
            lblAppearanceHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            lblAppearanceHeader.ForeColor = colorText;
            lblAppearanceHeader.Text = "🎨 Appearance";
            
            // DARK MODE CHECKBOX - Switches to dark theme
            // Position: X=30, Y=520 | Size: Auto
            CheckBox chkDarkMode = new CheckBox();
            chkDarkMode.Name = "chkDarkMode";
            chkDarkMode.Location = new System.Drawing.Point(30, 520);
            chkDarkMode.Size = new System.Drawing.Size(600, 30);
            chkDarkMode.Font = new System.Drawing.Font("Segoe UI", 12F);
            chkDarkMode.ForeColor = colorText;
            chkDarkMode.Text = "🌙 Enable Dark Mode";
            chkDarkMode.Checked = false;  // Load from config later
            
            // Dark mode info
            Label lblDarkModeInfo = new Label();
            lblDarkModeInfo.Location = new System.Drawing.Point(50, 560);
            lblDarkModeInfo.Size = new System.Drawing.Size(1200, 40);
            lblDarkModeInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblDarkModeInfo.ForeColor = colorTextLight;
            lblDarkModeInfo.Text = "Switch between light and dark color schemes. Dark mode uses darker backgrounds\n" +
                                    "and lighter text for comfortable viewing in low-light environments.";
            
            // Wire up dark mode toggle event
            chkDarkMode.CheckedChanged += (s, e) => {
                // Will implement theme switching logic
                ApplyTheme(chkDarkMode.Checked);
            };
            
            // Store checkbox reference for access in MainForm.cs
            this.Controls.Add(chkHideApiKeys);
            this.Controls.Add(chkDarkMode);
            chkHideApiKeys.BringToFront();
            chkDarkMode.BringToFront();
            
            this.tabSettings.Controls.AddRange(new Control[] { 
                btnSettings, lblSettingsInfo, 
                lblPrivacyHeader, chkHideApiKeys, lblPrivacyInfo,
                lblAppearanceHeader, chkDarkMode, lblDarkModeInfo
            });

            // ====================================================================
            // TAB 5: SYSTEM - System control buttons
            // ====================================================================
            this.tabSystem.Text = "🖥️ System";
            this.tabSystem.BackColor = colorCard;
            this.tabSystem.Padding = new Padding(20);

            // LOCK BUTTON - Position: X=30, Y=30 | Size: 400x70
            this.btnLock.Location = new System.Drawing.Point(30, 30);
            this.btnLock.Size = new System.Drawing.Size(400, 70);
            this.btnLock.Text = "🔒 Lock Session";
            this.btnLock.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            StyleBtn(this.btnLock, colorTextLight);
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);

            // RESTART BUTTON - Position: X=30, Y=130 | Size: 400x70
            this.btnRestart.Location = new System.Drawing.Point(30, 130);
            this.btnRestart.Size = new System.Drawing.Size(400, 70);
            this.btnRestart.Text = "� Restart System";
            this.btnRestart.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            StyleBtn(this.btnRestart, colorWarning);
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);

            // SHUTDOWN BUTTON - Position: X=30, Y=230 | Size: 400x70
            this.btnShutdown.Location = new System.Drawing.Point(30, 230);
            this.btnShutdown.Size = new System.Drawing.Size(400, 70);
            this.btnShutdown.Text = "⚠️ Shutdown System";
            this.btnShutdown.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            StyleBtn(this.btnShutdown, colorDanger);
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);

            // Warning label
            Label lblSystemWarning = new Label();
            lblSystemWarning.Location = new System.Drawing.Point(30, 330);
            lblSystemWarning.Size = new System.Drawing.Size(1200, 50);
            lblSystemWarning.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSystemWarning.ForeColor = colorDanger;
            lblSystemWarning.Text = "⚠️ Warning: System controls will affect your computer. Use with caution.";
            
            this.tabSystem.Controls.AddRange(new Control[] { btnLock, btnRestart, btnShutdown, lblSystemWarning });

            // ====================================================================
            // TAB 5: CONSOLE - Activity log and command input
            // ====================================================================
            this.tabConsole.Text = "📊 Console";
            this.tabConsole.BackColor = colorCard;
            this.tabConsole.Padding = new Padding(20);

            // CONSOLE LOG BOX - Shows activity and upload logs
            // Position: X=20, Y=20 | Size: Width=900px, Height=620px
            this.txtConsoleLog.BackColor = System.Drawing.Color.FromArgb(15, 23, 42); // Dark slate
            this.txtConsoleLog.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); // Light slate
            this.txtConsoleLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsoleLog.Font = fontLog;
            this.txtConsoleLog.Location = new System.Drawing.Point(20, 20);
            this.txtConsoleLog.Size = new System.Drawing.Size(900, 620);
            this.txtConsoleLog.ReadOnly = true;
            this.txtConsoleLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtConsoleLog.Text = "> BLVDE Content Studio v12\n> Ready to upload content.\n";

            // COMMAND INPUT SECTION
            // Label - Position: X=940, Y=20
            this.lblAgentPrompt.Text = "Command Input:";
            this.lblAgentPrompt.Location = new System.Drawing.Point(940, 20);
            this.lblAgentPrompt.AutoSize = true;
            this.lblAgentPrompt.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblAgentPrompt.ForeColor = colorText;

            // Text input box - Position: X=940, Y=55 | Size: Width=340px, Height=480px
            this.txtAgentInput.Location = new System.Drawing.Point(940, 55);
            this.txtAgentInput.Size = new System.Drawing.Size(340, 480);
            this.txtAgentInput.Multiline = true;
            this.txtAgentInput.BackColor = colorCard;
            this.txtAgentInput.ForeColor = colorText;
            this.txtAgentInput.BorderStyle = BorderStyle.FixedSingle;
            this.txtAgentInput.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtAgentInput.Text = "awaiting_instruction...";
            this.txtAgentInput.ScrollBars = ScrollBars.Vertical;

            // Execute button - Position: X=940, Y=555 | Size: Width=340px, Height=85px
            this.btnExecuteAgent.Location = new System.Drawing.Point(940, 555);
            this.btnExecuteAgent.Size = new System.Drawing.Size(340, 85);
            this.btnExecuteAgent.Text = "▶ Execute Command";
            this.btnExecuteAgent.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            StyleBtn(this.btnExecuteAgent, colorPrimary, true);
            
            this.tabConsole.Controls.AddRange(new Control[] { txtConsoleLog, lblAgentPrompt, txtAgentInput, btnExecuteAgent });

            // ====================================================================
            // ADD TABS TO TAB CONTROL
            // Order: Load Media → Upload → Notes → Prompts → Settings → System → Console
            // ====================================================================
            this.mainTabControl.Controls.AddRange(new Control[] { 
                tabLoadMedia, tabUpload, tabNotes, tabPrompts, tabSettings, tabSystem, tabConsole 
            });
            
            // ====================================================================
            // ADD CONTROLS TO MAIN FORM
            // ====================================================================
            this.Controls.AddRange(new Control[] { pictureBoxLogo, lblSystemStatus, mainTabControl });
            
            // Finalize layout
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.tabLoadMedia.ResumeLayout(false);
            this.tabUpload.ResumeLayout(false);
            this.tabUpload.PerformLayout();
            this.tabNotes.ResumeLayout(false);
            this.tabPrompts.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSystem.ResumeLayout(false);
            this.tabConsole.ResumeLayout(false);
            this.tabConsole.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
