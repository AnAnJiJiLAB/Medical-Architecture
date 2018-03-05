using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
public partial class Userlogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection Conn = new SqlConnection();
        string connString = WebConfigurationManager.ConnectionStrings["MedicalConnection"].ConnectionString;
        Conn.ConnectionString = connString;
        string str_sql = "";
       
        if (hos_ID.Text.Length != 0)
        {

            str_sql = " and ID = '" + hos_ID.Text + "'";
        }
        else
        {
            error.Text = "please check ID or Password ";
            return; 
        }

        if (hos_password.Text.Length != 0)
        {
            str_sql = " and password = '" + hos_password.Text + "'";
        }
        else
        {
            error.Text = "please check ID or Password ";
            return;
        }

        //label 顯示leads 

        str_sql = " select ID , password ,level,hospital_id from hos＿user where ID = '" + hos_ID.Text + "' and password ='"+ hos_password.Text + "'";
        SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
        DataSet ds = new DataSet();
        cmd1.Fill(ds, "User");
       
       
        int ECG_Rows = ds.Tables["User"].Rows.Count;
       if (ds.Tables["User"].Rows.Count == 1)
        {


            Session["ID"] = ds.Tables["User"].Rows[0].ItemArray[0].ToString();
            Session["Password"] = ds.Tables["User"].Rows[0].ItemArray[1].ToString();
            Session["level"] = ds.Tables["User"].Rows[0].ItemArray[2].ToString();
            Session["hospital_id"] = ds.Tables["User"].Rows[0].ItemArray[3].ToString();
            Response.Redirect("Medical_Menu.aspx");
            //error.Text = "Login success2";
            
        }
        else {

            error.Text = "Login failed";

        }
    }
}