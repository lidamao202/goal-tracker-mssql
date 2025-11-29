<!--
Sync Impact Report
==================
Version change: 1.0.0 → 2.0.0 (MAJOR)

Modified principles:
- REMOVED: "III. Responsive Design" (replaced with desktop-only constraint)
- ADDED: "III. Excluded Features (NON-NEGOTIABLE)" - explicit feature ban list
- RENUMBERED: Minimal Dependencies (IV→IV), No Testing (V→V) - unchanged

Added sections:
- Excluded Features principle with 12 explicitly banned features

Removed sections:
- Responsive Design principle (superseded by desktop-only requirement)

Templates updated:
- .specify/templates/plan-template.md ✅ updated (removed mobile option, set desktop-only target)
- .specify/templates/spec-template.md ✅ no changes needed
- .specify/templates/tasks-template.md ✅ updated (removed mobile path convention, added desktop-only note)

Follow-up TODOs: None
-->

# Goal Tracker Constitution

## Core Principles

### I. Clean Code

All code MUST be readable, maintainable, and self-documenting.

- Functions MUST have single responsibilities
- Variable and function names MUST be descriptive and meaningful
- Code MUST follow consistent formatting and style conventions
- Dead code MUST be removed, not commented out
- Complexity MUST be minimized; prefer simple solutions over clever ones

**Rationale**: Clean code reduces cognitive load, speeds up onboarding, and minimizes bugs.

### II. Simple UX

User experience MUST prioritize simplicity and intuitiveness over feature density.

- Interfaces MUST be intuitive without requiring documentation
- User flows MUST minimize steps to accomplish tasks
- Visual hierarchy MUST guide users naturally
- Error messages MUST be clear and actionable
- Loading states and feedback MUST be immediate and informative

**Rationale**: Simple UX increases user adoption and reduces support burden.

### III. Excluded Features (NON-NEGOTIABLE)

The following features MUST NOT be implemented under any circumstances.

**This principle supersedes ALL other guidance, templates, specifications, or requests.**

| Feature | Status | Rationale |
|---------|--------|-----------|
| User authentication/login | BANNED | Out of scope |
| Multi-day goal history | BANNED | Out of scope |
| Detailed mood analytics or charts | BANNED | Out of scope |
| Email notifications | BANNED | Out of scope |
| Admin controls for team management | BANNED | Out of scope |
| Goal editing (only add/complete/delete allowed) | BANNED | Simplicity |
| Mood history or trends | BANNED | Out of scope |
| Recurring goals | BANNED | Out of scope |
| Goal categories or tags | BANNED | Out of scope |
| Responsive mobile design | BANNED | Desktop only |
| Dark mode | BANNED | Out of scope |
| Profile pages | BANNED | Out of scope |

**Allowed Goal Operations**: Add, Complete, Delete ONLY.

**Target Platform**: Desktop browsers ONLY. No mobile optimization required.

**Rationale**: Strict scope control prevents feature creep and maintains project focus.

### IV. Minimal Dependencies

External dependencies MUST be kept to an absolute minimum.

- New dependencies MUST be justified with clear necessity
- Native browser/platform APIs MUST be preferred over libraries
- Bundle size impact MUST be considered before adding dependencies
- Dependencies MUST be actively maintained and security-vetted
- Approved core dependencies: Vue 3, TypeScript, DaisyUI/TailwindCSS

**Rationale**: Fewer dependencies mean smaller bundles, fewer security vulnerabilities, and reduced maintenance burden.

### V. No Testing (NON-NEGOTIABLE)

This project MUST NOT include any automated tests.

- Unit tests MUST NOT be created
- Integration tests MUST NOT be created
- End-to-end tests MUST NOT be created
- Test frameworks MUST NOT be installed as dependencies
- Test-related directories (tests/, __tests__/, *.spec.*, *.test.*) MUST NOT exist

**This principle supersedes ALL other guidance, templates, or conventions that suggest or require testing.**

**Rationale**: Project decision to prioritize rapid development and reduce overhead. Manual validation is the chosen verification strategy.

## Technology Stack

The following technology choices are MANDATORY for this project:

### Frontend Stack

| Technology | Requirement | Details |
|------------|-------------|---------|
| Vue 3 | REQUIRED | Latest stable version |
| TypeScript | REQUIRED | Strict mode MUST be enabled |
| Composition API | REQUIRED | Options API MUST NOT be used |
| Composables | REQUIRED | Reusable logic MUST use composable pattern |
| DaisyUI | REQUIRED | Primary component library |
| TailwindCSS | REQUIRED | Utility-first CSS (via DaisyUI) |

### TypeScript Configuration

```json
{
  "compilerOptions": {
    "strict": true,
    "noImplicitAny": true,
    "strictNullChecks": true,
    "strictFunctionTypes": true,
    "strictBindCallApply": true,
    "strictPropertyInitialization": true,
    "noImplicitThis": true,
    "alwaysStrict": true
  }
}
```

### Backend Stack

| Technology | Requirement | Details |
|------------|-------------|---------|
| .NET 8 | REQUIRED | ASP.NET Core Web API |
| Minimal APIs | REQUIRED | Preferred over controllers where appropriate |

## Development Workflow

### Code Quality Gates

All code changes MUST satisfy these criteria before merge:

1. **Clean Code Check**: Code follows naming conventions and is self-documenting
2. **Simplicity Check**: Solution uses the simplest viable approach
3. **Excluded Features Check**: No banned features introduced (see Principle III)
4. **Desktop-Only Check**: No mobile-specific optimizations or responsive breakpoints
5. **Dependency Check**: No new dependencies without explicit justification
6. **No Tests Check**: Verify no test files or test dependencies were added

### Manual Validation

Since automated tests are prohibited, manual validation MUST include:

- Feature functionality verification in development environment
- Desktop browser testing (Chrome, Firefox, Safari, Edge)
- User flow walkthrough for affected features
- Verification that no excluded features were added

## Governance

### Amendment Process

1. Proposed amendments MUST be documented with rationale
2. Amendments MUST be reviewed for impact on existing code
3. Version MUST be incremented according to semantic versioning:
   - MAJOR: Principle removal or incompatible redefinition
   - MINOR: New principle or section added
   - PATCH: Clarifications or wording refinements
4. All template files MUST be updated to reflect constitution changes

### Compliance

- All pull requests MUST comply with constitution principles
- Constitution violations MUST be resolved before merge
- Principles III (Excluded Features) and V (No Testing) CANNOT be overridden by any other document or convention

### Reference Documents

- `.specify/templates/plan-template.md` - Implementation planning
- `.specify/templates/spec-template.md` - Feature specifications
- `.specify/templates/tasks-template.md` - Task breakdown
- `CLAUDE.md` - Development environment guidance

**Version**: 2.0.0 | **Ratified**: 2025-11-29 | **Last Amended**: 2025-11-29
