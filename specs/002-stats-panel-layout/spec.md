# Feature Specification: Stats Panel Two-Column Layout

**Feature Branch**: `002-stats-panel-layout`
**Created**: 2025-11-29
**Status**: Draft
**Input**: User description: "In the team statistic panel, the layout should be divided into two columns between goal completion and mood distribution. And below that it should show mood distribution. Goal completion has background blue/purple, complete percentage, and description eg. 2 of 3 goals complete. Team mood has background pink."

## User Scenarios & Validation *(mandatory)*

### User Story 1 - View Goal Completion Statistics (Priority: P1)

As a team member, I want to see goal completion statistics displayed prominently with a colored background so I can quickly understand the team's progress at a glance.

**Why this priority**: Goal completion is the primary metric for team productivity tracking and should be immediately visible.

**Manual Validation**: Open the dashboard and verify the Goal Completion card appears on the left side with a blue/purple background, showing percentage and goal count.

**Acceptance Scenarios**:

1. **Given** the Stats Panel is displayed, **When** goals exist for the team, **Then** the Goal Completion card shows on the left with blue/purple background, large percentage text, and "X of Y goals complete" description
2. **Given** the Stats Panel is displayed, **When** viewing Goal Completion, **Then** the percentage is calculated as (completed goals / total goals * 100) rounded to whole number

---

### User Story 2 - View Team Mood Summary (Priority: P2)

As a team member, I want to see the dominant team mood displayed prominently with a pink background so I can quickly gauge team morale.

**Why this priority**: Team mood provides immediate insight into team wellbeing alongside productivity metrics.

**Manual Validation**: Open the dashboard and verify the Team Mood card appears on the right side with a pink background, showing the dominant mood emoji and member count.

**Acceptance Scenarios**:

1. **Given** the Stats Panel is displayed, **When** team members have moods set, **Then** the Team Mood card shows on the right with pink background, large mood emoji, and "Mood Name (X member(s))" description
2. **Given** multiple members have the same mood, **When** viewing Team Mood, **Then** it displays the most common mood with the count of members sharing that mood

---

### User Story 3 - View Mood Distribution (Priority: P3)

As a team member, I want to see a breakdown of all mood types below the summary cards so I can understand the full distribution of team morale.

**Why this priority**: Detailed mood distribution provides context beyond just the dominant mood.

**Manual Validation**: Open the dashboard and verify the Mood Distribution section appears below the two-column layout, showing each mood with its count.

**Acceptance Scenarios**:

1. **Given** the Stats Panel is displayed, **When** team members have various moods, **Then** the Mood Distribution section shows below the Goal Completion and Team Mood cards
2. **Given** the Mood Distribution is displayed, **When** viewing the list, **Then** each mood type shows the emoji, mood label, and a count badge aligned to the right

---

### Edge Cases

- What happens when there are no goals? Display "0%" and "0 of 0 goals complete" in the Goal Completion card
- What happens when no moods are set? Display "No mood data" in the Team Mood card and empty Mood Distribution section
- What happens when multiple moods tie for most common? Display the first one (lowest enum value - happiest mood wins ties)

## Requirements *(mandatory)*

### Functional Requirements

**Layout Structure**
- **FR-001**: Stats Panel MUST display a two-column layout at the top with Goal Completion on the left and Team Mood on the right
- **FR-002**: Stats Panel MUST display a Mood Distribution section below the two-column layout spanning full width
- **FR-003**: Both columns MUST have equal width and consistent height

**Goal Completion Card**
- **FR-004**: Goal Completion card MUST have a blue/purple gradient or solid background color
- **FR-005**: Goal Completion card MUST display "Goal Completion" as the section title
- **FR-006**: Goal Completion card MUST display the completion percentage as a large prominent number (e.g., "67%")
- **FR-007**: Goal Completion card MUST display "X of Y goals complete" as descriptive text below the percentage

**Team Mood Card**
- **FR-008**: Team Mood card MUST have a pink/magenta background color
- **FR-009**: Team Mood card MUST display "Team Mood" as the section title
- **FR-010**: Team Mood card MUST display the dominant mood emoji prominently
- **FR-011**: Team Mood card MUST display "Mood Name (X member(s))" below the emoji

**Mood Distribution Section**
- **FR-012**: Mood Distribution section MUST have a "Mood Distribution" heading
- **FR-013**: Mood Distribution MUST display each mood type that has at least one member
- **FR-014**: Each mood row MUST show the emoji, mood label text, and a count badge on the right
- **FR-015**: The count badge MUST display the number of members with that mood

### Key Entities

- **Goal Completion Stats**: Percentage complete, completed count, total count
- **Team Mood**: Dominant mood type, emoji representation, member count for that mood
- **Mood Distribution**: List of mood types with their respective member counts

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Users can identify the team's goal completion percentage within 1 second of viewing the panel
- **SC-002**: Users can identify the dominant team mood within 1 second of viewing the panel
- **SC-003**: The two-column layout displays correctly without visual overlap or misalignment
- **SC-004**: Color contrast meets accessibility standards (text readable on colored backgrounds)
- **SC-005**: Layout is consistent across desktop browser viewport sizes (1024px and above)

## Assumptions

- The existing Stats Panel component will be refactored rather than replaced
- Color values: Blue/purple for Goal Completion (approximately #5B4FE8), Pink for Team Mood (approximately #EC4899)
- Desktop-only layout per project constitution (no mobile responsive requirements)
- Light theme only per project constitution
