<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MovieTrackerApp.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - Movie Tracker</title>
    <link href="Content/custom.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
            padding-top: 50px;
        }
        .register-container {
            max-width: 500px;
            margin: 0 auto;
            background-color: #fff;
            padding: 30px;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }
        .register-title {
            text-align: center;
            margin-bottom: 30px;
            color: #343a40;
        }
        .btn-register {
            width: 100%;
            margin-top: 15px;
        }
        .login-link {
            text-align: center;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="register-container">
            <h2 class="register-title">Create an Account</h2>
            <form id="form1" runat="server">
                <div class="mb-3">
                    <label for="txtUsername" class="form-label">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Choose a username" required></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                        CssClass="text-danger" ErrorMessage="Username is required" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="Enter your email" required></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        CssClass="text-danger" ErrorMessage="Email is required" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        CssClass="text-danger" ErrorMessage="Please enter a valid email address"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Create a password" required></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                        CssClass="text-danger" ErrorMessage="Password is required" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="txtConfirmPassword" class="form-label">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm your password" required></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                        CssClass="text-danger" ErrorMessage="Please confirm your password" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPassword" runat="server" ControlToValidate="txtConfirmPassword"
                        ControlToCompare="txtPassword" CssClass="text-danger" ErrorMessage="Passwords do not match" Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary btn-register" OnClick="btnRegister_Click" />
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
                <div class="login-link">
                    <p>Already have an account? <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Login.aspx">Login here</asp:HyperLink></p>
                </div>
            </form>
        </div>
    </div>
</body>
</html>