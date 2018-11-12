google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        type: "POST",
        url: "/Admin/Report/InventoryJS",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dt = new google.visualization.DataTable();
            dt.addColumn('string', 'Name');
            dt.addColumn('number', 'Quantity');
            for (var i = 0; i < data.length; i++) {
                dt.addRow([data[i].Name, data[i].Quantity]);
            }
            var options = {
                title: 'Product inventories',
                height: 400,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
            };
            var chart = new google.visualization.ColumnChart($("#inventoryChart")[0]);
            chart.draw(dt, options);
        },
        failure: function (r) {
            alert(r.d);
        },
        error: function (r) {
            alert(r.d);
        }
    });
}