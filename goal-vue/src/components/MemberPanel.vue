<script setup lang="ts">
import type { TeamMember } from '@/types'
import MemberCard from './MemberCard.vue'

defineProps<{
  members: TeamMember[]
}>()

const emit = defineEmits<{
  toggleGoal: [goalId: number]
  deleteGoal: [goalId: number]
}>()

function handleToggle(goalId: number) {
  emit('toggleGoal', goalId)
}

function handleDelete(goalId: number) {
  emit('deleteGoal', goalId)
}
</script>

<template>
  <div class="card bg-base-100 shadow-xl">
    <div class="card-body">
      <h2 class="card-title">Team Members</h2>

      <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mt-4">
        <MemberCard
          v-for="member in members"
          :key="member.id"
          :member="member"
          @toggle-goal="handleToggle"
          @delete-goal="handleDelete"
        />
      </div>

      <div v-if="members.length === 0" class="text-center py-8 text-base-content/50">
        No team members found
      </div>
    </div>
  </div>
</template>
