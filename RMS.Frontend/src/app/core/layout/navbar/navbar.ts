import { Component } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [AsyncPipe], 
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.scss']
})
export class Navbar {
  
  public pageHeaderService = {
    title$: new BehaviorSubject<string>('Resource Management System')
  };

  // Çıkış yapma mantığı Sidebar'a taşındığı için constructor(Router) ve logout() metodu tamamen silindi.
}