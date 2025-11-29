import { ref } from 'vue'
import type { TeamMember } from '@/types'

const members = ref<TeamMember[]>([])
const loading = ref(false)
const error = ref<string | null>(null)

export function useTeamMembers() {
  function setMembers(newMembers: TeamMember[]) {
    members.value = newMembers
  }

  function updateMember(updatedMember: TeamMember) {
    const index = members.value.findIndex(m => m.id === updatedMember.id)
    if (index !== -1) {
      members.value[index] = updatedMember
    }
  }

  function setLoading(isLoading: boolean) {
    loading.value = isLoading
  }

  function setError(errorMessage: string | null) {
    error.value = errorMessage
  }

  return {
    members,
    loading,
    error,
    setMembers,
    updateMember,
    setLoading,
    setError
  }
}
