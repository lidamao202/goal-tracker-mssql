-- Create database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GoalTracker')
BEGIN
    CREATE DATABASE GoalTracker;
END
GO

USE GoalTracker;
GO

-- TeamMembers table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TeamMembers]') AND type in (N'U'))
BEGIN
    CREATE TABLE TeamMembers (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Mood INT NOT NULL DEFAULT 2,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT CK_TeamMembers_Mood CHECK (Mood >= 0 AND Mood <= 4),
        CONSTRAINT CK_TeamMembers_Name_Length CHECK (LEN(Name) > 0)
    );
END
GO

-- Goals table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Goals]') AND type in (N'U'))
BEGIN
    CREATE TABLE Goals (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TeamMemberId INT NOT NULL,
        Description NVARCHAR(200) NOT NULL,
        IsCompleted BIT NOT NULL DEFAULT 0,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Goals_TeamMembers FOREIGN KEY (TeamMemberId)
            REFERENCES TeamMembers(Id) ON DELETE CASCADE,
        CONSTRAINT CK_Goals_Description_Length CHECK (LEN(Description) > 0)
    );
END
GO

-- Index for faster goal lookups by member
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Goals_TeamMemberId' AND object_id = OBJECT_ID('Goals'))
BEGIN
    CREATE INDEX IX_Goals_TeamMemberId ON Goals(TeamMemberId);
END
GO

-- Seed data: Pre-configured team members (only if table is empty)
IF NOT EXISTS (SELECT 1 FROM TeamMembers)
BEGIN
    INSERT INTO TeamMembers (Name, Mood) VALUES
        ('Alice', 1),      -- Content
        ('Bob', 2),        -- Neutral
        ('Charlie', 0),    -- Happy
        ('Diana', 2),      -- Neutral
        ('Eve', 3);        -- Sad
END
GO

-- Seed data: Sample goals for demonstration (only if table is empty)
IF NOT EXISTS (SELECT 1 FROM Goals)
BEGIN
    INSERT INTO Goals (TeamMemberId, Description, IsCompleted) VALUES
        (1, 'Complete project documentation', 1),
        (1, 'Review pull requests', 0),
        (2, 'Fix login bug', 1),
        (2, 'Update dependencies', 0),
        (2, 'Write API specs', 0),
        (3, 'Design dashboard mockup', 1),
        (4, 'Setup CI/CD pipeline', 0),
        (5, 'Security audit', 0);
END
GO

PRINT 'GoalTracker database initialized successfully!';
GO
