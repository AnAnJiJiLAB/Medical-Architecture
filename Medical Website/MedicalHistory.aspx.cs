using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class MedicalHistory : System.Web.UI.Page
{
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["level"].ToString() != "doc")
        {
            text_ID.Text = Session["ID"].ToString();
            text_hos_ID.Text = Session["hospital_id"].ToString();
        }
        else if(Session["E_hospital_id"]!=null && Session["E_ID"]!=null)
        {

            text_hos_ID.Text = Session["E_hospital_id"].ToString();
            text_ID.Text = Session["E_ID"].ToString();
	    Session.Remove("E_hospital_id");
            Session.Remove("E_ID");

        }
        
    }

    protected void Search_Click(object sender, EventArgs e)
    {


        // 顯示波型
        BindGridToSource();


        if (Session["level"].ToString() == "doc")
        {
            Panel4.Visible = true;
            SqlConnection Conn = new SqlConnection();
            string connString = WebConfigurationManager.ConnectionStrings["MedicalConnection"].ConnectionString;
            Conn.ConnectionString = connString;
            string str_sql = "";
            string leads = "";
            string[] Split_leads = null;
            if (text_ID.Text != "")
            {

                str_sql = " and ID = '" + text_ID.Text + "'";
            }
            else if (text_hos_ID.Text != "")
            {
                str_sql = " and hos_ID = '" + text_hos_ID.Text + "'";
            }
            else
            {
                error.Text = "請輸入ID或醫院代號";
                return;
            }
            if (ddl_date_samples.SelectedValue != "")
            {
                str_sql = " and Unique_Value = '" + ddl_date_samples.Text.Substring(0, 10) + "' " + str_sql; ///

            }
	    if(ddl_leads.SelectedValue.ToString() =="請選擇"|| ddl_leads.SelectedValue.ToString() == "")
            {
            	error.Text = "請選擇Leads";
            	return;

             }

            /*if (ddl_date.SelectedValue != "")
            {
                str_sql = " and ECG_Date = '" + ddl_date.Text.Substring(0, 10) + "' " + str_sql; ///

            }*/
            str_sql = "select anterior_pb,antero_lateral_pb,antero_septal_pb,inferior_pb,infero_lateral_pb,status from Analysis_results "
                      + " where 1=1 " + str_sql;


            SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
            DataSet ds = new DataSet();
            cmd1.Fill(ds, "ECG");
            int ECG_Rows = ds.Tables["ECG"].Rows.Count;

            if (ECG_Rows > 0)
            {
                string Sick_Health = ds.Tables["ECG"].Rows[0].ItemArray[5].ToString().Trim();

                if (Sick_Health == "0")
                {
                    Label_sick_health.Text = "Positive";
                    Label_sick_health.ForeColor = Color.Red;
                    Label_Anterior.Text = ds.Tables["ECG"].Rows[0].ItemArray[0].ToString().Trim();
                    Label_Antero_septal.Text = ds.Tables["ECG"].Rows[0].ItemArray[1].ToString().Trim();
                    Label_Antero_lateral.Text = ds.Tables["ECG"].Rows[0].ItemArray[2].ToString().Trim();
                    Label_Inferior.Text = ds.Tables["ECG"].Rows[0].ItemArray[3].ToString().Trim();
                    Label_Infero_latera.Text = ds.Tables["ECG"].Rows[0].ItemArray[4].ToString().Trim();
                }
                else if (Sick_Health == "1")
                {
                    Label_sick_health.Text = "Negative";
                    Label_sick_health.ForeColor = Color.Green;
                    Label_Anterior.Text = "";
                    Label_Antero_septal.Text = "";
                    Label_Antero_lateral.Text = "";
                    Label_Inferior.Text = "";
                    Label_Infero_latera.Text = "";
                    

                }
            }
            else {

                Label_sick_health.Text = "";
                
                Label_Anterior.Text = "";
                Label_Antero_septal.Text = "";
                Label_Antero_lateral.Text = "";
                Label_Inferior.Text = "";
                Label_Infero_latera.Text = "";
                
            }
        }
        else
        {
            Panel4.Visible = false;
            return;
        }
    }

    private void BindGridToSource()
    {
        SqlConnection Conn = new SqlConnection();
        string connString = WebConfigurationManager.ConnectionStrings["DataCenter"].ConnectionString;
        Conn.ConnectionString = connString;
        string str_sql = "";
        string leads = "";
        string[] Split_leads = null;
        if (text_ID.Text != "")
        {

            str_sql = " and ID = '" + text_ID.Text + "'";
        }
        else if (text_hos_ID.Text !="")
        {
            str_sql = " and hos_ID = '" + text_hos_ID.Text + "'";
        }
        else
        {
            error.Text = "請輸入ID或醫院代號";
            return;
        }
        //if (ddl_samples.SelectedValue != "")
        //{
        //    str_sql = " and Unique_Value = '" + ddl_samples.Text + "' " + str_sql; ///

        //}
        if (ddl_date_samples.SelectedValue == "請選擇")
        {
            error.Text = "請選擇Leads/日期";
            return;
        }
        else {

            str_sql = " and ECG_Date = '" + ddl_date_samples.Text.Substring(11, 10) + "' and Unique_Value ='" + ddl_date_samples.Text.Substring(0, 10) + "' " + str_sql; ///

        }


        if (ddl_leads.SelectedValue != "")
        {
            leads = ddl_leads.Text.Replace("、", ",");
            Split_leads = leads.Split(',').ToArray();
            // str_sql = "select" + Split_leads[1] + "," + Split_leads[2] + "," + Split_leads[3] + "from patient where 1=1 " + str_sql;
            str_sql = "select " + leads + " from patient2 where 1=1 " + str_sql;
        }

         
        if(ddl_leads.SelectedValue.ToString() =="請選擇"|| ddl_leads.SelectedValue.ToString() == "")
        {
            error.Text = "請選擇Leads";
            return;

        }

	
        //label 顯示leads 
        itov4.Text = Split_leads[0].ToString();
        iitov5.Text = Split_leads[1].ToString();
        iiitov6.Text = Split_leads[2].ToString();


   

        SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
        DataSet ds = new DataSet();
        cmd1.Fill(ds, "ECG");
        
        int ECG_Rows = ds.Tables["ECG"].Rows.Count;
        double[] X_I = new double[ds.Tables["ECG"].Rows.Count];
        double[] Y_I = new double[ds.Tables["ECG"].Rows.Count];
        double[] Y_II = new double[ds.Tables["ECG"].Rows.Count];
        double[] Y_III = new double[ds.Tables["ECG"].Rows.Count];
        if (ds.Tables["ECG"].Rows.Count != 0)
        {
            for (int i = 0; i < ECG_Rows; i++)
            {

                    X_I[i] = i+1;
                    Y_I[i] = Convert.ToDouble(ds.Tables["ECG"].Rows[i].ItemArray[0].ToString());//I、avr、v1、v4
                    Y_II[i] = Convert.ToDouble(ds.Tables["ECG"].Rows[i].ItemArray[1].ToString());//II avl v2 v5
                    Y_III[i] = Convert.ToDouble(ds.Tables["ECG"].Rows[i].ItemArray[2].ToString());//III avf v3 v6
                
            }
                      
        }
        double[] xValues = X_I;
        double[] yValues1 = Y_I;
        double[] yValues2 = Y_II;
        double[] yValues3 = Y_III;
        string[] AA = ddl_size.SelectedValue.Split('*').ToArray();
        Chart1.Width = Convert.ToInt32(AA[0].ToString());
        Chart1.Height = Convert.ToInt32(AA[1].ToString());

        Chart2.Width = Convert.ToInt32(AA[0].ToString());
        Chart2.Height = Convert.ToInt32(AA[1].ToString());

        Chart3.Width = Convert.ToInt32(AA[0].ToString());
        Chart3.Height = Convert.ToInt32(AA[1].ToString());

        //double[] yValues = Y_I;
        if (xValues == null)
        {
            return;

        }
        else
        {
            Chart1.Series["Series1"].Points.DataBindXY(xValues, yValues1);
            Chart2.Series["Series1"].Points.DataBindXY(xValues, yValues2);
            Chart3.Series["Series1"].Points.DataBindXY(xValues, yValues3);
        }

    }



   




    protected void ddl_leads_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_date_samples.Items.Clear();
        ddl_samples.Items.Clear();
        SqlConnection Conn = new SqlConnection();
        string connString = WebConfigurationManager.ConnectionStrings["DataCenter"].ConnectionString;
        Conn.ConnectionString = connString;
        string str_sql = "";
       
        str_sql = "select distinct ECG_Date,Unique_Value from patient2 where 1=1 and ID = '" + text_ID.Text + "' or hos_id = '" + text_hos_ID.Text + "'";

        
        SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
        DataSet ds = new DataSet();
        cmd1.Fill(ds, "ECG");

        int ECG_Rows = ds.Tables["ECG"].Rows.Count;
        

        for (int i = 0; i < ECG_Rows; i++)
        {
            string[] ECG_Date = new string[ECG_Rows];
            string[] samples = new string[ECG_Rows];
            ECG_Date[i] = ds.Tables["ECG"].Rows[i].ItemArray[0].ToString();
            samples[i] = ds.Tables["ECG"].Rows[i].ItemArray[1].ToString();
            ddl_date_samples.Items.Add(samples[i] + "|"+ECG_Date[i]);
            //ddl_samples.Items.Add(samples[i]);
        }
        // DropDownList2.DataBind();         //不用此行亦可，原因未知

    }




    protected void Analysis_Click(object sender, EventArgs e)
    {

    }
}
