using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class LoginPage : System.Web.UI.Page
{
    string connectionString = "Data Source = PRD; Initial Catalog = dbMoviebase; Integrated Security = True";

    protected void Page_Load(object sender, EventArgs e)
    {
        btnSubmitLoginCheck.Click += new EventHandler(btnSubmitLoginCheck_Click);
       
    }

    protected void btnSubmitLoginCheck_Click (object sender, EventArgs e)
    {
       
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("stpQueryAdmin", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader rdr = null;

        conn.Open();

        cmd.Parameters.Add(new SqlParameter("@username", txtUsername.Text));
        cmd.Parameters.Add(new SqlParameter("@password", txtPass.Text));

        rdr = cmd.ExecuteReader();

        rdr.Read();
      
        if (rdr.HasRows)
        {
            Response.Redirect("MoviebaseAdminView.aspx");
        }
        else
        {
            errorMessage.Style.Add("color", "red");
            errorMessage.InnerHtml = "Please enter valid username and password";
        }
          

        conn.Close();
    }
}