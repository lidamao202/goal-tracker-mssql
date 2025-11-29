<script setup lang="ts">
import { computed } from 'vue'
import type { DashboardStats } from '@/types'
import { Mood, MoodEmoji, MoodLabel } from '@/types'

const props = defineProps<{
  stats: DashboardStats
}>()

const completionText = computed(() => {
  return `${props.stats.completedGoals} of ${props.stats.totalGoals} goals complete`
})

const dominantMoodEmoji = computed(() => {
  if (props.stats.dominantMood === null || props.stats.dominantMood === undefined) {
    return null
  }
  return MoodEmoji[props.stats.dominantMood]
})

const dominantMoodText = computed(() => {
  if (props.stats.dominantMood === null || props.stats.dominantMood === undefined) {
    return 'No mood data'
  }
  const label = MoodLabel[props.stats.dominantMood]
  const count = props.stats.moodDistribution.find(m => m.mood === props.stats.dominantMood)?.count ?? 0
  return `${label} (${count} member${count !== 1 ? 's' : ''})`
})

const hasMoodData = computed(() => {
  return props.stats.dominantMood !== null && props.stats.dominantMood !== undefined
})

const filteredMoodDistribution = computed(() => {
  return props.stats.moodDistribution.filter(m => m.count > 0)
})
</script>

<template>
  <div class="card bg-base-100 shadow-xl">
    <div class="card-body">
      <h2 class="card-title">Team Statistics</h2>

      <!-- Two-column layout: Goal Completion + Team Mood -->
      <div class="grid grid-cols-2 gap-4 mt-4">
        <!-- Goal Completion Card (Left) -->
        <div class="rounded-xl bg-indigo-600 p-6 text-white">
          <h3 class="text-sm font-medium text-white/80 mb-2">Goal Completion</h3>
          <div class="text-5xl font-bold">{{ stats.completionPercentage }}%</div>
          <div class="text-sm text-white/80 mt-2">{{ completionText }}</div>
        </div>

        <!-- Team Mood Card (Right) -->
        <div class="rounded-xl bg-pink-500 p-6 text-white">
          <h3 class="text-sm font-medium text-white/80 mb-2">Team Mood</h3>
          <div v-if="hasMoodData" class="text-5xl">{{ dominantMoodEmoji }}</div>
          <div class="text-sm text-white/80 mt-2">{{ dominantMoodText }}</div>
        </div>
      </div>

      <!-- Mood Distribution Section (Full Width Below) -->
      <div class="mt-6">
        <h3 class="font-semibold mb-3">Mood Distribution</h3>
        <div v-if="filteredMoodDistribution.length > 0" class="space-y-2">
          <div
            v-for="moodCount in filteredMoodDistribution"
            :key="moodCount.mood"
            class="flex justify-between items-center py-2 border-b border-base-200 last:border-b-0"
          >
            <div class="flex items-center gap-2">
              <span class="text-xl">{{ MoodEmoji[moodCount.mood as Mood] }}</span>
              <span>{{ MoodLabel[moodCount.mood as Mood] }}</span>
            </div>
            <span class="badge badge-primary">{{ moodCount.count }}</span>
          </div>
        </div>
        <div v-else class="text-base-content/50">
          No mood data
        </div>
      </div>
    </div>
  </div>
</template>
