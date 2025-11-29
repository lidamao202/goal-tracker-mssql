<script setup lang="ts">
import { computed } from 'vue'
import type { TeamMember } from '@/types'
import { MoodEmoji } from '@/types'
import GoalItem from './GoalItem.vue'

const props = defineProps<{
  member: TeamMember
}>()

const emit = defineEmits<{
  toggleGoal: [goalId: number]
  deleteGoal: [goalId: number]
}>()

const completionCount = computed(() => {
  const completed = props.member.goals.filter(g => g.isCompleted).length
  const total = props.member.goals.length
  return `${completed}/${total}`
})

const moodEmoji = computed(() => MoodEmoji[props.member.mood])

function handleToggle(goalId: number) {
  emit('toggleGoal', goalId)
}

function handleDelete(goalId: number) {
  emit('deleteGoal', goalId)
}
</script>

<template>
  <div class="card bg-base-100 shadow-md">
    <div class="card-body p-4">
      <div class="flex items-center justify-between">
        <h3 class="card-title text-lg">
          <span class="text-2xl">{{ moodEmoji }}</span>
          {{ member.name }}
        </h3>
        <span class="badge badge-outline">{{ completionCount }}</span>
      </div>

      <div class="mt-2">
        <div v-if="member.goals.length === 0" class="text-base-content/50 text-sm py-2">
          No goals yet
        </div>
        <GoalItem
          v-for="goal in member.goals"
          :key="goal.id"
          :goal="goal"
          @toggle="handleToggle"
          @delete="handleDelete"
        />
      </div>
    </div>
  </div>
</template>
