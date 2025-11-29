<script setup lang="ts">
import { ref } from 'vue'
import type { TeamMember } from '@/types'

defineProps<{
  members: TeamMember[]
}>()

const emit = defineEmits<{
  addGoal: [memberId: number, description: string]
}>()

const selectedMemberId = ref<number | null>(null)
const description = ref('')
const isSubmitting = ref(false)

async function handleSubmit() {
  if (!selectedMemberId.value || !description.value.trim()) {
    return
  }

  isSubmitting.value = true
  emit('addGoal', selectedMemberId.value, description.value.trim())

  // Reset form
  description.value = ''
  isSubmitting.value = false
}
</script>

<template>
  <div class="card bg-base-100 shadow-xl">
    <div class="card-body">
      <h2 class="card-title">Add Goal</h2>

      <form @submit.prevent="handleSubmit" class="space-y-4 mt-2">
        <div class="form-control">
          <label class="label">
            <span class="label-text">Team Member</span>
          </label>
          <select
            v-model="selectedMemberId"
            class="select select-bordered w-full"
            required
          >
            <option :value="null" disabled>Select a member</option>
            <option
              v-for="member in members"
              :key="member.id"
              :value="member.id"
            >
              {{ member.name }}
            </option>
          </select>
        </div>

        <div class="form-control">
          <label class="label">
            <span class="label-text">Goal Description</span>
            <span class="label-text-alt">{{ description.length }}/200</span>
          </label>
          <input
            v-model="description"
            type="text"
            class="input input-bordered w-full"
            placeholder="Enter goal description"
            maxlength="200"
            required
          />
        </div>

        <button
          type="submit"
          class="btn btn-primary w-full"
          :disabled="!selectedMemberId || !description.trim() || isSubmitting"
        >
          <span v-if="isSubmitting" class="loading loading-spinner loading-sm"></span>
          Add Goal
        </button>
      </form>
    </div>
  </div>
</template>
