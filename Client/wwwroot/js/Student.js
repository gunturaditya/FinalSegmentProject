let nim = $("#nim").html();
console.log(nim);

$.ajax({
    url: "https://localhost:7004/api/Student/StudentByNim/" + nim,
    type: "Get",
    headers: {
        'Content-Type': 'application/json'
    },
    data: "json"
}).done(res => {
    $("#startdate").html(res.data[0].startDate);
    $("#enddate").html(res.data[0].endDate);
    $("#score").html(res.data[0].score);
});


$.ajax({
    url: "https://localhost:7004/api/Employee/getEmployee/" + nim,
    type: "Get",
    headers: {
        'Content-Type': 'application/json'
    },
    data: "json"
}).done(res => {
    if (res.data[0].gender == 0) {
        $("#gender").html("Pria");
    } else {
        $("#gender").html("Wanita");
    }
    $("#fullname").html(res.data[0].fullName);
    $("#email").html(res.data[0].email);
    $("#phone").html(res.data[0].phoneNumber);
    $("#department").html(res.data[0].name);
});

$.ajax({
    url: "https://localhost:7004/api/Student/" + nim,
    type: "Get",
    headers: {
        'Content-Type': 'application/json'
    },
    data: "json"
}).done(res => {
    console.log(res);
    $("#firstname").val(res.data.firstName);
    $("#lastname").val(res.data.lastName);
    $("#email").val(res.data.email);
    $("#university").val(res.data.birthDate);
    $("#degree").val(res.data.degree);
    $("#phonenumber").val(res.data.phoneNumber);
});

function changePass() {

    let pass = $("#pass").val();
    let confirm = $("#confirmPass").val();
    if (pass != confirm) {
        $("#validate").html("password do not match")
    } else {
        var obj = new Object();
        obj.accountStudentId = nim;
        obj.password = confirm;
        $.ajax({
            url: "https://localhost:7004/api/AccountStudent/" + nim,
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
            window.location.href = "Index";

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
