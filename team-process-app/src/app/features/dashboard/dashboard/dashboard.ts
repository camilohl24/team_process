import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { AttendanceService } from '../../../core/services/attendance.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { Attendance, AttendanceStatus } from '../../../core/models/attendance.model';
import { Employee } from '../../../core/models/employee.model';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  private attendanceService = inject(AttendanceService)
  private employeeService = inject(EmployeeService)
  private cdr = inject(ChangeDetectorRef)
  attendances: Attendance[] = [];
  employees: Employee[] = [];

  totalEmployees = 0;
  presents = 0;
  late = 0;
  absents = 0;

  ngOnInit(): void {
    const today = new Date().toLocaleDateString('En-CA')
    forkJoin({
      employees: this.employeeService.getEmployees(),
      attendances: this.attendanceService.getAttendances()
    }).subscribe(({ employees, attendances }) => {
      this.employees = employees,
        this.attendances = attendances

      this.totalEmployees = employees.length;
      this.presents = attendances.filter(a => a.status === AttendanceStatus.Present && a.date.split('T')[0] === today).length;
      this.absents = attendances.filter(a => a.status === AttendanceStatus.Absent && a.date.split('T')[0] === today).length;
      this.late = attendances.filter(a => a.status === AttendanceStatus.Late && a.date.split('T')[0] === today).length;
      this.cdr.detectChanges();
    })
  }
}
