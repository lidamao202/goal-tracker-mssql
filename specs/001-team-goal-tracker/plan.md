# Implementation Plan: Team Daily Goal Tracker

**Branch**: `001-team-goal-tracker` | **Date**: 2025-11-29 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-team-goal-tracker/spec.md`

## Summary

Build a team daily goal tracker dashboard that displays team members, their goals, and moods in a single-page application. The frontend is a Vue 3 + TypeScript SPA using DaisyUI components, communicating with a .NET 8 Web API backend that uses Dapper ORM for SQL Server data access. The dashboard includes four main panels: Stats (completion % and mood distribution), Add Goal, Update Mood, and Member Cards.

## Technical Context

**Language/Version**: TypeScript 5.x (Frontend), C# / .NET 8 (Backend)
**Primary Dependencies**:
- Frontend: Vue 3, TypeScript, DaisyUI, TailwindCSS, Axios
- Backend: ASP.NET Core 8, Dapper, Microsoft.Data.SqlClient
**Storage**: SQL Server (Docker container, port 1433, sa password: YourStrong@Passw0rd)
**Target Platform**: Desktop browsers (Chrome, Firefox, Safari, Edge) - NO mobile
**Project Type**: Web application (separate frontend + backend)
**Performance Goals**: Stats Panel updates within 1 second of any change, single-page load under 2 seconds
**Constraints**: No authentication, no multi-day history, desktop-only, no automated tests
**Scale/Scope**: Small team tracker (up to 10 members), single dashboard page

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Status | Notes |
|-----------|--------|-------|
| I. Clean Code | PASS | Will follow naming conventions, single responsibility, remove dead code |
| II. Simple UX | PASS | Single-page dashboard, intuitive panels, immediate feedback |
| III. Excluded Features | PASS | No auth, no history, no analytics, no mobile, no dark mode, no goal editing |
| IV. Minimal Dependencies | PASS | Using approved stack (Vue 3, TypeScript, DaisyUI) + justified additions (Dapper, Axios) |
| V. No Testing | PASS | No test frameworks, no test files, manual validation only |

**Dependency Justification**:
- **Dapper**: Lightweight ORM for SQL Server access (simpler than EF, per user request)
- **Axios**: HTTP client for API calls (standard Vue practice, minimal overhead)
- **Microsoft.Data.SqlClient**: Required for SQL Server connection with Dapper

## Project Structure

### Documentation (this feature)

```text
specs/001-team-goal-tracker/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output (OpenAPI spec)
└── tasks.md             # Phase 2 output (/speckit.tasks command)
```

### Source Code (repository root)

```text
goal-api/                    # .NET 8 Web API Backend
├── Controllers/
│   └── GoalTrackerController.cs
├── Models/
│   ├── TeamMember.cs
│   ├── Goal.cs
│   └── Mood.cs
├── Repositories/
│   ├── ITeamMemberRepository.cs
│   ├── TeamMemberRepository.cs
│   ├── IGoalRepository.cs
│   └── GoalRepository.cs
├── Services/
│   ├── IGoalTrackerService.cs
│   └── GoalTrackerService.cs
├── Data/
│   └── DapperContext.cs
├── DTOs/
│   ├── TeamMemberDto.cs
│   ├── GoalDto.cs
│   ├── CreateGoalRequest.cs
│   ├── UpdateMoodRequest.cs
│   └── DashboardStatsDto.cs
├── appsettings.json
├── appsettings.Development.json
└── Program.cs

goal-vue/                    # Vue 3 Frontend
├── src/
│   ├── components/
│   │   ├── StatsPanel.vue
│   │   ├── AddGoalPanel.vue
│   │   ├── UpdateMoodPanel.vue
│   │   ├── MemberPanel.vue
│   │   ├── MemberCard.vue
│   │   └── GoalItem.vue
│   ├── composables/
│   │   ├── useGoals.ts
│   │   ├── useMoods.ts
│   │   ├── useTeamMembers.ts
│   │   └── useStats.ts
│   ├── services/
│   │   └── api.ts
│   ├── types/
│   │   └── index.ts
│   ├── App.vue
│   └── main.ts
├── index.html
├── package.json
├── tsconfig.json
├── tailwind.config.js
└── vite.config.ts

docker/                      # Docker configuration
└── docker-compose.yml       # SQL Server container setup

scripts/                     # Database scripts
└── init.sql                 # Schema + seed data
```

**Structure Decision**: Web application structure with separate `goal-api/` (backend) and `goal-vue/` (frontend) directories. This matches the existing project layout per CLAUDE.md and supports independent development of each tier.

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| Repository Pattern | User explicitly requested | Direct Dapper calls would work but violate request |
| Dapper ORM | User explicitly requested | EF Core is heavier than needed for simple CRUD |
| Separate DTOs | Clean API contracts | Using domain models directly would couple layers |

## Technology Decisions Summary

### Frontend Architecture
- **State Management**: Composables with reactive refs (no Vuex/Pinia needed for this scope)
- **HTTP Client**: Axios for API calls
- **Styling**: DaisyUI components + TailwindCSS utilities
- **Build Tool**: Vite (default Vue 3 setup)

### Backend Architecture
- **Pattern**: Repository + Service + Controller (per user request)
- **Data Access**: Dapper with raw SQL queries
- **DI**: Built-in ASP.NET Core dependency injection
- **API Style**: REST with JSON payloads

### Database
- **Engine**: SQL Server 2022 (Docker)
- **Schema**: 3 tables (TeamMembers, Goals, Moods enum as int)
- **Seed Data**: 5 pre-configured team members
