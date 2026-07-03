export interface Employee{
    id: number
    name: string
    lastName: string
    position: string
    entryDate: string
    departmentName: string
}

export interface EmployeeRequest{
    name: string
    lastName: string
    position: string
    entryDate: string
    departmentId: number
}