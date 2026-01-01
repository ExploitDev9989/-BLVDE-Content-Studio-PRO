# BLVDE Content Studio - Changelog

## [11.3.0-PRO] - 2026-01-01

### Fixed
- **YouTube Shorts Classification**: Videos now automatically upload as YouTube Shorts instead of regular videos
  - Automatically adds `#Shorts` hashtag to video title and description
  - Ensures proper categorization in YouTube's Shorts feed
  - Smart title truncation to respect YouTube's 100-character limit

### Technical Details
- Modified `BlotatoService.cs` to intelligently append `#Shorts` if not already present
- Optimized title length handling for YouTube API compliance
- Added comprehensive inline documentation for YouTube Shorts requirements

---

## [11.2.0-PRO] - 2025-12-31

### Added
- Triple-Uplink Engine for simultaneous posting to TikTok, Instagram, and YouTube Shorts
- Cyber-Midnight UI optimized for 2160x1080 resolution
- Real-time color-coded terminal logging
- Smart-Link Protocol for Google Drive URL conversion
- Multi-platform account management via CONFIG_API_NODES

### Features
- Blotato API v2 integration with complete error handling
- Secure token handling via `blotato_config.json`
- Remote command support via `remote_command.txt`
- High-resolution dashboard with modern aesthetics

---

## [11.0.0] - Initial Release
- Core application framework
- Basic posting functionality
