using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class MoviebaseAdminView : System.Web.UI.Page
{
    string connectionString = "Data Source = PRD; Initial Catalog = dbMoviebase; Integrated Security = True";
    DateTime newTime = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        //--------------------------------------------------------------------------grab and set data (remember post backs)
        ddlMovies.Items.Insert(0, new ListItem("--Select Genre--", "0"));

        //-------------------------------------------------------------------------event handlers
        btnSubmitNewGenre.Click += new EventHandler(BtnSubmitNewGenre_Click);
        btnSubmitNewMovie.Click += new EventHandler(btnSubmitNewMovie_Click);
        btnRemoveMovie.Click += new EventHandler(btnRemoveMovie_Click);
        btnRemoveGenre.Click += new EventHandler(btnRemoveGenre_Click);
        btnAddNewUser.Click += new EventHandler(BtnAddUser_Click);
        btnRemoveUser.Click += new EventHandler(BtnRemoveUser_Click);
        ddlAdminControll.SelectedIndexChanged += new EventHandler(ddlAdminControll_SelectedIndexChange);
        ddlGenres.SelectedIndexChanged += new EventHandler(ddlGenres_SelectedIndexChanged);

        if (!Page.IsPostBack)
        {
            addGenre.Visible = false;
            remGenre.Visible = false;
            addMovie.Visible = false;
            remMovie.Visible = false;
            addUser.Visible = false;
            removeUser.Visible = false;
            UpdateGenreDdl();
            UpdateUserDdl();

            ddlGenres.Items.Insert(0, new ListItem("--Select Genre--", "0"));
            ddlGenres.SelectedIndex = 0;
        }
    }
 
    //--------------------------------------------------------------------------Gets Genre ID to pass along to new movie button
    private string getGenreID()
    {
        string sqlQuery = "SELECT genre_ID FROM [dbMoviebase].[dbo].[tblGenres] WHERE genre_name = '" + ddlGenre.Text + "';";
        string genreID = null;

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataReader rdr = null;

        conn.Open();

        rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            genreID = rdr[0].ToString();
        }

        return genreID;
    }

    //-------------------------------------------------------------------------- updates the list of movies in the remove ddl
    protected void UpdateMovieDdl()
    {
        string ddlGenreValue = ddlGenres.SelectedItem.ToString();
        string queryString = "SELECT movie_name FROM [dbMoviebase].[dbo].[tblMovies] INNER JOIN  [dbMoviebase].[dbo].[tblGenres] ON tblMovies.genre_ID = tblGenres.genre_ID WHERE tblGenres.genre_name = '" + ddlGenreValue + "';";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(queryString);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;
        conn.Open();

        ddlMovies.DataSource = cmd.ExecuteReader();
        ddlMovies.DataTextField = "movie_name";
        ddlMovies.DataValueField = "movie_name";
        ddlMovies.DataBind();

        conn.Close();
    }

    //--------------------------------------------------------------------------updates the list of genres in the remove ddl
    protected void UpdateGenreDdl()
    {
        string queryString = "SELECT genre_name FROM [dbMoviebase].[dbo].[tblGenres] ORDER BY genre_name ASC";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(queryString);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;
        conn.Open();

        ddlGenres.DataSource = cmd.ExecuteReader();
        ddlGenres.DataTextField = "genre_name";
        ddlGenres.DataValueField = "genre_name";
        ddlGenres.DataBind();

        conn.Close();

        conn.Open();

        ddlRemoveGenre.DataSource = cmd.ExecuteReader();
        ddlRemoveGenre.DataTextField = "genre_name";
        ddlRemoveGenre.DataValueField = "genre_name";
        ddlRemoveGenre.DataBind();

        conn.Close();
    }

    //--------------------------------------------------------------------------updates the list of users ddl
    protected void UpdateUserDdl()
    {
        DataClassesDataContext myDb = new DataClassesDataContext();
        var userList = from username in myDb.GetTable<tblAdmin>() select username;

        ddlUsers.DataSource = userList;

        ddlUsers.DataTextField = "username";
        ddlUsers.DataValueField = "username";
        ddlUsers.DataBind();
    }

    //--------------------------------------------------------------------------Action to take on genre ddl change
    protected void ddlGenres_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateMovieDdl();
    }

    protected void ddlMovies_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    //--------------------------------------------------------------------------New Genre Button
    private void BtnSubmitNewGenre_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("stpEditGenres", conn);
        SqlDataReader rdr = null;
        cmd.CommandType = CommandType.StoredProcedure;

        conn.Open();

        cmd.Parameters.Add(new SqlParameter("@genre_name", txtGenreAdd.Text));
        cmd.Parameters.Add(new SqlParameter("@created_date", newTime));

        rdr = cmd.ExecuteReader();

        gdvGenres.DataBind();
        ddlGenre.DataBind();
        UpdateGenreDdl();
    }

    //--------------------------------------------------------------------------New Movie Button
    private void btnSubmitNewMovie_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("stpEditMovies", conn);
        SqlDataReader rdr = null;
        cmd.CommandType = CommandType.StoredProcedure;

        conn.Open();

        cmd.Parameters.Add(new SqlParameter("@movie_name", txtMovieAdd.Text));
        cmd.Parameters.Add(new SqlParameter("@genre_id", getGenreID()));
        cmd.Parameters.Add(new SqlParameter("@created_date", newTime));

        rdr = cmd.ExecuteReader();


        gdvMovies.DataBind();
        UpdateMovieDdl();
    }



    //--------------------------------------------------------------------------Remove Movie Button
    protected void btnRemoveMovie_Click(object sender, EventArgs e)
    {
        string selectedMovie = ddlMovies.SelectedItem.ToString();
        

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("stpDeleteMovies_and_Genres", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader rdr = null;

        conn.Open();

        cmd.Parameters.Add(new SqlParameter("@movie_name", selectedMovie));

        rdr = cmd.ExecuteReader();

        gdvMovies.DataBind();
        UpdateMovieDdl();
    }

    //--------------------------------------------------------------------------Remove Genre Button
    protected void btnRemoveGenre_Click(object sender, EventArgs e)
    {
        try
        {
            string genreName = ddlRemoveGenre.SelectedItem.ToString();
            string queryString = "DELETE FROM [dbMoviebase].[dbo].[tblGenres] WHERE genre_name = '" + genreName + "';";


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdr = null;

            conn.Open();


            rdr = cmd.ExecuteReader();

            gdvGenres.DataBind();
            UpdateGenreDdl();
        }
        catch (System.Data.SqlClient.SqlException)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please clear out all movies before attempting to delete a genre.')", true);
        }
    }

    //--------------------------------------------------------------------------Add User Button LINQ
    protected void BtnAddUser_Click(object sender, EventArgs e)
    {
        DataClassesDataContext myDb = new DataClassesDataContext();
        tblAdmin myTbl = new tblAdmin();

        myTbl.username = txtNewUser.Text;
        myTbl.pass = txtNewPass.Text;

        myDb.tblAdmins.InsertOnSubmit(myTbl);
        myDb.SubmitChanges();
        UpdateUserDdl();

        gdvUsers.DataBind();
    }

    //--------------------------------------------------------------------------Remove User Button LINQ
    protected void BtnRemoveUser_Click(object sender, EventArgs e)
    {
        DataClassesDataContext myDb = new DataClassesDataContext();
        tblAdmin myTbl = myDb.tblAdmins.Single(username => username.username == ddlUsers.SelectedItem.ToString());

        myDb.tblAdmins.DeleteOnSubmit(myTbl);
        myDb.SubmitChanges();

        UpdateUserDdl();

        gdvUsers.DataBind();
    }



    //--------------------------------------------------------------------------controls options for admin
    protected void ddlAdminControll_SelectedIndexChange(object sender, EventArgs e)
    {
        string selectedOption = ddlAdminControll.SelectedValue.ToString();
        if (selectedOption == "select")
        {
            addGenre.Visible = false;
            remGenre.Visible = false;
            addMovie.Visible = false;
            remMovie.Visible = false;
            addUser.Visible = false;
            removeUser.Visible = false;
        }
        else if (selectedOption == "aGenre")
        {
            addGenre.Visible = true;
            remGenre.Visible = false;
            addMovie.Visible = false;
            remMovie.Visible = false;
            addUser.Visible = false;
            removeUser.Visible = false;
        }
        else if (selectedOption == "rGenre")
        {
            addGenre.Visible = false;
            remGenre.Visible = true;
            addMovie.Visible = false;
            remMovie.Visible = false;
            addUser.Visible = false;
            removeUser.Visible = false;
        }
        else if (selectedOption == "aMovie")
        {
            addGenre.Visible = false;
            remGenre.Visible = false;
            addMovie.Visible = true;
            remMovie.Visible = false;
            addUser.Visible = false;
            removeUser.Visible = false;
        }
        else if (selectedOption == "rMovie")
        {
            addGenre.Visible = false;
            remGenre.Visible = false;
            addMovie.Visible = false;
            remMovie.Visible = true;
            addUser.Visible = false;
            removeUser.Visible = false;
        }
        else if (selectedOption == "aUser")
        {
            addGenre.Visible = false;
            remGenre.Visible = false;
            addMovie.Visible = false;
            remMovie.Visible = false;
            addUser.Visible = true;
            removeUser.Visible = false;
        }
        else if (selectedOption == "rUser")
        {
            addGenre.Visible = false;
            remGenre.Visible = false;
            addMovie.Visible = false;
            remMovie.Visible = false;
            addUser.Visible = false;
            removeUser.Visible = true;
        }
    }
}