<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberDashboard.aspx.cs" Inherits="MemberDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Member</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <style>
    
        body {
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: sans-serif;
            line-height: 1.5;
            min-height: 900px;
            background: #f3f3f3;
            flex-direction: column;
            margin: 0;
            background-image: url("gym.jpg");
            background-repeat: no-repeat;
            background-size: cover; 
            background-attachment: fixed; 
        }

        .navbar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 100%;
            padding: 10px 20px;
            background-color: rgba(0, 0, 0, 0.5);
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1000;
        }

        .navbar-logo {
            display: flex;
            align-items: center;
            color: white;
        }

        .navbar-logo img {
            height: 40px; 
            margin-right: 10px;
            filter: invert(100%);
        }

        .navbar-links {
            display: flex;
            gap: 20px;
        }

        .navbar-links button {
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            color: white;
            background-color: transparent;
            cursor: pointer;
        }

        .navbar-links button:hover {
            background-color: #186157;
        }

        .btn-logout{
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            color: white;
            background-color: transparent;
            cursor: pointer;
        }

        .btn-logout:hover{
            background-color: #186157;
        }

        .main {
            background-color: #fff;
            background-color: rgba(255, 255, 255, 0.589);
            border-radius: 15px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
            padding: 10px 20px;
            transition: transform 0.2s;
            width: 830px;
            text-align: center;
        }

        h1 {
            color: #228f80;
        }

        .button-container {
            display: flex;
            justify-content: center;
            gap: 20px;
            padding: 20px;
        }

        .aspbuttons {
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            color: white;
            background-color: #228f80;
            cursor: pointer;
        }

        .aspbuttons:hover {
            background-color: #186157;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <div class="navbar-logo">
                <img src="TRANSFORM.png" alt="TRANSFORM" style="padding-left: 10px;" />
                <!-- Replace "your-logo.png" with the path to your logo image -->
            </div>
            <div class="navbar-links">
                <button class="active">Home</button>
                <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn-logout" OnClick="btnLogout_Click"/>
            </div>
        </div>
        <div class="main">
            <h1>Welcome, Member!</h1>
            <div class="button-container">
                <asp:Button ID="CheckStatusButton" runat="server" CssClass="aspbuttons" Text="Check Status" OnClick="CheckStatusButton_Click" />
                <asp:Button ID="EditProfileButton" runat="server" CssClass="aspbuttons" Text="Edit Profile" OnClick="EditProfileButton_Click" />
                <asp:Button ID="ClassBookingButton" runat="server" CssClass="aspbuttons" Text="Class Booking" OnClick="ClassBookingButton_Click" />
             <asp:Button ID="ProgressReportButton" runat="server" CssClass="aspbuttons" Text="Progress Report" OnClick="ProgressReportButton_Click" /> 
<%--                <asp:Button ID="MembershipFeeButton" runat="server" CssClass="aspbuttons" Text="Membership Fee"/>--%>
            </div>
        </div>
    </form>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
