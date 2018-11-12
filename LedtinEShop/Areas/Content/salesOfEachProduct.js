google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        type: "POST",
        url: "/Admin/Report/SalesOfEachProductJS",
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
                title: 'Quantity Sales Of Each Product',
                height: 400,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
            };

            var chart = new google.visualization.ColumnChart($("#quantitySalesOfEachProductChart")[0]);
            chart.draw(dtQuantity, optionsQuantity);

            //amount chart
            var dtAmount = new google.visualization.DataTable();
            dtAmount.addColumn('string', 'Name');
            dtAmount.addColumn('number', 'Amount');
            for (var i = 0; i < data.length; i++) {
                dtAmount.addRow([data[i].Name, data[i].Amount]);
            }

            var optionsAmount = {
                title: 'Amount Sales Of Each Product',
                height: 400,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
            };

            var chart = new google.visualization.ColumnChart($("#amountSalesOfEachProductChart")[0]);
            chart.draw(dtAmount, optionsAmount);

            //amount percent pie chart
            var dtAmountPercent = new google.visualization.DataTable();
            dtAmountPercent.addColumn('string', 'Name');
            dtAmountPercent.addColumn('number', 'Percent');
            for (var i = 0; i < data.length; i++) {
                dtAmountPercent.addRow([data[i].Name, data[i].Amount]);
            }

            var optionsAmountPercent = {
                title: "Amount Sales Percent Of Each Product",
                position: "none",
                is3D: true,
                fontsize: "100px",
            };
            var chart = new google.visualization.PieChart(document.getElementById('amountSalesOfEachProductPieChart'));
            chart.draw(dtAmountPercent, optionsAmountPercent);
        },
        failure: function (r) {
            alert(r.d);
        },
        error: function (r) {
            alert(r.d);
        }
    });
}