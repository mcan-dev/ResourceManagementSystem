import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SettingsService } from '../../core/services/settings.service';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

export function matchValidator(matchTo: string, reverse?: boolean) {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control.parent && reverse) {
      const c = (control.parent?.controls as any)[matchTo] as AbstractControl;
      if (c) {
        c.updateValueAndValidity();
      }
      return null;
    }
    return !!control.parent &&
      !!control.parent.value &&
      control.value === (control.parent?.controls as any)[matchTo].value
      ? null
      : { matching: true };
  };
}

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [
    CommonModule, 
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatSlideToggleModule,
    FormsModule
  ], 
  templateUrl: './settings.html',
  styleUrls: ['./settings.scss']
})
export class Settings implements OnInit {
  private fb = inject(FormBuilder);
  private settingsService = inject(SettingsService);
  private snackBar = inject(MatSnackBar);
  private cdr = inject(ChangeDetectorRef); 

  emailForm!: FormGroup;
  passwordForm!: FormGroup;

  currentEmployeeId = 1; 

  isEmailLoading = false;
  isPasswordLoading = false;
  isDarkTheme = false;

  ngOnInit() {
    this.initEmailForm();
    this.initPasswordForm();
    
  }

  private initEmailForm() {
    this.emailForm = this.fb.group({
      newEmail: ['', [Validators.required, Validators.email, matchValidator('confirmEmail', true)]],
      confirmEmail: ['', [Validators.required, Validators.email, matchValidator('newEmail')]]
    });
  }

  private initPasswordForm() {
    this.passwordForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, matchValidator('confirmPassword', true)]],
      confirmPassword: ['', [Validators.required, matchValidator('newPassword')]]
    });
  }

  onEmailUpdate() {
    if (this.emailForm.valid) {
      this.isEmailLoading = true;
      const request = {
        employeeId: this.currentEmployeeId,
        newEmail: this.emailForm.value.newEmail,
        confirmEmail: this.emailForm.value.confirmEmail
      };
      
      this.settingsService.changeEmail(request).subscribe({
        next: () => {
          this.snackBar.open('Email başarıyla güncellendi.', 'Kapat', { duration: 3000 });
          this.emailForm.reset();
          this.isEmailLoading = false;
          this.cdr.detectChanges(); // <-- Angular'ı ekranı yenilemesi için tetikler
        },
        error: () => {
          this.snackBar.open('Email güncellenirken hata oluştu.', 'Kapat', { duration: 3000 });
          this.isEmailLoading = false;
          this.cdr.detectChanges(); // <-- Angular'ı ekranı yenilemesi için tetikler
        }
      });
    }
  }

  onPasswordUpdate() {
    if (this.passwordForm.valid) {
      this.isPasswordLoading = true;
      const request = {
        employeeId: this.currentEmployeeId,
        currentPassword: this.passwordForm.value.currentPassword,
        newPassword: this.passwordForm.value.newPassword,
        confirmPassword: this.passwordForm.value.confirmPassword
      };

      this.settingsService.changePassword(request).subscribe({
        next: () => {
          this.snackBar.open('Şifre başarıyla güncellendi.', 'Kapat', { duration: 3000 });
          this.passwordForm.reset();
          this.isPasswordLoading = false;
          this.cdr.detectChanges(); // <-- Aynı çözüm buraya da eklendi
        },
      error: (err) => {
        // Backend'den gelen özel hata mesajını (ex.Message) yakalayıp ekrana basıyoruz
        const errorMessage = err.error?.message || 'Şifre güncellenirken hata oluştu.';
        this.snackBar.open(errorMessage, 'Kapat', { duration: 3000 });
        this.isPasswordLoading = false;
        this.cdr.detectChanges(); 
      }
      });
    }
  }
}
  
