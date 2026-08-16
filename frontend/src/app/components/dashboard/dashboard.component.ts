import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  activeTab: 'customers' | 'accounts' | 'transfers' = 'customers';
  companyCode: string = 'BNK1';

  setTab(tab: 'customers' | 'accounts' | 'transfers'): void {
    this.activeTab = tab;
  }
}
