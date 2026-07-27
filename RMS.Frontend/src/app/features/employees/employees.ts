import { Component } from '@angular/core';
import { PageHeaderService } from '../../core/services/page-header';

@Component({
  selector: 'app-employees',
  imports: [],
  templateUrl: './employees.html',
  styleUrl: './employees.scss',
})
export class Employees {

constructor(
    private pageHeaderService: PageHeaderService // Hatanızı çözecek kritik satır
  ) {}

  ngOnInit() {
    
    this.pageHeaderService.setTitle('Ekip ve Kapasite Yönetimi');
  }
}