# Tasks: Stats Panel Two-Column Layout

**Input**: Design documents from `/specs/002-stats-panel-layout/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, quickstart.md

**Organization**: Tasks are grouped by user story to enable independent implementation and validation of each story.

**Note**: This project does NOT include automated tests per constitution principle V (No Testing).

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Path Conventions

- **Frontend**: `goal-vue/src/`
- NOTE: Desktop browsers ONLY per constitution (no mobile)

---

## Phase 1: Setup (Component Refactor Preparation)

**Purpose**: Prepare the existing component for refactoring

- [x] T001 Review existing StatsPanel.vue component structure in goal-vue/src/components/StatsPanel.vue
- [x] T002 Verify TailwindCSS color classes are available (bg-indigo-600, bg-pink-500) in goal-vue/tailwind.config.js

**Checkpoint**: Setup complete - ready to implement user stories

---

## Phase 2: User Story 1 - Goal Completion Statistics (Priority: P1) MVP

**Goal**: Display Goal Completion card on the left with blue/purple background, large percentage, and "X of Y goals complete" description

**Manual Validation**: Open dashboard, verify Goal Completion card appears on left with blue/purple background, shows percentage and goal count

### Implementation for User Story 1

- [x] T003 [US1] Create two-column grid layout structure in goal-vue/src/components/StatsPanel.vue template
- [x] T004 [US1] Implement Goal Completion card with bg-indigo-600 background in goal-vue/src/components/StatsPanel.vue
- [x] T005 [US1] Add "Goal Completion" title with white text styling in goal-vue/src/components/StatsPanel.vue
- [x] T006 [US1] Style percentage as large prominent number (text-5xl font-bold) in goal-vue/src/components/StatsPanel.vue
- [x] T007 [US1] Add "X of Y goals complete" description text in goal-vue/src/components/StatsPanel.vue
- [x] T008 [US1] Remove progress bar element (not in new design) from goal-vue/src/components/StatsPanel.vue
- [x] T009 [US1] Handle edge case: display "0%" and "0 of 0 goals complete" when no goals exist

**Checkpoint**: Goal Completion card is fully functional and verifiable independently

---

## Phase 3: User Story 2 - Team Mood Summary (Priority: P2)

**Goal**: Display Team Mood card on the right with pink background, dominant mood emoji, and member count

**Manual Validation**: Open dashboard, verify Team Mood card appears on right with pink background, shows emoji and member count

### Implementation for User Story 2

- [x] T010 [US2] Implement Team Mood card with bg-pink-500 background in goal-vue/src/components/StatsPanel.vue
- [x] T011 [US2] Add "Team Mood" title with white text styling in goal-vue/src/components/StatsPanel.vue
- [x] T012 [US2] Style dominant mood emoji as large prominent display (text-5xl) in goal-vue/src/components/StatsPanel.vue
- [x] T013 [US2] Add "Mood Name (X member(s))" description text in goal-vue/src/components/StatsPanel.vue
- [x] T014 [US2] Update dominantMoodDisplay computed property to separate emoji and text for new layout
- [x] T015 [US2] Handle edge case: display "No mood data" when no moods are set

**Checkpoint**: Both Goal Completion and Team Mood cards work independently

---

## Phase 4: User Story 3 - Mood Distribution (Priority: P3)

**Goal**: Display Mood Distribution section below the two cards, showing each mood with emoji, label, and count badge

**Manual Validation**: Open dashboard, verify Mood Distribution appears below two-column layout with all moods listed

### Implementation for User Story 3

- [x] T016 [US3] Create Mood Distribution section spanning full width below the grid in goal-vue/src/components/StatsPanel.vue
- [x] T017 [US3] Add "Mood Distribution" heading with appropriate styling in goal-vue/src/components/StatsPanel.vue
- [x] T018 [US3] Implement mood row layout with flex justify-between for each mood type in goal-vue/src/components/StatsPanel.vue
- [x] T019 [US3] Style each mood row: emoji + label on left, count badge on right in goal-vue/src/components/StatsPanel.vue
- [x] T020 [US3] Filter to only show moods with at least one member in goal-vue/src/components/StatsPanel.vue
- [x] T021 [US3] Handle edge case: show empty state or "No mood data" when no moods exist

**Checkpoint**: All three user stories are fully functional

---

## Phase 5: Polish & Cross-Cutting Concerns

**Purpose**: Final validation and cleanup

- [x] T022 Verify equal column widths across desktop viewport sizes (1024px+) in goal-vue/src/components/StatsPanel.vue
- [x] T023 Verify text contrast meets accessibility standards (white on colored backgrounds)
- [x] T024 Test edge case: no goals (should show 0% and 0 of 0)
- [x] T025 Test edge case: no moods (should show "No mood data")
- [x] T026 Run full quickstart.md manual validation checklist
- [x] T027 Clean up any unused code from previous layout in goal-vue/src/components/StatsPanel.vue

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **User Story 1 (Phase 2)**: Depends on Setup - Creates the two-column grid structure
- **User Story 2 (Phase 3)**: Depends on Phase 2 (grid structure exists)
- **User Story 3 (Phase 4)**: Depends on Phase 2 (grid structure exists)
- **Polish (Phase 5)**: Depends on all user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Creates grid layout - MUST complete first as other stories depend on this layout
- **User Story 2 (P2)**: Can start after US1 creates grid structure
- **User Story 3 (P3)**: Can start after US1 creates grid structure (independent of US2)

### Within Each User Story

- Structure before styling
- Core display before edge cases
- Manual validation at each checkpoint

### Parallel Opportunities

Since all tasks modify the same file (StatsPanel.vue), most tasks are sequential within the file. However:
- T010-T015 (US2) and T016-T021 (US3) can be worked in parallel by different developers if they coordinate on the template sections

---

## Parallel Example: User Stories 2 & 3

```bash
# After US1 creates the grid layout, US2 and US3 can be worked in parallel:
# Developer A works on Team Mood card (right column)
# Developer B works on Mood Distribution section (below grid)
# They work on different template sections to avoid conflicts
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001-T002)
2. Complete Phase 2: User Story 1 (T003-T009)
3. **STOP and VALIDATE**: Verify two-column grid layout works, Goal Completion card displays correctly
4. Deploy/demo if ready

### Incremental Delivery

1. Complete Setup → Ready to refactor
2. Add User Story 1 → Validate manually → Grid layout + Goal Completion working
3. Add User Story 2 → Validate manually → Team Mood card added
4. Add User Story 3 → Validate manually → Mood Distribution added
5. Polish → Full validation → Feature complete

---

## Summary

| Phase | Tasks | Description |
|-------|-------|-------------|
| Phase 1: Setup | T001-T002 | Preparation (2 tasks) |
| Phase 2: US1 Goal Completion | T003-T009 | MVP - Grid + Goal card (7 tasks) |
| Phase 3: US2 Team Mood | T010-T015 | Team Mood card (6 tasks) |
| Phase 4: US3 Mood Distribution | T016-T021 | Mood list section (6 tasks) |
| Phase 5: Polish | T022-T027 | Validation & cleanup (6 tasks) |

**Total Tasks**: 27
**Completed Tasks**: 27
**MVP Scope**: Phase 1-2 (9 tasks)
**Single File Change**: goal-vue/src/components/StatsPanel.vue

---

## Notes

- All implementation tasks target the same file: StatsPanel.vue
- No API changes required - using existing data structures
- No new dependencies required
- Each user story builds on the layout created in US1
- Manual validation is the only verification method (per constitution)
