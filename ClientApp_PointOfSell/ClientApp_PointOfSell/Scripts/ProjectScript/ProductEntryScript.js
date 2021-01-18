$(document).ready(function () {
    AutoRefreshProductEntryList();
});

function AutoRefreshProductEntryList() {
    $.getJSON('https://localhost:44302/api/ProductEntry/GetProductEntryList', {}, function(data) {
        var htmlRow = '';
        $.each(data, function (index, item) {
            htmlRow += '<tr><td>' + item.voucherNumber + '</td><td>' + item.productName + '</td><td>' + item.brandName + '</td><td>' + item.categoryName + '</td><td>' + item.quantity + '</td><td>' + item.unitPrice + '</td><td>' + item.discount_Percentage + '</td><td>' + item.subTotalCost + '</td><td>' + item.dateOfEntry + '</td><td>' + item.EmpName + '</td><td><button type="button" class="btn btn-warning" id="btnEdit" onclick="EditProductEntry(' + item.productEntryId + ')">Edit<span class="glyphicon glyphicon-pencil"></span>></button>|<button type="button" class="btn btn-danger" id="btnDelete" onclick="DeleteProductEntry(' + item.productEntryId +')">Delete<span class="glyphicon glyphicon-pencil"></span>></button></td></tr>';

        });

        $("#tblProductEntryDisplay").empty();
        $("#tblProductEntryDisplay").prepend(htmlRow);


    },
      function (error, xhr, status) {
            console.log(error);
            console.log(xhr.responseText);
            console.log(status.statusText);
    })
}

function EditProductEntry(productEntryId) {

}


function DeleteProductEntry(productEntryId) {

}

