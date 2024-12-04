function Product(name, price = 30000) {
    this.name = name;
    this.price = price;
}

var products = {
    comHeoQuay: new Product("Cơm Heo Quay", 35000),
    comChienHeoQuay: new Product("Cơm Chiên Heo Quay", 35000),
    comHeoQuayDuaCai: new Product("Cơm Heo Quay Dưa Cải", 35000),
    comThapTamNuong: new Product("Cơm Xá Xíu Thập Tam Nương", 35000),
    comGaKhoGung: new Product("Cơm Gà Kho Gừng", 45000),

    comBoSotTieuDen: new Product("Cơm Bò Sốt Tiêu Đen", 45000),
    comBoXaoBongHe: new Product("Cơm Bò Xào Bông Hẹ", 45000),
    comBoXaoDauRong: new Product("Cơm Bò Xào Đậu Rồng", 45000),
    comBoXaoKhoQua: new Product("Cơm Bò Xào Khổ Qua",45000),
    comBoXaoOtChuong: new Product("Cơm Bò Xào Ớt Chuông", 45000),

    canhCaiNgotThit: new Product("Canh Cải Ngọt Thịt", 20000),
    canhRongBienThit: new Product("Canh Rong Biển Thịt", 20000),
    khoQuaXaoTrung: new Product("Khổ Qua Xào Trứng", 15000),
    trungQuay: new Product("Trứng Quậy", 15000)
};

function addProduct(id, quanlity = 1) {
    if (localStorage[id] === undefined) {
        console.log(id + " is undefined");
        localStorage.setItem(id, quanlity);
    }
    else{
        let cur = parseInt(localStorage.getItem(id));
        if(cur + quanlity > 100){
            console.log("more than 100");
            localStorage.setItem(id, 100);
        }
        else{
            console.log("add " + quanlity);
            localStorage.setItem(id, cur + quanlity);
        }
    }

}