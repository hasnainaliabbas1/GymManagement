<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Form1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>TRANSFORM</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <style>
        body {
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: sans-serif;
            line-height: 1.5;
            min-height: 100vh;
            background: #f3f3f3;
            margin: 0;
            background-image: url("gym.jpg");
            background-repeat: no-repeat;
            background-size: cover;
            background-attachment: fixed;
        }

        .main {
            background-color: rgba(255, 255, 255, 0.9);
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
            padding: 30px 40px;
            width: 400px;
            text-align: left;
        }

        h1 {
            text-align: center;
            color: #228f80;
            font-size: 2rem;
            margin-bottom: 10px;
        }

        h3 {
            text-align: center;
            color: #228f80;
            font-size: 1.2rem;
            margin-bottom: 20px;
        }

        label {
            display: block;
            margin-bottom: 5px;
            color: #333;
            font-weight: bold;
            font-size: 0.9rem;
            text-align: left;
        }

        input, select {
            width: 100%;
            padding: 10px;
            margin-bottom: 10px; /* Reduced space for alignment */
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 1rem;
        }

        .text-danger {
            display: block;
            margin-top: -10px;
            margin-bottom: 15px; /* Extra space for error text */
            color: red;
            font-size: 0.85rem;
        }

        .form-group {
            margin-bottom: 25px; /* Ensures uniform spacing between fields */
        }

        button {
            display: block;
            width: 100%;
            padding: 10px;
            margin-top: 20px;
            border-radius: 5px;
            border: none;
            color: white;
            cursor: pointer;
            background-color: #007bff;
            font-size: 1rem;
            transition: background-color 0.3s ease;
        }

        button:hover {
            background-color: #0056b3;
        }

        .navbar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px 20px;
            background-color: rgba(0, 0, 0, 0.5);
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            z-index: 10;
        }

        .navbar-logo {
            color: white;
            font-size: 1.2rem;
            font-weight: bold;
        }

        .btn-Register {
            color: white;
            background-color: transparent;
            border: none;
            cursor: pointer;
            font-size: 1rem;
        }

        .btn-Register:hover {
            background-color: #186157;
        }
    </style>
</head>
<body>
    <form runat="server" defaultbutton="Button1">
        <div class="navbar">
            <div class="navbar-logo">
                <img src="TRANSFORM.png" alt="TRANSFORM" style="height: 40px; margin-right: 10px;">
                TRANSFORM
            </div>
            <div class="navbar-links">
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn-Register" OnClick="BtnRegister_Click" CausesValidation="false" />
            </div>
        </div>

        <div class="main">
            <h1>TRANSFORM</h1>
            <h3>Enter your login credentials</h3>

            <!-- Email Field -->
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="UserName" runat="server" placeholder="Enter your Username"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server" ControlToValidate="UserName" ErrorMessage="Email is required!" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <!-- Password Field -->
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="Password" runat="server" TextMode="Password" placeholder="Enter your Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass" runat="server" ControlToValidate="Password" ErrorMessage="Password is required!" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <!-- Role Selection -->
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Select Role"></asp:Label>
                <asp:DropDownList ID="ddlRole" runat="server">
                    <asp:ListItem Text="Select Role" Value="" />
                    <asp:ListItem Text="Member" Value="Member" />
                    <asp:ListItem Text="Trainer" Value="Trainer" />
                    <asp:ListItem Text="Manager" Value="Manager" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorRole" runat="server" ControlToValidate="ddlRole" InitialValue="" ErrorMessage="Role is required!" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <!-- Submit Button -->
            <asp:Button ID="Button1" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="Button1_Click" />

            <!-- Error Label -->
            <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </form>
</body>
</html>
