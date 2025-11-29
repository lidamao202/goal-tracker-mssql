# Quickstart: Team Daily Goal Tracker

**Feature**: 001-team-goal-tracker
**Date**: 2025-11-29

## Prerequisites

- **Docker Desktop**: For SQL Server container
- **Node.js 18+**: For Vue frontend
- **.NET 8 SDK**: For API backend
- **Git**: For version control

## Quick Start (3 Steps)

### 1. Start Database

```bash
# From repository root
cd docker
docker-compose up -d
```

Wait ~30 seconds for SQL Server to initialize, then run the schema:

```bash
# Connect to SQL Server and run init script
docker exec -i goal-tracker-sql /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P 'YourStrong@Passw0rd' \
  -i /scripts/init.sql
```

**Verify**: `docker ps` shows container running on port 1433

### 2. Start Backend API

```bash
# From repository root
cd goal-api
dotnet restore
dotnet run
```

**Verify**: Open http://localhost:5003/swagger - should show API documentation

### 3. Start Frontend

```bash
# From repository root (new terminal)
cd goal-vue
npm install
npm run dev
```

**Verify**: Open http://localhost:5173 - should show the Goal Tracker dashboard

---

## Detailed Setup

### Docker Compose Configuration

Create `docker/docker-compose.yml`:

```yaml
version: '3.8'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: goal-tracker-sql
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
      - MSSQL_PID=Developer
    ports:
      - "1433:1433"
    volumes:
      - sqlserver-data:/var/opt/mssql
      - ../scripts:/scripts
    healthcheck:
      test: /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -Q "SELECT 1"
      interval: 10s
      timeout: 3s
      retries: 10

volumes:
  sqlserver-data:
```

### Database Schema

Create `scripts/init.sql`:

```sql
-- Create database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GoalTracker')
BEGIN
    CREATE DATABASE GoalTracker;
END
GO

USE GoalTracker;
GO

-- Drop existing tables if they exist (for clean setup)
IF OBJECT_ID('Goals', 'U') IS NOT NULL DROP TABLE Goals;
IF OBJECT_ID('TeamMembers', 'U') IS NOT NULL DROP TABLE TeamMembers;
GO

-- TeamMembers table
CREATE TABLE TeamMembers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Mood INT NOT NULL DEFAULT 2,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT CK_TeamMembers_Mood CHECK (Mood >= 0 AND Mood <= 4)
);

-- Goals table
CREATE TABLE Goals (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TeamMemberId INT NOT NULL,
    Description NVARCHAR(200) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_Goals_TeamMembers FOREIGN KEY (TeamMemberId)
        REFERENCES TeamMembers(Id) ON DELETE CASCADE
);

CREATE INDEX IX_Goals_TeamMemberId ON Goals(TeamMemberId);
GO

-- Seed team members
INSERT INTO TeamMembers (Name, Mood) VALUES
    ('Alice', 1),
    ('Bob', 2),
    ('Charlie', 0),
    ('Diana', 2),
    ('Eve', 3);

-- Seed sample goals
INSERT INTO Goals (TeamMemberId, Description, IsCompleted) VALUES
    (1, 'Complete project documentation', 1),
    (1, 'Review pull requests', 0),
    (2, 'Fix login bug', 1),
    (2, 'Update dependencies', 0),
    (3, 'Design dashboard mockup', 1);
GO

PRINT 'Database setup complete!';
```

### Backend Configuration

Update `goal-api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=GoalTracker;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Frontend Configuration

Create `goal-vue/.env.development`:

```env
VITE_API_BASE_URL=http://localhost:5003/api
```

---

## Verification Checklist

After setup, verify each component works:

### Database
- [ ] Docker container running: `docker ps | grep goal-tracker-sql`
- [ ] Can connect: `docker exec -it goal-tracker-sql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd' -Q "SELECT COUNT(*) FROM GoalTracker.dbo.TeamMembers"`
- [ ] Shows 5 team members

### Backend API
- [ ] Swagger UI loads: http://localhost:5003/swagger
- [ ] GET /api/dashboard returns data
- [ ] GET /api/members returns 5 members

### Frontend
- [ ] Dev server running: http://localhost:5173
- [ ] Dashboard displays Stats Panel with completion %
- [ ] Member cards show names and moods
- [ ] Can add a new goal
- [ ] Can toggle goal completion
- [ ] Can update member mood

---

## Common Issues

### Docker: Port 1433 already in use
```bash
# Check what's using the port
netstat -ano | findstr :1433
# Stop existing SQL Server or change port in docker-compose.yml
```

### Backend: Connection refused to database
```bash
# Ensure Docker container is running
docker ps
# Check container logs
docker logs goal-tracker-sql
# Wait 30s after starting container for SQL Server to initialize
```

### Frontend: CORS errors
Ensure backend has CORS configured in `Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// ...
app.UseCors();
```

### Frontend: Cannot find module errors
```bash
cd goal-vue
rm -rf node_modules package-lock.json
npm install
```

---

## Development Workflow

1. **Make backend changes**: Edit C# files, `dotnet run` auto-reloads
2. **Make frontend changes**: Edit Vue/TS files, Vite hot-reloads
3. **Database changes**: Update `scripts/init.sql`, re-run in container
4. **Manual testing**: Use the dashboard UI to verify all features

---

## URLs Reference

| Service | URL | Purpose |
|---------|-----|---------|
| Frontend | http://localhost:5173 | Dashboard UI |
| Backend API | http://localhost:5003/api | REST endpoints |
| Swagger | http://localhost:5003/swagger | API documentation |
| SQL Server | localhost:1433 | Database (Docker) |
