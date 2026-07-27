import { Component } from '@angular/core';
import { PageHeaderService } from '../../core/services/page-header';
@Component({
  selector: 'app-leave-request',
  imports: [],
  templateUrl: './leave-request.html',
  styleUrl: './leave-request.scss',
})
export class LeaveRequest {
  constructor(
      private pageHeaderService: PageHeaderService // Hatanızı çözecek kritik satır
    ) {}
  
    ngOnInit() {
      
      this.pageHeaderService.setTitle('İzin Yönetimi');
    }
}
