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

        public static List<Schedule> GetScheduleByDate(DateTime dt)
        {
            int start = (int)dt.DayOfWeek;
            int target = (int)DayOfWeek.Sunday;
            if (target <= start)
                target += 7;
            //return date.AddDays(target - start);
            //HttpContext.Current.Response.Write(dt.ToShortDateString());
            var connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "Files/schedule.xls" + ";Extended Properties=Excel 8.0;";
            var conn = new OleDbConnection(connStr);
            conn.Open();
            var cmd = new OleDbCommand("SELECT * FROM [Schedule$]", conn);
            var adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            var ds = new DataSet();
            adapter.Fill(ds, "XLData");
            var query = from sch in ds.Tables[0].AsEnumerable()
                        where sch.Field<string>("Date") == dt.AddDays(target - start).ToShortDateString()
                        select sch;
            
            List<Schedule> ret = new List<Schedule>();
            foreach (var q in query)
            {
                var sched = new Schedule();
                sched.HomeTeam = q["Home Team"].ToString();
                sched.AwayTeam = q["Away Team"].ToString();
                sched.Field = q["Field"].ToString();
                sched.ScheduleDate = q["Date"].ToString();
                DateTime s;
                DateTime.TryParse(q["Time"].ToString(), out s);
                sched.ScheduleTime = s;
                ret.Add(sched);
            }
            conn.Close();
            return ret;

            
        }
    }


}