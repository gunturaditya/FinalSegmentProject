let student = document.querySelector("#student");
let employee = document.querySelector("#employee");
let form = document.querySelector(".user");

student.addEventListener("click", () => {
    student.classList.remove("btn-light");
    student.classList.add("btn-primary");
    employee.classList.remove("btn-primary");
    employee.classList.add("btn-light");
    form.setAttribute("asp-action", "Login");
    form.setAttribute("action", "Authenfikasi/Login");
});

employee.addEventListener("click", () => {
    employee.classList.remove("btn-light");
    employee.classList.add("btn-primary");
    student.classList.remove("btn-primary");
    student.classList.add("btn-light");
    form.setAttribute("asp-action", "LoginEmployee");
    form.setAttribute("action", "Authenfikasi/LoginEmployee");
});