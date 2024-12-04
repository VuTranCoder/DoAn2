const menu = document.getElementById("gom-nhom");
const navbar = document.getElementById("thanh-dieu-huong");

menu.addEventListener("click", () => {
    navbar.classList.toggle("show");
});