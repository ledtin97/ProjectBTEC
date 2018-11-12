google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        type: "POST",
        url: "/Admin/Report/SalesOfEachYearJS",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //amount chart
            var dtAmount = new google.visualization.DataTable();
            dtAmount.addColumn('string', 'Year');
            dtAmount.addColumn('number', 'Amount');
            for (var i = 0; i < data.length; i++) {
                dtAmount.addRow([data[i].Id, data[i].Amount]);
            }

            var optionsAmount = {
                title: 'Amount Sales Of Each Year',
                height: 400,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
            };

            var chart = new google.visualization.ColumnChart($("#amountSalesOfEachYearChart")[0]);
            chart.draw(dtAmount, optionsAmount);


            //amount percent pie chart
            var dtAmountPercent = new google.visualization.DataTable();
            dtAmountPercent.addColumn('string', 'Year');
            dtAmountPercent.addColumn('number', 'Percent');
            for (var i = 0; i < data.length; i++) {
                dtAmountPercent.addRow([data[i].Id, data[i].Amount]);
            }

            var optionsAmountPercent = {
                title: "Amount Sales Percent Of Each Year",
                position: "none",
                is3D: true,
                fontsize: "100px",
            };
            var chart = new google.visualization.PieChart(document.getElementById('amountSalesOfEachYearPieChart'));
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