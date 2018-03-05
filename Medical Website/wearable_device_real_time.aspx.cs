using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using System.Drawing;
using System.Diagnostics;
using com.sun.tools.corba.se.logutil;
using System.Net;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using NPOI.HSSF.Record.Formula.Functions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class wearable_device_real_time : System.Web.UI.Page
{
    JObject obj;
    int seq = 0;
    double[] X_I;// = X_I[].Length ;//new double[60000];
    double[] Y_I;//= new double[60000];
    double[] Y_II;//= new double[60000];
    double[] Y_III;// = new double[60000];
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime localDate = DateTime.Now;

            ddl_date.Items.Add(localDate.ToString());//2017-08-05
            ID_2.Text = Session["ID"].ToString().Trim();
            Hospital_ID_2.Text = Session["hospital_id"].ToString().Trim();
            //MqttProcessing();// call to the MQTT client setup steps.
            // HttpContext.Current.Session["message"] = "";

            //< label > Female2 </ label >
        }
    }

    protected void MqttProcessing()
    {
        string ip = "your ip";
        
        // create client instance 
        // MqttClient client = new MqttClient(MQTT_BROKER_HOST_NAME);
        MqttClient client = new MqttClient(ip);

        // register to message received 
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        //string clientId = Guid.NewGuid().ToString();
        string clientId = "James";
        client.Connect(clientId);

        // subscribe to the topic "/home/temperature" with QoS 2 
        //topic
        client.Subscribe(new string[] { "H1" }, new byte[]{ MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        
    }

    protected void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {

        string  result = System.Text.Encoding.UTF8.GetString(e.Message).ToString();
        try
        {
            obj = JObject.Parse(result);
            // Debug.WriteLine(obj["data"]);
        }
        catch (Exception ex)
        {
            error.Text = ex.Message.ToString();
            return;
        }
        string readjson_data = obj["data"].ToString();
        
        string[] final_readjson_data = readjson_data.Replace("\r","").Split((','));

        SqlConnection Conn = new SqlConnection();

        string start = "0000000000"; 
        // seq = seq + 1;//這邊要改
        string Unique_Value = "";
        string connString = WebConfigurationManager.ConnectionStrings["DataCenter"].ConnectionString;
        Conn.ConnectionString = connString;
        string str_sql = "select  max(Unique_Value) as 'Unique_Value' from patient2";
        SqlDataAdapter cmd1 = new SqlDataAdapter(str_sql, Conn);
        DataSet ds = new DataSet();
        cmd1.Fill(ds, "ECG");
        int ECG_Rows = ds.Tables["ECG"].Rows.Count;
        string a = ds.Tables["ECG"].Rows[0].ItemArray[0].ToString();
        if (a == "")
        {
            Unique_Value = "0000000001";
        }
        else if (ECG_Rows <= 60000)
        {
            Unique_Value = ds.Tables["ECG"].Rows[0].ItemArray[0].ToString().Trim();
            //int numVal = Int32.Parse(temp);
            //numVal = numVal + 1;

            //Unique_Value = numVal.ToString().PadLeft(10, '0');


        }
        else if (ECG_Rows > 60000)
        {
            string temp = ds.Tables["ECG"].Rows[0].ItemArray[0].ToString().Trim();
            int numVal = Int32.Parse(temp);
            numVal = numVal + 1;

            Unique_Value = numVal.ToString().PadLeft(10, '0');

        }


        
        using (SqlConnection TranConn = new SqlConnection(connString))
        {
            
            TranConn.Open();

            //建立交易Transaction
            SqlTransaction Tran = TranConn.BeginTransaction();
            string Tran_SQL = "";
            Tran_SQL = "insert into patient2 (Unique_Value,hos_id,ID,i,ii,iii,avr,avl,avf,v1,v2,v3,v4,v5,v6,ECG_Date) " +
                                    "values (@Unique_Value,@hos_id,@ID,@i,@ii,@iii,@avr,@avl,@avf,@v1,@v2,@v3,@v4,@v5,@v6,@ECG_Date)";
            using (SqlCommand TranCmmd = new SqlCommand(Tran_SQL, TranConn, Tran))
            {
                //db_seq();
                TranCmmd.CommandTimeout = 90;
                TranCmmd.Parameters.Add("@Unique_Value", SqlDbType.VarChar, 50).Value = Unique_Value.ToString().Trim();
                TranCmmd.Parameters.Add("@hos_id", SqlDbType.VarChar, 50).Value = Hospital_ID_2.Text.ToString().Trim(); 
                TranCmmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = ID_2.Text.ToString().Trim();//Arduino 記得加
                TranCmmd.Parameters.Add("@i", SqlDbType.VarChar, 50).Value = final_readjson_data[1];
                TranCmmd.Parameters.Add("@ii", SqlDbType.VarChar, 50).Value = final_readjson_data[2];
                TranCmmd.Parameters.Add("@iii", SqlDbType.VarChar, 50).Value = final_readjson_data[3];
                TranCmmd.Parameters.Add("@avr", SqlDbType.VarChar, 50).Value = final_readjson_data[4];
                TranCmmd.Parameters.Add("@avl", SqlDbType.VarChar, 50).Value = final_readjson_data[5];
                TranCmmd.Parameters.Add("@avf", SqlDbType.VarChar, 50).Value = final_readjson_data[6];
                TranCmmd.Parameters.Add("@v1", SqlDbType.VarChar, 50).Value = final_readjson_data[7];
                TranCmmd.Parameters.Add("@v2", SqlDbType.VarChar, 50).Value = final_readjson_data[8];
                TranCmmd.Parameters.Add("@v3", SqlDbType.VarChar, 50).Value = final_readjson_data[9];
                TranCmmd.Parameters.Add("@v4", SqlDbType.VarChar, 50).Value = final_readjson_data[10];
                TranCmmd.Parameters.Add("@v5", SqlDbType.VarChar, 50).Value = final_readjson_data[11];
                TranCmmd.Parameters.Add("@v6", SqlDbType.VarChar, 50).Value = final_readjson_data[12];
                TranCmmd.Parameters.Add("@ECG_Date", SqlDbType.VarChar, 50).Value = ddl_date.Text.Substring(0, 10);
               // TranCmmd.Parameters.Add("@seq2", SqlDbType.VarChar, 10).Value = seq;
               

                int rows = TranCmmd.ExecuteNonQuery();
            }

            Tran.Commit();

        }

    }   


    protected void Button1_Click(object sender, EventArgs e)
    {
        
        MqttProcessing();
        Connect_Label.Text = " Connected";
        //把當天時間加進去
    }

   


    protected void Button2_Click(object sender, EventArgs e)
    {
        Connect_Label.Text = " Disconnect";
        return;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Prompt", "id");

        //Label1.Text = Convert.ToString(Session["message"]);
        //form1.InnerHtml = "";
        //form1.InnerHtml += "<button onclick='send(" + result + ")'>click</button>";
    }

    private void db_seq() {
        seq = seq + 1; 
    }



    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindGridToSource();
        Chart1_Load();
    }

    private void BindGridToSource()
    {
        SqlConnection Conn = new SqlConnection();
        string connString = WebConfigurationManager.ConnectionStrings["DataCenter"].ConnectionString;
        Conn.ConnectionString = connString;
        string str_sql = "";
        string leads = "";
        string[] Split_leads = null;
        
        if (ddl_date.SelectedValue != "")
        {
            str_sql = " and ECG_Date = '" + ddl_date.Text.Substring(0,10) + "' " ; ///2017-08-05
            //str_sql = " and ECG_Date = '2017 - 08 - 05' ";
        }

        if (ddl_leads.SelectedValue != "")
        {
            leads = ddl_leads.Text.Replace("、", ",");
            Split_leads = leads.Split(',').ToArray();
            // str_sql = "select" + Split_leads[1] + "," + Split_leads[2] + "," + Split_leads[3] + "from patient where 1=1 " + str_sql;
            str_sql = "select " + leads + " from patient2 where 1=1 and id = '" + ID_2.Text + "' and hos_id = '" + Hospital_ID_2.Text + "' " + str_sql +" order by seq2 ASC";
        }

        if (ddl_leads.SelectedValue.ToString() == "請選擇" || ddl_leads.SelectedValue.ToString() == "")
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
        cmd1.Fill(ds, "ECG3");

        int ECG_Rows = ds.Tables["ECG3"].Rows.Count;
        X_I = new double[ECG_Rows];
        Y_I = new double[ECG_Rows];
        Y_II = new double[ECG_Rows];
        Y_III = new double[ECG_Rows];
        if (ds.Tables["ECG3"].Rows.Count != 0)
        {
            for (int i = 0; i < ECG_Rows; i++)
            {

                X_I[i] = i+1;
                Y_I[i] = Convert.ToDouble(ds.Tables["ECG3"].Rows[i].ItemArray[0].ToString());//I、avr、v1、v4
                Y_II[i] = Convert.ToDouble(ds.Tables["ECG3"].Rows[i].ItemArray[1].ToString());//II avl v2 v5
                Y_III[i] = Convert.ToDouble(ds.Tables["ECG3"].Rows[i].ItemArray[2].ToString());//III avf v3 v6

            }

        }
        else
        {
            return;
        }
    }

    private void Chart1_Load()
    {

        double[] xValues = X_I;
        double[] yValues1 = Y_I;
        double[] yValues2 = Y_II;
        double[] yValues3 = Y_III;
        //string[] AA = ddl_size.SelectedValue.Split('*').ToArray();
        //Chart1.Width = Convert.ToInt32(AA[0].ToString());
        //Chart1.Height = Convert.ToInt32(AA[1].ToString());

        //Chart2.Width = Convert.ToInt32(AA[0].ToString());
        //Chart2.Height = Convert.ToInt32(AA[1].ToString());

        //Chart3.Width = Convert.ToInt32(AA[0].ToString());
        //Chart3.Height = Convert.ToInt32(AA[1].ToString());

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


    
}