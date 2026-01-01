# BLVDE Content Studio - Supabase Setup Guide

## 🚀 Quick Setup Instructions

### Step 1: Run the SQL Schema in Supabase

1. Open your **Supabase Dashboard** at https://app.supabase.com
2. Select your project (or create a new one)
3. Go to **SQL Editor** (left sidebar)
4. Click **New Query**
5. Copy and paste the entire contents of `supabase_schema.sql`
6. Click **Run** to execute the SQL

This will create all necessary tables:
- `users` - User accounts and authentication
- `user_settings` - Theme preferences, window size, etc.
- `platform_accounts` - Multi-account support (TikTok, Instagram, YouTube)
- `upload_history` - Track all uploads
- `scheduled_posts` - Queue for scheduled uploads
- `notes` - Cloud-synced notes
- `prompts` - Cloud-synced prompt templates

### Step 2: Enable Supabase Authentication

1. In Supabase Dashboard, go to **Authentication** → **Providers**
2. Enable **Email** provider
3. Configure email templates (optional)
4. Copy your **Project URL** and **anon public** API key

### Step 3: Get Your Supabase Credentials

You'll need these for the C# app:

```
Supabase URL: https://[YOUR-PROJECT-ID].supabase.co
Supabase Anon Key: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Find these in: **Settings** → **API** → **Project API keys**

### Step 4: Install Supabase C# Client

Run in your project directory:
```bash
dotnet add package supabase-csharp
```

### Step 5: Connection Configuration

The app will prompt you to enter:
- Supabase URL
- Supabase Anon Key

These will be stored encrypted in your config file.

## 🔒 Security Features

- **Row Level Security (RLS)** enabled - Users can only access their own data
- **Encrypted API keys** in database
- **Secure authentication** via Supabase Auth
- **Hide sensitive data** in Streamer Mode

## 📊 Database Features

### Multi-Account Support
- Store multiple TikTok/Instagram/YouTube accounts per user
- Set default accounts per platform
- Toggle accounts active/inactive

### Upload Tracking
- Complete history of all uploads
- Track success/failure per platform
- Error logging for debugging

### Scheduling
- Queue posts for future upload
- Per-platform scheduling
- Status tracking (scheduled, posted, failed, cancelled)

### Cloud Sync
- Notes synced across devices
- Prompts synced across devices
- Settings synced across devices

## 🎯 Next Steps

After running the SQL:
1. The app will connect to Supabase automatically
2. Create your user account through the app
3. Add your platform accounts
4. Start uploading!

## 📝 Sample Queries

View all your platform accounts:
```sql
SELECT * FROM platform_accounts WHERE user_id = '[YOUR-USER-ID]';
```

View upload history:
```sql
SELECT * FROM upload_history 
WHERE user_id = '[YOUR-USER-ID]' 
ORDER BY upload_timestamp DESC 
LIMIT 10;
```

View scheduled posts:
```sql
SELECT * FROM scheduled_posts 
WHERE user_id = '[YOUR-USER-ID]' 
AND status = 'scheduled'
ORDER BY scheduled_time ASC;
```
