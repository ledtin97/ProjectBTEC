google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        type: "POST",
        url: "/Admin/Report/SalesOfEachMonthJS",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //amount chart
            var dtAmount = new google.visualization.DataTable();
            dtAmount.addColumn('string', 'Month');
            dtAmount.addColumn('number', 'Amount');
            for (var i = 0; i < data.length; i++) {
                dtAmount.addRow([data[i].Id, data[i].Amount]);
            }

            var optionsAmount = {
                title: 'Amount Sales Of Each Month',
                height: 400,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
            };

            var chart = new google.visualization.ColumnChart($("#amountSalesOfEachMonthChart")[0]);
            chart.draw(dtAmount, optionsAmount);


            //amount percent pie chart
            var dtAmountPercent = new google.visualization.DataTable();
            dtAmountPercent.addColumn('string', 'Month');
            dtAmountPercent.addColumn('number', 'Percent');
            for (var i = 0; i < data.length; i++) {
                dtAmountPercent.addRow([data[i].Id, data[i].Amount]);
            }

            var optionsAmountPercent = {
                title: "Amount Sales Percent Of Each Month",
                position: "none",
                is3D: true,
                fontsize: "100px",
            };
            var chart = new google.visualization.PieChart(document.getElementById('amountSalesOfEachMonthPieChart'));
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