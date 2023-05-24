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
let table = new $("#tableStudent");
let table1 = new $("#tableStudentAproval");
let table2 = new $("#tableStudentNoAproval");
let table3 = new $("#tableEmployee");
//let table4 = new $("#tableGetStudent");
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

    table.DataTable({

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
    table1.DataTable({

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
    table2.DataTable({

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
    table3.DataTable({

        "ajax": {
            "url": "https://localhost:7004/api/Employee/ProfileEmployee",
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
                    return `<button class="btn btn-info" onclick="getBynik('${row.nik}')">Cek</button>`
                }
            },
        ]

    });

    let table4 = new $("#tableGetStudent").DataTable();
});

/*function getBynik(stringUrl) {
    table4.DataTable({
        "ajax": {
            "url": "https://localhost:7004/api/Student/student/" + stringUrl,
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
                    return `<button class="btn btn-info" onclick="getBynim('${row.nim}')">Detail</button>`
                }
            },
        ]

    });
}*/
function getBynik(stringUrl) {
    $.ajax({
        url: "https://localhost:7004/api/Student/student/" + stringUrl
    }).done((res) => {
        console.log(res);
        let temp = "";
        $.each(res.data, (key, val) => {
            
            temp += `<tr>
              <td>${val.nim}</td>
              <td>${val.fullName}</td>
              <td>${val.email}</td>
              <td>${val.university}</td>
              <td>${val.endDate}</td>
              <td>${val.status}</td>
              <td>
             <button type="button" class="btn btn-info "onclick="getNimStudent('${val.nim}')" style="margin:10px 0px" data-bs-toggle="modal" data-bs-target="#ModalDetail">Detail</button>
              </td>
                </tr>`;
        });
        $("#tbStudent").html(temp);
    });
}
function getNimStudent(stringUrl) {
    $.ajax({
        url: "https://localhost:7004/api/Student/StudentByNim/" + stringUrl
    }).done((res) => {
        //console.log(res);
        $.each(res.data, (key, val) => {
            $("#fullname").html(val.fullName);
            $("#universias").html(val.university);
            $("#Nim").html(val.nim);
            $("#email").html(val.email);
            $("#Major").html(val.major);
            $("#degree").html(val.degree);
            $("#Gpa").html(val.gpa);
        })
    })
}
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
            'success',
        )
        table.ajax.reload()
       
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
            'success',
        )
        table.ajax.reload()
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
        table1.ajax.reload();
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
                table2.ajax.reload();
                
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

let xmlhttp = new XMLHttpRequest();
let url = "https://localhost:7004/api/Student/StudentChart";
xmlhttp.open("GET", url, true);
xmlhttp.send();
xmlhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        let res = JSON.parse(this.responseText);
        console.log(res)
        universitasName = res.data.map(function (elem) {
            return elem.universitasName;
        })
       // console.log(universitasName);
        count = res.data.map(function (elem) {
            return elem.count;
        })
        //console.log(count);
        const ctx = document.getElementById('canvas');

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: universitasName,
                datasets: [{
                    data: count,
                    borderWidth: 1,
                    backgroundColor: "#ff33",
                    borderColor: '#36A2EB',
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                    }
                }
            }
        });
    }
}
