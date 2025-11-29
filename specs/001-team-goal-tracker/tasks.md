# Tasks: Team Daily Goal Tracker

**Input**: Design documents from `/specs/001-team-goal-tracker/`
**Prerequisites**: plan.md ✅, spec.md ✅, research.md ✅, data-model.md ✅, contracts/openapi.yaml ✅

**Organization**: Tasks are grouped by user story to enable independent implementation and validation of each story.

**Note**: This project does NOT include automated tests per constitution principle V (No Testing).

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Path Conventions

- **Backend**: `goal-api/` (.NET 8 Web API)
- **Frontend**: `goal-vue/` (Vue 3 + Vite)
- **Docker**: `docker/` (SQL Server container)
- **Scripts**: `scripts/` (Database initialization)
- NOTE: Desktop browsers ONLY per constitution (no mobile)

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization, Docker setup, and basic structure

- [x] T001 Create docker/docker-compose.yml with SQL Server 2022 container (port 1433, sa:YourStrong@Passw0rd)
- [x] T002 Create scripts/init.sql with database schema (TeamMembers, Goals tables) and seed data (5 members, sample goals)
- [x] T003 [P] Initialize .NET 8 Web API project in goal-api/ with `dotnet new webapi`
- [x] T004 [P] Initialize Vue 3 + Vite project in goal-vue/ with `npm create vue@latest`
- [x] T005 Add backend dependencies: Dapper, Microsoft.Data.SqlClient to goal-api/goal-api.csproj
- [x] T006 Add frontend dependencies: axios, tailwindcss, daisyui to goal-vue/package.json
- [x] T007 [P] Configure TypeScript strict mode in goal-vue/tsconfig.json per constitution
- [x] T008 [P] Configure TailwindCSS with DaisyUI plugin in goal-vue/tailwind.config.js

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

### Backend Foundation

- [x] T009 Create goal-api/Models/Mood.cs enum (Happy=0, Content=1, Neutral=2, Sad=3, Stressed=4)
- [x] T010 [P] Create goal-api/Models/TeamMember.cs (Id, Name, Mood, CreatedAt)
- [x] T011 [P] Create goal-api/Models/Goal.cs (Id, TeamMemberId, Description, IsCompleted, CreatedAt)
- [x] T012 Create goal-api/Data/DapperContext.cs with SQL Server connection factory
- [x] T013 [P] Create goal-api/Repositories/ITeamMemberRepository.cs interface
- [x] T014 [P] Create goal-api/Repositories/IGoalRepository.cs interface
- [x] T015 Create goal-api/Repositories/TeamMemberRepository.cs implementing ITeamMemberRepository with Dapper
- [x] T016 Create goal-api/Repositories/GoalRepository.cs implementing IGoalRepository with Dapper
- [x] T017 [P] Create goal-api/DTOs/TeamMemberDto.cs (Id, Name, Mood, Goals[])
- [x] T018 [P] Create goal-api/DTOs/GoalDto.cs (Id, TeamMemberId, Description, IsCompleted)
- [x] T019 [P] Create goal-api/DTOs/DashboardStatsDto.cs (TotalGoals, CompletedGoals, CompletionPercentage, MoodDistribution[], DominantMood)
- [x] T020 [P] Create goal-api/DTOs/MoodCountDto.cs (Mood, Count)
- [x] T021 Create goal-api/Services/IGoalTrackerService.cs interface (GetDashboard, GetMembers, CreateGoal, ToggleGoal, DeleteGoal, UpdateMood)
- [x] T022 Create goal-api/Services/GoalTrackerService.cs implementing IGoalTrackerService with stats calculation logic
- [x] T023 Configure dependency injection in goal-api/Program.cs (register repositories, services, DapperContext)
- [x] T024 Configure CORS in goal-api/Program.cs to allow requests from Vue dev server (localhost:5173)
- [x] T025 Configure appsettings.Development.json with SQL Server connection string

### Frontend Foundation

- [x] T026 Create goal-vue/src/types/index.ts with TypeScript interfaces (Mood enum, TeamMember, Goal, DashboardStats, MoodCount)
- [x] T027 Create goal-vue/src/services/api.ts with Axios instance configured for http://localhost:5003/api
- [x] T028 Create goal-vue/src/composables/useTeamMembers.ts (reactive members state, fetchMembers)
- [x] T029 Create goal-vue/src/composables/useStats.ts (reactive stats state, computed statistics)
- [x] T030 Setup base goal-vue/src/App.vue with dashboard layout structure (header + 4 panel grid)
- [x] T031 Configure goal-vue/src/main.ts to mount App with proper imports

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - View Team Dashboard (Priority: P1) 🎯 MVP

