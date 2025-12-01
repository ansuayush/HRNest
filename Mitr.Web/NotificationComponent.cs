using Microsoft.AspNet.SignalR;
using Mitr.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mitr
{
    public class Onlineuser
    {
        public long LoginID { get; set; }
        public string UserName { get; set; }

    }
    public class NotificationComponent
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        public IEnumerable<PushNotification> InvokeNotification()
        {
            long LoginID = 0;
            long.TryParse(clsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            var Push = new List<PushNotification>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                string SQL = @"SELECT NotificationID, Subject,GotoURL,MessageContent,IsRecent, Category,TableID, Status,
                               Priority, IsStatusRead, createdby, createdat, IsActive, IPAddress
                                FROM [dbo].[PushNotification] where IsActive=1 and isdeleted=0 and IsStatusRead=0 and createdby=" + LoginID+ " order by NotificationID desc ";
                using (var command = new SqlCommand(SQL, connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(sqlDep_OnChange);

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Push.Add(item: new PushNotification
                        {
                            NotificationID = (int)reader["NotificationID"],
                            Subject = reader["Subject"].ToString(),
                            MessageContent = reader["MessageContent"].ToString(),
                            GotoURL = reader["GotoURL"].ToString(),
                            Category = reader["Category"].ToString(),
                            TableID = Convert.ToInt64(reader["TableID"].ToString()),
                            Status = reader["Status"].ToString(),
                            Priority = Convert.ToInt32(reader["Priority"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            IsRecent = Convert.ToBoolean(reader["IsRecent"]),
                            IsStatusRead = Convert.ToBoolean(reader["IsStatusRead"]),
                            CreatedDate = Convert.ToDateTime(reader["createdat"]).ToString("dd-MMM-yy hh:mm:ss tt"),
                            CreatedByID = Convert.ToInt32(reader["createdby"]),
                            IPAddress = reader["IPAddress"].ToString()
                        });
                    }
                }

            }
            return Push;
        }



        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("fire");

            }
        }
    }
}