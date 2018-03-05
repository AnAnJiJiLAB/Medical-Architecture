<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Medical_Menu.aspx.cs" Inherits="Medical_Menu" %>

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
            width: 714px;
            height: 407px;
            margin-top: 0px;
        }
        .auto-style2 {
            height: 457px;
        }

    </style>
</head>
<body style="height: 607px; width: 865px;">
    <form id="form1" runat="server">
        <div>
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





<h2>Welcome to Medical website</h2>
<p>HI ! 
    <asp:Label ID="hos_ID" runat="server"></asp:Label>
        </p>

        </div>
        <p class="auto-style2">
            <img class="auto-style1" src="images/section1bg2.jpg" />https://www.medicaltravel.org.tw/</p>
    </form>
</body>
</html>