**Goal**: Display dashboard with all team members, their goals, and moods with real-time statistics

**Manual Validation**: Open the application and verify all four panels display correctly: Stats Panel shows team statistics, Add Goal panel placeholder is visible, Update Mood panel placeholder is visible, and Member panel shows all team member cards with their goals.

### Backend Implementation for US1

- [x] T032 [US1] Create goal-api/DTOs/DashboardResponse.cs (Members[], Stats)
- [x] T033 [US1] Implement GetDashboard in GoalTrackerService.cs (fetch members with goals, calculate stats)
- [x] T034 [US1] Create goal-api/Controllers/GoalTrackerController.cs with [Route("api")]
- [x] T035 [US1] Add GET /dashboard endpoint in GoalTrackerController.cs returning DashboardResponse

### Frontend Implementation for US1

- [x] T036 [P] [US1] Create goal-vue/src/components/StatsPanel.vue (completion %, X of Y goals, mood distribution)
- [x] T037 [P] [US1] Create goal-vue/src/components/GoalItem.vue (checkbox, description, delete placeholder)
- [x] T038 [US1] Create goal-vue/src/components/MemberCard.vue (name, mood emoji, goals list, X/Y count)
- [x] T039 [US1] Create goal-vue/src/components/MemberPanel.vue (grid of MemberCard components)
- [x] T040 [P] [US1] Create goal-vue/src/components/AddGoalPanel.vue placeholder (title + "Coming soon" message)
- [x] T041 [P] [US1] Create goal-vue/src/components/UpdateMoodPanel.vue placeholder (title + "Coming soon" message)
- [x] T042 [US1] Implement dashboard data fetching in App.vue using api.ts GET /dashboard
- [x] T043 [US1] Wire StatsPanel, MemberPanel, AddGoalPanel placeholder, UpdateMoodPanel placeholder into App.vue

**Checkpoint**: At this point, User Story 1 should be fully functional and verifiable independently - dashboard displays all members, goals, and statistics

---

## Phase 4: User Story 2 - Add Goals for Team Members (Priority: P2)

**Goal**: Allow users to add daily goals for any team member via dropdown and text input

**Manual Validation**: Select a team member from the dropdown, enter a goal description, click "Add Goal", and verify the goal appears in that member's card in the Member panel and stats update.

### Backend Implementation for US2

- [x] T044 [US2] Create goal-api/DTOs/CreateGoalRequest.cs (TeamMemberId, Description)
- [x] T045 [US2] Implement CreateGoal in GoalTrackerService.cs (validate, insert, return created goal)
- [x] T046 [US2] Add POST /goals endpoint in GoalTrackerController.cs

### Frontend Implementation for US2

- [x] T047 [US2] Create goal-vue/src/composables/useGoals.ts (addGoal function with API call)
- [x] T048 [US2] Implement AddGoalPanel.vue with member dropdown, description input (200 char limit), and Add Goal button
- [x] T049 [US2] Add validation in AddGoalPanel.vue (require member selection and description)
- [x] T050 [US2] Connect AddGoalPanel to useGoals composable and refresh dashboard after add

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently - can view dashboard AND add new goals

---

## Phase 5: User Story 3 - Mark Goals Complete (Priority: P3)

**Goal**: Allow users to toggle goal completion status via checkbox with immediate visual feedback

**Manual Validation**: Click the checkbox next to a goal in a member's card and verify the goal shows as completed (strikethrough) and the completion count and Stats Panel percentage update.

### Backend Implementation for US3

- [x] T051 [US3] Implement ToggleGoal in GoalTrackerService.cs (find goal, flip IsCompleted, return updated)
- [x] T052 [US3] Add PATCH /goals/{id}/toggle endpoint in GoalTrackerController.cs

### Frontend Implementation for US3

- [x] T053 [US3] Add toggleGoal function to useGoals.ts composable
- [x] T054 [US3] Implement checkbox click handler in GoalItem.vue calling toggleGoal
- [x] T055 [US3] Add completed styling (checkmark, strikethrough) to GoalItem.vue
- [x] T056 [US3] Refresh Stats and Member panels after toggle

**Checkpoint**: At this point, User Stories 1, 2, AND 3 should all work - can view, add, and complete goals

---

## Phase 6: User Story 4 - Update Team Member Mood (Priority: P4)

**Goal**: Allow users to update any team member's current mood via dropdown and emoji selection

**Manual Validation**: Select a team member from the Update Mood dropdown, click a mood emoji, click "Update Mood", and verify the member's card and Stats Panel mood distribution update.

### Backend Implementation for US4

