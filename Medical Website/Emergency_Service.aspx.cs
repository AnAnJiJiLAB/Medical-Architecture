using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Drawing;
//for Upload Excel==
using System.Data.OleDb;

public partial class Emergency_Service : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGridToSource();
    }


    private void BindGridToSource()
    {

        GridView3.DataSource = null;
       // GridView1.DataBind();
        GridView2.DataSource = null;
       // GridView2.DataBind();


        SqlConnection Conn = new SqlConnection();

        string connString = WebConfigurationManager.ConnectionStrings["MedicalConnection"].ConnectionString;
        Conn.ConnectionString = connString;
        string str_sql = "select a.Unique_Value , h.hospital_id , h.ID , h.age , h.sex , h.address  from hos_user h join Analysis_results a on h.hospital_id = a.hos_id where status = 0 and status_accept = 0";
        string str_sql2 = "select a.Unique_Value , h.hospital_id , h.ID , h.age , h.sex , h.address , a.status_accept from hos_user h join Analysis_results a on h.hospital_id = a.hos_id where status = 0 and " +
                        " ( status_accept = 1 or status_accept = 2  or status_accept = 3  or status_accept = 4)  ";
        SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
        SqlDataAdapter cmd2 = new SqlDataAdapter(str_sql2, Conn);
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        cmd1.Fill(ds, "Emgcy");
        cmd2.Fill(ds2, "Emgcy2");
        GridView3.DataSource = ds.Tables["Emgcy"].DefaultView;
        GridView3.DataBind();
        GridView2.DataSource = ds2.Tables["Emgcy2"].DefaultView;
        GridView2.DataBind();

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //0 有病無派車
        //1 派車中
        //2 確認出車
        //3 已達現場
        //4 返回確認
        //5 結案


        

        string Car_status = e.Row.Cells[6].Text;

        switch (Car_status)
        {
            case "1":
                e.Row.Cells[6].Text = "準備派車中";
                break;
            case "2":
                e.Row.Cells[6].Text = "確認出車";
                break;
            case "3":
                e.Row.Cells[6].Text = "已達現場";
                break;
            case "4":
                e.Row.Cells[6].Text = "返回確認";
                break;
            default:
                break;
        }

    }



    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Send_a_car")
        {
            //改變UpdatePanel外的控制項
            string str_sample, str_hospital_ID, str_ID, str_sex;
            int  i  = Int32.Parse(e.CommandArgument.ToString());
            str_sample = GridView3.Rows[i].Cells[0].Text.Trim();
            str_hospital_ID = GridView3.Rows[i].Cells[1].Text.Trim();
            //str_ID = GridView3.Rows[i].Cells[2].Text.Trim();
            //str_sex = GridView3.Rows[i].Cells[4].Text.Trim();
           

            SqlConnection Conn = new SqlConnection();

            string connString = WebConfigurationManager.ConnectionStrings["MedicalConnection"].ConnectionString;
            Conn.ConnectionString = connString;
            string str_sql = "update Analysis_results set status_accept = '1' where Unique_Value = '"+ str_sample + "' and hos_id = '"+ str_hospital_ID + "' ";
            SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
            DataSet ds = new DataSet();
            cmd1.Fill(ds, "Emgcy");
        }

        if (e.CommandName == "Cancel_C")
        {
            //改變UpdatePanel外的控制項
            string str_sample, str_hospital_ID;
            int i = Int32.Parse(e.CommandArgument.ToString());
            str_sample = GridView3.Rows[i].Cells[0].Text.Trim();
            str_hospital_ID = GridView3.Rows[i].Cells[1].Text.Trim();
            


            SqlConnection Conn = new SqlConnection();

            string connString = WebConfigurationManager.ConnectionStrings["MedicalConnection"].ConnectionString;
            Conn.ConnectionString = connString;
            string str_sql = "update Analysis_results set status_accept = '5' where Unique_Value = '" + str_sample + "' and hos_id = '" + str_hospital_ID + "' ";
            SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
            DataSet ds = new DataSet();
            cmd1.Fill(ds, "Emgcy");
        }


        if (e.CommandName == "Search_ECG")
        {
            int i = Int32.Parse(e.CommandArgument.ToString());
            Session["E_hospital_id"] = GridView3.Rows[i].Cells[1].Text.Trim();
            Session["E_ID"] = GridView3.Rows[i].Cells[2].Text.Trim();
            Session["E_sample"] = GridView3.Rows[i].Cells[0].Text.Trim();
            Response.Redirect("MedicalHistory.aspx");
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindGridToSource();
    }
}