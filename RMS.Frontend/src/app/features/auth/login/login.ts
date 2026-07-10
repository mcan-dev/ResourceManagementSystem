import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
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
  constructor(private fb: FormBuilder, private router: Router) {

    this.loginForm = this.fb.group({

      email: ['', [Validators.required, Validators.email]],

      password: ['', [Validators.required]]
      

    });

  }

  onSubmit() {

  console.log("Butona basıldı");

  if (this.loginForm.invalid) {
    this.loginForm.markAllAsTouched();
    return;
  }

  console.log("Dashboard'a gidiyor");

  this.router.navigateByUrl('/dashboard')
  .then(result => console.log('Navigate Result:', result))
  .catch(err => console.error(err));

}
}