import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from '../shared/models/customer.model';
import { DashboardService } from './dashboard.service';
import { AuthService } from '../login/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  customers!: Customer[];
  constructor(private router: Router, private dashboardService: DashboardService,
    private authService:AuthService
  ) {

  }

  ngOnInit() {
    this.loadCustomers();
  }

  loadCustomers() {
    this.dashboardService.getCustomers().subscribe((res) => {
      this.customers = res;
    })
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['login']);
  }
}
