let nik = $("#getnik").html();
console.log(nik);

let table = new $("#tableStudent");
let table1 = new $("#tableNilaiStudent");

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
                    return `<button class="btn btn-info" onclick="Detail('${row.nim}')">Detail</button>`
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
    obj.nim = nim;
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