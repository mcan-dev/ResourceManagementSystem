import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, Router } from '@angular/router'; // Router buraya eklendi
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [
    RouterLink, 
    CommonModule, 
    RouterModule,
    RouterLinkActive
  ],
  templateUrl: './sidebar.html',
  styleUrls: ['./sidebar.scss']
})
export class Sidebar implements OnInit {
  userRole: string = '';
  Role: string = '';
  userName: string = '';
  userInitials: string = '';

  // Router'ı kullanabilmek için constructor içine enjekte ettik
  constructor(private router: Router) {}

  ngOnInit() {
    // Tarayıcıdan giriş yapan kullanıcının bilgilerini çekiyoruz
    this.userRole = localStorage.getItem('userRole') || 'Personel';
    this.userName = localStorage.getItem('userName') || 'İsimsiz Kullanıcı';
    
    // İsmin baş harflerini alıp avatar için hazırlıyoruz (Örn: Deniz Koç -> DK)
    if (this.userName !== 'İsimsiz Kullanıcı') {
      const names = this.userName.trim().split(' ');
      if (names.length >= 2) {
        this.userInitials = (names[0][0] + names[names.length - 1][0]).toUpperCase();
      } else {
        this.userInitials = names[0].substring(0, 2).toUpperCase();
      }
    } else {
      this.userInitials = 'DK';
    }
  }

  // ÇIKIŞ YAP FONKSİYONU
  logout(): void {
    // Oturum verilerini temizle
    localStorage.clear();
    sessionStorage.clear();
    
    // Giriş sayfasına yönlendir
    this.router.navigate(['/login']);
  }
}