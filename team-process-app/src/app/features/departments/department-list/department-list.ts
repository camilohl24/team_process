import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { DepartmentService } from '../../../core/services/department.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Department, DepartmentRequest } from '../../../core/models/department.model';
import { LucideAngularModule, Pencil, Trash2, Plus } from 'lucide-angular';

@Component({
  selector: 'app-department-list',
  imports: [ReactiveFormsModule, LucideAngularModule],
  templateUrl: './department-list.html',
  styleUrl: './department-list.css',
})
export class DepartmentList implements OnInit {
  readonly Pencil = Pencil;
  readonly Trash2 = Trash2;
  readonly Plus = Plus;
  private departmentService = inject(DepartmentService)
  private formBuilder = inject(FormBuilder)

  private cdr = inject(ChangeDetectorRef);

  departments: Department[] = [];

  showModal = false;
  selectedDepartment: Department | null = null;

  form = this.formBuilder.group({
    name: ['']
  });

  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe(data => {
      this.departments = data;
      this.cdr.detectChanges();
    });
  }

  createDepartment() {
    this.departmentService.createDepartment(this.form.value as DepartmentRequest).subscribe(data => {
      this.departments = [...this.departments, data]
      this.showModal = false;
      this.form.reset();
      this.cdr.detectChanges();
    })
  }

  updateDepartment() {
    if (!this.selectedDepartment) return
    this.departmentService.updateDepartment(this.selectedDepartment.id, this.form.value as DepartmentRequest).subscribe(() => {
      this.departments = this.departments.map(d =>
        d.id === this.selectedDepartment!.id
          ? { ...d, ...this.form.value as DepartmentRequest }
          : d
      )
      this.showModal = false
      this.selectedDepartment = null
      this.form.reset()
      this.cdr.detectChanges();
    })
  }

  deleteDepartment(id: number) {
    this.departmentService.deleteDepartment(id).subscribe((data) => {
      this.departments = this.departments.filter((d) => d.id !== id);
      this.cdr.detectChanges();
    })
  }

  openCreateModal() {
    this.form.reset()
    this.selectedDepartment = null
    this.showModal = true
  }

  openEditModal(department: Department) {
    this.selectedDepartment = department
    this.form.patchValue({
      name: department.name
    })
    this.showModal = true
  }

  onSubmit() {
    if (this.selectedDepartment) {
      this.updateDepartment();
    }
    else {
      this.createDepartment();
    }
  }

}
