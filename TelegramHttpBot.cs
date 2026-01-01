#nullable disable
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;

namespace BLVDEContentStudio
{
    public class TelegramHttpBot
    {
        private readonly string _botToken;
        private readonly HttpClient _httpClient;
        private CancellationTokenSource _cts;
        private bool _isRunning = false;
        private long _lastUpdateId = 0;

        public bool IsRunning => _isRunning;

        public TelegramHttpBot(string botToken)
        {
            _botToken = botToken;
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<bool> StartAsync()
        {
            try
            {
                // Test connection
                var testUrl = $"https://api.telegram.org/bot{_botToken}/getMe";
                var response = await _httpClient.GetStringAsync(testUrl);
                
                if (!response.Contains("\"ok\":true"))
                {
                    MessageBox.Show("Failed to connect to Telegram bot", "Error");
                    return false;
                }

                _isRunning = true;
                _cts = new CancellationTokenSource();
                
                // Start polling for messages in background
                _ = Task.Run(() => PollForUpdates(_cts.Token));
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Telegram connection error: {ex.Message}", "Error");
                return false;
            }
        }

        public void Stop()
        {
            _cts?.Cancel();
            _isRunning = false;
        }

        private async Task PollForUpdates(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested && _isRunning)
            {
                try
                {
                    var url = $"https://api.telegram.org/bot{_botToken}/getUpdates?offset={_lastUpdateId + 1}&timeout=30";
                    var response = await _httpClient.GetStringAsync(url);
                    
                    using (JsonDocument doc = JsonDocument.Parse(response))
                    {
                        if (doc.RootElement.TryGetProperty("result", out var result))
                        {
                            foreach (var update in result.EnumerateArray())
                            {
                                if (update.TryGetProperty("update_id", out var updateId))
                                {
                                    _lastUpdateId = updateId.GetInt64();
                                }

                                if (update.TryGetProperty("message", out var message))
                                {
                                    await HandleMessage(message);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Ignore polling errors, just retry
                    await Task.Delay(5000, ct);
                }
            }
        }

        private async Task HandleMessage(JsonElement message)
        {
            try
            {
                if (message.TryGetProperty("text", out var textElement) &&
                    message.TryGetProperty("chat", out var chatElement) &&
                    chatElement.TryGetProperty("id", out var chatIdElement))
                {
                    string text = textElement.GetString();
                    long chatId = chatIdElement.GetInt64();

                    if (text.StartsWith("/"))
                    {
                        await HandleCommand(chatId, text);
                    }
                }
            }
            catch { }
        }

        private async Task HandleCommand(long chatId, string command)
        {
            string response = command.ToLower() switch
            {
                "/start" => "🎬 *BLVDE Content Studio Bot*\n\nCommands:\n/status - Check status\n/help - Show this",
                "/status" => "✅ Connected to BLVDE Studio\n🔌 All systems operational",
                "/help" => "🎬 *BLVDE Content Studio Bot*\n\nCommands:\n/status - Check status\n/help - Show this",
                _ => "❓ Unknown command. Type /help for help."
            };

            await SendMessage(chatId, response);
        }

        public async Task SendMessage(long chatId, string text)
        {
            try
            {
                var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(text)}&parse_mode=Markdown";
                await _httpClient.GetStringAsync(url);
            }
            catch { }
        }

        public async Task SendNotification(string message)
        {
            // Would need to store chat IDs of users who've messaged the bot
            // For now, just log
            await Task.CompletedTask;
        }
    }
}
