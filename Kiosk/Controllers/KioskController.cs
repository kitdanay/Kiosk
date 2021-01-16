using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kiosk.Models;
using WebApplicationNet.Models;

namespace Kiosk.Controllers
{
    public class KioskController : Controller
    {
        #region 
        public string xUser { get; set; }
        public string xPass { get; set; }
        public string xTitle { get; set; }
        public string xType { get; set; }
        public string xxType_local { get; set; }
        public string xxNumber_run { get; set; }
        public string xxlocation_id { get; set; }
        public string xxTitle { get; set; }
        public string xUrl { get; set; }
        public string location_code { get; set; }
        public string location_main { get; set; }
        public string xQueue { get; set; }
        public string xNation { get; set; }
        #endregion
        // GET: Kiosk
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult frmMain()
        {
            #region url

            StringBuilder str_check = new StringBuilder();
            DataTable dt_table = new DataTable();
            clsData check_data = new clsData();
            GetData url = new GetData();
            xUrl = url.Get_BindUrl();
            string text = xUrl;
            int text_length = text.Length;
            dt_table = url.Get_location(text_length);

            #endregion

            #region Loop
            List<manangment> ma = new List<manangment>();
            if (dt_table != null && dt_table.Rows.Count > 0)
            {
                int row = 0;
                foreach (DataRow dr in dt_table.Rows)
                {
                    row++;
                    xTitle = dr["title"].ToString();
                    xType = dr["location_type"].ToString();


                    if (row == 1)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 2)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 3)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 4)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 5)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 6)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 7)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 8)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 9)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 10)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                }
                #endregion
            }

            return View(ma.ToList());
        }

        [HttpPost]
        public ActionResult frmMain(manangment md)
        {
            //string xName = md.title;
            //string xLocation = md.location_id;
            //string xType_location = md.location_type;
            //string xRunning = md.number_running;
            //string xSubmt_location = md.submt_location;
            xNation = md.check_box;
            GetData url_ = new GetData();
            xUrl = url_.Get_BindUrl();
            string text_ = xUrl;
            int text_length_ = text_.Length;
            location_main = url_.Get_datalocation_(text_length_);
            if (location_main != null && location_main != "")
            {
                if (md.location_id == null && md.location_type != "")
                {
                    StringBuilder str__ = new StringBuilder();
                    DataTable dt__ = new DataTable();
                    DataTable dt_local = new DataTable();
                    clsData data__ = new clsData();
                    dt_local = url_.Get_locationPrint(location_main, md.location_type);
                    if (dt_local != null && dt_local.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt_local.Rows)
                        {
                            xxTitle = dr["title"].ToString();
                            xxType_local = dr["location_type"].ToString();
                            xxNumber_run = dr["number_running"].ToString();
                            xxlocation_id = dr["location_id"].ToString();
                        }
                        #region select_count
                        StringBuilder str_check = new StringBuilder();
                        DataTable dt_table = new DataTable();
                        clsData check_data = new clsData();
                        int aa = 0;
                        int bb = 0;
                        string queryCount = "SELECT count(queueno) + " + xxNumber_run + " from patientdetail  where type_group = '" + xxType_local + "' and location_print = '" + xxlocation_id + "' and cwhen_print::date = now()::date";
                        string xQueue_ = check_data.Return(queryCount);
                        if (xQueue_.Length == 1)
                        {
                            string queue_number = bb + xQueue_;
                            xQueue = aa + queue_number;
                        }
                        else if (xQueue_.Length == 2)
                        {
                            xQueue = aa + xQueue_;
                        }
                        else if (xQueue_.Length == 3)
                        {
                            xQueue = xQueue_;
                        }
                        string xQueueno_ = xxType_local + xQueue;
                        #endregion

                        #region insert_Queue
                        StringBuilder str_insert = new StringBuilder();
                        DataTable dt_insert = new DataTable();
                        clsData check_insert = new clsData();
                        if (xNation == "" || xNation == null)
                        {
                            xNation = "Thai";
                        }
                        string dtm = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"));
                        str_insert.AppendLine("INSERT INTO public.patientdetail(queueno, cwhen_print,location_print,nation,type_group) ");
                        str_insert.AppendLine(" VALUES ('" + xQueueno_ + "', '" + dtm + "','" + xxlocation_id + "','" + xNation + "','" + xxType_local + "' ) RETURNING  uid ");
                        dt_insert = check_insert.sqlDataTable(str_insert.ToString());
                        if (dt_insert != null)
                        {
                            TempData["mssg"] = "Success";
                            ViewBag.mssg = TempData["mssg"] as string;
                        }
                        #endregion
                    }
                    else
                    {
                        TempData["mssg"] = "Error";
                        ViewBag.mssg = TempData["mssg"] as string;
                    }




                }
                else if (md.submt_location != null && md.submt_location != "")
                {
                    #region ปริ้นบัตรคิว
                    if (location_main != null && location_main != "")
                    {
                        GetData url_Add = new GetData();
                        DataTable dt = url_Add.Get_locationPrint(location_main, md.location_type);
                        if (dt != null && dt.Rows.Count > 0)
                        { }
                        else
                        {

                            #region insert_Queue
                            StringBuilder str_insert = new StringBuilder();
                            DataTable dt_insert = new DataTable();
                            clsData check_insert = new clsData();
                            string dtm = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"));
                            str_insert.AppendLine("INSERT INTO public.tb_kiosk(title, cwhen,location_type,number_running,location_id,location_main) ");
                            str_insert.AppendLine(" VALUES ('" + md.title + "', '" + dtm + "','" + md.location_type + "', ");
                            str_insert.AppendLine(" '" + md.number_running + "' , '" + md.location_id + "' ,'" + location_main + "' ) RETURNING  uid ");
                            dt_insert = check_insert.sqlDataTable(str_insert.ToString());
                            #endregion
                            if (dt_insert != null)
                            {
                                TempData["mssg"] = "Success";
                                ViewBag.mssg = TempData["mssg"] as string;
                            }
                        }
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript( GetType(), "alertMessage", "successalert1();", true);
                        //ViewBag.showAlert = true;
                        //ViewBag.alertMessage = "ไม่สำเร็จ";
                        //Response.Write("<script>alert('login successful');</script>");
                        TempData["mssg"] = "Error";
                        ViewBag.mssg = TempData["mssg"] as string;
                    }
                    #endregion


                }
                else { }
            }
            else
            {
                TempData["mssg"] = "Error_Location";
                ViewBag.mssg = TempData["mssg"] as string;
            }
            #region loop_listKiosk
            StringBuilder str_list = new StringBuilder();
            DataTable dt_list = new DataTable();
            clsData cls_list = new clsData();
            GetData url = new GetData();
            xUrl = url.Get_BindUrl();
            string text = xUrl;
            int text_length = text.Length;
            dt_list = url.Get_location(text_length);
            List<manangment> ma = new List<manangment>();
            if (dt_list != null && dt_list.Rows.Count > 0)
            {
                int row = 0;
                foreach (DataRow dr in dt_list.Rows)
                {
                    row++;
                    xTitle = dr["title"].ToString();
                    xType = dr["location_type"].ToString();


                    if (row == 1)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 2)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 3)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 4)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 5)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 6)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 7)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 8)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 9)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                    if (row == 10)
                    {
                        ma.Add(new manangment { title = xTitle, location_type = xType });
                    }
                }

            }

            #endregion

            return View(ma.ToList());


            //return Json(moldtScanQueue, JsonRequestBehavior.AllowGet);

            //return View();
        }

    }
}