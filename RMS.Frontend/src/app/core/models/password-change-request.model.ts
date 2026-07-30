export interface PasswordChangeRequest {
  employeeId: number;
  currentPassword: string;
  newPassword: string;
  confirmPassword: string;
}