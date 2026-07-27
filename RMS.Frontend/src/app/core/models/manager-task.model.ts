import { Assignee } from './assignee.model';

export interface ManagerTask {
    taskId: number;
    taskName: string;
    projectName: string;
    taskStatus: string;
    startDate?: string;
    endDate?: string;
    assignees: Assignee[];
    totalAssignedHours: number;
    totalCompletedHours: number;
    totalRemainingHours: number;
    progressPercentage: number;
}