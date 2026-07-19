import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { AttendanceService } from '../../../core/services/attendance.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { LucideAngularModule, Plus, Pencil, Trash2 } from 'lucide-angular';
import { DatePipe } from '@angular/common';
import { EmployeeService } from '../../../core/services/employee.service';
import { Employee } from '../../../core/models/employee.model';
import {
  Attendance,
  AttendanceRequest,
  AttendanceStatus,
} from '../../../core/models/attendance.model';

@Component({
  selector: 'app-attendance-list',
  imports: [ReactiveFormsModule, LucideAngularModule, DatePipe],
  templateUrl: './attendance-list.html',
  styleUrl: './attendance-list.css',
})
export class AttendanceList {
  private attendanceService = inject(AttendanceService);
  private formBuilder = inject(FormBuilder);
  private employeeService = inject(EmployeeService);
  private cdr = inject(ChangeDetectorRef);
  readonly Pencil = Pencil;
  readonly Trash2 = Trash2;
  readonly Plus = Plus;
  readonly AttendanceStatus = AttendanceStatus;

  employees: Employee[] = [];
  attendances: Attendance[] = [];

  showModal = false;
  form = this.formBuilder.group({
    employeeId: [0],
    date: [''],
    status: [AttendanceStatus.Present],
  });

  selectedAttendance: Attendance | null = null;

  ngOnInit(): void {
    this.loadEmployees();
    this.loadAttendances();
  }
  loadAttendances() {
    this.attendanceService.getAttendances().subscribe((data) => {
      this.attendances = data;
      this.cdr.detectChanges();
    });
  }
  loadEmployees() {
    this.employeeService.getEmployees().subscribe((data) => {
      this.employees = data;
      this.cdr.detectChanges();
    });
  }

  createAttendance() {
    const formValue = {
      ...this.form.value,
      employeeId: Number(this.form.value.employeeId),
      status: Number(this.form.value.status),
      date: new Date(this.form.value.date!).toISOString(),
    };
    console.log('Datos enviados:', JSON.stringify(formValue));
    this.attendanceService
      .createAttendance(formValue as unknown as AttendanceRequest)
      .subscribe((data) => {
        this.attendances = [...this.attendances, data];
        this.showModal = false;
        this.form.reset();
        this.cdr.detectChanges();
      });
  }

  updateAttendance() {
    const formValue = {
      ...this.form.value,
      employeeId: Number(this.form.value.employeeId),
      status: Number(this.form.value.status),
      date: new Date(this.form.value.date!).toISOString(),
    };
    if (!this.selectedAttendance) return;
    this.attendanceService
      .updateAttendance(this.selectedAttendance.id, formValue as unknown as AttendanceRequest)
      .subscribe((data) => {
        this.loadAttendances();
        this.showModal = false;
        this.selectedAttendance = null;
        this.form.reset();
        this.cdr.detectChanges();
      });
  }

  deleteAttendance(id: number) {
    this.attendanceService.deleteAttendance(id).subscribe((data) => {
      this.attendances = this.attendances.filter((a) => a.id !== id);
      this.cdr.detectChanges();
    });
  }
  openCreateModal() {
    this.form.reset();
    this.selectedAttendance = null;
    this.showModal = true;
  }

  openEditAttendance(attendance: Attendance) {
    this.selectedAttendance = attendance;
    this.form.patchValue({
      employeeId: attendance.employeeId,
      date: attendance.date.split('T')[0],
      status: attendance.status,
    });
    this.showModal = true;
  }

  onSubmit() {
    if (this.selectedAttendance) {
      this.updateAttendance();
    } else {
      this.createAttendance();
    }
  }

  getStatusLabel(status: AttendanceStatus) {
    const labels = {
      [AttendanceStatus.Present]: 'Presente',
      [AttendanceStatus.Absent]: 'Ausente',
      [AttendanceStatus.Late]: 'Tarde',
    };
    return labels[status] ?? 'Desconocido';
  }
}
