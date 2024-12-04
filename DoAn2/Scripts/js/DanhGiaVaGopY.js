function kiemtramail(){
    var email = document.getElementById("email");
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/; 
    if(email.value != ""){
        if (!filter.test(email.value)) { 
            var mail = '**-Hãy nhập địa chỉ email hợp lệ-**\nExample@gmail.com\n(bạn có thể để trống thông tin này)\n';
            email.focus; 
        }
        else{ 
            var mail = '-Email hợp lệ.\n';
        } 
    }
    else{
        var mail="";
    }
    return mail;
}
function kiemtradienthoai(){
    var dt = document.getElementById("dienthoai");
    var filter = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    if(dt.value != ""){
        if(!filter.test(dt.value)){
            var so = "**-Hãy nhập số điện thoại hợp lệ-**\n(bạn có thể để trống thông tin này)\n";
        }
        else{
            var so = "-Số điện thoại hợp lệ.\n";
        }
    }
    else{
        var so="";
    }
    return so;
}
function kiemtrathongtin(){
    if(kiemtramail()=="" && kiemtradienthoai()==""){
        alert("Bạn vui lòng nhập email hoặc số điện thoại để kiểm tra thông tin.\n(bạn có thể để trống thông tin liên lạc)");
    }
    else{
        alert(""+kiemtramail()+kiemtradienthoai());
    }
}
function kiemtrachon(){
    var x=0;
    var a = document.getElementsByName("sanpham");
    var b = document.getElementsByName("danhgia");
    for(var i=0;i<a.length;i++){
        if(a[i].checked == true){
            x=x+1;
        }
    }
    for(var j=0;j<b.length;j++){
        if(b[j].checked == true){
            x=x+1;
        }
    }
    return x;
}
function kiemtragopy(){
    var x = document.getElementById("textykien");
    if(x.value==""){
        var u=0;
    }
    else{
        var u=1;
    }
    return u;
}
function submit(){ 
    if(kiemtramail()=="**-Hãy nhập địa chỉ email hợp lệ-**\nExample@gmail.com\n(bạn có thể để trống thông tin này)\n" || 
    kiemtradienthoai()=="**-Hãy nhập số điện thoại hợp lệ-**\n(bạn có thể để trống thông tin này)\n"){
        alert("Không thể gửi do thông tin liên lạc chưa hợp lệ.\n(bạn có thể để trống thông tin liên lạc)");
    }
    else if(kiemtrachon() < 2 && kiemtragopy()==0){
        alert("Bạn vui lòng để lại đánh giá cho sản phẩm hoặc để lại ý kiến đóng góp");
    }
    else{
        alert("Đã gửi");
    }
}
function kiemtra(){
    if(kiemtramail()=="**-Hãy nhập địa chỉ email hợp lệ-**\nExample@gmail.com\n(bạn có thể để trống thông tin này)\n" || 
    kiemtradienthoai()=="**-Hãy nhập số điện thoại hợp lệ-**\n(bạn có thể để trống thông tin này)\n"){
        return false;
    }
    else if(kiemtrachon() < 2 && kiemtragopy()==0){
        return false;
    }
    else{
        return true;
    }
}
var u = document.getElementsByName("danhgia");
for(var i=0;i<u.length;i++){
    u[i].addEventListener("change",function(){
        if(kiemtrasanphamchon()==0){
            alert("Để đánh giá sản phẩm bạn vui lòng chọn sản phẩm trước.");
            this.checked=false;
        }
    });
}
function kiemtrasanphamchon(){
    var x=0;
    var a = document.getElementsByName("sanpham");
    for(var i=0;i<a.length;i++){
        if(a[i].checked == true){
            x=x+1;
        }
    }
    return x;
}
var f=document.getElementById("textnhanxet");
f.addEventListener("change",function(){
    if(kiemtradanhgiachon()==0){
        alert("Để nhận xét sản phẩm bạn vui lòng chọn sản phẩm và để lại đánh giá trước.");
        f.value=null;
    }
});
function kiemtradanhgiachon(){
    var y=0;
    var b=document.getElementsByName("danhgia");
    for(var j=0;j<b.length;j++){
        if(b[j].checked == true){
            y=y+1;
        }
    }
    return y;
}