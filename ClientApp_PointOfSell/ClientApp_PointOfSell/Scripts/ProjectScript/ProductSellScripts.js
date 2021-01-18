$(document).ready(function () {
    AutoRefreshSellData();

    $("#btnShowSellPage").click(function () {
        window.location.href = "/Home/DisplaySellPage";
    });

});

function ShowModal() {
    $("#btnShowModal").click(function () {
        $("#ProductSellModal").modal('show');
    });
}

function LoadSellDataAfterChangeBrandId() {

    var brandId =$("#dptBrands").val();
    $.getJSON('https://localhost:44302/api/Product_Sell/GetBrandWiseProductList/' + brandId, {}, function (data) {

        var productDetail = '';

        $.each(data, function (index, item) {
            productDetail += '<div class="col-md-4"><img src="#" alt="Alternate Text" /><table  class="table table-bordered"><tr><td>Product Name</td><td>' + item.categoryName + '</td></tr><tr><td>Brand</td><td>' + item.brandName + '</td></tr><tr><td>Stock Available</td><td>' + item.stockAvailable + '</td></tr><tr><td>Unit Price</td><td>' + item.unitPrice +'</td></tr><tr><td>Quantiy</td><td><input type="text" name="Quantiy" value="" class="form-control" /></td></tr><tr><td></td><td><button type="button" class="btn btn-success" name="AddToCartItem" onclick="AddToCart()">Add To Cart</button></td></tr></table></div>';
        });
        $("#LoadProductList").empty();
        $("#LoadProductList").append(productDetail);


    }, function(error) {

    });
}


function AddToCart() {

    $('#LoadProductList table button[name="AddToCartItem"]').click(function() {
        var quantity = $('table button[name="AddToCartItem"]').closest("tr").find('input[name="Quantiy"]').val();
        console.log(quantity);
    })


};





function AutoRefreshSellData() {
    $.getJSON('https://localhost:44302/api/Product_Sell/GetProduct_SellList', {}, function(data) {


        var tblrow = '';
        $.each(data, function(index,data) {
            tblrow += '<tr><td>' + data.customerName + '</td><td>' + data.customerMobileNo + '</td><td>' + data.productName+'</td><td>' + data.brandName + '</td><td>' + data.categoryName + '</td><td>' + data.quantity + '</td><td>' + data.unitPrice + '</td><td>' + data.discount_Percentage + '</td><td>' + data.subTotalCost + '</td><td>' + data.dateOfSell + '</td><td>' + data.empName + '</td><td><button type="button" class="btn btn-warning" onclick="GetEditProductSellData(' + data.product_SellId + ')">Edit</button>|<button type="button" class="btn btn-danger" onclick="DeleteSellData(' + data.product_SellId +')">Delete</button></td></tr>';
        })
        $("#tblSellDataDisplay").empty();
        $("#tblSellDataDisplay").prepend(tblrow);

    }, function(error,status,xhr) {
            console.log(error);
            console.log(status.statusText);
            console.log(xhr.responseText);
           
    })
}


function DeleteSellData(product_SellId){
    var ans = confirm("Are you sure you want to delete this record!!");
    if (ans) {
        $.ajax({
            type: 'delete',
            url: 'https://localhost:44302/api/Product_Sell/DeleteProduct_Sell/' + product_SellId,
            data: {},
            dataType:'JSON',
            contentType: 'application/json',
            success: function(data) {
                AutoRefreshSellData();
                window.location.reload();
                alert(data);
            },
            error: function(error) {
                console.log(data);
                AutoRefreshSellData();
            },

        })
    }
}

function GetEditProductSellData(product_SellId){
    $.getJSON('https://localhost:44302/api/Product_Sell/GetProduct_Sell/'+product_SellId, {}, function(data) {
        console.log("Edit Data Come");
        console.log(data);
    },
    function(error,status,xhr) {
        console.log(error);
        console.log(status.statusText);
        console.log(xhr.responseText);

     })
}


function PostSellProductRecord() {

    var frmData = {
        Quantity :$("#Quantity").val(),
        UnitPrice :$("#UnitPrice").val(),
        Discount_Percentage :$("#Discount_Percentage").val(),
        CategoryId : $("#dptCategoty").val(),
        EmployeeId: $("#dptEmployee").val(),
        CustomerId :$("#CustomerId").val()
    }

    console.log(frmData);

    $.ajax({
        type: 'POST',
        url: 'https://localhost:44302/api/Product_Sell/PostProduct_Sell',
        data: JSON.stringify(frmData),
        dataType:"JSON",
        contentType: 'application/json; charset=utf-8',
        success: function(data) {
            alert(data);
            window.location.href = "/Home/Index";
           
        },
        error: function(error) {
            console.log(error);
        }
    })
}


function StockRemaining() {
    var id = $("#dptCategoty").val();
    $.getJSON('https://localhost:44302/api/StockCount/StockRemainingQuantity/'+id, {}, function(data) {
        $("#stockCount").text(data);

        

        if (data <= 0) {
            $("#btnAdd").prop("disabled", true);
        } 
        else {
            $("#btnAdd").prop("disabled", false);
        }

    }, function(error) {
            console.log(error);
    })
}

//StockRemainingQuantity