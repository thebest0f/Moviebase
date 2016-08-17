using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FetchMovieTitles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static List<string> GetMovieTitles(string movie_name)
    {
        List<string> MovieTitle = new List<string>();

        string connectionString = "Data Source = PRD; Initial Catalog = dbMoviebase; Integrated Security = True";
        string sqlQuery = string.Format("SELECT movie_name FROM [dbMoviebase].[dbo].[tblMovies] WHERE movie_name LIKE '%{0}%'", movie_name);
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataReader rdr = null;

        conn.Open();

        rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            MovieTitle.Add(rdr.GetString(0));
        }

        return MovieTitle;
    }
}