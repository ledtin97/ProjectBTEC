$(function () {

    $("[data-delivery2]").click(function () {
        var id = $(this).attr("data-delivery2");

        $.ajax({
            url: "/Admin/OrdersManagement/Delivered2",
            data: { id: id },
            success: function () {

            },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
        });
        
        //Hiệu ứng Xóa một dòng trên giao diện tốc độ 0.5s
        $(this).parents("tr").hide(500);
    });

    $("[data-cancle-delivery2]").click(function () {
        var id = $(this).attr("data-cancle-delivery2");

        $.ajax({
            url: "/Admin/OrdersManagement/CancelDelivered2",
            data: { id: id },
            success: function () {
                
            },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
        });

        //Hiệu ứng Xóa một dòng trên giao diện tốc độ 0.5s
        $(this).parents("tr").hide(500);
    });
});