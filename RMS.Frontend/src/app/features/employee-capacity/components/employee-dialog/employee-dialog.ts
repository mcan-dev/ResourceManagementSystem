import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef } from '@angular/material/dialog';
import { EmployeeService } from '../../../../core/services/employee.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-employee-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './employee-dialog.html',
  styleUrl: './employee-dialog.scss'
})
export class EmployeeDialog {

  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<EmployeeDialog>);
  private employeeService: EmployeeService = inject(EmployeeService);
  private data = inject(MAT_DIALOG_DATA, { optional: true });

  employeeForm = this.fb.group({
  name: ['', Validators.required],
  surname: ['', Validators.required],
  email: ['', [Validators.required, Validators.email]],

  gender: ['', Validators.required],

  teamId: [1, Validators.required],
  titleId: [1, Validators.required],
  status: ['aktif', Validators.required]
});

cancel(): void {
  this.dialogRef.close();
}

save(): void {

  console.log("Save çalıştı");
  console.log("Valid:", this.employeeForm.valid);
  console.log(this.employeeForm.value);

  if (this.employeeForm.invalid) {
    this.employeeForm.markAllAsTouched();
    return;
  }

const employee = {
  name: this.employeeForm.value.name,
  surname: this.employeeForm.value.surname,
  email: this.employeeForm.value.email,

  gender: this.employeeForm.value.gender,
  teamId: this.employeeForm.value.teamId,
  titleId: this.employeeForm.value.titleId,

  username: this.employeeForm.value.email, // veya farklı benzersiz bir değer

  password: null,
  userRole: 'Çalışan',

  status: this.employeeForm.value.status
};

  if (this.data?.employee) {

    this.employeeService
      .updateEmployee(this.data.employee.id, employee)
      .subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (err: any) => {
          console.error(err);
        }
      });

  } else {

    this.employeeService
      .createEmployee(employee)
      .subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (err: any) => {
          console.error(err);
        }
      });

  }

}
}
