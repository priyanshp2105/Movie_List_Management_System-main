<%@ Page Title="Add Movie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMovie.aspx.cs" Inherits="MovieTrackerApp.AddMovie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <h3 class="mb-0">Add New Movie</h3>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <label for="<%: txtTitle.ClientID %>" class="form-label">Movie Title</label>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter movie title" required></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                CssClass="text-danger" ErrorMessage="Movie title is required" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <label for="<%: txtDirector.ClientID %>" class="form-label">Director</label>
                            <asp:TextBox ID="txtDirector" runat="server" CssClass="form-control" placeholder="Enter director name"></asp:TextBox>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="<%: txtReleaseYear.ClientID %>" class="form-label">Release Year</label>
                                <asp:TextBox ID="txtReleaseYear" runat="server" CssClass="form-control" placeholder="Enter release year" TextMode="Number"></asp:TextBox>
                                <asp:RangeValidator ID="rvReleaseYear" runat="server" ControlToValidate="txtReleaseYear"
                                    CssClass="text-danger" ErrorMessage="Please enter a valid year (1900-2030)" 
                                    MinimumValue="1900" MaximumValue="2030" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="<%: ddlGenre.ClientID %>" class="form-label">Genre</label>
                                <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="-- Select Genre --" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Action" Value="Action"></asp:ListItem>
                                    <asp:ListItem Text="Adventure" Value="Adventure"></asp:ListItem>
                                    <asp:ListItem Text="Animation" Value="Animation"></asp:ListItem>
                                    <asp:ListItem Text="Comedy" Value="Comedy"></asp:ListItem>
                                    <asp:ListItem Text="Crime" Value="Crime"></asp:ListItem>
                                    <asp:ListItem Text="Documentary" Value="Documentary"></asp:ListItem>
                                    <asp:ListItem Text="Drama" Value="Drama"></asp:ListItem>
                                    <asp:ListItem Text="Fantasy" Value="Fantasy"></asp:ListItem>
                                    <asp:ListItem Text="Horror" Value="Horror"></asp:ListItem>
                                    <asp:ListItem Text="Mystery" Value="Mystery"></asp:ListItem>
                                    <asp:ListItem Text="Romance" Value="Romance"></asp:ListItem>
                                    <asp:ListItem Text="Sci-Fi" Value="Sci-Fi"></asp:ListItem>
                                    <asp:ListItem Text="Thriller" Value="Thriller"></asp:ListItem>
                                    <asp:ListItem Text="Western" Value="Western"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label for="<%: ddlWatchStatus.ClientID %>" class="form-label">Watch Status</label>
                                <asp:DropDownList ID="ddlWatchStatus" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Unwatched" Value="Unwatched" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Watched" Value="Watched"></asp:ListItem>
                                    <asp:ListItem Text="Plan to Watch" Value="Plan to Watch"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="<%: ddlRating.ClientID %>" class="form-label">Rating</label>
                                <asp:DropDownList ID="ddlRating" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="-- Select Rating --" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1 - Poor" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2 - Fair" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3 - Good" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4 - Very Good" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5 - Excellent" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="<%: txtNotes.ClientID %>" class="form-label">Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter any notes about the movie"></asp:TextBox>
                        </div>
                        <div class="mt-4">
                            <asp:Button ID="btnSave" runat="server" Text="Save Movie" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary ms-2" OnClick="btnCancel_Click" CausesValidation="false" />
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>