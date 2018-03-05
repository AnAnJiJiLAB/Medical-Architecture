<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wearable_device_real_time.aspx.cs" Inherits="wearable_device_real_time" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">

        .style53
        {
            font-size: x-large;
        }
        
.dropbtn {
    background-color: #4CAF50;
    color: white;
    padding: 16px;
    font-size: 16px;
    border: none;
    cursor: pointer;
}

.dropdown {
    position: relative;
    display: inline-block;
}

.dropdown-content {
    display: none;
    position: absolute;
    background-color: #f9f9f9;
    min-width: 250px;
    box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
    z-index: 1;
}

.dropdown-content a {
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    display: block;
}

.dropdown-content a:hover {background-color: #f1f1f1}

.dropdown:hover .dropdown-content {
    display: block;
}

.dropdown:hover .dropbtn {
    background-color: #3e8e41;
}
.button {
  background-color: #4CAF50;
    color: white;
    padding: 16px;
    font-size: 16px;
    border: none;
    cursor: pointer;
}

.button:hover {background-color: #3e8e41}

.button:active {
  background-color: #3e8e41;
  box-shadow: 0 5px #666;
  transform: translateY(4px);
}
        </style>
</head>
<body>
    <div></div>
    <form id="form1" runat="server">
            <%-- home --%>
<input class="button" type="button"value="Home" onclick="javascript: window.location.href = 'Medical_Menu.aspx'"/>
    <%-- User Features --%>
<div class="dropdown">
  <button class="dropbtn">User Features</button>
  <div class="dropdown-content">
    <a href="wearable_device_real_time.aspx">Wearable Device</a>
    <a href="MedicalHistory.aspx">ECG files</a>
  </div>
</div>
  <%-- Doctor Features --%>
 <div class="dropdown">
  <button class="dropbtn">Doctor Features</button>
  <div class="dropdown-content">
    <a href="#">User Medical Records</a>
    <a href="MedicalHistory.aspx">ECG files / Analysis </a>
    <a href="Emergency_Service.aspx">Emergency Service / Emergency Service </a>
  </div>
</div>

      <%-- Logout --%>

 <input class="button" type="button"value="Logout" onclick="javascript: window.location.href = 'Userlogin.aspx'"/>


    <div>
    
        <br />
        <br />
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Connect" />
    
        <asp:Button ID="Button2" runat="server" Text="Disconnect" OnClick="Button2_Click" />
    
        <asp:Label ID="Connect_Label" runat="server"></asp:Label>
        <br />
                        <asp:Label ID="Timer" runat="server" Text="Timer" CssClass="style53" Visible="False"></asp:Label>
        <br />
        &nbsp;<asp:Label ID="Label18" runat="server" Text="ID :"></asp:Label>
                <asp:Label ID="ID_2" runat="server"></asp:Label>
                &nbsp;
                <asp:Label ID="Label13" runat="server" Text="Hospital_ID :"></asp:Label>
                <asp:Label ID="Hospital_ID_2" runat="server"></asp:Label>
                &nbsp;
                <asp:Label ID="Label17" runat="server" Text="Leads"></asp:Label>
                <asp:DropDownList ID="ddl_leads" runat="server" AutoPostBack="True" Height="27px" Width="111px">
                    <asp:ListItem Selected="True">請選擇</asp:ListItem>
                    <asp:ListItem>I、II、III</asp:ListItem>
                    <asp:ListItem>avr、avf、avl</asp:ListItem>
                    <asp:ListItem>v1、v2、v3</asp:ListItem>
                    <asp:ListItem>v4、v5、v6</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="Label14" runat="server" Text="Date"></asp:Label>
                <asp:DropDownList ID="ddl_date" runat="server" Height="27px" Width="190px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="error" runat="server"></asp:Label>
                &nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <br />
                <asp:Label ID="itov4" runat="server"></asp:Label>
                <br />
                <asp:Chart ID="Chart1" runat="server" EnableViewState="False" Height="100px" Width="900px">
                    <Series>
                        <asp:Series ChartType="Line" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <br />
                <asp:Label ID="iitov5" runat="server"></asp:Label>
                <br />
                <asp:Chart ID="Chart2" runat="server" EnableViewState="False" Height="100px" Width="900px">
                    <Series>
                        <asp:Series ChartType="Line" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <br />
                <asp:Label ID="iiitov6" runat="server"></asp:Label>
                <br />
                <asp:Chart ID="Chart3" runat="server" EnableViewState="False" Height="100px" Width="900px">
                    <Series>
                        <asp:Series ChartType="Line" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <br />
                <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick">
                </asp:Timer>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    
        <br />
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server"  />
        <br />
    </div>
    </form>

<%--<script type="text/javascript">
function send(id) 
{

alert(id);

}
    function openPrompt(arg) {
      prompt("Test", arg);
      var I =  arg.split(',')[0] ;
    }  
    var arr = result;
 
</script>--%>

   <script type="text/javascript">

    //設定倒數秒數
    //var t = 60;

    //顯示倒數秒收
    //function showTime() {
        //t -= 1;
        //document.getElementById('Timer').innerHTML = t;
        <%--if (t == 0) {
            document.getElementById('<%= Btnsearh.ClientID %>').click();
            t = 60;
        }--%>
        
        //每秒執行一次,showTime()
    //    setTimeout("showTime()", 1000);
    //}

    //執行showTime()
    //showTime();
</script>




</body>
</html>
