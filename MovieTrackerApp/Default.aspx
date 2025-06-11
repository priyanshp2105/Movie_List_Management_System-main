<%@ Page Title="Movie Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MovieTrackerApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container py-4">
            <div class="row mb-4">
                <div class="col-md-8">
                    <h1>My Movie Collection </h1>
                    <p class="lead">Track, organize, and discover your favorite movies</p>
                </div>
                <div class="col-md-4 text-end">
                    <asp:Button ID="btnAddMovie" runat="server" Text="Add New Movie" CssClass="btn btn-primary" OnClick="btnAddMovie_Click" />
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header bg-light">
                            <div class="row">
                                <div class="col-md-6">
                                    <h5 class="mb-0">My Movies</h5>
                                </div>
                                <div class="col-md-6 text-end">
                                    <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-select d-inline-block w-auto" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                        <asp:ListItem Text="All Movies" Value="" />
                                        <asp:ListItem Text="Watched" Value="Watched" />
                                        <asp:ListItem Text="Plan to Watch" Value="Plan to Watch" />
                                        <asp:ListItem Text="Unwatched" Value="Unwatched" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Label ID="lblNoMovies" runat="server" Text="You haven't added any movies yet. Click 'Add New Movie' to get started!" Visible="false" CssClass="text-muted"></asp:Label>
                            
                            <asp:GridView ID="gvMovies" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover" 
                                DataKeyNames="MovieID" OnRowCommand="gvMovies_RowCommand" OnRowDataBound="gvMovies_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                    <asp:BoundField DataField="Director" HeaderText="Director" SortExpression="Director" />
                                    <asp:BoundField DataField="ReleaseYear" HeaderText="Year" SortExpression="ReleaseYear" />
                                    <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("WatchStatus") %>' CssClass='<%# Eval("WatchStatus").ToString() == "Watched" ? "badge bg-success" : "badge" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rating">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRating" runat="server" Text='<%# Eval("Rating") != DBNull.Value ? Eval("Rating").ToString() + "/5" : "Not Rated" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditMovie" CommandArgument='<%# Eval("MovieID") %>' CssClass="btn btn-sm btn-outline-primary me-1">Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteMovie" CommandArgument='<%# Eval("MovieID") %>' CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to delete this movie?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="text-center p-4">
                                        <p class="text-muted">No movies found matching your criteria.</p>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                        <div class="card-body">
                            <h5 class="card-title">Movies Watched</h5>
                            <h2 class="display-4 text-primary"><asp:Label ID="lblWatchedCount" runat="server" Text="0"></asp:Label></h2>
                            <p class="card-text">Movies you've already watched</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                        <div class="card-body">
                            <h5 class="card-title">Plan to Watch</h5>
                            <h2 class="display-4 text-warning"><asp:Label ID="lblPlanToWatchCount" runat="server" Text="0"></asp:Label></h2>
                            <p class="card-text">Movies on your watchlist</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                        <div class="card-body">
                            <h5 class="card-title">Unwatched Movies</h5>
                            <h2 class="display-4 text-info"><asp:Label ID="lblUnwatchedCount" runat="server" Text="0"></asp:Label></h2>
                            <p class="card-text">Movies you haven't watched yet</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
