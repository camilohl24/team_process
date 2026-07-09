import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { EmployeeService } from '../../../core/services/employee.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Employee, EmployeeRequest } from '../../../core/models/employee.model';
import { DepartmentService } from '../../../core/services/department.service';
import { Department } from '../../../core/models/department.model';
import { LucideAngularModule, Pencil, Trash2, Plus } from 'lucide-angular';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-list',
  imports: [ReactiveFormsModule, LucideAngularModule, DatePipe],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.css',
})
export class EmployeeList implements OnInit {
  readonly Plus = Plus;
  readonly Trash2 = Trash2;
  readonly Pencil = Pencil;
  private employeeService = inject(EmployeeService)
  private departmentService = inject(DepartmentService)
  private formBuilder = inject(FormBuilder)

  private cdr = inject(ChangeDetectorRef)

  employees: Employee[] = [];
  departments: Department[] = [];
  showModal = false;

  form = this.formBuilder.group({
    name: [''],
    lastName: [''],
    position: [''],
    entryDate: [''],
    departmentId: [0]
  })
  selectedEmployee: Employee | null = null;

  ngOnInit(): void {
    this.loadEmployees();
    this.loadDepartments();
  }
  loadDepartments() {
    this.departmentService.getDepartments().subscribe(data => {
      this.departments = data;
      this.cdr.detectChanges();
    })
  }
  loadEmployees() {
    this.employeeService.getEmployees().subscribe(data => {
      this.employees = data;
      this.cdr.detectChanges();
    })
  }


  createEmployee() {
    this.employeeService.createEmployee(this.form.value as unknown as EmployeeRequest).subscribe(data => {
      this.employees = [...this.employees, data]
      this.showModal = false
      this.form.reset();
      this.cdr.detectChanges();
    })
  }

  updateEmployee() {
    if (!this.selectedEmployee) return
    this.employeeService.updateEmployee(this.selectedEmployee.id, this.form.value as unknown as EmployeeRequest).subscribe(() => {
      this.loadEmployees();
      this.showModal = false;
      this.selectedEmployee = null
      this.form.reset();
      this.cdr.detectChanges();
    })
  }

  deleteEmployee(id: number) {
    this.employeeService.deleteEmployee(id).subscribe((data) => {
      this.employees = this.employees.filter(e => e.id !== id)
      this.cdr.detectChanges();
    })
  }
  openCreateModal() {
    this.form.reset();
    this.selectedEmployee = null
    this.showModal = true;
  }

  openEditModal(employee: Employee) {
    this.selectedEmployee = employee
    this.form.patchValue({
      name: employee.name,
      lastName: employee.lastName,
      position: employee.position,
      entryDate: employee.entryDate.split('T')[0],
      departmentId: employee.departmentId
    })
    this.showModal = true;
  }

  onSubmit() {
    if (this.selectedEmployee) {
      this.updateEmployee();
    }
    else {
      this.createEmployee();
    }
  }


}
