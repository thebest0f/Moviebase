using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleMovie : System.Web.UI.Page
{
    string movieToLoad;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie genreCookie = new HttpCookie("movie");
        genreCookie = Request.Cookies["movie"];

        movieToLoad = genreCookie.Value;

        ValuesToLoad();
        
    }

    protected void ValuesToLoad()
    {
        h1MovieTitle.InnerHtml = movieToLoad;

        DataClassesDataContext mydb = new DataClassesDataContext();
        tblMovies mytbl = new tblMovies();

         var mYear = (from mn in mydb.tblMovies
                        where mn.movie_name == movieToLoad
                        select mn.movie_year).FirstOrDefault();

        var mDesc = (from mn in mydb.tblMovies
                     where mn.movie_name == movieToLoad
                     select mn.movie_description).FirstOrDefault();
        var mDire = (from mn in mydb.tblMovies
                     where mn.movie_name == movieToLoad
                     select mn.director).FirstOrDefault();

        if (mYear != null)
        {
            h2MovieYear.InnerHtml = mYear.ToString();
        }
        if (mDesc != null)
        {
            h3MovieDescription.InnerHtml = mDesc.ToString();
        }
        if (mDire != null)
        {
            h2MovieDirector.InnerHtml = mDire.ToString();
        }


    }
}