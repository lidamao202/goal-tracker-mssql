# Implementation Plan: Stats Panel Two-Column Layout

**Branch**: `002-stats-panel-layout` | **Date**: 2025-11-29 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/002-stats-panel-layout/spec.md`

## Summary

Refactor the existing StatsPanel.vue component to display a two-column layout with Goal Completion (blue/purple background) on the left and Team Mood (pink background) on the right. The Mood Distribution section will be displayed below spanning full width. This is a UI-only change with no backend modifications required.

## Technical Context

**Language/Version**: TypeScript 5.x (Frontend only)
**Primary Dependencies**: Vue 3, TypeScript, DaisyUI, TailwindCSS (existing stack)
**Storage**: N/A (no changes - using existing API)
**Target Platform**: Desktop browsers (Chrome, Firefox, Safari, Edge) - NO mobile
**Project Type**: Frontend component refactor
**Performance Goals**: No performance impact - same data, different layout
**Constraints**: No new dependencies, desktop-only, no automated tests
**Scale/Scope**: Single component refactor (StatsPanel.vue)

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Status | Notes |
|-----------|--------|-------|
| I. Clean Code | PASS | Component refactor follows existing patterns |
| II. Simple UX | PASS | Two-column layout improves visual hierarchy |
| III. Excluded Features | PASS | UI-only change, no new features |
| IV. Minimal Dependencies | PASS | No new dependencies required |
| V. No Testing | PASS | Manual validation only |

## Project Structure

### Documentation (this feature)

```text
specs/002-stats-panel-layout/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── quickstart.md        # Phase 1 output
├── checklists/          # Spec validation
│   └── requirements.md
└── tasks.md             # Phase 2 output (/speckit.tasks command)
```

### Source Code (affected files only)

```text
goal-vue/
├── src/
│   └── components/
│       └── StatsPanel.vue    # Component to refactor
```

**Structure Decision**: This is a single-component UI refactor. No new files required. The existing StatsPanel.vue will be modified in place to implement the new two-column layout.

## Complexity Tracking

No complexity violations. This is a straightforward CSS/template refactor using existing TailwindCSS utilities.

## Design Decisions

### Layout Approach
- **Method**: TailwindCSS grid (`grid grid-cols-2 gap-4`) for the two-column layout
- **Card Styling**: Custom background colors via Tailwind classes
  - Goal Completion: `bg-indigo-600` (approximately #5B4FE8)
  - Team Mood: `bg-pink-500` (approximately #EC4899)
- **Text Contrast**: White text (`text-white`) on colored backgrounds

### Component Changes
1. **Remove**: Progress bar (not in spec)
2. **Add**: Two equal-width colored cards at top
3. **Restructure**: Mood Distribution as full-width section below
4. **Style**: Each mood row as flex with emoji, label, and count badge on right

### No Changes Required
- No API changes
- No data model changes
- No new dependencies
- No new components (refactor existing)
