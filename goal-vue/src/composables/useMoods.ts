import { membersApi } from '@/services/api'
import type { TeamMember } from '@/types'
import { Mood } from '@/types'

export function useMoods() {
  async function updateMood(memberId: number, mood: Mood): Promise<TeamMember | null> {
    try {
      const response = await membersApi.updateMood(memberId, { mood })
      return response.data
    } catch (error) {
      console.error('Failed to update mood:', error)
      return null
    }
  }

  return {
    updateMood
  }
}
