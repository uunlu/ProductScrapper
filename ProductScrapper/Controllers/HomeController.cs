using ProductScrapper.AppServices.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ProductScrapper.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
          


            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetLinks()
        {

            var url = "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&ppp=144&categoryId=63266056";

            //var response2 = await DispatchAsync(new ScrapProduct.Request() {Url = url });
            var response = await mediator.SendAsync(new ScrapAllProducts.Request() { Url = url });
            return Json(response);

        }

        [HttpPost]
        public async Task<JsonResult> GetProductDeatils()
        {

            var url = "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&ppp=144&categoryId=63266056";

            var response = await mediator.SendAsync(new ScrapProduct.Request() );
            return Json(response);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> ScrapeAllProducts()
        {
            //var response = await DispatchAsync(new ScrapAllProducts.Request() { });
            var response2 = await DispatchAsync(new ScrapProduct.Request() { });
            return View();
        }
    }
}