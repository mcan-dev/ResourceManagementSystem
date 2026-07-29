export interface LeaveRequestModel {
  id: number;
  employeeId: number;
  startDate: string; // ISO 8601 format (YYYY-MM-DD)
  endDate: string; // ISO 8601 format
  leaveTypeId: number;
  leaveTypeName?: string; // UI'da göstermek için (opsiyonel)
  statusId: number;
  statusName?: string; // UI'da göstermek için (opsiyonel)
  description: string;
  createdAt: string;
}

export interface CreateLeaveRequestDto {
  employeeId: number;
  startDate: string;
  endDate: string;
  leaveTypeId: number;
  description: string;
}