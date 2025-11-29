# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Goal Tracker is a full-stack application with a .NET 8 Web API backend and Vue 3 frontend.

## Architecture

```
goal-tracker-mssql/
├── goal-api/          # .NET 8 Web API backend
└── goal-vue/          # Vue 3 + Vite frontend
```

## Development Commands

### Backend (goal-api)

```bash
cd goal-api
dotnet build          # Build the project
dotnet run            # Run the API (http://localhost:5003)
```

The API includes Swagger UI at `/swagger` in development mode.

### Frontend (goal-vue)

```bash
cd goal-vue
npm install           # Install dependencies
npm run dev           # Start dev server
npm run build         # Production build
npm run preview       # Preview production build
```

## Tech Stack

- **Backend**: .NET 8, ASP.NET Core Minimal APIs, Swagger/OpenAPI
- **Frontend**: Vue 3 (Composition API), Vite 5
