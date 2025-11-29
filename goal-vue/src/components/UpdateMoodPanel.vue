<script setup lang="ts">
import { ref } from 'vue'
import type { TeamMember } from '@/types'
import { Mood, MoodEmoji, MoodLabel } from '@/types'

defineProps<{
  members: TeamMember[]
}>()

const emit = defineEmits<{
  updateMood: [memberId: number, mood: Mood]
}>()

const selectedMemberId = ref<number | null>(null)
const selectedMood = ref<Mood | null>(null)
const isSubmitting = ref(false)

const moods = [
  Mood.Happy,
  Mood.Content,
  Mood.Neutral,
  Mood.Sad,
  Mood.Stressed
]

async function handleSubmit() {
  if (!selectedMemberId.value || selectedMood.value === null) {
    return
  }

  isSubmitting.value = true
  emit('updateMood', selectedMemberId.value, selectedMood.value)

  // Reset form
  selectedMood.value = null
  isSubmitting.value = false
}
</script>

<template>
  <div class="card bg-base-100 shadow-xl">
    <div class="card-body">
      <h2 class="card-title">Update Mood</h2>

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
            <span class="label-text">Select Mood</span>
          </label>
          <div class="flex gap-2 flex-wrap">
            <button
              v-for="mood in moods"
              :key="mood"
              type="button"
              class="btn btn-lg"
              :class="selectedMood === mood ? 'btn-primary' : 'btn-outline'"
              @click="selectedMood = mood"
              :title="MoodLabel[mood]"
            >
              {{ MoodEmoji[mood] }}
            </button>
          </div>
        </div>

        <button
          type="submit"
          class="btn btn-primary w-full"
          :disabled="!selectedMemberId || selectedMood === null || isSubmitting"
        >
          <span v-if="isSubmitting" class="loading loading-spinner loading-sm"></span>
          Update Mood
        </button>
      </form>
    </div>
  </div>
</template>
