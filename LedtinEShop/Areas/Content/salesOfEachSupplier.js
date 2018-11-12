google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        type: "POST",
        url: "/Admin/Report/SalesOfEachSupplierJS",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //quantity chart
            var dtQuantity = new google.visualization.DataTable();
            dtQuantity.addColumn('string', 'Name');
            dtQuantity.addColumn('number', 'Quantity');
            for (var i = 0; i < data.length; i++) {
                dtQuantity.addRow([data[i].Name, data[i].Quantity]);
            }

            var optionsQuantity = {
                title: 'Quantity Sales Of Each Supplier',
                height: 400,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
            };

            var chart = new google.visualization.ColumnChart($("#quantitySalesOfEachSupplierChart")[0]);
            chart.draw(dtQuantity, optionsQuantity);


            //quantity percent pie chart
            var dtQuantityPercent = new google.visualization.DataTable();
            dtQuantityPercent.addColumn('string', 'Name');
            dtQuantityPercent.addColumn('number', 'Percent');
            for (var i = 0; i < data.length; i++) {
                dtQuantityPercent.addRow([data[i].Name, data[i].Quantity]);
            }

            var optionsQuantityPercent = {
                title: "Quantity Sales Percent Of Each Supplier",
                position: "none",
                is3D: true,
                fontsize: "100px",
            };
            var chart = new google.visualization.PieChart(document.getElementById('quantitySalesOfEachSupplierPieChart'));
            chart.draw(dtQuantityPercent, optionsQuantityPercent);
        },
        failure: function (r) {
            alert(r.d);
        },
        error: function (r) {
            alert(r.d);
        }
    });
}