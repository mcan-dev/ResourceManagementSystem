import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { LoginRequest } from '../../../core/models/login.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatIconButton
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {

  loginForm: FormGroup;
  hidePassword = true;

  constructor(private fb: FormBuilder, private router: Router, private authService: AuthService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  onSubmit()  {
    console.log("YENİ LOGIN TS ÇALIŞIYOR");
    console.log("Butona basıldı");

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const loginData: LoginRequest = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    };

  this.authService.login(loginData).subscribe({
      next: (response: any) => {
        console.log('Login başarılı:', response);

        const role = response.role || response.Role;

       
        localStorage.setItem('userRole', role);
        localStorage.setItem('userId', response.userId);
        localStorage.setItem('userName', response.userName || response.UserName);
              
        if (role === 'Sistem Yöneticisi' || role === 'Proje Yöneticisi') {
       
          this.router.navigate(['/dashboard']); 
        } else {
          
          this.router.navigate(['/employee-dashboard']); 
        }
      },
      error: (error) => {
        console.log(error);
        console.log("Status:", error.status);
        console.log("Body:", error.error);
        alert(JSON.stringify(error.error, null, 2));
      }
    });
  }
}