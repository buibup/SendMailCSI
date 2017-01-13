using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace SendEmailCSI.DA.InterSystems
{
    public class InterSystemsDA
    {

        public static DataTable GetDTSendEmailCSIData(string dateFrom, string dateTo, string connectionString)
        {
            try
            {
                DataTable dt = new DataTable();
                string CommandText = "{CALL \"Custom_THSV_Report_ZEN_StoredProc\".\"SVNHSendEmailCSI2_GetData\".('" + dateFrom + "','" + dateTo + "')}";
                dt = DataTableBindDataCommand(CommandText, connectionString); 
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable DataTableBindDataCommand(string cmdString, string connectionString)
        {
            DataTable dt = new DataTable();

            using(CacheConnection con = new CacheConnection(connectionString))
            {
                con.Open();
                    using (CacheDataAdapter da = new CacheDataAdapter(cmdString,con))
                    {
                        da.Fill(dt);
                    }
            }

            return dt;
        }

        public static DataSet DataSetBindDataCommand(string cmdString, string connectionString)
        {
            DataSet ds = new DataSet();

            using (CacheConnection con = new CacheConnection(connectionString))
            {
                using(CacheDataAdapter adt = new CacheDataAdapter(cmdString,con))
                {
                    con.Open();
                    adt.Fill(ds);
                }
            }

            return ds;
        }
    }
}
