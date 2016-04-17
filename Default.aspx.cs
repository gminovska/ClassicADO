using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//add this
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    //decare the grants and services class so it is available
    //to all methods
    AuthorsAndBooks gs = new AuthorsAndBooks();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if it is not a postback--ie. if it is not
        //a repost due to an action on the page
        //call the FillDropDownList Method
        if (!Page.IsPostBack)
        {
            FillDropDownList();
        }
    }
    protected void BooksDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //when the index of the selected item in the
        //drop down box changes call the FillGridViewMethod
        FillGridView();
    }

    protected void FillDropDownList()
    {
        //Declare the dataTabe
        DataTable table = null; ;
        try
        {
            //call the method in the class to fill the table
             table= gs.GetAuthors();
        }
        catch(Exception ex)
        {
            ErrorLabel.Text=ex.Message;
        }
        //Attach the table as a datasource for the 
        //drop down list
        //assign the display and value fields
        AuthorDropDownList.DataSource = table;
        AuthorDropDownList.DataTextField = "AuthorName";
        AuthorDropDownList.DataValueField = "AuthorKey";
        AuthorDropDownList.DataBind();
        AuthorDropDownList.Items.Insert(0, new ListItem("Select an author", "0"));        
    }

    protected void FillGridView()
    {
        //Get the value from the drop down list
        //selected value
        int authorKey = int.Parse(AuthorDropDownList.SelectedValue.ToString());
        DataTable tbl = new DataTable();
        try
        {
            //call the GetServiceGrants field and pass 
            //it the service key
            //tbl = gs.;
            tbl = gs.GetBooks(authorKey);
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = ex.Message;
        }

        //attach the table as a data source to the
        //gridView
        BooksGridView.DataSource = tbl;
        BooksGridView.DataBind();
    }
}