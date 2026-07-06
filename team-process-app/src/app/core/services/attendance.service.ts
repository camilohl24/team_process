import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Attendance, AttendanceRequest } from "../models/attendance.model";

@Injectable({
    providedIn: 'root'
})

export class AttendanceService {
    private http = inject(HttpClient)
    private apiUrl = 'https://localhost:7227/api/attendance';

    getAttendances(): Observable<Attendance[]> {
        return this.http.get<Attendance[]>(this.apiUrl);
    }


    createAttendance(data: AttendanceRequest): Observable<Attendance> {
        return this.http.post<Attendance>(this.apiUrl, data);
    }

    updateAttendance(id: number, data: AttendanceRequest): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, data)
    }

    deleteAttendance(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`)
    }
}