using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        DataClasses2DataContext dc = new DataClasses2DataContext();

        public ActionResult Index()
        {           
            return View(dc.Tables.DistinctBy(i => i.catagory).ToList());     
        }
        
        public ActionResult Contact()
        {
            String category = Request.Form["cat"];
            List<Table> lists = dc.Tables.ToList<Table>();
            List<Table> list = new List<Table>();
            if (lists != null)
            {
                foreach (var q in lists)
                {
                    if (q.catagory == category)
                    {
                        list.Add(q);
                    }
                }
            }
            if (list != null)
                return View(list);
            else return View();
        }

        public ActionResult Edit()
        {
            int id = Convert.ToInt32(Request.Form["id"]);           
            return View(dc.Tables.First(s => s.id == id));          
        }
        public ActionResult EditDone()
        {
            int ide = Convert.ToInt32(Request["ido"]);
            var a = dc.Tables.First(s => s.id == ide);
            a.name = Request["name"];
            a.catagory = Request["catagory"];
            a.price = Request["price"];
            a.description = Request["description"];
            dc.SubmitChanges();
            return RedirectToAction("index");
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddDone()
        {
            string name = Request["name"];
            string catagory = Request["catagory"];
            string price = Request["price"];
            string description = Request["description"];
            Table t = new Table();
            t.name = name;
            t.price = price;
            t.catagory = catagory;
            t.description = description;
            dc.Tables.InsertOnSubmit(t);
            dc.SubmitChanges();
            return RedirectToAction("index");
        }
    }
}