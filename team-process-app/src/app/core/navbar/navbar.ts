import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { LucideAngularModule,LayoutDashboard,Users,Building2,CalendarCheck } from 'lucide-angular';
@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, LucideAngularModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  readonly layoutDashboard = LayoutDashboard;
  readonly users = Users;
  readonly building2 = Building2;
  readonly calendarCheck = CalendarCheck;
}
