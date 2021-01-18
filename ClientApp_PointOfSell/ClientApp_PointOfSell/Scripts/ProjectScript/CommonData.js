$(document).ready(function () {
    LoadProduct();
    LoadEmployee();

    $("#btnCustomerSearch").click(function() {
        SearchCustomer();
    })
});

function LoadProduct() {
  
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44302/api/CommonData/LoadProductList',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            var html = '';
            $.each(data, function (index, item) {
                html += '<option value="' + item.productId + '">' + item.productName + '</option>';
            })
            $("#dptProducts").empty();
            $("#dptProducts").append(html);
            $("#dptProducts").prepend('<option value="value">Select Category</option>');
        },
        error: function (error) {
            console.log("product error");
            console.log(error);
        }
    })
}

function SearchCustomer() {
    var SearchText =$("#CustomerMobile").val();

    var searchParams = {
        "CustomerMobileNo": SearchText,
        "CustomerId": SearchText,
    }

    console.log(searchParams);


    $.ajax({
        type: 'POST',
        url: 'https://localhost:44302/api/CommonData/SearchCustomer',
        data:JSON.stringify(searchParams),
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);
            var html = '<option value="' + data.customerId + '">' + data.customerName + '</option>';
            $("#CustomerId").empty();
            $("#CustomerId").append(html);
        },
        error: function(error) {
            console.log(eror);
        }
    })
}


function LoadEmployee() {

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44302/api/CommonData/LoadEmployeeList',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {

            var html = '';
            $.each(data, function (index, item) {
                html += '<option value="' + item.employeeId + '">' + item.empName + '</option>';
            })
            $("#dptEmployee").empty();
            $("#dptEmployee").append(html);
            $("#dptEmployee").prepend('<option value="value">Select Employee</option>');
        },
        error: function (error) {
            console.log("product error");
            console.log(error);
        }
    })
}

function LoadBrandsByProductId() {
    var id=$("#dptProducts").val();
    $.ajax({
        url: 'https://localhost:44302/api/CommonData/LoadBrandListByProductId/' + id,
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (data) {

            var html = '';
            $.each(data, function (index, item) {
                html += '<option value="' + item.brandId + '">' + item.brandName + '</option>';
            })
            $("#dptBrands").empty();
            $("#dptBrands").append(html);
            $("#dptBrands").prepend('<option value="0">Select Brand</option>');
        },
        error: function (error) {
            console.log("brand error");
            console.log(error);
        }
    })
}



function LoadCategoryByBrandId() {
    var id = $("#dptBrands").val();
    $.ajax({
        url: 'https://localhost:44302/api/CommonData/LoadCategoryListByBrandId/' + id,
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (data) {

            var html = '';
            $.each(data, function (index, item) {
                html += '<option value="' + item.categoryId + '">' + item.categoryName + '</option>';
            })
            $("#dptCategoty").empty();
            $("#dptCategoty").append(html);
            $("#dptCategoty").prepend('<option value="0">Select Product</option>');
        },
        error: function (error) {
            console.log("Category error");
            console.log(error);
        }
    })
}

