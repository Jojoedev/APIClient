using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAPIClientServer.Models;

namespace WebAPIClientServer.Controllers
{
    public class PoductController : Controller
    {
        HttpClient _httpClient;

        public PoductController()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri("https://localhost:7135/api/Product");
        }
        [HttpGet]
        public ActionResult<List<Product>> Index()
        {
            List<Product> products = new List<Product>();
            var request = _httpClient.GetAsync(_httpClient.BaseAddress).Result;

            if (request.IsSuccessStatusCode)
            {
                var content = request.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult<Product> CreateItem(Product product)
        {
            var content = JsonConvert.SerializeObject(product);

            var json = new StringContent(content, Encoding.UTF8, "application/json");

            var request = _httpClient.PostAsync(_httpClient.BaseAddress, json).Result;

            if (request.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet("id")]
        
        public ActionResult<Product> Details(int? id)
        {
            Product product = new Product();
       
        //https://localhost:7135/api/Product?id=1006
       // https://localhost:7135/api/Product/id?id=3
            var request = _httpClient.GetAsync(_httpClient.BaseAddress+ "/id?id=" + id).Result;
            if (request.IsSuccessStatusCode)
            {
                
                var content = request.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(content);
                return View(product);
            }
            return NotFound();
            
           
        }


        
        public ActionResult Edit(int? id)
        
        {
            Product product = new();
        //https://localhost:7135/api/Product/id?1005
        //https://localhost:7135/api/Product/id?id=1005
            // https://localhost:7135/api/Product/id?id=2
            var clientProduct = _httpClient.GetAsync(_httpClient.BaseAddress + "/id?id="+id).Result;
           
            if(clientProduct.IsSuccessStatusCode)
            {
                var content =  clientProduct.Content.ReadAsStringAsync().Result;
                product=  JsonConvert.DeserializeObject<Product>(content);
                return View(product);
            }
            return NotFound();

        }

        [HttpPost]
        public ActionResult<Product> Edit(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //https://localhost:7135/api/Product?id=3
            var request = _httpClient.PutAsync(_httpClient.BaseAddress+"/?id="+product.Id, content).Result;


            if (request.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            var request = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/?id="+ id).Result;
            if (request.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
