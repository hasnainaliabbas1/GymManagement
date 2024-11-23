<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberProgressForTrainer.aspx.cs" Inherits="MemberProgressForTrainer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>TRANSFORM</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <style>
        /* CSS styles */
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
            background-size: cover; /* Cover the entire page */
            background-attachment: fixed; /* Remain fixed in position */
            text-align: -webkit-center;
            text-align-last: center;
        }
        .main {
            background-color: #fff;
            background-color: rgba(255, 255, 255, 0.589);
            border-radius: 15px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
            padding: 10px 20px;
            transition: transform 0.2s;
            width: 500px;
            text-align: center;
        }

        h1 {
            color: #228f80;
        }

        label {
            display: block;
            width: 100%;
            margin-top: 10px;
            margin-bottom: 5px;
            text-align: left;
            color: #555;
            font-weight: bold;
        }

        input {
            display: block;
            margin: 0 auto; /* Center the input boxes */
            width: 100%; /* Adjust width as needed */
            margin-bottom: 15px;
            padding: 10px;
            box-sizing: border-box;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        button {
            padding: 15px;
            border-radius: 10px;
            margin-top: 15px;
            margin-bottom: 15px;
            border: none;
            color: white;
            cursor: pointer;
            background-color: #228f80;
            width: 100%;
            font-size: 16px;
        }

        .sub {
            display: flex;
            justify-content: center;
            align-items: center;
        }


        .navbar {
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 100%;
            padding: 10px 20px;
            background-color: rgba(0, 0, 0, 0.5); /* Adjust opacity as needed */
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
            height: 40px; /* Adjust height as needed */
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

    </style>
</head>
<body>
    <form runat="server" defaultbutton="Button1">
        <div class="navbar">
            <div class="navbar-logo">
                <img src="TRANSFORM.png" alt="TRANSFORM" style="padding-left: 10px;" />
                <!-- Replace "your-logo.png" with the path to your logo image -->
            </div>
            <div class="navbar-links">
                <asp:Button ID="btnHome" runat="server" Text="Home" CssClass="btn-logout" OnClick="btnHome_Click"/>
                <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn-logout" OnClick="btnLogout_Click"/>
            </div>
        </div>

        <!-- Main container for input field and button -->
        <div class="main" style=" margin: 10px 0px 10px 0px;">
            <h1>Member Progress</h1>
            <h3 style="color: #228f80;">Check Progress of a Member</h3>
            <div class="form-group">
                <div>
                        <asp:TextBox ID="MemberID" CssClass="form-control" runat="server" placeholder="Enter MemberID" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMemberID" CssClass="text-danger" runat="server" ErrorMessage="Required Field !" ControlToValidate="MemberID"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="sub">
                <div class="col-md-6">
                    <asp:Button ID="Button1" runat="server" Text="Check History" CssClass="btn btn-primary" OnClick="Button1_Click"/>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-2"></div>
                <div class="col-md-6">
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </div>

        <div class="main">
            <div class="progress-container">
                <asp:Literal ID="ltlMemberProgress" runat="server"></asp:Literal>
            </div>
        </div>

        <div class="main" style="width:1100px">
            <h2>Member Information</h2>
            <asp:GridView ID="GridViewMemberInfo" runat="server" CssClass="table" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="MemberID" HeaderText="Member ID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" />
                    <asp:BoundField DataField="MembershipType" HeaderText="Membership Type" />
                    <asp:BoundField DataField="MembershipStatus" HeaderText="Membership Status" />
                    <asp:BoundField DataField="Goals" HeaderText="Goals" />
                    <asp:BoundField DataField="Preferences" HeaderText="Preferences" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
