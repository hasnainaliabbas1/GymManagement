<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

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
            min-height: 900px;
            background: #f3f3f3;
            flex-direction: column;
            margin: 0;
            background-image: url("gym.jpg");
            background-repeat: no-repeat;
            background-size: cover; /* Cover the entire page */
            background-attachment: fixed; /* Remain fixed in position */
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
            margin-top: 105px;
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

        .btn-Login{
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            color: white;
            background-color: transparent;
            cursor: pointer;
        }

        .btn-Login:hover{
            background-color: #186157;
        }

    </style>
</head>
<body>
    <form runat="server" onsubmit="return validateDate()">
        <div class="navbar">
            <div class="navbar-logo">
                <img src="TRANSFORM.png" alt="TRANSFORM" style="padding-left: 10px;" />
                <!-- Replace "your-logo.png" with the path to your logo image -->
            </div>
            <div class="navbar-links">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-Login" OnClick="ButtonLogin_Click" CausesValidation="false"/>
            </div>
        </div>

        <div class="main">
            <h1>TRANSFORM</h1>
            <h3 style="color: #228f80;">Register your account</h3>
                <div class="form-group">
                    <asp:Label ID="LabelFirstName" runat="server" CssClass="col-md-2 control-label" Text="First Name"></asp:Label>
                    <div>
                        <asp:TextBox ID="FirstName" CssClass="form-control" runat="server" placeholder="Enter your First Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" CssClass="text-danger" runat="server" ErrorMessage="The First Name field is Required !" ControlToValidate="FirstName"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelLastName" runat="server" CssClass="col-md-2 control-label" Text="Last Name"></asp:Label>
                    <div>
                        <asp:TextBox ID="LastName" CssClass="form-control" runat="server" placeholder="Enter your Last Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastName" CssClass="text-danger" runat="server" ErrorMessage="The Last Name field is Required !" ControlToValidate="LastName"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelEmail" runat="server" CssClass="col-md-2 control-label" Text="Email"></asp:Label>
                    <div>
                        <asp:TextBox ID="Email" CssClass="form-control" runat="server" placeholder="Enter your Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" CssClass="text-danger" runat="server" ErrorMessage="The Email field is Required !" ControlToValidate="Email"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelPassword" runat="server" CssClass="col-md-2 control-label" Text="Password"></asp:Label>
                    <div>
                        <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password" placeholder="Enter your Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" CssClass="text-danger" runat="server" ErrorMessage="The Password field is Required !" ControlToValidate="Password"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelPhone" runat="server" CssClass="col-md-2 control-label" Text="Phone"></asp:Label>
                    <div>
                        <asp:TextBox ID="Phone" CssClass="form-control" runat="server" placeholder="Enter your Phone"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" CssClass="text-danger" runat="server" ErrorMessage="The Phone field is Required !" ControlToValidate="Phone"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelGender" runat="server" CssClass="col-md-2 control-label" Text="Gender"></asp:Label>
                    <div>
                        <asp:DropDownList ID="Gender" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Select Gender" Value=""></asp:ListItem>
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorGender" CssClass="text-danger" runat="server" ErrorMessage="The Gender field is Required !" ControlToValidate="Gender"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelBirthDate" runat="server" CssClass="col-md-2 control-label" Text="BirthDate"></asp:Label>
                    <asp:TextBox ID="Day" runat="server" CssClass="form-control" placeholder="DD"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDay" CssClass="text-danger" runat="server" ErrorMessage="The Day field is Required !" ControlToValidate="Day"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="Month" runat="server" CssClass="form-control" placeholder="MM"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonth" CssClass="text-danger" runat="server" ErrorMessage="The Month field is Required !" ControlToValidate="Month"></asp:RequiredFieldValidator>                
                </div>
                <div class="form-group">
                    <asp:TextBox ID="Year" runat="server" CssClass="form-control" placeholder="YYYY"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" CssClass="text-danger" runat="server" ErrorMessage="The Year field is Required !" ControlToValidate="Year"></asp:RequiredFieldValidator>                
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelMembershipType" runat="server" CssClass="col-md-2 control-label" Text="Membership Type"></asp:Label>
                    <div>
                        <asp:DropDownList ID="MembershipType" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Select Membership Type" Value=""></asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                            <asp:ListItem Text="Annual" Value="Annual"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMembershipType" CssClass="text-danger" runat="server" ErrorMessage="The Membership Type field is Required !" ControlToValidate="MembershipType"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelGoals" runat="server" CssClass="col-md-2 control-label" Text="Goals"></asp:Label>
                    <div>
                        <asp:TextBox ID="Goals" CssClass="form-control" runat="server" placeholder="Enter your Goals"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelPreferences" runat="server" CssClass="col-md-2 control-label" Text="Preferences"></asp:Label>
                    <div>
                        <asp:TextBox ID="Preferences" CssClass="form-control" runat="server" placeholder="Enter your Preferences"></asp:TextBox>
                    </div>
                </div>
                <div class="sub">
                    <div class="col-md-6">
                        <asp:Button ID="ButtonRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="ButtonRegister_Click"/>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-2"></div>
                    <div class="col-md-6">
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
                <asp:HiddenField ID="HiddenDate" runat="server" />
        </div>
    </form>
    <script>
        function validateDate() {
            var day = document.getElementById('<%= Day.ClientID %>').value;
            var month = document.getElementById('<%= Month.ClientID %>').value;
            var year = document.getElementById('<%= Year.ClientID %>').value;

            // Combine day, month, and year into a single string
            var dateString = year + "-" + month + "-" + day;

            // Convert the string to a Date object
            var dateObject = new Date(dateString);

            // Check if the Date object is valid
            if (isNaN(dateObject.getTime())) {
                alert("Invalid Birth date. Please enter a valid date.");
                return false;
            }
            document.getElementById('<%= HiddenDate.ClientID %>').value = dateString;
            return true;
        }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
