using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// ActionLog 的摘要描述
/// </summary>
public class ActionLog
{
	public ActionLog()
	{
	}

  public static void LogNewAction(string userName, string actionName)
  {
    LogNewAction(userName, actionName, "");
  }

  public static void LogNewAction(string userName, string actionName, string notes)
  {
    string conString = WebConfigurationManager.ConnectionStrings["WMSConnection_3"].ConnectionString;
    string strSQL = "";

    using (var conn = new SqlConnection(conString))
    {
      conn.Open();
      var tran = conn.BeginTransaction();

      // 寫入 Log
      strSQL = " INSERT INTO WebUserActionLog(UserName, ActionName, Notes) " +
                    " VALUES(@UserName, @ActionName, @Notes) ";

      using (var cmd = new SqlCommand(strSQL, conn, tran))
      {
        cmd.CommandTimeout = 90;
        cmd.Parameters.Add("@UserName", SqlDbType.Char, 30).Value = userName;
        cmd.Parameters.Add("@actionName", SqlDbType.Char, 60).Value = actionName;
        cmd.Parameters.Add("@Notes", SqlDbType.Char, 60).Value = notes;
        if (cmd.ExecuteNonQuery() > 0)
        {
          tran.Commit();
        }
        else
        {
          tran.Rollback();
        }
      }
    }
  }

}