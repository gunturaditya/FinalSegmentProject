var status = sessionStorage.getItem("Status");

if (status == "True") {
    var name = sessionStorage.getItem("FullName");
    $(".card-title").html(`Hi! ${name} Welcome Back!`);
    $(".card-text").html(`Congratulations, your CV has been approved. please prepare yourself for the internship. We hope you will enjoy your internship at our company.`)
}

if (status == "False") {
    var name = sessionStorage.getItem("FullName");
    $(".card-title").html(`Hi! ${name} Welcome Back!`);
    $(".card-text").html(`Sorry,we have not been able to accept you for an internship. maybe at this time we have not been able to accept your offer for an internship. Maybe on another occasion we can receive you. keep spirit and don't give up.`)
}

if (status == "null" || status == "") {
    var name = sessionStorage.getItem("FullName");
    $(".card-title").html(`Hi! ${name} Welcome!`);
    $(".card-text").html(`Please wait your approval for internship. Please check your profile again if there is a mistake. and complete your cv as attractive as possible. you can click button below to upload your CV.
      <a style="display:block;" href="UploadCv" class="btn btn-primary mt-2">Upload CV</a>`)
}
