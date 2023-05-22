console.log("TES1")
$.ajax({
    url: "https://localhost:7004/api/Student"
}).done((res) => {
    console.log(res)
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
       console.log(res)
    })
}

function getId(stringUrl) {
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
}

$(document).ready(function () {
 
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
                    return `<button class="btn btn-danger" onclick="Delete('${row.nim}')">Delete</button>`
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
       alert("berhasil")
        //buat alert pemberitahuan jika success
    }).fail((error) => {
        console.log(error)
       alert("gagal")
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
        alert("berhasil")
        //buat alert pemberitahuan jika success
    }).fail((error) => {
        console.log(error)
        alert("gagal")
        //alert pemberitahuan jika gagal
    })
}

$(document).ready(function (e) {
    $("#Department").click(function () {
        $.ajax({
            url: "https://localhost:7004/api/Department"
        }).done((res) => {
            console.log(res)
            let temp = "";
                temp += `<option value="">----Pilih Department---</option>`;
            $.each(res.data, (key, val) => {
                temp += `<option value="${val.name}">${val.name}</option>`;
                console.log(val.name)
            });
            $("#Department").html(temp);
        })
    })
})


