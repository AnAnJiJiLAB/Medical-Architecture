<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Userlogin.aspx.cs" Inherits="Userlogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>

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
            width: 466px;
            height: 360px;
            float: left;
            margin-left: 0px;
            margin-right: 0px;
        }
        .auto-style2 {
            height: 741px;
            width: 1300px;
            text-align: center;
        }
        .auto-style3 {
            width: 416px;
            height: 360px;
            float: left;
            margin-left: 0px;
            margin-top: 0px;
        }
        .auto-style4 {
            width: 406px;
            height: 367px;
            float: left;
            margin-left: 0px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style2">
    
        <br />
        <img class="auto-style3" src="images/Surface-Hub-Healthcare.png" /><img class="auto-style1" src="images/p1.jpg" /><img class="auto-style4" src="images/-Wbk9kpTURBXy82MDlhZTU0M2Q2MDAwZTA3YjUxMzI3M2UwN2FmY2E1NS5qcGeRlALNBLAAwoGhMQI.jpg" /><br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Medical Service Entrance" style="font-size: xx-large; font-weight: 700"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="hos_ID" runat="server" Width="250px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="hos_password" runat="server" type="password" Width="250px"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Login" runat="server" OnClick="Button1_Click" Text="Login" style="text-align: center" Height="47px" Width="107px" />
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="error" runat="server" style="color: #FF0000"></asp:Label>
    
    製作人: 張家銘</div>
    </form>
</body>
</html>
