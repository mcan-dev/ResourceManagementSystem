import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule], // DİKKAT: Burası boştu, RouterModule eklendi
  templateUrl: './sidebar.html',
  styleUrls: ['./sidebar.scss']
})
export class Sidebar implements OnInit {
  
  employeeName: string = 'Deniz Koç';
  employeeRole: string = 'Proje Yöneticisi';

  constructor(private router: Router) {}

  ngOnInit(): void {
    // İleride kullanıcı bilgilerini servisten çekeceğimiz yer
  }

  // İsim ve soyismin ilk harflerini almak için (Örn: "Deniz Koç" -> "DK")
  getInitials(name: string): string {
    if (!name) return '';
    const parts = name.split(' ');
    if (parts.length === 1) return parts[0].substring(0, 2).toUpperCase();
    return (parts[0][0] + parts[parts.length - 1][0]).toUpperCase();
  }

  logout() {
    this.router.navigate(['/login']);
  }
}