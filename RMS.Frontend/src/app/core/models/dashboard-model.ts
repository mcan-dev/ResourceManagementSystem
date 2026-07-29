export interface AdminDashboardData {
  totalCapacityHours: number;
  usedCapacityHours: number;
  availableCapacityHours: number;
  usagePercentage: number;
  upcomingProjectsCount: number;
  pendingLeaveRequestsCount: number;
  teamCapacities: TeamCapacity[];
  upcomingTasks: UpcomingTask[];
  criticalEmployees: CriticalEmployee[];
}

export interface TeamCapacity {
  teamId: number;
  teamName: string;
  employeeCount: number;
  averageCapacity: number;
}

export interface UpcomingTask {
  taskId: number;
  taskName: string;
  projectName: string;
  assignees: string;
  statusName: string;
  deadline: string;
}

export interface CriticalEmployee {
  employeeId: number;
  employeeInitials: string;
  employeeName: string;
  titleName: string;
  usedCapacityPercentage: number;
}