import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Employee, EmployeeRequest } from "../models/employee.model";

@Injectable({
    providedIn: 'root'
})

export class EmployeeService {
    private http = inject(HttpClient)
    private apiUrl = 'https://localhost:7227/api/employee';

    getEmployees(): Observable<Employee[]> {
        return this.http.get<Employee[]>(this.apiUrl);
    }


    createEmployee(data: EmployeeRequest): Observable<Employee> {
        return this.http.post<Employee>(this.apiUrl, data);
    }

    updateEmployee(id: number, data: EmployeeRequest): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, data)
    }

    deleteEmployee(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`)
    }
}