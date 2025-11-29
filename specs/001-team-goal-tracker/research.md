# Research: Team Daily Goal Tracker

**Feature**: 001-team-goal-tracker
**Date**: 2025-11-29
**Status**: Complete

## Technology Decisions

### Frontend: Vue 3 + TypeScript + DaisyUI

**Decision**: Use Vue 3 with Composition API, TypeScript strict mode, and DaisyUI component library.

**Rationale**:
- Constitution mandates Vue 3, TypeScript strict, Composition API, and DaisyUI
- Composition API enables better code organization through composables
- DaisyUI provides pre-built accessible components (buttons, cards, dropdowns, checkboxes)
- TypeScript strict mode catches type errors at compile time

**Alternatives Considered**:
- Options API: Rejected (constitution requires Composition API)
- Vuetify/Quasar: Rejected (constitution specifies DaisyUI)
- JavaScript: Rejected (constitution requires TypeScript)

**Best Practices Applied**:
- Composables for reusable stateful logic (`useGoals`, `useMoods`, `useTeamMembers`, `useStats`)
- Single-file components with `<script setup lang="ts">`
- Strict TypeScript configuration per constitution
- DaisyUI semantic class names (`btn`, `card`, `dropdown`, `checkbox`)

---

### Backend: .NET 8 Web API + Dapper

**Decision**: Use ASP.NET Core 8 Minimal APIs with Dapper ORM and Repository pattern.

**Rationale**:
- User explicitly requested .NET 8 with Dapper (no Entity Framework)
- User explicitly requested Repository pattern with dependency injection
- Dapper is lightweight and provides full SQL control
- .NET 8 is the latest LTS version with performance improvements

**Alternatives Considered**:
- Entity Framework Core: Rejected (user requested Dapper specifically)
- Minimal APIs only: Would work but user requested MVC pattern with controllers
- Direct SQL in controllers: Rejected (user requested Repository pattern)

**Best Practices Applied**:
- Repository interfaces for testability and DI
- Service layer for business logic (statistics calculation)
- DTOs for API contracts (separate from domain models)
- Async/await throughout for non-blocking I/O
- Connection string from configuration (not hardcoded)

---

### Database: SQL Server on Docker

**Decision**: SQL Server 2022 running in Docker container with pre-seeded team member data.

**Rationale**:
- User specified SQL Server with Docker
- Docker provides consistent development environment
- Pre-seeded data avoids need for user management (banned by constitution)

**Configuration**:
- Port: 1433 (standard SQL Server port)
- SA Password: `YourStrong@Passw0rd` (per user specification)
- Database: `GoalTracker`

**Schema Design**:
- `TeamMembers` table: Pre-seeded with 5 members
- `Goals` table: Foreign key to TeamMembers, daily goals
- Mood stored as integer enum on TeamMembers table (simpler than separate table)

---

### State Management: Composables (No Vuex/Pinia)

**Decision**: Use Vue 3 composables with reactive refs for state management.

**Rationale**:
- Application scope is small (single dashboard page)
- Composables provide sufficient reactivity for this use case
- No need for complex state management library
- Reduces bundle size and complexity

**Pattern**:
```typescript
// useGoals.ts
export function useGoals() {
  const goals = ref<Goal[]>([])
  const loading = ref(false)

  async function fetchGoals() { ... }
  async function addGoal(memberId: number, description: string) { ... }
  async function toggleGoal(goalId: number) { ... }
  async function deleteGoal(goalId: number) { ... }

  return { goals, loading, fetchGoals, addGoal, toggleGoal, deleteGoal }
}
```

---

### HTTP Client: Axios

**Decision**: Use Axios for HTTP requests from Vue frontend to .NET API.

**Rationale**:
- Standard practice in Vue ecosystem
- Provides interceptors for error handling
- TypeScript support out of the box
- Simpler API than native fetch for this use case

**Configuration**:
- Base URL: `http://localhost:5003/api` (matches goal-api default)
- JSON content type headers
- Error handling in composables

---

### API Design: REST with JSON

**Decision**: RESTful API with JSON request/response bodies.

**Rationale**:
- Simple and widely understood
- .NET 8 has excellent JSON serialization support
- Vue/Axios handle JSON natively
- Matches user's MVC pattern request

**Endpoints Designed**:
| Method | Endpoint | Purpose |
|--------|----------|---------|
| GET | /api/dashboard | Get all data (members, goals, stats) |
| GET | /api/members | Get all team members |
| POST | /api/goals | Create a new goal |
| PATCH | /api/goals/{id}/toggle | Toggle goal completion |
| DELETE | /api/goals/{id} | Delete a goal |
| PATCH | /api/members/{id}/mood | Update member mood |

---

## Resolved Clarifications

| Topic | Resolution | Source |
|-------|------------|--------|
| Team members source | Pre-seeded in database (5 members) | Constitution (no user management) |
| Default mood | Neutral (enum value 2) | Reasonable default |
| Daily reset | Manual/session-based (no cron jobs) | Constitution (no multi-day history) |
| Goal limit per member | No limit (keep simple) | Simplicity principle |
| Concurrent access | Last-write-wins (no locking) | Small team, low risk |

---

## Package Versions (Recommended)

### Frontend (package.json)
```json
{
  "vue": "^3.4.0",
  "typescript": "^5.3.0",
  "daisyui": "^4.4.0",
  "tailwindcss": "^3.4.0",
  "axios": "^1.6.0",
  "vite": "^5.0.0"
}
```

### Backend (.csproj)
```xml
<PackageReference Include="Dapper" Version="2.1.28" />
<PackageReference Include="Microsoft.Data.SqlClient" Version="5.1.2" />
```

---

## Risk Assessment

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| SQL injection | Low | High | Use Dapper parameterized queries |
| CORS issues | Medium | Low | Configure CORS in Program.cs |
| Docker not running | Medium | High | Document in quickstart.md |
| Type mismatches | Low | Medium | TypeScript strict mode |
