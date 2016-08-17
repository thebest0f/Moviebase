using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class MovieBaseMasterPage : System.Web.UI.MasterPage
{

    string connectionString = "Data Source = PRD; Initial Catalog = dbMoviebase; Integrated Security = True";

    protected void Page_Load(object sender, EventArgs e)
    {
        btnSubmitMovieSearch.Click += new EventHandler(BtnSubmitMovieSearch_Click);
    }

    protected void BtnSubmitMovieSearch_Click(object sender, EventArgs e)
    {
        HttpCookie movieCookie = new HttpCookie("movie");
        movieCookie.Value = txtSearch.Text;
        Response.Cookies.Add(movieCookie);
        Response.Redirect("SingleMovie.aspx");
    }
}
