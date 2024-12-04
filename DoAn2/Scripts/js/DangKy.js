var emailReg = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]{2,4}$/;
var passReg = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[\W])/;

var x = document.getElementById("myInput1");
var y = document.getElementById("myInput2");

function frmValiDate() {
    let email = document.getElementById("email");
    if (emailReg.test(email.value) == false) {
        alert("Lỗi Email");
        return false;
    }

    if (passReg.test(x.value) == false) {
        alert("Lỗi Mật Khẩu, vui lòng nhập mậ khẩu chứa chữ hoa, chữ thường, chữ số, kí tự đặt biệt");
        return false;
    }
    if (x.value.length < 8) {
        alert("Mật khẩu phải có độ dài trên 8 kí tự");
        return false;
    }

    if (x.value != y.value) {
        alert("Mật khẩu phải trùng nhau");
        return false;
    }

    return true;
}

function showPassWord1() {
    if (x.type === "password" || y.type === "password") {
        x.type = "text";
        y.type = "text";
    } else {
        x.type = "password";
        y.type = "password";
    }
}