- [x] T057 [US4] Create goal-api/DTOs/UpdateMoodRequest.cs (Mood)
- [x] T058 [US4] Implement UpdateMood in GoalTrackerService.cs (find member, update mood, return updated)
- [x] T059 [US4] Add PATCH /members/{id}/mood endpoint in GoalTrackerController.cs

### Frontend Implementation for US4

- [x] T060 [US4] Create goal-vue/src/composables/useMoods.ts (updateMood function with API call)
- [x] T061 [US4] Implement UpdateMoodPanel.vue with member dropdown, 5 mood emoji buttons, and Update Mood button
- [x] T062 [US4] Add validation in UpdateMoodPanel.vue (require member selection and mood selection)
- [x] T063 [US4] Connect UpdateMoodPanel to useMoods composable and refresh dashboard after update

**Checkpoint**: At this point, User Stories 1-4 should all work - full mood tracking functionality

---

## Phase 7: User Story 5 - Delete Goals (Priority: P5)

**Goal**: Allow users to delete goals that are no longer needed

**Manual Validation**: Click the delete action on a goal and verify the goal is removed from the member's card and all statistics update.

### Backend Implementation for US5

- [x] T064 [US5] Implement DeleteGoal in GoalTrackerService.cs (find goal, delete, return success)
- [x] T065 [US5] Add DELETE /goals/{id} endpoint in GoalTrackerController.cs

### Frontend Implementation for US5

- [x] T066 [US5] Add deleteGoal function to useGoals.ts composable
- [x] T067 [US5] Implement delete button/icon in GoalItem.vue with click handler
- [x] T068 [US5] Refresh Stats and Member panels after delete

**Checkpoint**: All user stories should now be independently functional

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories and final validation

- [x] T069 [P] Add loading states to all panels during API calls
- [x] T070 [P] Add error handling with user-friendly messages in all API interactions
- [x] T071 [P] Add "No goals yet" message in MemberCard.vue when member has zero goals
- [x] T072 [P] Add "No mood data" display in StatsPanel when no moods are set
- [x] T073 Verify all mood emoji mappings are correct (😀😊😐😞😤)
- [x] T074 Verify 200-character limit on goal description input
- [x] T075 Add page title "Team Daily Goal Tracker" in App.vue
- [x] T076 Final manual validation using quickstart.md checklist
- [x] T077 Code cleanup - remove unused imports, dead code, console.logs

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3-7)**: All depend on Foundational phase completion
  - User stories can proceed in parallel (if staffed)
  - Or sequentially in priority order (P1 → P2 → P3 → P4 → P5)
- **Polish (Phase 8)**: Depends on all user stories being complete

### User Story Dependencies

| Story | Can Start After | Dependencies |
|-------|-----------------|--------------|
| US1 (P1) | Phase 2 complete | None - MVP core |
| US2 (P2) | Phase 2 complete | None (uses dashboard refresh) |
| US3 (P3) | Phase 2 complete | GoalItem.vue from US1 |
| US4 (P4) | Phase 2 complete | None (uses dashboard refresh) |
| US5 (P5) | Phase 2 complete | GoalItem.vue from US1 |

### Within Each User Story

1. Backend models/DTOs first
2. Backend service implementation
3. Backend controller endpoint
4. Frontend composable (if new)
5. Frontend component implementation
6. Integration and manual validation

### Parallel Opportunities

- T003, T004: Backend and frontend project init
- T007, T008: TypeScript and TailwindCSS config
- T010, T011: TeamMember and Goal models
- T013, T014: Repository interfaces
- T017, T018, T019, T020: All DTOs
- T036, T037: StatsPanel and GoalItem
- T040, T041: Panel placeholders
- All Phase 8 [P] tasks

---

## Task Summary

| Phase | Task Range | Count | Description |
|-------|------------|-------|-------------|
| Setup | T001-T008 | 8 | Project initialization |
| Foundational | T009-T031 | 23 | Core infrastructure |
| US1 (P1) | T032-T043 | 12 | View Team Dashboard |
| US2 (P2) | T044-T050 | 7 | Add Goals |
| US3 (P3) | T051-T056 | 6 | Mark Goals Complete |
| US4 (P4) | T057-T063 | 7 | Update Mood |
| US5 (P5) | T064-T068 | 5 | Delete Goals |
| Polish | T069-T077 | 9 | Final improvements |
| **Total** | | **77** | |

---

## Notes

- [P] tasks = different files, no dependencies, can run in parallel
- [Story] label maps task to specific user story for traceability
- Each user story should be independently completable and verifiable
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently (manual validation)
- Avoid: vague tasks, same file conflicts, cross-story dependencies that break independence
- NO automated tests per constitution principle V
