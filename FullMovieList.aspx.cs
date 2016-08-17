using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class FullMovieList : System.Web.UI.Page
{

    string connectionString = "Data Source = PRD; Initial Catalog = dbMoviebase; Integrated Security = True";
    string genreToLoad;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie genreCookie = new HttpCookie("genre");
        genreCookie = Request.Cookies["genre"];

        genreToLoad = genreCookie.Value;

        GetValuesForPageLoad();
    }

    private void GetValuesForPageLoad()
    {
        genreLabel.InnerHtml = genreToLoad;


        string sqlQuery = "SELECT movie_name FROM [dbMoviebase].[dbo].[tblMovies] INNER JOIN[dbMoviebase].[dbo].[tblGenres] ON tblMovies.genre_ID = tblGenres.genre_ID WHERE tblGenres.genre_name = '" + genreToLoad + "' ORDER BY tblMovies.movie_name ASC;";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataReader rdr = null;

        conn.Open();

        rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            HtmlGenericControl createLi = new HtmlGenericControl("li");
            createLi.ID = "li" + rdr[0];
            createLi.Attributes["class"] = "liMovies";
            createLi.InnerHtml = rdr[0].ToString();
            FullMoviePanel.Controls.Add(createLi);
        }
    }
}