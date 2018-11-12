google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawChart);
function drawChart() {
    $.ajax({
        type: "POST",
        url: "/Admin/Report/SalesOfEachCustomerJS",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //amount chart
            var dtAmount = new google.visualization.DataTable();
            dtAmount.addColumn('string', 'Name');
            dtAmount.addColumn('number', 'Amount');
            for (var i = 0; i < data.length; i++) {
                dtAmount.addRow([data[i].Name, data[i].Amount]);
            }

            var optionsAmount= {
                title: 'Amount Sales Of Each Customer',
                height: 400,
                is3D: true,
                bar: { groupWidth: "100%" },
                legend: { position: "none" },
            };

            var chart = new google.visualization.ColumnChart($("#quantitySalesOfEachCustomerChart")[0]);
            chart.draw(dtAmount, optionsAmount);


            //amount percent pie chart
            var dtAmountPercent = new google.visualization.DataTable();
            dtAmountPercent.addColumn('string', 'Name');
            dtAmountPercent.addColumn('number', 'Percent');
            for (var i = 0; i < data.length; i++) {
                dtAmountPercent.addRow([data[i].Name, data[i].Amount]);
            }

            var optionsAmountPercent = {
                title: "Amount Sales Percent Of Each Customer",
                position: "none",
                is3D: true,
                fontsize: "100px",
            };
            var chart = new google.visualization.PieChart(document.getElementById('quantitySalesOfEachCustomerPieChart'));
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