var today=new Date();
function namnhuan(){
    if(document.getElementById("nam").value % 400 == 0){
        return 1;
    }
    else if(document.getElementById("nam").value % 4 == 0 && document.getElementById("nam").value % 100 != 0){
        return 1;
    }
    else{
        return 0;
    }
}
function anchonngay(arg){
    var h=today.getMonth();
    h=h+1;
    var r=namnhuan();
    if(document.getElementById("ipthang").value == h && arg <= today.getDate() ){
        alert(" Lỗi \n Ngày này đã qua.");
    }
    else if(document.getElementById("ipthang").value == 2 && arg > 28+r){
       alert(" Lỗi \n Không có ngày này.");
    }
    else{
        if(document.getElementById("ipthang").value>0){
            var ip=document.getElementById("ipngay");
            var a;
            ip.value=arg;
            ip.checked=true;
            for(var i=1;i<=31;i++){
                a = document.getElementById([i]);
                a.style.backgroundColor="";
                a.style.color="black";
            }
            a=document.getElementById(arg);
            if(arg==ip.value){
                a.style.backgroundColor="var(--mau-nen)";
                a.style.color="aliceblue";
            }
            // alert(""+ipngay.value);
        }
        else{
            alert("Bạn vui lòng chọn tháng trước.");
        }
    }
}
function anchonthang(arg){
    var h=today.getMonth();
    h=h+1;
    if(document.getElementById("nam").value == today.getFullYear() && arg < h){
        alert(" Lỗi\n Tháng này đã qua.");
    }
    else{
        var ip=document.getElementById("ipthang");
        var a;
        ip.value=arg;
        ip.checked=true;
        for(var i=1;i<=12;i++){
            a = document.getElementById("thang"+[i]);
            a.style.backgroundColor="";
            a.style.color="black";
        }
        a=document.getElementById("thang"+arg);
        if(arg==ip.value){
            a.style.backgroundColor="var(--mau-nen)";
            a.style.color="aliceblue";
        }
        for(var i=1;i<=31;i++){
            a = document.getElementById([i]);
            a.style.backgroundColor="";
            a.style.color="black";
            document.getElementById("ipngay").checked=false;
            document.getElementById("ipngay").value=0;
        }
        // alert(""+ip.value);
    }
}
// alert(""+document.getElementById("ipnam").value);
document.getElementById("nam").value=today.getFullYear();
document.getElementById("ipnam").value=today.getFullYear();
document.getElementById("ipnam").checked=true;
function anchonnam(arg){
    var ip=document.getElementById("ipnam");
    var z=document.getElementById("nam").value;
    if(arg==1 && document.getElementById("nam").value > today.getFullYear()){
        z=z-1 ;
        for(var i=1;i<=12;i++){
            a = document.getElementById("thang"+[i]);
            a.style.backgroundColor="";
            a.style.color="black";
            document.getElementById("ipthang").checked=false;
            document.getElementById("ipthang").value=0;
        }
        for(var i=1;i<=31;i++){
            a = document.getElementById([i]);
            a.style.backgroundColor="";
            a.style.color="black";
            document.getElementById("ipngay").checked=false;
            document.getElementById("ipngay").value=0;
        }
    }
    else if(arg==2 && document.getElementById("nam").value < today.getFullYear()+1){
        z=today.getFullYear()+1;
        for(var i=1;i<=12;i++){
            c = document.getElementById("thang"+[i]);
            c.style.backgroundColor="";
            c.style.color="black";
            document.getElementById("ipthang").checked=false;
            document.getElementById("ipthang").value=0;
        }
        for(var i=1;i<=31;i++){
            c = document.getElementById([i]);
            c.style.backgroundColor="";
            c.style.color="black";
            document.getElementById("ipngay").checked=false;
            document.getElementById("ipngay").value=0;
        }
    }
    document.getElementById("nam").value=z;
    ip.value=z;
    ip.checked=true;
    // alert(""+ip.value);
}
function datlich(){
    var i=document.getElementById("ipnam").value;
    var p=document.getElementById("ipngay").value;
    var k=document.getElementById("ipthang").value;
    if(document.getElementById("ipngay").value==0){
        alert("Vui lòng chọn ngày hẹn.")
    }
    else{
        alert("Đã gửi. \nChúng tôi sẽ sắp xếp buổi gặp vào ngày: "+p+"/"+k+"/"+i+
        ".\n(chúng tôi sẽ liên lạc với bạn để thông báo chi tiết hơn về buổi gặp)");
    }
}
function kiemtra(){
    if(document.getElementById("ipngay").value==0){
        return false;
    }
    else{
        return true;
    }
}
