$(function () {
    $("[data-add-to-cart]").click(function () {
        var id = $(this).attr("data-add-to-cart");

        $.ajax({
            url: "/Cart/Add",
            data: { id: id },
            success: function (response) {
                $("#cart-cnt").html(response.Count);
                $("#cart-amt").html(Math.round(response.Amount))
            }
        });


        //hiệu ứng bay vào giỏ hàng (tốc độ 0.5s) và giỏ hàng rung khi có hàng mới
        var img = $(this).parents(".panel").find(".panel-body img");
        var css = ".cart-fly {background-image: url('" + img.attr("src") + "');background-size: 100% 100%;}";
        $("#cart-fly").html(css);

        img.stop().effect("transfer", { to: "#cart-img", className: "cart-fly" }, 500, function () {
            $("#cart-img").stop().effect("bounce");
        });


    });

    $("[data-remove-from-cart]").click(function () {
        var id = $(this).attr("data-remove-from-cart");

        $.ajax({
            url: "/Cart/Remove",
            data: { id: id },
            success: function (response) {
                //Cập nhập trên cart info
                $("#cart-cnt").html(response.Count);
                $("#cart-amt").html(Math.round(response.Amount));

                //Cập nhập trên bảng (trang Cart/Index)
                $("#table-cart-cnt").text(response.Count);
                $("#table-cart-amt").text(Math.round(response.Amount))
            }
        });

        //Hiệu ứng Xóa một dòng trên giao diện tốc độ 0.5s
        $(this).parents("tr").hide(500);
    });

    $("[data-update-cart]").change(function () {
        var id = $(this).attr("data-update-cart");
        var qty = $(this).val();

        $.ajax({
            url: "/Cart/Update",
            data: { id: id, newqty: qty },
            success: function (response) {
                //Cập nhập trên cart info
                $("#cart-cnt").html(response.Count);
                $("#cart-amt").html(Math.round(response.Amount));

                //Cập nhập trên bảng (trang Cart/Index)
                $("#table-cart-cnt").text(response.Count);
                $("#table-cart-amt").text(Math.round(response.Amount))
            }
        });

        //Cập nhật trên giao diện
        var price = parseFloat($(this).parents("tr").find("td:eq(2)").text());
        var discount = parseFloat($(this).parents("tr").find("td:eq(3)").text());
        var amount = Math.round( price * qty * (1 - discount));
        $(this).parents("tr").find("td:eq(5)").text(amount);
    });
});