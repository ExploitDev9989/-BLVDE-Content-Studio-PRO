// ============================================================================
// BLVDE Content Studio - Blotato API Service
// ============================================================================
// This class handles all communication with the Blotato API
// It sends your videos to TikTok, Instagram, and YouTube Shorts
// ============================================================================

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BLVDEContentStudio
{
    // ========================================================================
    // BLOTATO SERVICE CLASS - Handles API communication
    // ========================================================================
    public class BlotatoService
    {
        // ====================================================================
        // PRIVATE FIELDS (Data this class needs to remember)
        // ====================================================================
        
        // The HTTP client - used to make web requests
        private readonly HttpClient _httpClient;
        
        // Your Blotato API key (keep this secret!)
        private readonly string _apiKey;
        
        // Platform-specific account IDs
        private readonly string _tiktokAccountId;
        private readonly string _instagramAccountId;
        private readonly string _youtubeAccountId;
        
        // The base URL for Blotato API
        private const string BASE_URL = "https://backend.blotato.com/v2";

        // ====================================================================
        // CONSTRUCTOR - Sets up the service when created
        // ====================================================================
        public BlotatoService(string apiKey, string tiktokId, string instaId, string youtubeId)
        {
            _apiKey = apiKey;
            _tiktokAccountId = tiktokId;
            _instagramAccountId = instaId;
            _youtubeAccountId = youtubeId;
            
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("blotato-api-key", _apiKey);
            _httpClient.Timeout = TimeSpan.FromSeconds(120); // 2 minutes for uploads
        }

        // ====================================================================
        // TEST CONNECTION - Check if API key works
        // ====================================================================
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                // Try to get your accounts list
                var response = await _httpClient.GetAsync($"{BASE_URL}/accounts");
                
                // If status code is 200, connection works!
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // If anything goes wrong, connection failed
                return false;
            }
        }

        // ====================================================================
        // POST TO TIKTOK - Main method to post a video
        // ====================================================================
        /// <summary>
        /// Posts a video to TikTok via Blotato API
        /// </summary>
        /// <param name="videoUrl">Public URL to the video file</param>
        /// <param name="caption">Caption text for the video</param>
        /// <returns>Result object with success status and message</returns>
        public async Task<PostResult> PostToTikTokAsync(string videoUrl, string caption)
        {
            try
            {
                // ============================================================
                // CREATE THE REQUEST PAYLOAD
                // ============================================================
                // This is the data we send to Blotato
                // It includes ALL required fields to avoid the 401 error!
                
                var payload = new
                {
                    post = new
                    {
                        accountId = _tiktokAccountId,
                        
                        // Content section - what to post
                        content = new
                        {
                            text = caption,              // The caption
                            mediaUrls = new[] { videoUrl }, // Array of video URLs
                            platform = "tiktok"          // Which platform
                        },
                        
                        // Target section - TikTok-specific settings
                        // These are REQUIRED or you get the 401 error!
                        target = new
                        {
                            targetType = "tiktok",           // Platform type
                            privacyLevel = "PUBLIC_TO_EVERYONE", // Public video
                            disabledComments = false,        // Allow comments
                            disabledDuet = false,            // Allow duets
                            disabledStitch = false,          // Allow stitches
                            isBrandedContent = false,        // Not branded content
                            isYourBrand = false,             // Not your brand
                            isAiGenerated = true             // AI-generated content
                        }
                    }
                };

                // ============================================================
                // CONVERT PAYLOAD TO JSON
                // ============================================================
                // Turn our C# object into JSON text
                string jsonPayload = JsonConvert.SerializeObject(payload);
                
                // Create HTTP content with JSON
                var content = new StringContent(
                    jsonPayload,                    // The JSON data
                    Encoding.UTF8,                  // Text encoding
                    "application/json"              // Content type
                );

                // ============================================================
                // SEND THE REQUEST
                // ============================================================
                // Post to Blotato API
                var response = await _httpClient.PostAsync(
                    $"{BASE_URL}/posts",  // API endpoint
                    content               // The JSON payload
                );

                // Get the response text
                string responseText = await response.Content.ReadAsStringAsync();

                // ============================================================
                // CHECK IF IT WORKED
                // ============================================================
                if (response.IsSuccessStatusCode)
                {
                    // Success! Video posted
                    return new PostResult
                    {
                        Success = true,
                        Message = "Video posted successfully to TikTok!",
                        ResponseData = responseText
                    };
                }
                else
                {
                    // Failed - return error details
                    return new PostResult
                    {
                        Success = false,
                        Message = $"Failed to post. Status: {response.StatusCode}",
                        ErrorDetails = responseText
                    };
                }
            }
            catch (Exception ex)
            {
                // If anything crashes, return the error
                return new PostResult
                {
                    Success = false,
                    Message = "Error posting video",
                    ErrorDetails = ex.Message
                };
            }
        }

        // ====================================================================
        // POST TO INSTAGRAM - Post to Instagram Reels
        // ====================================================================
        public async Task<PostResult> PostToInstagramAsync(string videoUrl, string caption)
        {
            try
            {
                // Similar to TikTok but for Instagram
                var payload = new
                {
                    post = new
                    {
                        accountId = _instagramAccountId,
                        content = new
                        {
                            text = caption,
                            mediaUrls = new[] { videoUrl },
                            platform = "instagram"
                        },
                        target = new
                        {
                            targetType = "instagram"
                        }
                    }
                };

                string jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{BASE_URL}/posts", content);
                string responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new PostResult
                    {
                        Success = true,
                        Message = "Video posted successfully to Instagram!",
                        ResponseData = responseText
                    };
                }
                else
                {
                    return new PostResult
                    {
                        Success = false,
                        Message = $"Failed to post to Instagram. Status: {response.StatusCode}",
                        ErrorDetails = responseText
                    };
                }
            }
            catch (Exception ex)
            {
                return new PostResult
                {
                    Success = false,
                    Message = "Error posting to Instagram",
                    ErrorDetails = ex.Message
                };
            }
        }

        // ====================================================================
        // POST TO YOUTUBE - Post to YouTube Shorts
        // ====================================================================
        public async Task<PostResult> PostToYouTubeShortsAsync(string videoUrl, string caption)
        {
            try
            {
                // ============================================================
                // YOUTUBE SHORTS REQUIREMENTS:
                // ============================================================
                // For a video to be recognized as a YouTube Short:
                // 1. Duration must be 60 seconds or less (handled by your video)
                // 2. Aspect ratio must be 9:16 vertical (handled by your video)
                // 3. Must include #Shorts in title OR description
                // ============================================================
                
                // Ensure #Shorts hashtag is present for proper categorization
                string shortsCaption = caption;
                if (!caption.Contains("#Shorts", StringComparison.OrdinalIgnoreCase))
                {
                    shortsCaption = caption + " #Shorts";
                }
                
                // Create a title that includes #Shorts and fits YouTube's limit
                string shortsTitle = shortsCaption.Length > 85 
                    ? shortsCaption.Substring(0, 85) + " #Shorts" 
                    : (shortsCaption.Contains("#Shorts", StringComparison.OrdinalIgnoreCase) 
                        ? shortsCaption 
                        : shortsCaption + " #Shorts");
                
                // Ensure title doesn't exceed 100 characters
                if (shortsTitle.Length > 100)
                {
                    shortsTitle = shortsTitle.Substring(0, 97) + "...";
                }
                
                var payload = new
                {
                    post = new
                    {
                        accountId = _youtubeAccountId,
                        content = new
                        {
                            text = shortsCaption,  // Description with #Shorts
                            mediaUrls = new[] { videoUrl },
                            platform = "youtube"
                        },
                        target = new
                        {
                            targetType = "youtube",
                            title = shortsTitle,  // Title with #Shorts
                            privacyStatus = "public",
                            shouldNotifySubscribers = true
                        }
                    }
                };

                string jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BASE_URL}/posts", content);
                string responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new PostResult
                    {
                        Success = true,
                        Message = "Video posted successfully to YouTube Shorts!",
                        ResponseData = responseText
                    };
                }
                else
                {
                    return new PostResult
                    {
                        Success = false,
                        Message = $"Failed to post to YouTube. Status: {response.StatusCode}",
                        ErrorDetails = responseText
                    };
                }
            }
            catch (Exception ex)
            {
                return new PostResult
                {
                    Success = false,
                    Message = "Error posting to YouTube",
                    ErrorDetails = ex.Message
                };
            }
        }

        // ====================================================================
        // DISPOSE - Clean up when done
        // ====================================================================
        public void Dispose()
        {
            // Close the HTTP client
            _httpClient?.Dispose();
        }
    }

    // ========================================================================
    // POST RESULT CLASS - Stores the result of a post attempt
    // ========================================================================
    public class PostResult
    {
        // Did the post succeed?
        public bool Success { get; set; }
        
        // Message to show user
        public string Message { get; set; }
        
        // Raw response from API (if successful)
        public string ResponseData { get; set; }
        
        // Error details (if failed)
        public string ErrorDetails { get; set; }
    }
}
