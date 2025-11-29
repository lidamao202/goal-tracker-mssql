# Data Model: Team Daily Goal Tracker

**Feature**: 001-team-goal-tracker
**Date**: 2025-11-29

## Entity Relationship Diagram

```
┌─────────────────┐       ┌─────────────────┐
│  TeamMembers    │       │     Goals       │
├─────────────────┤       ├─────────────────┤
│ Id (PK)         │──────<│ Id (PK)         │
│ Name            │       │ TeamMemberId(FK)│
│ Mood            │       │ Description     │
│ CreatedAt       │       │ IsCompleted     │
└─────────────────┘       │ CreatedAt       │
                          └─────────────────┘

Relationship: TeamMember (1) ──< Goals (Many)
```

## Entities

### TeamMember

Represents a person on the team whose goals and mood are tracked.

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Id | int | PK, Identity | Unique identifier |
| Name | nvarchar(100) | NOT NULL | Team member's display name |
| Mood | int | NOT NULL, DEFAULT 2 | Current mood (0-4 enum) |
| CreatedAt | datetime2 | NOT NULL, DEFAULT GETUTCDATE() | Record creation timestamp |

**Mood Enum Values**:
| Value | Name | Emoji | Description |
|-------|------|-------|-------------|
| 0 | Happy | 😀 | Very positive mood |
| 1 | Content | 😊 | Positive mood |
| 2 | Neutral | 😐 | Default/neutral mood |
| 3 | Sad | 😞 | Negative mood |
| 4 | Stressed | 😤 | Very negative mood |

**Validation Rules**:
- Name: Required, max 100 characters
- Mood: Must be 0-4

---

### Goal

Represents a daily task assigned to a team member.

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Id | int | PK, Identity | Unique identifier |
| TeamMemberId | int | FK, NOT NULL | Reference to TeamMember |
| Description | nvarchar(200) | NOT NULL | Goal description text |
| IsCompleted | bit | NOT NULL, DEFAULT 0 | Completion status |
| CreatedAt | datetime2 | NOT NULL, DEFAULT GETUTCDATE() | Record creation timestamp |

**Validation Rules**:
- Description: Required, max 200 characters
- TeamMemberId: Must reference existing TeamMember
- IsCompleted: Can only be toggled (not edited per constitution)

**Allowed Operations** (per constitution):
- ✅ Add (create new goal)
- ✅ Complete/Uncomplete (toggle IsCompleted)
- ✅ Delete (remove goal)
- ❌ Edit description (BANNED)

---

## SQL Schema

```sql
-- Create database
CREATE DATABASE GoalTracker;
GO

USE GoalTracker;
GO

-- TeamMembers table
CREATE TABLE TeamMembers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Mood INT NOT NULL DEFAULT 2,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT CK_TeamMembers_Mood CHECK (Mood >= 0 AND Mood <= 4),
    CONSTRAINT CK_TeamMembers_Name_Length CHECK (LEN(Name) > 0)
);

-- Goals table
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

-- Index for faster goal lookups by member
CREATE INDEX IX_Goals_TeamMemberId ON Goals(TeamMemberId);
```

---

## Seed Data

```sql
-- Pre-configured team members (required since no user management)
INSERT INTO TeamMembers (Name, Mood) VALUES
    ('Alice', 1),      -- Content
    ('Bob', 2),        -- Neutral
    ('Charlie', 0),    -- Happy
    ('Diana', 2),      -- Neutral
    ('Eve', 3);        -- Sad

-- Sample goals for demonstration
INSERT INTO Goals (TeamMemberId, Description, IsCompleted) VALUES
    (1, 'Complete project documentation', 1),
    (1, 'Review pull requests', 0),
    (2, 'Fix login bug', 1),
    (2, 'Update dependencies', 0),
    (2, 'Write API specs', 0),
    (3, 'Design dashboard mockup', 1),
    (4, 'Setup CI/CD pipeline', 0),
    (5, 'Security audit', 0);
```

---

## C# Models

### TeamMember.cs
```csharp
namespace GoalApi.Models;

public class TeamMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Mood Mood { get; set; } = Mood.Neutral;
    public DateTime CreatedAt { get; set; }
}

public enum Mood
{
    Happy = 0,
    Content = 1,
    Neutral = 2,
    Sad = 3,
    Stressed = 4
}
```

### Goal.cs
```csharp
namespace GoalApi.Models;

public class Goal
{
    public int Id { get; set; }
    public int TeamMemberId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

## TypeScript Types

### types/index.ts
```typescript
export enum Mood {
  Happy = 0,
  Content = 1,
  Neutral = 2,
  Sad = 3,
  Stressed = 4
}

export interface TeamMember {
  id: number
  name: string
  mood: Mood
  goals: Goal[]
}

export interface Goal {
  id: number
  teamMemberId: number
  description: string
  isCompleted: boolean
}

export interface DashboardStats {
  totalGoals: number
  completedGoals: number
  completionPercentage: number
  moodDistribution: MoodCount[]
  dominantMood: Mood
}

export interface MoodCount {
  mood: Mood
  count: number
}
```

---

## Data Flow

### Add Goal
1. User selects member + enters description
2. Frontend validates (not empty, ≤200 chars)
3. POST `/api/goals` with `{ teamMemberId, description }`
4. Backend validates, inserts into Goals table
5. Return created goal
6. Frontend updates state, Stats recalculates

### Toggle Goal Completion
1. User clicks checkbox
2. PATCH `/api/goals/{id}/toggle`
3. Backend toggles IsCompleted
4. Return updated goal
5. Frontend updates state, Stats recalculates

### Delete Goal
1. User clicks delete
2. DELETE `/api/goals/{id}`
3. Backend removes from Goals table
4. Frontend removes from state, Stats recalculates

### Update Mood
1. User selects member + clicks mood emoji
2. PATCH `/api/members/{id}/mood` with `{ mood: number }`
3. Backend updates TeamMember.Mood
4. Return updated member
5. Frontend updates state, Stats recalculates
