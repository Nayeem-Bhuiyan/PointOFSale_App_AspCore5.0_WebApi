$(document).ready(function () {
    LoadBrandsByProductId();
});

function LoadBrandsByProductId() {
    var id = 1;
    $.ajax({
        url: 'https://localhost:44302/api/Brands/' + id,
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        success: function (data) {

            var html = '';
            $.each(data, function (index, item) {
                html += '<option value="' + item.brandId + '">' + item.brandName + '</option>';
            })
            $("#dptBrand").empty();
            $("#dptBrand").append(html);
            $("#dptBrand").prepend('<option value="0">Select Brand</option>');
        },
        error: function (error) {
            alert("Error has found");
        }
    })
}
