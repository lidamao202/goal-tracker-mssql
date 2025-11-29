<script setup lang="ts">
import { onMounted } from 'vue'
import { dashboardApi } from '@/services/api'
import { useTeamMembers } from '@/composables/useTeamMembers'
import { useStats } from '@/composables/useStats'
import { useGoals } from '@/composables/useGoals'
import { useMoods } from '@/composables/useMoods'
import { Mood } from '@/types'
import StatsPanel from '@/components/StatsPanel.vue'
import MemberPanel from '@/components/MemberPanel.vue'
import AddGoalPanel from '@/components/AddGoalPanel.vue'
import UpdateMoodPanel from '@/components/UpdateMoodPanel.vue'

const { members, setMembers, setLoading, setError, loading, error } = useTeamMembers()
const { stats, setStats } = useStats()
const { addGoal, toggleGoal, deleteGoal } = useGoals()
const { updateMood } = useMoods()

async function fetchDashboard() {
  setLoading(true)
  setError(null)
  try {
    const response = await dashboardApi.getDashboard()
    setMembers(response.data.members)
    setStats(response.data.stats)
  } catch (err) {
    setError('Failed to load dashboard data')
    console.error(err)
  } finally {
    setLoading(false)
  }
}

async function handleAddGoal(memberId: number, description: string) {
  const result = await addGoal(memberId, description)
  if (result) {
    await fetchDashboard()
  }
}

async function handleToggleGoal(goalId: number) {
  const result = await toggleGoal(goalId)
  if (result) {
    await fetchDashboard()
  }
}

async function handleDeleteGoal(goalId: number) {
  const result = await deleteGoal(goalId)
  if (result) {
    await fetchDashboard()
  }
}

async function handleUpdateMood(memberId: number, mood: Mood) {
  const result = await updateMood(memberId, mood)
  if (result) {
    await fetchDashboard()
  }
}

onMounted(() => {
  fetchDashboard()
})
</script>

<template>
  <div class="min-h-screen bg-base-200 p-6">
    <header class="mb-6">
      <h1 class="text-3xl font-bold text-center">Team Daily Goal Tracker</h1>
    </header>

    <div v-if="loading" class="flex justify-center items-center h-64">
      <span class="loading loading-spinner loading-lg"></span>
    </div>

    <div v-else-if="error" class="alert alert-error max-w-md mx-auto">
      <span>{{ error }}</span>
      <button class="btn btn-sm" @click="fetchDashboard">Retry</button>
    </div>

    <div v-else class="grid grid-cols-1 lg:grid-cols-4 gap-6">
      <!-- Stats Panel -->
      <div class="lg:col-span-1">
        <StatsPanel :stats="stats" />
      </div>

      <!-- Control Panels -->
      <div class="lg:col-span-1 space-y-6">
        <AddGoalPanel :members="members" @add-goal="handleAddGoal" />
        <UpdateMoodPanel :members="members" @update-mood="handleUpdateMood" />
      </div>

      <!-- Member Panel -->
      <div class="lg:col-span-2">
        <MemberPanel
          :members="members"
          @toggle-goal="handleToggleGoal"
          @delete-goal="handleDeleteGoal"
        />
      </div>
    </div>
  </div>
</template>
