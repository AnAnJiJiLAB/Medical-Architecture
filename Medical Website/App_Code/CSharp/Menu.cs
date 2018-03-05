using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Menu 的摘要描述
/// </summary>
public class Menucs
{
    //public Menucs()
    //{
    //}

  public static void LoadMenu(string userName, System.Web.UI.WebControls.Panel myMenuPanel)
  {
    string conString = WebConfigurationManager.ConnectionStrings["WMSConnection_3"].ConnectionString;
    string mySqlString = "";
    string myAuthorizationLevel = "";
    string myMenuID = "";
    string myMenuName = "";
    string myMenuAction = "";

    if (string.IsNullOrEmpty(userName))
      return;

    using (var myConnection = new SqlConnection(conString))
    {
      myConnection.Open();

      #region 取得 user 的 AuthorizationLevel
      mySqlString = "Select AuthorizationLevel FROM WebUser WHERE UserName = @UserName ";
      using (SqlCommand MyCommand = new SqlCommand(mySqlString, myConnection))
      {
        //MyCommand.CommandTimeout = 30;
        //MyCommand.Parameters.Clear();
        //MyCommand.Parameters.Add("@UserName", SqlDbType.Char).Value = userName;
        //using (
        //            resultReader = MyCommand.ExecuteReader())
        //{
        //  if (resultReader.Read())
        //  {
        //    myAuthorizationLevel = Convert.ToString(resultReader["AuthorizationLevel"]);
        //  }
        //}
      }
      #endregion

      #region 取得最上層的選單

      if (myAuthorizationLevel != "")
      {
        #region 輸出 Contianer
        
        myMenuPanel.Controls.Add(new LiteralControl("<div id='box'>"));
        myMenuPanel.Controls.Add(new LiteralControl("<div id='menubox'>"));
        myMenuPanel.Controls.Add(new LiteralControl("<ul class='sf-menu'>"));
        
        #endregion

        mySqlString = "Select [MenuID], [MenuName], [Action] FROM MenuItem WHERE ParentMenuID = 0 AND AuthorizationLevel <= @AuthorizationLevel  Order By SeqNo";
        SqlCommand MyCommand = new SqlCommand(mySqlString, myConnection);
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.Add("@AuthorizationLevel", SqlDbType.Char).Value = myAuthorizationLevel;

        using (SqlDataReader resultReader = MyCommand.ExecuteReader())
        {
          DataTable dtTopMenu = new DataTable();
          dtTopMenu.Load(resultReader); //取得reader

          if (dtTopMenu.Rows.Count > 0)
          {
            for (int i = 0; i < dtTopMenu.Rows.Count; i++)
            {
              myMenuID = dtTopMenu.Rows[i]["MenuID"].ToString();
              myMenuName = dtTopMenu.Rows[i]["MenuName"].ToString();
              myMenuAction = dtTopMenu.Rows[i]["Action"].ToString();
              LoadChildMenu(myMenuID, myMenuName, myMenuAction, myAuthorizationLevel, myMenuPanel);
            }
          }
        }

        myMenuPanel.Controls.Add(new LiteralControl("</ul>"));
        myMenuPanel.Controls.Add(new LiteralControl("</div>"));
        myMenuPanel.Controls.Add(new LiteralControl("</div>"));
      }

      #endregion
    }
  }

  public static void LoadChildMenu(string menuid, string menuName, string menuAction, string myAuthorizationLevel, System.Web.UI.WebControls.Panel myMenuPanel)
  {
    string conString = WebConfigurationManager.ConnectionStrings["WMSConnection_3"].ConnectionString;
    string myHtml = "";
    string myChildMenuID = "";
    string myChildMenuName = "";
    string myChildMenuAction = "";

    try
    {
      if (string.IsNullOrEmpty(menuAction))
        menuAction = "#";

      #region 輸出子選單

      using (var myConnection = new SqlConnection(conString))
      {
        myConnection.Open();

        string mySqlstring = "Select [MenuID], [MenuName], [Action] FROM MenuItem WHERE ParentMenuID = @MenuID  AND AuthorizationLevel <= @AuthorizationLevel Order By SeqNo";
        using (SqlCommand MyCommand = new SqlCommand(mySqlstring, myConnection))
        {
          MyCommand.CommandTimeout = 30;
          MyCommand.Parameters.Clear();
          MyCommand.Parameters.Add("@MenuID", SqlDbType.Char).Value = menuid;
          MyCommand.Parameters.Add("@AuthorizationLevel", SqlDbType.Char).Value = myAuthorizationLevel;
          using (SqlDataReader resultReader = MyCommand.ExecuteReader())
          {
            DataTable dtChildMenu = new DataTable();
            dtChildMenu.Load(resultReader); //取得reader

            if (dtChildMenu.Rows.Count > 0)
            {
              myHtml = string.Format("<li><a href='{0}' class='sf-with-ul'>{1}<span class='sf-sub-indicator'>»</span></a>", menuAction, menuName);
              myMenuPanel.Controls.Add(new LiteralControl(myHtml));
              myMenuPanel.Controls.Add(new LiteralControl("<ul style='float: none; width: 12em; display: none; visibility: hidden;'>"));
                     
              for (int i = 0; i < dtChildMenu.Rows.Count; i++)
              {
                myChildMenuID = dtChildMenu.Rows[i]["MenuID"].ToString();
                myChildMenuName = dtChildMenu.Rows[i]["MenuName"].ToString();
                myChildMenuAction = dtChildMenu.Rows[i]["Action"].ToString();
                LoadChildMenu(myChildMenuID, myChildMenuName, myChildMenuAction, myAuthorizationLevel, myMenuPanel);
              }
              myMenuPanel.Controls.Add(new LiteralControl("</ul></li>"));
            }
            else
            {
              myHtml = String.Format("<li><a href='{0}' class='sf-with-ul'>{1}</a></li>", menuAction, menuName);
              myMenuPanel.Controls.Add(new LiteralControl(myHtml));
            }
          }
        }
      }

      #endregion
    }
    catch
    {
    }
  }

}