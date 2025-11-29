import { ref } from 'vue'
import type { DashboardStats } from '@/types'

const stats = ref<DashboardStats>({
  totalGoals: 0,
  completedGoals: 0,
  completionPercentage: 0,
  moodDistribution: [],
  dominantMood: null
})

export function useStats() {
  function setStats(newStats: DashboardStats) {
    stats.value = newStats
  }

  return {
    stats,
    setStats
  }
}
