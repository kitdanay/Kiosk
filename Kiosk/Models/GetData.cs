using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Kiosk.Models
{
    public class GetData
    {
        public string xUrl { get; set; }
        public string location_code { get; set; }
        public string Get_BindUrl()
        {
            string url_code = HttpContext.Current.Request.Url.AbsoluteUri;
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string host = HttpContext.Current.Request.Url.Host;

            //string uri = string.Format(SignalRAPI + "QueueList?locationCode=" );
            //WebClient ws = new WebClientWithTimeout();
            //ws.Encoding = ASCIIEncoding.UTF8;
            ////var json = ws.DownloadString(url_code);
            //ws.Dispose();

            //return path;
            //return host;
            return url_code;
        }

        public DataTable Get_location(int text)
        {
            StringBuilder str_check = new StringBuilder();
            DataTable dt_table = new DataTable();
            clsData check_data = new clsData();
            xUrl = Get_BindUrl();
            if (text >= 35)
            {
                char[] delimiterChars = { '=' }; //https://q.mobile.com/TH/Hn_Vn_yymmdd_HHMM https://q.mobile.com/TH/775617_0001_200810_1729
                string txtHn = xUrl.Trim();

                string[] HN = txtHn.Split(delimiterChars);
                location_code = HN[1].Trim();
                if (location_code != "" && location_code != null)
                {
                    str_check.AppendLine("select * from patientcategory where location_main = '" + location_code + "'  ORDER BY location_type ASC  ");
                    dt_table = check_data.sqlDataTable(str_check.ToString());
                }
                //if (location_code == "Index")
                //{
                //    str_check.AppendLine("select * from tb_kiosk   ");
                //    dt_table = check_data.sqlDataTable(str_check.ToString());
                //}
                //else if (location_code == "Index?location")
                //{
                //    string xxlocation_code = HN[5].Trim();
                //    str_check.AppendLine("select * from tb_kiosk where location_main = '" + xxlocation_code + "'   ");
                //    dt_table = check_data.sqlDataTable(str_check.ToString());
                //}
                //else
                //{
                //    str_check.AppendLine("select * from tb_kiosk where location_main = '" + location_code + "'   ");
                //    dt_table = check_data.sqlDataTable(str_check.ToString());
                //}

            }
            else
            {
                str_check.AppendLine("select * from patientcategory  ORDER BY location_type ASC   ");
                dt_table = check_data.sqlDataTable(str_check.ToString());
            }
            return dt_table;
        }

        public string Get_datalocation_(int text)
        {
            string locate = "";
            xUrl = Get_BindUrl();
            if (text >= 35)
            {
                char[] delimiterChars = { '=' }; //https://q.mobile.com/TH/Hn_Vn_yymmdd_HHMM https://q.mobile.com/TH/775617_0001_200810_1729
                string txtHn = xUrl.Trim();

                string[] HN = txtHn.Split(delimiterChars);
                location_code = HN[1].Trim();
                locate = location_code;
            }
            return locate;
        }
        public DataTable Get_locationPrint(string location_main, string type_local)
        {
            StringBuilder str_check = new StringBuilder();
            DataTable dt_table = new DataTable();
            clsData check_data = new clsData();

            if (location_main != null && location_main != "")
            {
                str_check.AppendLine("select * from patientcategory where location_main = '" + location_main + "'  and location_type = '" + type_local + "'  ");
                dt_table = check_data.sqlDataTable(str_check.ToString());
            }
            else
            {

            }


            return dt_table;
        }
    }
}