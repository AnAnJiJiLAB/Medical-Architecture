using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MathWorks.MATLAB.NET.Arrays;
using CalcPow;

public partial class testjavascript : System.Web.UI.Page

{
    public string[] aa  ;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "+();", true);
        //aa = new string[] { "1","3","5","7" };
        //json 早上用用看
        // aa = Request.QueryString["1,2,3,4,5,6,7"];
        //Response.Write("A"+aa);

        POW p = new POW();
        MWArray mw = p.CalcPow((MWArray)2, (MWArray)10);//計算2的10次方
        int res = int.Parse(mw.ToString());

        Response.Write(res);
       

    }
}