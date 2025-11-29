# Quickstart: Stats Panel Two-Column Layout

**Feature**: 002-stats-panel-layout
**Date**: 2025-11-29

## Prerequisites

The following must already be running (from feature 001):

1. **SQL Server** (Docker container on port 1433)
2. **Backend API** (http://localhost:5003)
3. **Frontend Dev Server** (http://localhost:5173)

If not running, start them:

```bash
# Terminal 1: Start SQL Server
docker compose -f docker/docker-compose.yml up -d

# Terminal 2: Start Backend
cd goal-api
dotnet run

# Terminal 3: Start Frontend
cd goal-vue
npm run dev
```

## Manual Validation Steps

### 1. View Goal Completion Card (FR-004 to FR-007)

1. Open http://localhost:5173 in a desktop browser
2. Locate the Stats Panel (left side of dashboard)
3. Verify the Goal Completion card:
   - [ ] Appears on the **left** side of a two-column layout
   - [ ] Has a **blue/purple background** (indigo color)
   - [ ] Displays "Goal Completion" as the title
   - [ ] Shows a **large percentage number** (e.g., "67%")
   - [ ] Shows "X of Y goals complete" text below the percentage

### 2. View Team Mood Card (FR-008 to FR-011)

1. In the same Stats Panel view
2. Verify the Team Mood card:
   - [ ] Appears on the **right** side of the two-column layout
   - [ ] Has a **pink background** (magenta/pink color)
   - [ ] Displays "Team Mood" as the title
   - [ ] Shows a **large mood emoji** prominently
   - [ ] Shows "Mood Name (X member(s))" text below the emoji

### 3. View Mood Distribution Section (FR-012 to FR-015)

1. Below the two-column layout
2. Verify the Mood Distribution section:
   - [ ] Spans **full width** below the two cards
   - [ ] Has "Mood Distribution" heading
   - [ ] Each mood row shows: emoji, mood label, and count badge on the right
   - [ ] Only moods with at least one member are displayed

### 4. Edge Case: No Goals

1. Delete all goals from the database (or use empty team)
2. Verify:
   - [ ] Goal Completion shows "0%" and "0 of 0 goals complete"

### 5. Edge Case: No Mood Data

1. Set all team members to have no mood (null)
2. Verify:
   - [ ] Team Mood card shows "No mood data"
   - [ ] Mood Distribution section shows empty or "No mood data"

### 6. Layout Verification (FR-001 to FR-003)

1. Resize browser window (desktop sizes only: 1024px+)
2. Verify:
   - [ ] Two columns remain **equal width**
   - [ ] No visual overlap or misalignment
   - [ ] Layout is consistent across different desktop viewport sizes

### 7. Visual Verification (SC-001 to SC-004)

1. First impression test:
   - [ ] Goal completion percentage identifiable within 1 second
   - [ ] Dominant team mood identifiable within 1 second
   - [ ] Text is readable on colored backgrounds (sufficient contrast)

## Expected Layout

```
+--------------------------------+--------------------------------+
|       GOAL COMPLETION          |         TEAM MOOD              |
|       (blue/purple bg)         |         (pink bg)              |
|                                |                                |
|           67%                  |            emoji                |
|    2 of 3 goals complete       |      Happy (3 members)         |
+--------------------------------+--------------------------------+
|                                                                 |
|                      MOOD DISTRIBUTION                          |
|                                                                 |
|   emoji Happy ................................ [3]              |
|   emoji Content .............................. [2]              |
|   emoji Stressed ............................. [1]              |
|                                                                 |
+-----------------------------------------------------------------+
```

## Troubleshooting

**Issue**: Colors don't appear correctly
- Check TailwindCSS is loading (inspect element for applied classes)
- Verify no CSS conflicts in browser dev tools

**Issue**: Layout not two-column
- Check grid classes are applied: `grid grid-cols-2 gap-4`
- Ensure parent container has sufficient width

**Issue**: Data not displaying
- Check browser console for API errors
- Verify backend is running and accessible
- Check network tab for failed requests
