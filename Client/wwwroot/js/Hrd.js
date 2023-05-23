console.log("TES1")
$.ajax({
    url: "https://localhost:7004/api/Student"
}).done((res) => {
    console.log(res)
});

/*function getById(stringUrl) {
    $.ajax({
        url: "https://localhost:7004/api/Student/StudentByNim/" + stringUrl,
        type: "Get",
        headers: {
            'Content-Type': 'application/json'
        },
        data: "json"
    }).done(res => {
       console.log(res)
    })
}*/

/*function getId(stringUrl) {
    $.ajax({
        url: "https://localhost:7004/api/University/" + stringUrl,
        type: "Get",
        headers: {
            'Content-Type': 'application/json'
        },
        data: "json"
    }).done(res => {
        console.log(res)
    })
}*/

$(document).ready(function () {

    $.ajax({
        url: "https://localhost:7004/api/Student/CountNullAproval",
        type: "Get",
        headers: {
            'Content-Type': 'application/json'
        },
        data: "json"
    }).done(res => {
        console.log(res.data);
        $("#StudentNull").html(res.data);
    })
    $.ajax({
        url: "https://localhost:7004/api/Student/CountTrueAproval",
        type: "Get",
        headers: {
            'Content-Type': 'application/json'
        },
        data: "json"
    }).done(res => {
        console.log(res.data);
        $("#StudentAproval").html(res.data);
    })

    let table = new $('#tableStudent').DataTable({

        "ajax": {
            "url": "https://localhost:7004/api/Student/StudentNullAproval",
            "dataType": "json",
            "dataSrc": "data",

        },
        columns: [
            { data: "nim" },
            { data: "fullname" },
            { data: "email" },
            { data: "name" },
            { data: "major" },
            { data: "degree" },
            { data: "gpa" },
            { data: "phoneNumber" },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-info" onclick="getById('${row.nim}')" data-bs-toggle="modal" data-bs-target="#ModalDetail">Aproval</button>`
                }
            },
        ]

    });
    let table1 = new $('#tableStudentAproval').DataTable({

        "ajax": {
            "url": "https://localhost:7004/api/Student/StudentTrueAproval",
            "dataType": "json",
            "dataSrc": "data",

        },
        columns: [
            { data: "nim" },
            { data: "fullname" },
            { data: "email" },
            { data: "name" },
            { data: "major" },
            { data: "degree" },
            { data: "gpa" },
            { data: "phoneNumber" },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-info" onclick="getById('${row.nim}')" data-bs-toggle="modal" data-bs-target="#ModalDetail">Penempatan</button>`
                }
            },
        ]

    });
    let table2 = new $('#tableStudentNoAproval').DataTable({

        "ajax": {
            "url": "https://localhost:7004/api/Student/StudentFalseAproval",
            "dataType": "json",
            "dataSrc": "data",

        },
        columns: [
            { data: "nim" },
            { data: "fullname" },
            { data: "email" },
            { data: "name" },
            { data: "major" },
            { data: "degree" },
            { data: "gpa" },
            { data: "phoneNumber" },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-danger" onclick="DeleteStudent('${row.nim}')">Delete</button>`
                }
            },
        ]

    });
});

function getById(stringUrl) {
    $.ajax({
        url: "https://localhost:7004/api/Student/StudentByNim/" + stringUrl,
        type: "Get",
        headers: {
            'Content-Type': 'application/json'
        },
        data: "json"
    }).done(res => {
        console.log(res.data);
        $("#nim").val(res.data[0].nim);
        $("#fullname").val(res.data[0].fullName);
        $("#universitas").val(res.data[0].university);
        $("#major").val(res.data[0].major);
        $("#degree").val(res.data[0].degree);
        $("#gpa").val(res.data[0].gpa);
    })
}

function Aproval() {

    let nim = $("#nim").val();

    var obj = new Object();
    obj.Nim = nim;

    $.ajax({
        url: "https://localhost:7004/api/Student/AprovalTrue/" + nim,
        type: "put",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj)
    }).done((result) => {
        console.log(result);
        Swal.fire(
            'Berhasil?',
            'input data',
            'success'
        )
       
        //buat alert pemberitahuan jika success
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal?',
            'input data',
            'warning'
        )
        //alert pemberitahuan jika gagal
    })
}

function NoAproval() {

    let nim = $("#nim").val();

    var obj = new Object();
    obj.Nim = nim;

    $.ajax({
        url: "https://localhost:7004/api/Student/AprovalFalse/" + nim,
        type: "put",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj)
    }).done((result) => {
        console.log(result);
        Swal.fire(
            'Berhasil?',
            'input data',
            'success'
        )
        //buat alert pemberitahuan jika success
    }).fail((error) => {
        console.log(error)
        Swal.fire(
            'Gagal?',
            'input data',
            'warning'
        )
        //alert pemberitahuan jika gagal
    })
}

$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7004/api/Department"
    }).done((res) => {
        console.log(res)
        let temp = "";
        temp += `<option value="">----Pilih Department---</option>`;
        $.each(res.data, (key, val) => {
            temp += `<option value="${val.id}">${val.name}</option>`;

        });
        $("#Department").html(temp);
    })


   /* $("#Department").change(function (e) {
        let id = $("Department").val();
        console.log(id)
    })*/
})

//tambah getdepartment select option
function getdepart(value) {
    $.ajax({
        url: "https://localhost:7004/api/Employee/employe/"+value
    }).done((res) => {
        console.log(res)
        let temp = "";
        temp += `<option value="">----Pilih mentor---</option>`;
        $.each(res.data, (key, val) => {
            temp += `<option value="${val.nik}">${val.fullname}</option>`;

        });
        $("#Mentor").html(temp);
    })
}

//simpan penempatan
function SimpanStatus() {
    var obj = new Object();
    obj.studentId = $("#nim").val();
    obj.departmentId = $("#Department").val();
    obj.mentorId = $("#Mentor").val();
    obj.startDate = $("#startdate").val();
    obj.endDate = $("#endDate").val();
    obj.status1 = true

    $.ajax({
        url: ("https://localhost:7004/api/Status"),
        type: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(obj) //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        console.log(result);
        Swal.fire(
            'Berhasil?',
            'input data',
            'success'
        )
        //buat alert pemberitahuan jika success
    }).fail((error) => {
        Swal.fire(
            'Gagal?',
            'input data',
            'warning'
        )
        //alert pemberitahuan jika gagal
    })

}

function DeleteStudent(stringUrl) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            swalWithBootstrapButtons.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
            $.ajax({
                url: " https://localhost:7004/api/AccountStudent/student/" + stringUrl,
                type: "DELETE",
                headers: {
                    'Content-Type': 'application/json'
                },
                data: "json"
            }).done(res => {
                console.log(res)
                
            })
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your imaginary file is safe :)',
                'error'
            )
        }
    })
}