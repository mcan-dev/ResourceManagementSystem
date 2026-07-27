import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AsyncPipe } from '@angular/common'; // 1. Ekleme: Bunu içeri aktarıyoruz
import { PageHeaderService } from '../../services/page-header';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {

  constructor(public pageHeaderService: PageHeaderService,
     private router: Router) {}

  logout() {
    this.router.navigate(['/login']);
  }

}