-- ============================================================================
-- BLVDE Content Studio - Supabase Database Schema
-- ============================================================================
-- This schema supports remote user authentication, multi-account management,
-- upload tracking, and scheduling features
-- ============================================================================

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ============================================================================
-- USERS TABLE - Core authentication and user profiles
-- ============================================================================
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    email VARCHAR(255) UNIQUE NOT NULL,
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(100),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    last_login TIMESTAMP WITH TIME ZONE,
    is_active BOOLEAN DEFAULT TRUE,
    subscription_tier VARCHAR(20) DEFAULT 'free', -- free, pro, enterprise
    theme_preference VARCHAR(20) DEFAULT 'light' -- light, dark
);

-- ============================================================================
-- USER_SETTINGS - Store user preferences and configurations
-- ============================================================================
CREATE TABLE user_settings (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    window_width INTEGER DEFAULT 1920,
    window_height INTEGER DEFAULT 1080,
    hide_api_keys BOOLEAN DEFAULT FALSE,
    dark_mode BOOLEAN DEFAULT FALSE,
    background_image_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ============================================================================
-- PLATFORM_ACCOUNTS - Multi-account support for TikTok, Instagram, YouTube
-- ============================================================================
CREATE TABLE platform_accounts (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    platform VARCHAR(20) NOT NULL, -- 'tiktok', 'instagram', 'youtube'
    account_name VARCHAR(100) NOT NULL, -- Friendly name user gives it
    account_id VARCHAR(255) NOT NULL, -- Platform's account ID
    blotato_api_key TEXT, -- Encrypted API key
    is_active BOOLEAN DEFAULT TRUE,
    is_default BOOLEAN DEFAULT FALSE, -- Default account for this platform
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(user_id, platform, account_id)
);

-- ============================================================================
-- UPLOAD_HISTORY - Track all uploads
-- ============================================================================
CREATE TABLE upload_history (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    video_url TEXT NOT NULL,
    caption TEXT,
    upload_timestamp TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    platforms_posted TEXT[], -- Array: ['tiktok', 'instagram', 'youtube']
    status VARCHAR(20) DEFAULT 'pending', -- pending, success, failed, partial
    error_message TEXT,
    tiktok_account_id UUID REFERENCES platform_accounts(id),
    instagram_account_id UUID REFERENCES platform_accounts(id),
    youtube_account_id UUID REFERENCES platform_accounts(id),
    tiktok_status VARCHAR(20), -- success, failed, skipped
    instagram_status VARCHAR(20),
    youtube_status VARCHAR(20)
);

-- ============================================================================
-- SCHEDULED_POSTS - Queue for scheduled uploads
-- ============================================================================
CREATE TABLE scheduled_posts (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    video_url TEXT NOT NULL,
    caption TEXT,
    scheduled_time TIMESTAMP WITH TIME ZONE NOT NULL,
    platforms TEXT[] NOT NULL, -- ['tiktok', 'instagram', 'youtube']
    tiktok_account_id UUID REFERENCES platform_accounts(id),
    instagram_account_id UUID REFERENCES platform_accounts(id),
    youtube_account_id UUID REFERENCES platform_accounts(id),
    status VARCHAR(20) DEFAULT 'scheduled', -- scheduled, posted, failed, cancelled
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    posted_at TIMESTAMP WITH TIME ZONE
);

-- ============================================================================
-- NOTES - User notes synced to cloud
-- ============================================================================
CREATE TABLE notes (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    content TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ============================================================================
-- PROMPTS - User prompt templates synced to cloud
-- ============================================================================
CREATE TABLE prompts (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    user_id UUID REFERENCES users(id) ON DELETE CASCADE,
    content TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ============================================================================
-- INDEXES for better query performance
-- ============================================================================
CREATE INDEX idx_users_email ON users(email);
CREATE INDEX idx_users_username ON users(username);
CREATE INDEX idx_platform_accounts_user_id ON platform_accounts(user_id);
CREATE INDEX idx_platform_accounts_platform ON platform_accounts(platform);
CREATE INDEX idx_upload_history_user_id ON upload_history(user_id);
CREATE INDEX idx_upload_history_timestamp ON upload_history(upload_timestamp);
CREATE INDEX idx_scheduled_posts_user_id ON scheduled_posts(user_id);
CREATE INDEX idx_scheduled_posts_scheduled_time ON scheduled_posts(scheduled_time);
CREATE INDEX idx_scheduled_posts_status ON scheduled_posts(status);

-- ============================================================================
-- ROW LEVEL SECURITY (RLS) - Users can only access their own data
-- ============================================================================

-- Enable RLS on all tables
ALTER TABLE users ENABLE ROW LEVEL SECURITY;
ALTER TABLE user_settings ENABLE ROW LEVEL SECURITY;
ALTER TABLE platform_accounts ENABLE ROW LEVEL SECURITY;
ALTER TABLE upload_history ENABLE ROW LEVEL SECURITY;
ALTER TABLE scheduled_posts ENABLE ROW LEVEL SECURITY;
ALTER TABLE notes ENABLE ROW LEVEL SECURITY;
ALTER TABLE prompts ENABLE ROW LEVEL SECURITY;

-- Users can view their own profile
CREATE POLICY "Users can view own profile" ON users
    FOR SELECT USING (auth.uid() = id);

-- Users can update their own profile
CREATE POLICY "Users can update own profile" ON users
    FOR UPDATE USING (auth.uid() = id);

-- User settings policies
CREATE POLICY "Users can view own settings" ON user_settings
    FOR SELECT USING (auth.uid() = user_id);

CREATE POLICY "Users can insert own settings" ON user_settings
    FOR INSERT WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update own settings" ON user_settings
    FOR UPDATE USING (auth.uid() = user_id);

-- Platform accounts policies
CREATE POLICY "Users can view own accounts" ON platform_accounts
    FOR SELECT USING (auth.uid() = user_id);

CREATE POLICY "Users can insert own accounts" ON platform_accounts
    FOR INSERT WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update own accounts" ON platform_accounts
    FOR UPDATE USING (auth.uid() = user_id);

CREATE POLICY "Users can delete own accounts" ON platform_accounts
    FOR DELETE USING (auth.uid() = user_id);

-- Upload history policies
CREATE POLICY "Users can view own uploads" ON upload_history
    FOR SELECT USING (auth.uid() = user_id);

CREATE POLICY "Users can insert own uploads" ON upload_history
    FOR INSERT WITH CHECK (auth.uid() = user_id);

-- Scheduled posts policies
CREATE POLICY "Users can view own scheduled posts" ON scheduled_posts
    FOR SELECT USING (auth.uid() = user_id);

CREATE POLICY "Users can insert own scheduled posts" ON scheduled_posts
    FOR INSERT WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update own scheduled posts" ON scheduled_posts
    FOR UPDATE USING (auth.uid() = user_id);

CREATE POLICY "Users can delete own scheduled posts" ON scheduled_posts
    FOR DELETE USING (auth.uid() = user_id);

-- Notes policies
CREATE POLICY "Users can view own notes" ON notes
    FOR SELECT USING (auth.uid() = user_id);

CREATE POLICY "Users can insert own notes" ON notes
    FOR INSERT WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update own notes" ON notes
    FOR UPDATE USING (auth.uid() = user_id);

-- Prompts policies
CREATE POLICY "Users can view own prompts" ON prompts
    FOR SELECT USING (auth.uid() = user_id);

CREATE POLICY "Users can insert own prompts" ON prompts
    FOR INSERT WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update own prompts" ON prompts
    FOR UPDATE USING (auth.uid() = user_id);

-- ============================================================================
-- FUNCTIONS - Useful database functions
-- ============================================================================

-- Function to update the updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Triggers to auto-update updated_at
CREATE TRIGGER update_user_settings_updated_at
    BEFORE UPDATE ON user_settings
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_platform_accounts_updated_at
    BEFORE UPDATE ON platform_accounts
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_notes_updated_at
    BEFORE UPDATE ON notes
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_prompts_updated_at
    BEFORE UPDATE ON prompts
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at_column();

-- ============================================================================
-- SAMPLE DATA (Optional - for testing)
-- ============================================================================

-- Insert a test user (password: 'test123' - hashed with bcrypt)
-- You should use Supabase Auth instead of this for production
INSERT INTO users (email, username, password_hash, full_name) VALUES
    ('demo@blvde.com', 'demo_user', '$2a$10$EixZaYVK1fsbw1ZfbX3OXePaWxn96p36WQoeG6Lruj3vjPGga31lW', 'Demo User');
