<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testjavascript.aspx.cs" Inherits="testjavascript" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

    <canvas id="myChart" width="100" height="100"></canvas>
<script type ="text/javascript">
    function ShowResults(value, index, ar) {
        document.write("value: " + value);
        document.write(" index: " + index);
        document.write("<br />");
    }
    var text;
    var aa2 = ["<%=aa%>"];
    for (i = 0; i < aa2.length; i++) {
        text += aa2[i] + "<br>";
    }
    
   // aa2.forEach(ShowResults);
   // alert(aa);
        var ctx = document.getElementById("myChart");
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ["0", "1"], //軸
                datasets: [{

                    label: '# of Votes',
                    //backgroundColor: window.chartColors.red,
                    borderColor: 'rgb(255, 99, 132)',
                    //data y 軸
                    data: ["1", aa2],
                    fill: false,
                    pointRadius: 0,
                    pointStyle: false,
                    borderWidth: 1
                }]
            },
            options: {

                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        }
        );
   

</script>
</body>
</html>
