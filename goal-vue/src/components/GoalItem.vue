<script setup lang="ts">
import type { Goal } from '@/types'

const props = defineProps<{
  goal: Goal
}>()

const emit = defineEmits<{
  toggle: [goalId: number]
  delete: [goalId: number]
}>()

function handleToggle() {
  emit('toggle', props.goal.id)
}

function handleDelete() {
  emit('delete', props.goal.id)
}
</script>

<template>
  <div class="flex items-center gap-2 py-2 border-b border-base-300 last:border-b-0">
    <input
      type="checkbox"
      class="checkbox checkbox-primary checkbox-sm"
      :checked="goal.isCompleted"
      @change="handleToggle"
    />
    <span
      class="flex-1"
      :class="{ 'line-through text-base-content/50': goal.isCompleted }"
    >
      {{ goal.description }}
    </span>
    <button
      class="btn btn-ghost btn-xs text-error"
      @click="handleDelete"
      title="Delete goal"
    >
      ✕
    </button>
  </div>
</template>
