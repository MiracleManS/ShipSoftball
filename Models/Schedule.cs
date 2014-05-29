using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace ShipSoftball.Models
{
    public class Schedule
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Field { get; set; }
        public string ScheduleDate { get; set; }
        public DateTime ScheduleTime{ get; set; }

        public static List<Schedule> GetFullSchedule()
        {
            var connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "Files/schedule.xls" +";Extended Properties=Excel 8.0;";
            var conn = new OleDbConnection(connStr);
            conn.Open();
            var cmd = new OleDbCommand("SELECT * FROM [Schedule$]", conn);
            var adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            var ds = new DataSet();
            adapter.Fill(ds, "XLData");
            List<Schedule> ret = new List<Schedule>();
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                var sched = new Schedule();
                var row = ds.Tables[0].Rows[i];
                sched.HomeTeam = row["Home Team"].ToString();
                sched.AwayTeam = row["Away Team"].ToString();
                sched.Field = row["Field"].ToString();
                sched.ScheduleDate = row["Date"].ToString();
                DateTime s;
                DateTime.TryParse(row["Time"].ToString(), out s);
                sched.ScheduleTime = s;
                ret.Add(sched);
            }
            conn.Close();
            return ret;
        }
    }


}