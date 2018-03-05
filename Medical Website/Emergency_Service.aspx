<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emergency_Service.aspx.cs" Inherits="Emergency_Service" %>

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

         .auto-style1 {
             color: #FF0000;
             font-size: 30pt;
         }
         .auto-style2 {
             font-size: 30pt;
             color: #006600;
         }

         .auto-style3 {
             height: 817px;
         }
         .auto-style4 {
             height: 729px;
         }

         .style53
        {
            font-size: x-large;
        }
        
         </style>
</head>
<body style="height: 978px">
    <form id="form1" runat="server" class="auto-style3">
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

    <div class="auto-style4">
    
        <br />
                        <asp:Label ID="Timer" runat="server" Text="Timer" CssClass="style53" Visible="False"></asp:Label>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <strong>
                <asp:Label ID="Label1" runat="server" CssClass="auto-style1" Text="Alert "></asp:Label>
                </strong>
<br />
                <br />
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView3_RowCommand" Width="772px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Unique_Value" HeaderText="Case" />
                        <asp:BoundField DataField="hospital_id" HeaderText="Hospital ID" />
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="age" HeaderText="Age" />
                        <asp:BoundField DataField="sex" HeaderText="Sex" />
                        <asp:BoundField DataField="address" HeaderText="Position" />
                        <asp:ButtonField CommandName="Send_a_car" HeaderText="Send a car" Text="Confirm" ButtonType ="Button">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ButtonField>
                        <asp:ButtonField  CommandName="Search_ECG" HeaderText="Search ECG" Text="Search" ButtonType ="Button">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Phone" Text="Call" ButtonType ="Button"/>
                        <asp:ButtonField CommandName="Cancel_C" HeaderText="Cancel" Text="Cancel" ButtonType ="Button"/>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
                <br />
                <br />
                <strong>
                <asp:Label ID="Label2" runat="server" CssClass="auto-style2" Text="Send an ambulance status"></asp:Label>
                </strong>
                <br />
                <br />
                <asp:GridView ID="GridView2" runat="server" Height="136px" Width="734px" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView2_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Unique_Value" HeaderText="Case" />
                        <asp:BoundField HeaderText="Hospital ID" DataField="hospital_id">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Name" DataField="ID">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="age" HeaderText="Age" />
                        <asp:BoundField DataField="sex" HeaderText="Sex" />
                        <asp:BoundField HeaderText="Position" DataField="address">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Car status" DataField="status_accept">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Timer ID="Timer1" runat="server" Interval="100" OnTick="Timer1_Tick">
                </asp:Timer>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
       
    
    </div>
         <br />
         <br />
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>