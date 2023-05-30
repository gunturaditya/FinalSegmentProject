//initialize
let tableEmployee = new $("#tableEmployee");
let tableStudent = new $("#tableStudent");

$(document).ready(function () {
    tableStudent.DataTable({
        "ajax": {
            "url": "https://localhost:7167/api/Student/StudentProfil",
            "dataType": "json",
            "dataSrc": "data",
        },
        columns: [
            { data: "nim" },
            { data: "fullName" },
            { data: "email" },
            { data: "university" },
            { data: "endDate" },
            { data: "status" },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-info" onclick="getBynim('${row.nim}')" data-bs-toggle="modal" data-bs-target="#ModalDetail">Detail</button>`
                }
            },
        ]
    });

    tableEmployee.DataTable({
        "ajax": {
            "url": "https://localhost:7167/api/Employee/ProfileEmployee",
            "dataType": "json",
            "dataSrc": "data",
        },
        columns: [
            { data: "nik" },
            { data: "fullName" },
            { data: "email" },
            { data: "university" },
            { data: "major" },
            { data: "name" },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-info" onclick="getBynik('${row.nik}')">Detail</button>`
                }
            },
        ]
    });        
});

