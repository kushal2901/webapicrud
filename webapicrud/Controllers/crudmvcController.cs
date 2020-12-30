using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using webapicrud.Models;

namespace webapicrud.Controllers
{
    public class crudmvcController : Controller
    {
        HttpClient hc = new HttpClient();

        // GET: crudmvc
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Getdata()
        {
            IEnumerable<EmpClass> empobj = null;
            hc.BaseAddress = new Uri("https://localhost:44335/api/crudwebapi");

            var consume = hc.GetAsync("crudwebapi");
            consume.Wait();

            var display = consume.Result;
            
            if(display.IsSuccessStatusCode)
            {
                var readdata = display.Content.ReadAsAsync<IList<EmpClass>>();
                readdata.Wait();
                empobj = readdata.Result;
            }
            else
            {
                empobj = Enumerable.Empty<EmpClass>();
                ModelState.AddModelError(string.Empty, "No Record Found");
            }
            return View(empobj);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(EmpClass ec)
        {
            hc.BaseAddress = new Uri("https://localhost:44335/api/crudwebapi/insert");
            var insertdata = hc.PostAsJsonAsync<EmpClass>("insert", ec);

            var savedata = insertdata.Result;

            if(savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Getdata");
            }
            return View("Insert");
        }

        public ActionResult GetEmpId(int id)
        {
            //IEnumerable<EmpClass> empobj = null;
            EmpClass empobj = null;
            hc.BaseAddress = new Uri("https://localhost:44335/api/");

            var consume = hc.GetAsync("crudwebapi?id=" + id.ToString());
            consume.Wait();

            var display = consume.Result;

            if(display.IsSuccessStatusCode)
            {
                //var readdata = display.Content.ReadAsAsync<IList<EmpClass>>();
                var readdata = display.Content.ReadAsAsync<EmpClass>();
                readdata.Wait();
                empobj = readdata.Result;
                return View(empobj);
            }
            else
            {
                return RedirectToAction("GetData");
            }
            /*else
            {
                empobj = Enumerable.Empty<EmpClass>();
                ModelState.AddModelError(string.Empty, "No Record Found");
            }*/
            
        }

        
        public ActionResult Update(int id)
        {
            //IEnumerable<EmpClass> empobj = null;
            EmpClass empobj = null;
            hc.BaseAddress = new Uri("https://localhost:44335/api/");

            var consume = hc.GetAsync("crudwebapi?id=" + id.ToString());
            consume.Wait();

            var display = consume.Result;

            if (display.IsSuccessStatusCode)
            {
                //var readdata = display.Content.ReadAsAsync<IList<EmpClass>>();
                var readdata = display.Content.ReadAsAsync<EmpClass>();
                readdata.Wait();
                empobj = readdata.Result;
                return View(empobj);
            }
            else
            {
                return RedirectToAction("Getdata");
            }
            /*else
            {
                empobj = Enumerable.Empty<EmpClass>();
                ModelState.AddModelError(string.Empty, "No Record Found");
            }*/
            
        }

        [HttpPost]
        public ActionResult Update(EmpClass ec)
        {
            hc.BaseAddress = new Uri("https://localhost:44335/api/crudwebapi");
            var insertdata = hc.PutAsJsonAsync<EmpClass>("crudwebapi", ec);

            var savedata = insertdata.Result;

            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Getdata");
            }
            return View("Update");
        }

        
        public ActionResult Delete(int id)
        {
            hc.BaseAddress = new Uri("https://localhost:44335/api/crudwebapi");
            var deletedata = hc.DeleteAsync("crudwebapi/" + id.ToString());
            deletedata.Wait();

            var displaydata = deletedata.Result;

            if(displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Getdata");
            }

            return View("Getdata");
        }
    }
}