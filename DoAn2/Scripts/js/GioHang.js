function receiptBodyToHTML() {
    var receiptBody = document.getElementById("body-receipt");

    console.log(Object.keys(localStorage).length);

    if (Object.keys(localStorage).length <= 0) {
        let newCol = document.createElement("tr");

        let title = document.createElement("td");
        title.setAttribute("colspan", "5");
        title.innerHTML = "Chưa có sản phẩm nào trong giỏ hàng, tìm <a href='@Url.Action("SanPham","SanPham")' class='link-button'>Sản Phẩm </a>";

        newCol.appendChild(title);
        receiptBody.appendChild(newCol);
        return;
    }

    Object.keys(localStorage).forEach(function (key, index) {
        let name = products[key].name;
        let quanlity = localStorage[key];
        let price = products[key].price;
        let total = price * quanlity;

        let newCol = document.createElement("tr");

        let td_index = document.createElement("td");
        let td_name = document.createElement("td");
        let td_quanlity = document.createElement("td");
        let td_price = document.createElement("td");
        let td_total = document.createElement("td");

        td_index.innerHTML = index + 1;
        newCol.appendChild(td_index);

        td_name.innerHTML = name;
        td_name.setAttribute("text-align", "left");
        newCol.appendChild(td_name);

        td_quanlity.innerHTML = quanlity;
        newCol.appendChild(td_quanlity);

        td_price.innerHTML = price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        newCol.appendChild(td_price);

        td_total.innerHTML = total.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        newCol.appendChild(td_total);

        receiptBody.appendChild(newCol);
    });
}

function totalReceiptToHTML() {
    var totalReceipt = document.getElementById("total-receipt");

    var total_bill = calcTotalPrice();
    var vat_bill = calVatPrice();

    function totalCol() {
        var column = document.createElement("tr");

        // Tittle col
        let td_total = document.createElement("td");
        td_total.setAttribute("colspan", "4");
        td_total.innerText = "Tổng giá trị đơn hàng";
        column.appendChild(td_total);

        // Value col
        let td_total_value = document.createElement("td");
        td_total_value.innerText = total_bill.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        column.appendChild(td_total_value);

        totalReceipt.appendChild(column);
    }

    function vatCol() {
        var column = document.createElement("tr");

        // Tittle col
        let td_vat = document.createElement("td");
        td_vat.setAttribute("colspan", "4");
        td_vat.innerText = "Thuế đơn hàng";
        column.appendChild(td_vat);

        // Value col
        let td_vat_value = document.createElement("td");
        td_vat_value.innerText = vat_bill.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        column.appendChild(td_vat_value);

        totalReceipt.appendChild(column);
    }

    function finalTotal() {
        var column = document.createElement("tr");

        // Tittle col
        let td_final = document.createElement("td");
        td_final.setAttribute("colspan", "4");
        td_final.innerText = "Thành tiền"
        column.appendChild(td_final);

        // Value col
        let td_final_value = document.createElement("td");
        td_final_value.setAttribute("colspan", "4");
        td_final_value.innerText = (vat_bill + total_bill).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        column.appendChild(td_final_value);

        totalReceipt.appendChild(column);
    }

    totalCol();
    vatCol();
    finalTotal();
}

function timeDeleverToHTML(time = 3600000) {
    var timeDeliverHTML = document.getElementById("time-deliver");

    let timeDeliverMilliseconds = Date.now() + parseInt(time);

    let timeDeliver = new Date(timeDeliverMilliseconds);
    let text = "";
    if (timeDeliver.getMinutes() <= 9) {
        text = timeDeliver.getHours() + "h0" + timeDeliver.getMinutes();
    }
    else {
        text = timeDeliver.getHours() + "h" + timeDeliver.getMinutes();
    }

    timeDeliverHTML.innerText = text;

}// thiết lập thời gian giao hàng


function calcTotalPrice() {
    var total = 0;
    Object.keys(localStorage).forEach(function (key, index) {
        let quanlity = localStorage[key];
        let price = products[key].price;

        let sum = parseInt(price * quanlity);

        total += sum == null ? 0 : sum;
    });

    return total;
}//tính tổng giá trị đơn hàng

function calVatPrice() {
    var total_bill = calcTotalPrice();
    var remaining_amount = total_bill;

    var total_vat = 0;

    var tax_level = [100000, 200000, 500000, 1000000];
    var tax_rate = [0, 0.01, 0.02, 0.05];

    tax_level.forEach(function (element, index) {
        if (remaining_amount > element) {
            total_vat += tax_rate[index] * element;
            remaining_amount -= element;
        }
        else {
            total_vat += tax_rate[index] * remaining_amount;
            return;
        }
    });

    return total_vat;
}//tính thuế VAT

receiptBodyToHTML();
totalReceiptToHTML();
timeDeleverToHTML();