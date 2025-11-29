import { goalsApi } from '@/services/api'
import type { Goal } from '@/types'

export function useGoals() {
  async function addGoal(teamMemberId: number, description: string): Promise<Goal | null> {
    try {
      const response = await goalsApi.createGoal({ teamMemberId, description })
      return response.data
    } catch (error) {
      console.error('Failed to add goal:', error)
      return null
    }
  }

  async function toggleGoal(goalId: number): Promise<Goal | null> {
    try {
      const response = await goalsApi.toggleGoal(goalId)
      return response.data
    } catch (error) {
      console.error('Failed to toggle goal:', error)
      return null
    }
  }

  async function deleteGoal(goalId: number): Promise<boolean> {
    try {
      await goalsApi.deleteGoal(goalId)
      return true
    } catch (error) {
      console.error('Failed to delete goal:', error)
      return false
    }
  }

  return {
    addGoal,
    toggleGoal,
    deleteGoal
  }
}
