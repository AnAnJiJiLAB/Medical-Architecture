<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalHistory.aspx.cs" Inherits="MedicalHistory" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style>

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

         .auto-style2 {
             width: 159px;
         }
         .auto-style3 {
             text-align: left;
             width: 343px;
         }
         .auto-style4 {
             width: 343px;
         }
         .auto-style5 {
             width: 278px;
         }
         .auto-style6 {
             width: 509px;
             font-size: xx-large;
         }

         .auto-style7 {
             width: 159px;
             height: 19px;
         }
         .auto-style8 {
             width: 343px;
             height: 19px;
         }
         .auto-style9 {
             width: 278px;
             height: 19px;
         }
         .auto-style10 {
             width: 346px;
             height: 324px;
         }

     </style>
</head>
<body style="height: 978px">
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

 <input class="button" type="button"value="Logout" onclick="javascript: window.location.href ='Userlogin.aspx'"/>

    <div style="height: 478px">
    
        <br />
        <br />
        <br />
    
        <asp:Label ID="Label12" runat="server" Text="ID"></asp:Label>
        <asp:TextBox ID="text_ID" runat="server"></asp:TextBox>
&nbsp;<asp:Label ID="Label13" runat="server" Text="Hospital_ID"></asp:Label>
        <asp:TextBox ID="text_hos_ID" runat="server" Height="19px"></asp:TextBox>
        <asp:Label ID="Label17" runat="server" Text="Leads"></asp:Label>
        <asp:DropDownList ID="ddl_leads" runat="server" Height="27px" Width="111px" AutoPostBack="True" OnSelectedIndexChanged="ddl_leads_SelectedIndexChanged">
            <asp:ListItem Selected="True">請選擇</asp:ListItem>
            <asp:ListItem>I、II、III</asp:ListItem>
            <asp:ListItem>avr、avf、avl</asp:ListItem>
            <asp:ListItem>v1、v2、v3</asp:ListItem>
            <asp:ListItem>v4、v5、v6</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddl_samples" runat="server" Height="27px" Width="111px" AutoPostBack="True" Visible="False" >
            <asp:ListItem>請選擇</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label14" runat="server" Text="Date"></asp:Label>
        <asp:DropDownList ID="ddl_date_samples" runat="server" Height="27px" Width="190px">
            <asp:ListItem>請選擇</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label15" runat="server" Text="Width x Height"></asp:Label>
        <asp:DropDownList ID="ddl_size" runat="server">
            <asp:ListItem>2000*100</asp:ListItem>
            <asp:ListItem>10000*500</asp:ListItem>
        </asp:DropDownList>
&nbsp;<asp:Label ID="error" runat="server"></asp:Label>
        <br />
        <br />
    
        <asp:Button ID="Btn_Search" runat="server" OnClick="Search_Click" Text="Search" Width="82px" />
       
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Analysis" runat="server" Text="Analysis" Visible="False" OnClick="Analysis_Click" />
       
    
        <br />
       
    
        <br />
        <asp:Label ID="itov4" runat="server"></asp:Label>
        <br />
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="1000px" HorizontalAlign="Left">
            <asp:Chart ID="Chart1" runat="server" Width="2000px" EnableViewState="False" Height="100px">
                <Series>
                    <asp:Series ChartType="Line" Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </asp:Panel>
       
    
        <asp:Label ID="iitov5" runat="server"></asp:Label>
        <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" Width="1000px" HorizontalAlign="Left">
            <asp:Chart ID="Chart2" runat="server" Width="2000px" EnableViewState="False" Height="100px">
                <Series>
                    <asp:Series ChartType="Line" Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </asp:Panel>
       
    
        <asp:Label ID="iiitov6" runat="server"></asp:Label>
        <br />
        <asp:Panel ID="Panel3" runat="server" ScrollBars="Horizontal" Width="1000px" HorizontalAlign="Left">
            <asp:Chart ID="Chart3" runat="server" Width="2000px" EnableViewState="False" Height="100px">
                <Series>
                    <asp:Series ChartType="Line" Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </asp:Panel>
       
    
        <br />
       
    
        <br />
       
    
    </div>
        
         <br />
         <br />
         <br />
         <strong>
         <br />
         <br />
         </strong>
         <br />
         <br />
         <br />
         <asp:Panel ID="Panel4" runat="server" Visible="False">
             <strong>Myocardial Infarction status :
             <asp:Label ID="Label_sick_health" runat="server"></asp:Label>
             <br />
             Assuming the location of Myocardial Infarction probability: </strong>
             <br />
             <table style="width:100%;">
                 <tr>
                     <td class="auto-style2">
                         <br />
                         <img class="auto-style10" src="images/Heart.png" />
                     </td>
                     <td class="auto-style3">
                         <h1 class="auto-style6">Anterior :
                             <asp:Label ID="Label_Anterior" runat="server"></asp:Label>
                         </h1>
                         <h1 class="auto-style6">Antero-septal :
                             <asp:Label ID="Label_Antero_septal" runat="server"></asp:Label>
                         </h1>
                         <h1 class="auto-style6">Antero-lateral :
                             <asp:Label ID="Label_Antero_lateral" runat="server"></asp:Label>
                         </h1>
                         <h1 class="auto-style6">Inferior :
                             <asp:Label ID="Label_Inferior" runat="server"></asp:Label>
                         </h1>
                         <h1 class="auto-style6">Infero-latera :
                             <asp:Label ID="Label_Infero_latera" runat="server"></asp:Label>
                         </h1>
                     </td>
                     <td class="auto-style5">&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style2">&nbsp;</td>
                     <td class="auto-style4">&nbsp;</td>
                     <td class="auto-style5">&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style7"></td>
                     <td class="auto-style8"></td>
                     <td class="auto-style9"></td>
                 </tr>
             </table>
         </asp:Panel>
         <br />
         <br />
         <br />
         <br />
         <br />
         <br />
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
