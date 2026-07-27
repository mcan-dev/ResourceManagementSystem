// core/models/leave-request-admin.model.ts

export interface LeaveRequestAdminModel {
  id: number;
  employeeId: number;
  employeeName: string;
  teamName: string;
  leaveTypeId: number;
  leaveTypeName: string;
  isPaid: boolean;
  startDate: string;
  endDate: string;
  totalDays: number;
  statusId: number;
  statusName: string;
}