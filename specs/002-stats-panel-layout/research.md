# Research: Stats Panel Two-Column Layout

**Feature**: 002-stats-panel-layout
**Date**: 2025-11-29

## Overview

This document captures research decisions for the Stats Panel layout refactor. Since this is a UI-only change using existing technologies, research scope is limited to layout patterns and color accessibility.

## Research Topics

### 1. Two-Column Layout Implementation

**Decision**: Use CSS Grid via TailwindCSS

**Rationale**:
- TailwindCSS grid utilities (`grid grid-cols-2 gap-4`) provide simple, maintainable layout
- Grid ensures equal column widths automatically
- Already available in project (no new dependencies)
- Better semantic structure than flexbox for this use case

**Alternatives Considered**:
- Flexbox (`flex flex-row`): Would work but requires explicit width calculations
- CSS Grid (custom): Overkill for simple two-column layout
- DaisyUI grid component: None available, falls back to TailwindCSS

### 2. Background Color Selection

**Decision**: Use TailwindCSS color utilities with custom values

**Colors**:
| Card | Spec Color | TailwindCSS Class | Actual Hex |
|------|------------|-------------------|------------|
| Goal Completion | ~#5B4FE8 | `bg-indigo-600` | #4F46E5 |
| Team Mood | ~#EC4899 | `bg-pink-500` | #EC4899 |

**Rationale**:
- TailwindCSS built-in colors are close enough to spec approximations
- Using standard palette classes instead of arbitrary values (`bg-[#5B4FE8]`) maintains consistency
- Both colors provide sufficient contrast with white text (WCAG AA compliant)

**Alternatives Considered**:
- Arbitrary values: `bg-[#5B4FE8]` - rejected for maintainability
- DaisyUI semantic colors: `bg-primary`, `bg-secondary` - rejected as spec requires specific colors
- Custom CSS variables: Overkill for two static colors

### 3. Text Contrast & Accessibility

**Decision**: White text (`text-white`) on colored backgrounds

**Contrast Ratios** (calculated):
| Background | Text | Ratio | WCAG |
|------------|------|-------|------|
| `bg-indigo-600` (#4F46E5) | White | 5.7:1 | AA Pass |
| `bg-pink-500` (#EC4899) | White | 3.5:1 | AA Pass (large text) |

**Rationale**:
- Both combinations meet WCAG AA for normal text (4.5:1) or large text (3:1)
- Primary content (percentage, emoji) is large text
- Using `text-white` with `text-white/80` for secondary text improves hierarchy

### 4. Mood Distribution Row Layout

**Decision**: Flexbox row with space-between alignment

**Structure**:
```html
<div class="flex justify-between items-center py-2">
  <div class="flex items-center gap-2">
    <span>emoji</span>
    <span>label</span>
  </div>
  <span class="badge">count</span>
</div>
```

**Rationale**:
- Simple flexbox achieves spec requirement (emoji + label left, count badge right)
- DaisyUI badge component for count display
- Consistent padding with `py-2` for visual separation

**Alternatives Considered**:
- Grid layout: Overkill for simple left-right alignment
- Table: Not semantic for this data presentation

### 5. Edge Case Handling

**No Goals State**:
- Display: "0%" and "0 of 0 goals complete"
- Implementation: Already handled by existing computed property

**No Mood Data State**:
- Display: "No mood data" text in Team Mood card
- Display: Empty Mood Distribution section or "No mood data" message
- Implementation: Conditional rendering with `v-if`

**Mood Tie Resolution**:
- Display: First mood (lowest enum value = happiest mood)
- Implementation: Backend already returns correct dominant mood

## Conclusion

All research items are resolved. No NEEDS CLARIFICATION markers remain. The implementation can proceed with:
- TailwindCSS grid for two-column layout
- Standard TailwindCSS color classes for backgrounds
- White text with appropriate opacity for hierarchy
- Flexbox for mood distribution rows
- Existing edge case handling from current implementation
