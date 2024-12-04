var toLeftButton = document.getElementById("nut-chuyen-trai");
var toRightButton = document.getElementById("nut-chuyen-phai");

var carousel = document.getElementById("carousel");

toLeftButton.addEventListener("click", () => {
    carousel.scrollBy(-1, 0);
});

toRightButton.addEventListener("click", () => {
    carousel.scrollBy(1, 0);
});