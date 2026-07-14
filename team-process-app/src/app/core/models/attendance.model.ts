export interface Attendance{
    id: number
    employeeName: string
    date: string
    status: AttendanceStatus
    employeeId: number
}

export interface AttendanceRequest{
    employeeId: number
    date: string
    status: AttendanceStatus
}
export enum AttendanceStatus{
    Present = 0,
    Absent = 1,
    Late = 2
}

