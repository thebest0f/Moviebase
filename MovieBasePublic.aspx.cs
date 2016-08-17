using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class MovieBasePublic : System.Web.UI.Page
{
    string connectionString = "Data Source = PRD; Initial Catalog = dbMoviebase; Integrated Security = True";
    int genreCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        ObtainGenresData();
    }


    private void ObtainGenresData()
    {

        string sqlQuery = "SELECT genre_name FROM [dbMoviebase].[dbo].[tblGenres];";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataReader rdr = null;

        conn.Open();

        rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            string genreResults = rdr[0].ToString();

            HtmlGenericControl createH1 = new HtmlGenericControl("h1");

            createH1.ID = "" + rdr[0];
            createH1.Attributes["class"] = "h1Titles";
            createH1.Attributes["value"] = "" + rdr[0];
            createH1.InnerHtml = genreResults;
            MoviePanels.Controls.Add(createH1);


            HtmlGenericControl createP = new HtmlGenericControl("p");
            MoviePanels.Controls.Add(createP);


            HtmlGenericControl createPanel = new HtmlGenericControl("PANEL");
            createPanel.ID = "panel" + rdr[0];
            createPanel.Attributes["class"] = "MoviePanelsCSS";
            MoviePanels.Controls.Add(createPanel);

            ObtainMovieData(genreResults, createPanel);
        }
    }

    private void ObtainMovieData (string genreTitle, HtmlGenericControl panelToAdd)
    {
        string sqlQuery = "SELECT TOP 5 movie_name FROM [dbMoviebase].[dbo].[tblMovies] INNER JOIN[dbMoviebase].[dbo].[tblGenres] ON tblMovies.genre_ID = tblGenres.genre_ID WHERE tblGenres.genre_name = '" + genreTitle + "' ORDER BY tblMovies.created_date DESC;";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataReader rdr = null;

        conn.Open();

        rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            HtmlGenericControl createDiv = new HtmlGenericControl("li");
            createDiv.ID = "li" + rdr[0];
            createDiv.Attributes["class"] = "liMovies";
            createDiv.InnerHtml = rdr[0].ToString();
            panelToAdd.Controls.Add(createDiv);
        }
    }
}