import axios from 'axios'
import type { DashboardResponse, Goal, TeamMember, CreateGoalRequest, UpdateMoodRequest } from '@/types'

const api = axios.create({
  baseURL: 'http://localhost:5003/api',
  headers: {
    'Content-Type': 'application/json'
  }
})

export const dashboardApi = {
  getDashboard: () => api.get<DashboardResponse>('/dashboard'),
}

export const membersApi = {
  getMembers: () => api.get<TeamMember[]>('/members'),
  updateMood: (id: number, request: UpdateMoodRequest) =>
    api.patch<TeamMember>(`/members/${id}/mood`, request),
}

export const goalsApi = {
  createGoal: (request: CreateGoalRequest) =>
    api.post<Goal>('/goals', request),
  toggleGoal: (id: number) =>
    api.patch<Goal>(`/goals/${id}/toggle`),
  deleteGoal: (id: number) =>
    api.delete(`/goals/${id}`),
}

export default api
