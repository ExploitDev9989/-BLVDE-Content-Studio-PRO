using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BLVDEContentStudio
{
    public partial class MainForm : Form
    {
        // ====================================================================
        // COLOR DEFINITIONS - Change these RGB values to modify theme colors
        // ====================================================================
        private readonly Color colRed = Color.FromArgb(239, 68, 68);         // Error/Warning color
        private readonly Color colGreen = Color.FromArgb(34, 197, 94);       // Success/Online color
        private readonly Color colCyan = Color.FromArgb(99, 102, 241);       // Accent/Highlight color
        private readonly Color colYellow = Color.FromArgb(251, 146, 60);     // Processing/Busy color

        // ====================================================================
        // CONFIGURATION VARIABLES - API keys and user preferences
        // ====================================================================
        private string _apiKey = "";             // Blotato API authentication key
        private string _tiktokId = "";           // TikTok account ID
        private string _instaId = "";            // Instagram account ID
        private string _youtubeId = "";          // YouTube account ID
        
        // ====================================================================
        // WINDOW SIZE SETTINGS - Main application window dimensions
        // Change these values to adjust default window size
        // ====================================================================
        private int _prefWidth = 1920;           // Window width in pixels (default)
        private int _prefHeight = 1080;           // Window height in pixels (default)
        private const string CONFIG_FILE = "blotato_config.json";

        private readonly string PATH_PENDING = Path.Combine(Application.StartupPath, "Content", "Pending");
        private readonly string PATH_POSTED = Path.Combine(Application.StartupPath, "Content", "Posted");

        public MainForm()
        {
            InitializeComponent();
            SetupCustomControls();
            LoadConfiguration();
            EnsureDirectories();
            MessageBox.Show("SYSTEM_UPDATE_COMPLETE: VERSION_2.0\nLOAD_VIDEO_PROTOCOL_READY.", "BOOT_SEQUENCE");
        }

        private void SetupCustomControls()
        {
            // ====================================================================
            // WIRE UP EVENT HANDLERS - Connect buttons to their click events
            // ====================================================================
            // Load Video button is now defined in Designer.cs
            btnLoadVideo.Click += btnLoadVideo_Click;
            
            // Remote Timer
            System.Windows.Forms.Timer remoteTimer = new System.Windows.Forms.Timer() { Interval = 1000 };
            remoteTimer.Tick += RemoteTimer_Tick;
            remoteTimer.Start();
            
            btnExecuteAgent.Click += btnExecuteAgent_Click;
        }

        // ====================================================================
        // CONSOLE LOG FUNCTION - Writes messages to the terminal/console box
        // ====================================================================
        private void Log(string message, Color? color = null)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Log(message, color)));
                return;
            }
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            
            // DEFAULT LOG TEXT COLOR - RGB(0, 255, 127) - Bright green
            // Change this to modify the default console message color
            Color logColor = color ?? Color.FromArgb(0, 255, 127);

            // CONSOLE LOG OUTPUT FORMATTING
            txtConsoleLog.SelectionStart = txtConsoleLog.TextLength;
            txtConsoleLog.SelectionLength = 0;
            
            // TIMESTAMP COLOR - Gray for all timestamps
            txtConsoleLog.SelectionColor = Color.Gray;
            txtConsoleLog.AppendText($"[{timestamp}] ");
            
            // MESSAGE COLOR - Uses the color specified in the Log() call
            txtConsoleLog.SelectionColor = logColor;
            txtConsoleLog.AppendText($"{message}{Environment.NewLine}");
            
            txtConsoleLog.SelectionStart = txtConsoleLog.Text.Length;
            txtConsoleLog.ScrollToCaret();
        }

        private void RemoteTimer_Tick(object sender, EventArgs e)
        {
            string cmdFile = Path.Combine(Application.StartupPath, "remote_command.txt");
            if (File.Exists(cmdFile))
            {
                try
                {
                    string command = File.ReadAllText(cmdFile).Trim();
                    // Delete immediately to prevent duplicate execution
                    File.Delete(cmdFile);
                    
                    Log($"CMD_RECIEVED: {command}");
                    
                    if (command == "POST_ALL")
                    {
                        if (btnAutoPost.Enabled)
                        {
                            btnAutoPost.PerformClick();
                        }
                        else
                        {
                            Log("ERROR: SYSTEM_BUSY. COMMAND IGNORED.");
                        }
                    }
                    else
                    {
                         Log($"UNKNOWN_COMMAND: {command}");
                    }
                }
                catch (Exception ex)
                {
                    Log($"CMD_READ_ERROR: {ex.Message}");
                }
            }
        }

        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                openFileDialog.Filter = "Verified Links (*.txt)|*.txt|Video Files (*.mp4;*.mov)|*.mp4;*.mov|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    string destFile = Path.Combine(PATH_PENDING, Path.GetFileName(selectedFile));
                    
                    try 
                    {
                        // ERASE OLD PACKETS so we don't accidentally send the wrong one
                        Array.ForEach(Directory.GetFiles(PATH_PENDING), File.Delete);

                        if (selectedFile.EndsWith(".txt"))
                        {
                            // Robust Read: Allow reading even if file is open in Notepad/Word
                            using (var fs = new FileStream(selectedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (var sr = new StreamReader(fs))
                            {
                                string content = sr.ReadToEnd();
                                File.WriteAllText(destFile, content); // Write to Pending folder
                            }
                        }
                        else
                        {
                            File.Copy(selectedFile, destFile, true);
                        }

                        MessageBox.Show($"PACKET LOADED: {Path.GetFileName(selectedFile)}\nREADY FOR TRANSMISSION.", "FILE_LOADED");
                        lblLastAction.Text = "VIDEO_QUEUED";
                    }
                    catch (IOException)
                    {
                         MessageBox.Show("FILE_ACCESS_ERROR: The file is locked.\nPlease close the text file and try again.", "LOCK_ERROR");
                    }
                    catch (Exception ex)
                    {
                         MessageBox.Show($"LOAD_ERROR: {ex.Message}", "ERROR");
                    }
                }
            }
        }

        private void EnsureDirectories()
        {
            if (!Directory.Exists(PATH_PENDING)) Directory.CreateDirectory(PATH_PENDING);
            if (!Directory.Exists(PATH_POSTED)) Directory.CreateDirectory(PATH_POSTED);
        }

        private void LoadConfiguration()
        {
            if (File.Exists(CONFIG_FILE))
            {
                try
                {
                    string json = File.ReadAllText(CONFIG_FILE);
                    dynamic config = JsonConvert.DeserializeObject(json);
                    _apiKey = config.ApiKey;
                    _tiktokId = config.TikTokId ?? config.AccountId ?? "";
                    _instaId = config.InstaId ?? config.AccountId ?? "";
                    _youtubeId = config.YoutubeId ?? config.AccountId ?? "";
                    _prefWidth = config.ResWidth ?? 1200;
                    _prefHeight = config.ResHeight ?? 800;
                    
                    // Apply resolution
                    if (_prefWidth > 100 && _prefHeight > 100)
                    {
                        this.Size = new Size(_prefWidth, _prefHeight);
                    }
                    lblSystemStatus.Text = "> SYSTEM_STATUS: ONLINE_ [API LINKED]";
                    lblSystemStatus.ForeColor = colGreen; // Ensure it turns green on success 

                }
                catch
                {
                    lblSystemStatus.Text = "> SYSTEM_STATUS: CONFIG_ERROR_";
                }
            }
            else
            {
                lblSystemStatus.Text = "> SYSTEM_STATUS: API_UNLINKED_";
            }
        }

        private void PromptForCredentials()
        {
             btnSettings.PerformClick();
        }

        // ====================================================================
        // INPUT DIALOG BOX - Popup windows for user input
        // ====================================================================
        private string ShowInputDialog(string prompt, string defaultValue = "")
        {
            // DIALOG WINDOW SIZE - Width=500px, Height=200px
            // Change these values to make input dialogs bigger/smaller
            Form promptForm = new Form()
            {
                Width = 500,
                Height = 200,
                FormBorderStyle = FormBorderStyle.None,
                Text = prompt,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = Color.Black,      // Dialog background color
                ForeColor = colGreen          // Dialog text color
            };
            
            promptForm.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, promptForm.ClientRectangle, colGreen, ButtonBorderStyle.Solid); };

            // DIALOG LABEL - Position: X=20, Y=20 | Font: Consolas 12pt
            Label textLabel = new Label() { Left = 20, Top = 20, Text = prompt, ForeColor = colGreen, Font = new Font("Consolas", 12F), AutoSize = true };
            
            // DIALOG TEXT INPUT BOX - Position: X=20, Y=60 | Size: Width=460px | Font: Consolas 12pt
            TextBox textBox = new TextBox() { Left = 20, Top = 60, Width = 460, Text = defaultValue, BackColor = Color.Black, ForeColor = colGreen, Font = new Font("Consolas", 12F), BorderStyle = BorderStyle.FixedSingle };
            
            // DIALOG SUBMIT BUTTON - Position: X=360, Y=100 | Size: Width=120px
            Button confirmation = new Button() { Text = "[ SUBMIT ]", Left = 360, Width = 120, Top = 100, DialogResult = DialogResult.OK, FlatStyle = FlatStyle.Flat, BackColor = Color.Black, ForeColor = colGreen };
            confirmation.MouseEnter += (s, e) => { confirmation.BackColor = colGreen; confirmation.ForeColor = Color.Black; };
            confirmation.MouseLeave += (s, e) => { confirmation.BackColor = Color.Black; confirmation.ForeColor = colGreen; };

            promptForm.Controls.Add(textLabel);
            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.AcceptButton = confirmation;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        // ====================================================================
        // SYSTEM OPS
        // ====================================================================

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "⚠ SYSTEM ALERT ⚠\n\nINITIATING KILL SEQUENCE.\nCONFIRM ACTION?",
                "ROOT_PERMISSION_REQUIRED",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes) Process.Start("shutdown", "/s /t 0");
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "⚠ SYSTEM ALERT ⚠\n\nREBOOT REQUIRED.\nCONFIRM ACTION?",
                "ROOT_PERMISSION_REQUIRED",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes) Process.Start("shutdown", "/r /t 0");
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SYSTEM_LOCKED. AUTHORIZATION_REQUIRED.", "SECURE_SESSION");
        }

        // ====================================================================
        // SETTINGS DIALOG - Configuration window for API keys and IDs
        // ====================================================================
        private void btnSettings_Click(object sender, EventArgs e)
        {
            // CHECK IF HIDE API KEYS IS ENABLED - For streaming protection
            // Find the checkbox in the Settings tab
            CheckBox chkHideApiKeys = this.Controls.Find("chkHideApiKeys", true).FirstOrDefault() as CheckBox;
            bool hideKeys = chkHideApiKeys?.Checked ?? false;
            
            // SETTINGS WINDOW SIZE - Width=550px, Height=550px
            // Change these values to make the settings window bigger/smaller
            Form inputForm = new Form();
            inputForm.Width = 550;
            inputForm.Height = 550;
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.Text = "SYSTEM_CONFIGURATION_OVERRIDE";
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.BackColor = Color.Black;         // Settings window background
            inputForm.ForeColor = colGreen;            // Settings window text color

            // ====================================================================
            // API KEY INPUT - First field in settings
            // ====================================================================
            // LABEL: Position X=20, Y=20 | Size: Width=500px, Height=40px | Font: Consolas 10pt
            Label lblKey = new Label() { Left = 20, Top = 20, Width = 500, Height = 40, Text = "BLOTATO_API_KEY (Required)", Font = new Font("Consolas", 10F) };
            // TEXTBOX: Position X=20, Y=50 | Size: Width=500px | Font: Consolas 12pt
            // MASKED if streaming mode is enabled
            TextBox txtKey = new TextBox() { 
                Left = 20, Top = 50, Width = 500, 
                Text = hideKeys ? "•••••••••••••••••••••" : _apiKey, 
                BackColor = Color.FromArgb(20, 20, 20), 
                ForeColor = colGreen, 
                Font = new Font("Consolas", 12F),
                UseSystemPasswordChar = hideKeys  // Additional masking protection
            };

            // ====================================================================
            // TIKTOK ID INPUT
            // ====================================================================
            // LABEL: Position X=20, Y=100 | Font: Consolas 10pt
            Label lblTk = new Label() { Left = 20, Top = 100, Width = 500, Text = ">> TIKTOK_ACCOUNT_ID (Numeric)", Font = new Font("Consolas", 10F) };
            // TEXTBOX: Position X=20, Y=130 | Size: Width=500px | Font: Consolas 12pt
            TextBox txtTk = new TextBox() { 
                Left = 20, Top = 130, Width = 500, 
                Text = hideKeys ? "•••••••••••" : _tiktokId, 
                BackColor = Color.FromArgb(20, 20, 20), 
                ForeColor = colGreen, 
                Font = new Font("Consolas", 12F) 
            };

            // ====================================================================
            // INSTAGRAM ID INPUT
            // ====================================================================
            // LABEL: Position X=20, Y=180 | Font: Consolas 10pt
            Label lblIn = new Label() { Left = 20, Top = 180, Width = 500, Text = ">> INSTAGRAM_ACCOUNT_ID (Numeric)", Font = new Font("Consolas", 10F) };
            // TEXTBOX: Position X=20, Y=210 | Size: Width=500px | Font: Consolas 12pt
            TextBox txtIn = new TextBox() { 
                Left = 20, Top = 210, Width = 500, 
                Text = hideKeys ? "•••••••••••" : _instaId, 
                BackColor = Color.FromArgb(20, 20, 20), 
                ForeColor = colGreen, 
                Font = new Font("Consolas", 12F) 
            };

            // ====================================================================
            // YOUTUBE ID INPUT
            // ====================================================================
            // LABEL: Position X=20, Y=260 | Font: Consolas 10pt
            Label lblYt = new Label() { Left = 20, Top = 260, Width = 500, Text = ">> YOUTUBE_ACCOUNT_ID (Numeric)", Font = new Font("Consolas", 10F) };
            // TEXTBOX: Position X=20, Y=290 | Size: Width=500px | Font: Consolas 12pt
            TextBox txtYt = new TextBox() { 
                Left = 20, Top = 290, Width = 500, 
                Text = hideKeys ? "•••••••••••" : _youtubeId, 
                BackColor = Color.FromArgb(20, 20, 20), 
                ForeColor = colGreen, 
                Font = new Font("Consolas", 12F) 
            };

            // ====================================================================
            // RESOLUTION SELECTOR - Dropdown for window size presets
            // ====================================================================
            // LABEL: Position X=20, Y=340 | Font: Consolas 10pt
            Label lblRes = new Label() { Left = 20, Top = 340, Width = 500, Text = ">> DISPLAY_RESOLUTION_PROFILE", Font = new Font("Consolas", 10F) };
            // DROPDOWN BOX: Position X=20, Y=370 | Size: Width=500px | Font: Consolas 12pt
            ComboBox cmbRes = new ComboBox() { Left = 20, Top = 370, Width = 500, BackColor = Color.FromArgb(20, 20, 20), ForeColor = colGreen, Font = new Font("Consolas", 12F), DropDownStyle = ComboBoxStyle.DropDownList };
            // Available resolution options - Add or modify these to change window size options
            cmbRes.Items.AddRange(new string[] { "1200 x 800 (Standard)", "1920 x 1080 (HD)", "2160 x 1080 (Ultra-Wide)", "2560 x 1440 (2K)", "3840 x 2160 (4K)" });
            
            // Set current selection based on existing pref
            string currentRes = $"{_prefWidth} x {_prefHeight}";
            bool found = false;
            for(int i=0; i<cmbRes.Items.Count; i++) {
                if (cmbRes.Items[i].ToString().Contains($"{_prefWidth} x {_prefHeight}")) {
                    cmbRes.SelectedIndex = i;
                    found = true;
                    break;
                }
            }
            if (!found) cmbRes.SelectedIndex = 0;

            // ====================================================================
            // SETTINGS DIALOG BUTTONS
            // ====================================================================
            // OK/COMMIT BUTTON: Position X=360, Y=430 | Size: Width=160px, Height=40px
            Button btnOk = new Button() { Text = "COMMIT_CHANGES", Left = 360, Top = 430, Width = 160, Height = 40, DialogResult = DialogResult.OK };
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.BackColor = colGreen;          // Green background
            btnOk.ForeColor = Color.Black;       // Black text
            
            // CANCEL/ABORT BUTTON: Position X=230, Y=430 | Size: Width=120px, Height=40px
            Button btnCancel = new Button() { Text = "ABORT", Left = 230, Top = 430, Width = 120, Height = 40, DialogResult = DialogResult.Cancel };
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = colGreen;      // Green text
            btnCancel.BackColor = Color.Black;   // Black background

            inputForm.Controls.AddRange(new Control[] { lblKey, txtKey, lblTk, txtTk, lblIn, txtIn, lblYt, txtYt, lblRes, cmbRes, btnOk, btnCancel });
            inputForm.AcceptButton = btnOk;
            inputForm.CancelButton = btnCancel;

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                // DON'T SAVE MASKED VALUES - Only save if user actually changed them
                // If hideKeys was true, the values shown were masked, so don't overwrite
                if (!hideKeys || !txtKey.Text.Contains("•"))
                {
                    _apiKey = txtKey.Text.Trim();
                    _tiktokId = txtTk.Text.Trim();
                    _instaId = txtIn.Text.Trim();
                    _youtubeId = txtYt.Text.Trim();
                }
                // If streaming mode was on and user didn't change values, keep original values

                // Parse Resolution
                string resText = cmbRes.SelectedItem.ToString();
                try {
                    var parts = resText.Split(' ')[0].Split('x');
                    _prefWidth = int.Parse(parts[0].Trim());
                    _prefHeight = int.Parse(resText.Split('x')[1].Split('(')[0].Trim());
                } catch { 
                    _prefWidth = 1200; _prefHeight = 800; 
                }

                var config = new { 
                    ApiKey = _apiKey, 
                    TikTokId = _tiktokId, 
                    InstaId = _instaId, 
                    YoutubeId = _youtubeId,
                    ResWidth = _prefWidth,
                    ResHeight = _prefHeight
                };
                
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, json);
                
                // Apply immediately
                this.Size = new Size(_prefWidth, _prefHeight);
                txtConsoleLog.ScrollToCaret(); // Scroll terminal iff needed
                
                MessageBox.Show("SYSTEM_NODES_UPDATED.\nRESTART_RECOMMENDED_FOR_DISPLAY_SYNC.", "SUCCESS");
                Log($"CONFIG_UPDATE: RES={_prefWidth}x{_prefHeight}, IDS_SYNCED");
            }
        }

        // ====================================================================
        // BOT NET CONTROLS (PIPELINE)
        // ====================================================================

        private async void btnAutoPost_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_apiKey) || (string.IsNullOrEmpty(_tiktokId) && string.IsNullOrEmpty(_instaId) && string.IsNullOrEmpty(_youtubeId)))
            {
                var response = MessageBox.Show("API_CREDENTIALS_MISSING.\nESTABLISH LINK?", "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (response == DialogResult.Yes) btnSettings.PerformClick();
                return;
            }

            // CHECK CONTENT FOLDER
            string[] pendingFiles = Directory.GetFiles(PATH_PENDING, "*.*")
                .Where(f => f.EndsWith(".mp4") || f.EndsWith(".mov") || f.EndsWith(".txt"))
                .OrderByDescending(f => f.EndsWith(".txt")) // PRIORITIZE TEXT FILES (Verified Links)
                .ToArray();

            if (pendingFiles.Length == 0)
            {
                // MessageBox.Show($"NO_PACKETS_IN_QUEUE.\nLOCATION: {PATH_PENDING}", "QUEUE_EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("ERROR: QUEUE_EMPTY. NO PACKETS FOUND.");
                return;
            }
 
            string fileToUpload = pendingFiles[0]; 
            string fileName = Path.GetFileName(fileToUpload);

            btnAutoPost.Enabled = false;
            btnAutoPost.Text = "[ UPLOADING_PACKET... ]";
            btnAutoPost.ForeColor = colYellow;
            
            Log("==========================================");
            Log($"INITIATING_SEQUENCE: UPLOAD");
            Log($"TARGET_PACKET: {fileName}");
            Log("==========================================");

            lblLastAction.Text = $"UPLOADING: {fileName}";
            lblSystemStatus.Text = "> PROCESS_STATUS: BUSY_";
            lblSystemStatus.ForeColor = colYellow;

            BlotatoService service = null;
            try
            {
                service = new BlotatoService(_apiKey, _tiktokId, _instaId, _youtubeId);
                
                string contentUrl = fileToUpload;
                if (fileToUpload.EndsWith(".txt"))
                {
                    contentUrl = File.ReadAllText(fileToUpload).Trim();
                    
                    // AUTO-FIX: Convert Google Drive View Links to Direct Download
                    if (contentUrl.Contains("drive.google.com") && contentUrl.Contains("/view"))
                    {
                        try 
                        {
                            // Extract ID: .../d/FILE_ID/view...
                            var parts = contentUrl.Split(new[] { "/d/" }, StringSplitOptions.None);
                            if (parts.Length > 1)
                            {
                                var idPart = parts[1].Split('/')[0];
                                string originalUrl = contentUrl;
                                contentUrl = $"https://drive.google.com/uc?export=download&id={idPart}";
                                Log($"SMART_FIX: CONVERTED DRIVE LINK");
                                Log($"FROM: {originalUrl}");
                                Log($"TO:   {contentUrl}");
                            }
                        }
                        catch { Log("WARNING: COULD NOT AUTO-CONVERT DRIVE LINK"); }
                    }

                    Log($"LINK_LOADED: {contentUrl}");
                }
                else
                {
                    Log("WARNING: LOCAL_FILE_DETECTED."); 
                    Log(">> API REQUIRES PUBLIC URL (HTTP/HTTPS).");
                    Log(">> IF UPLOAD FAILS, USE A .TXT FILE WITH A DIRECT LINK.");
                }

                // ====================================================================
                // CAPTION TEXT - Get caption from the description/caption text box
                // ====================================================================
                string caption = txtCaption.Text.Trim();
                
                // Use default caption if the text box is empty or has placeholder text
                if (string.IsNullOrWhiteSpace(caption) || caption == "Enter your video caption here...") {
                    caption = "Posted via BLVDE Content Studio #content #viral";
                }
                
                int successCount = 0;
                int failCount = 0;

                // Initialize results to a skipped state
                PostResult tiktokResult = new PostResult { Success = false, Message = "SKIPPED", ErrorDetails = "Platform ID not configured." };
                PostResult instaResult = new PostResult { Success = false, Message = "SKIPPED", ErrorDetails = "Platform ID not configured." };
                PostResult youtubeResult = new PostResult { Success = false, Message = "SKIPPED", ErrorDetails = "Platform ID not configured." };

                // 1. TikTok
                Log(">> CONNECTING TO TIKTOK UPLINK...");
                // Post to TikTok
                if (!string.IsNullOrEmpty(_tiktokId))
                {
                    Log(">> TARGETING: TIKTOK_UPLINK...", Color.Cyan);
                    lblLastAction.Text = "UPLOADING TO TIKTOK...";
                    tiktokResult = await service.PostToTikTokAsync(contentUrl, caption);
                    if (tiktokResult.Success) { Log("[+] TIKTOK: SUCCESS_", Color.Lime); successCount++; }
                    else { Log($"[-] TIKTOK: FAIL_ ({tiktokResult.Message})", Color.Red); failCount++; }
                }
                else { Log(">> TIKTOK_NODE: SKIPPED (DISCONNECTED)", Color.Gray); }

                // Post to Instagram
                Log(">> CONNECTING TO INSTAGRAM UPLINK...");
                if (!string.IsNullOrEmpty(_instaId))
                {
                    Log(">> TARGETING: INSTAGRAM_UPLINK...", Color.Cyan);
                    lblLastAction.Text = "UPLOADING TO INSTA...";
                    instaResult = await service.PostToInstagramAsync(contentUrl, caption);
                    if (instaResult.Success) { Log("[+] INSTAGRAM: SUCCESS_", Color.Lime); successCount++; }
                    else { Log($"[-] INSTAGRAM: FAIL_ ({instaResult.Message})", Color.Red); failCount++; }
                }
                else { Log(">> INSTAGRAM_NODE: SKIPPED (DISCONNECTED)", Color.Gray); }

                // Post to YouTube Shorts
                Log(">> CONNECTING TO YOUTUBE UPLINK...");
                if (!string.IsNullOrEmpty(_youtubeId))
                {
                    Log(">> TARGETING: YOUTUBE_UPLINK...", Color.Cyan);
                    lblLastAction.Text = "UPLOADING TO YOUTUBE...";
                    youtubeResult = await service.PostToYouTubeShortsAsync(contentUrl, caption);
                    if (youtubeResult.Success) { Log("[+] YOUTUBE_SHORTS: SUCCESS_", Color.Lime); successCount++; }
                    else { Log($"[-] YOUTUBE_SHORTS: FAIL_ ({youtubeResult.Message})", Color.Red); failCount++; }
                }
                else { Log(">> YOUTUBE_NODE: SKIPPED (DISCONNECTED)", Color.Gray); }
                
                // INTELLIGENT ERROR DETECTION
                if ((!tiktokResult.Success && tiktokResult.ErrorDetails.Contains("bigint")) || 
                    (!instaResult.Success && instaResult.ErrorDetails.Contains("bigint")) || 
                    (!youtubeResult.Success && youtubeResult.ErrorDetails.Contains("bigint")))
                {
                    Log("");
                    Log(">> !!! AUTH_ERROR: INVALID_ACCOUNT_ID_SYNTAX !!!");
                    Log(">> THE SERVER EXPECTS A NUMBER, BUT FOUND A UUID.");
                    Log(">> ACTION: CLICK [CONFIG_API_NODES] AND ENTER YOUR REAL ACCOUNT NUMBER.");
                    Log("");
                }

                // Compile Report
                string report = "TRANSMISSION REPORT:\n\n";
                report += tiktokResult.Success ? "[+] TIKTOK: SUCCESS\n" : $"[-] TIKTOK: FAIL ({tiktokResult.ErrorDetails})\n";
                report += instaResult.Success ? "[+] INSTAGRAM: SUCCESS\n" : $"[-] INSTAGRAM: FAIL ({instaResult.ErrorDetails})\n";
                report += youtubeResult.Success ? "[+] YOUTUBE: SUCCESS\n" : $"[-] YOUTUBE: FAIL ({youtubeResult.ErrorDetails})\n";

                // Move file if at least one succeeded
                if (tiktokResult.Success || instaResult.Success || youtubeResult.Success)
                {
                    string destPath = Path.Combine(PATH_POSTED, fileName);
                    if (File.Exists(destPath)) File.Delete(destPath);
                    File.Move(fileToUpload, destPath);
                    lblLastAction.Text = "BATCH_COMPLETE";
                    Log(">> PACKET ARCHIVED TO 'POSTED'.");
                    Log(">> SEQUENCE COMPLETE.");
                    // MessageBox.Show(report, "BATCH_COMPLETE");
                }
                else
                {
                    // MessageBox.Show($"ALL_UPLOADS_FAILED.\n\n{report}", "CRITICAL_FAILURE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log(">> CRITICAL FAILURE: ALL UPLOADS FAILED.");
                    lblLastAction.Text = "BATCH_FAILED";
                }
            }
            catch (Exception ex)
            {
                Log($"EXECUTION_FAILURE: {ex.Message}");
                // MessageBox.Show($"EXECUTION_FAILURE: {ex.Message}", "CRITICAL_ERROR");
            }
            finally
            {
                if (service != null) service.Dispose();
                
                btnAutoPost.Enabled = true;
                btnAutoPost.Text = ">> INITIATE_UPLOAD_SEQUENCE";
                btnAutoPost.ForeColor = colGreen;
                lblSystemStatus.Text = "> SYSTEM_STATUS: ONLINE_";
                lblSystemStatus.ForeColor = colGreen;
            }
        }

        private void btnGenerateContent_Click(object sender, EventArgs e)
        {
            string dummyFile = Path.Combine(PATH_PENDING, $"synth_clip_{DateTime.Now.Ticks}.txt");
            File.WriteAllText(dummyFile, "https://www.w3schools.com/html/mov_bbb.mp4"); 
            MessageBox.Show($"SYNTHETIC_MEDIA_GENERATED.\nADDED TO QUEUE: {dummyFile}", "GENERATION_COMPLETE");
            lblLastAction.Text = "NEW_CONTENT_QUEUED";
        }

        // ====================================================================
        // TERMINAL INPUT
        // ====================================================================

        public void btnExecuteAgent_Click(object sender, EventArgs e)
        {
            string instructions = txtAgentInput.Text;
            if (string.IsNullOrWhiteSpace(instructions) || instructions == "awaiting_instruction...") return;

            string path = Path.Combine(Application.StartupPath, "task_queue.log");
            File.AppendAllText(path, $"[{DateTime.Now}] {instructions}\n");
            
            if (instructions.StartsWith("/adduser"))
            {
                string[] parts = instructions.Split(' ');
                if (parts.Length == 3) {
                    LoginForm.AddUser(parts[1], parts[2]); 
                    MessageBox.Show($"USER [{parts[1]}] ADDED.", "ADMIN");
                }
            }
            else
            {
                MessageBox.Show($"INSTRUCTION_QUEUED", "ACK");
            }
            
            
            txtAgentInput.Text = "awaiting_instruction...";
        }

        // ====================================================================
        // THEME SWITCHING - Apply light or dark mode
        // ====================================================================
        private void ApplyTheme(bool isDarkMode)
        {
            // Define color schemes
            Color bgColor, cardColor, textColor, textLightColor, borderColor;
            
            if (isDarkMode)
            {
                // DARK MODE COLORS
                bgColor = Color.FromArgb(17, 24, 39);           // Dark gray-blue
                cardColor = Color.FromArgb(31, 41, 55);         // Slightly lighter dark
                textColor = Color.FromArgb(243, 244, 246);      // Light gray text
                textLightColor = Color.FromArgb(156, 163, 175); // Muted gray
                borderColor = Color.FromArgb(55, 65, 81);       // Dark border
            }
            else
            {
                // LIGHT MODE COLORS (default)
                bgColor = Color.FromArgb(245, 247, 250);
                cardColor = Color.White;
                textColor = Color.FromArgb(30, 41, 59);
                textLightColor = Color.FromArgb(100, 116, 139);
                borderColor = Color.FromArgb(226, 232, 240);
            }
            
            // Apply to main form
            this.BackColor = bgColor;
            
            // Apply to TabControl itself
            if (mainTabControl != null)
            {
                mainTabControl.BackColor = bgColor;
                
                // Apply to all tabs
                foreach (TabPage tab in mainTabControl.TabPages)
                {
                    tab.BackColor = cardColor;
                    tab.ForeColor = textColor;
                    
                    // Update all controls in tab
                    foreach (Control ctrl in tab.Controls)
                    {
                        if (ctrl is TextBox txt && ctrl.Name != "txtConsoleLog")
                        {
                            txt.BackColor = cardColor;
                            txt.ForeColor = textColor;
                        }
                        else if (ctrl is Label lbl)
                        {
                            lbl.ForeColor = lbl.Font.Bold ? textColor : textLightColor;
                        }
                        else if (ctrl is Button btn)
                        {
                            // Don't change primary action buttons, keep their colors
                            // Just update secondary/system buttons
                        }
                    }
                }
            }
            
            
            // Save preference
            SaveThemePreference(isDarkMode);
        }
        
        private void SaveThemePreference(bool isDarkMode)
        {
            // Save to config file
            try
            {
                var config = new {
                    ApiKey = _apiKey,
                    TikTokId = _tiktokId,
                    InstaId = _instaId,
                    YoutubeId = _youtubeId,
                    ResWidth = _prefWidth,
                    ResHeight = _prefHeight,
                    DarkMode = isDarkMode
                };
                
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, json);
            }
            catch { }
        }
    }
}
