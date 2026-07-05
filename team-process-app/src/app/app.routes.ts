import { Routes } from '@angular/router';
import { DepartmentList } from './features/departments/department-list/department-list';
import { EmployeeList } from './features/employees/employee-list/employee-list';
import { AttendanceList } from './features/attendances/attendance-list/attendance-list';
import { Dashboard } from './features/dashboard/dashboard/dashboard';

export const routes: Routes = [
    {path:'departments', component: DepartmentList},
    {path:'employees', component: EmployeeList},
    {path:'attendances', component: AttendanceList},
    {path:'dashboard', component: Dashboard},
    {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    {path: '**', redirectTo: 'dashboard'}
];
