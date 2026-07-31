export interface LeaveRequestModel {
  id: number;
  employeeId: number;
  startDate: string;
  endDate: string; 
  leaveTypeId: number;
  leaveTypeName?: string; 
  statusId: number;
  statusName?: string;
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