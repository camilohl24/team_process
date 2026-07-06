import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Department, DepartmentRequest } from "../models/department.model";

@Injectable({
    providedIn: 'root'
})

export class DepartmentService {
    private http = inject(HttpClient)
    private apiUrl = 'https://localhost:7227/api/department';

    getDepartments(): Observable<Department[]> {
        return this.http.get<Department[]>(this.apiUrl);
    }


    createDepartment(data: DepartmentRequest): Observable<Department> {
        return this.http.post<Department>(this.apiUrl, data);
    }

    updateDepartment(id: number, data: DepartmentRequest): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, data)
    }

    deleteDepartment(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`)
    }
}