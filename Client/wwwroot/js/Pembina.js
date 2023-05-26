let nik = $("#getnik").html();
console.log(nik);

function hilang_spasi(string) {
    return string.split(' ').join('');
}


let table = new $("#tableStudent");
let table1 = new $("#tableNilaiStudent");

$.ajax({
    url: "https://localhost:7004/api/Employee/Profil/" + nik,
    type: "Get",
    headers: {
        'Content-Type': 'application/json'
    },
    data: "json"
}).done(res => {
    console.log(res.data[0].university);
 $("#firstname").val(res.data[0].firstName);
    $("#lastname").val(res.data[0].lastName);
    $("#email").val(res.data[0].email);
    $("#university").val(res.data[0].university);
    $("#degree").val(res.data[0].degree);
    $("#phonenumber").val(res.data[0].phoneNumber);
});

$(document).ready(function () {


    table.DataTable({
        "ajax": {
            "url": "https://localhost:7004/api/Student/student/" + nik ,
            "dataType": "json",
            "dataSrc": "data",

        },

        columns: [
            { data: "nim" },
            { data: "fullName" },
            { data: "email" },
            { data: "university" },
            { data: "major" },
            { data: "startDate" },
            { data: "endDate" },
            { data: "status"},
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-info" onclick="Detail('${row.nim}')"data-bs-toggle="modal" data-bs-target="#ModalDetail">Detail</button>`
                }
            },
        ]

    })

    table1.DataTable({
        "ajax": {
            "url": "https://localhost:7004/api/Student/Penilaian/" + nik,
            "dataType": "json",
            "dataSrc": "data",

        },

        columns: [
            { data: "nim" },
            { data: "fullName" },
            { data: "email" },
            { data: "university" },
            { data: "major" },
            { data: "enddate" },
            { data: "datenow" },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-info" onclick="getnim('${row.nim}')"data-bs-toggle="modal" data-bs-target="#ModalDetail">Input Nilai</button>`
                }
            },
        ]

    })
});


function getnim(stringURl) {
    $.ajax({
        url: "https://localhost:7004/api/Student/StudentByNim/" + stringURl,
        type: "Get",
        headers: {
            'Content-Type': 'application/json'
        },
        data: "json"
    }).done(res => {
        $("#nim").val(res.data[0].nim);
    })
}


function InputNilai() {
    let nim = $("#nim").val();
    let aktifan = parseInt($("#keaktifan").val());
    let kehadiran = parseInt($("#keaktifan").val());
    let tugas = parseInt($("#tugas").val());
    let tambah = aktifan + kehadiran + tugas;
    let total = parseInt(tambah / 3);
    var obj = new Object();
    obj.nim = hilang_spasi(nim);
    obj.score = total;
    $.ajax({
        url: "https://localhost:7004/api/Employee/Penilaian/" + nim,
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

function Detail(stringUrl) {
    $.ajax({
        url: "https://localhost:7004/api/Student/StudentByNim/" + stringUrl
    }).done((res) => {
        //console.log(res);
        $.each(res.data, (key, val) => {
            $("#fullname").html(val.fullName);
            $("#universias").html(val.university);
            $("#Nim").html(val.nim);
            $("#score").html(val.score);
            $("#email").html(val.email);
            $("#Major").html(val.major);
            $("#degree").html(val.degree);
            $("#Gpa").html(val.gpa);
            $("#startdate").html(val.startDate);
            $("#enddate").html(val.endDate);
        })
    })
}

function changePass() {

    let pass = $("#pass").val();
    let confirm = $("#confirmPass").val();
    if (pass != confirm) {
        $("#validate").html("password do not match")
    } else {
        var obj = new Object();
        obj.accountId = nik;
        obj.password = confirm;
        $.ajax({
            url: "https://localhost:7004/api/Account/" + nik,
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

}