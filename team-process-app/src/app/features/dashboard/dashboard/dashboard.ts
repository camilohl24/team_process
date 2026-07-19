import { ChangeDetectorRef, Component, inject, ElementRef, ViewChild } from '@angular/core';
import { AttendanceService } from '../../../core/services/attendance.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { Attendance, AttendanceStatus } from '../../../core/models/attendance.model';
import { Employee } from '../../../core/models/employee.model';
import { forkJoin } from 'rxjs';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);


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
  @ViewChild('myChart') chartRef!: ElementRef<HTMLCanvasElement>;

  totalEmployees = 0;
  presents = 0;
  late = 0;
  absents = 0;


  createChart(): void {
    const byMont = this.attendances.reduce((acc, a) => {
      const month = new Date(a.date).getMonth();
      acc[month] = (acc[month] || 0) + 1;
      return acc;
    }, {} as Record<number, number>);

    const ctx = this.chartRef.nativeElement.getContext('2d')
    new Chart(ctx!, {
      type: 'bar',
      data: {
        labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        datasets: [{
          label: 'Asistencias',
          data: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11].map(m => byMont[m] || 0),
          backgroundColor: '#6366F1'
        }]
      }
    })

  }

  ngOnInit(): void {
    const today = new Date().toLocaleDateString('en-CA')
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
      this.createChart();
      this.cdr.detectChanges();
    })
  }
}
