var emailReg = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]{2,4}$/;
var passReg = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[\W])/;
function frmValiDate(form) {
  let email = document.getElementById("email");
  if (emailReg.test(email.value) == false) {
    alert("Lỗi Email");
    return false;
  }
  let password = document.getElementById("password");
  if (passReg.test(password.value) == false) {
    alert("Lỗi Mật Khẩu, vui lòng nhập mậ khẩu chứa chữ hoa, chữ thường, chữ số, kí tự đặt biệt");
    return false;
  }
  if (password.value.length < 8) {
    alert("Mật khẩu phải có độ dài trên 8 kí tự");
    return false;
  }
  return true;
}
function showPassWord(){
  var x = document.getElementById("password");
  if (x.type === "password") {
    x.type = "text";
  } else {
    x.type = "password";
  }
}