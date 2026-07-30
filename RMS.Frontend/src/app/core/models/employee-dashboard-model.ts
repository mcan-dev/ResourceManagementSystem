export interface EmployeeDashboardTaskDto {
  taskId: number;
  taskName: string;
  projectName: string;
  statusName: string;
  deadline: string;
  assignedHours: number;
  completedHours: number;
}

export interface EmployeeDashboardData {
  usedCapacityPercentage: number;
  activeTasksCount: number;
  remainingWorkHours: number;
  activeProjectsCount: number;
  pendingLeaveRequestsCount: number;
  upcomingTasks: EmployeeDashboardTaskDto[];
}