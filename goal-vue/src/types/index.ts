export enum Mood {
  Happy = 0,
  Content = 1,
  Neutral = 2,
  Sad = 3,
  Stressed = 4
}

export interface Goal {
  id: number
  teamMemberId: number
  description: string
  isCompleted: boolean
}

export interface TeamMember {
  id: number
  name: string
  mood: Mood
  goals: Goal[]
}

export interface MoodCount {
  mood: Mood
  count: number
}

export interface DashboardStats {
  totalGoals: number
  completedGoals: number
  completionPercentage: number
  moodDistribution: MoodCount[]
  dominantMood: Mood | null
}

export interface DashboardResponse {
  members: TeamMember[]
  stats: DashboardStats
}

export interface CreateGoalRequest {
  teamMemberId: number
  description: string
}

export interface UpdateMoodRequest {
  mood: Mood
}

export const MoodEmoji: Record<Mood, string> = {
  [Mood.Happy]: '😀',
  [Mood.Content]: '😊',
  [Mood.Neutral]: '😐',
  [Mood.Sad]: '😞',
  [Mood.Stressed]: '😤'
}

export const MoodLabel: Record<Mood, string> = {
  [Mood.Happy]: 'Happy',
  [Mood.Content]: 'Content',
  [Mood.Neutral]: 'Neutral',
  [Mood.Sad]: 'Sad',
  [Mood.Stressed]: 'Stressed'
}
