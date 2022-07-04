using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABCSupermarket_19001700_Final.Models;
using ABCSupermarket_19001700_Final.TableHandler;
using ABCSupermarket_19001700_Final.BlobHandler;

namespace musicwebapp.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index(string id)
        {
            // Editing action
            if (!string.IsNullOrEmpty(id))
            {
                //set the name of the table
                TableManager TableManagerObj = new TableManager("products");

                //retrieve the musician to be updated
                List<Products> ProductListObj = TableManagerObj.RetrieveEntity<Products>("RowKey eq '" + id + "'");
                Products ProductsObj = ProductListObj.FirstOrDefault();
                return View(ProductsObj);
            }

            return View(new Products());
        }

        [HttpPost]
        public ActionResult Index(string id, HttpPostedFileBase uploadFile, FormCollection formData)
        {
            Products ProductObj = new Products();
            ProductObj.ProductName = formData["ProductName"] == "" ? null : formData["ProductName"];
            ProductObj.ProductDescription = formData["ProductDescription"] == "" ? null : formData["ProductDescription"];
            double productPrice;
                if (double.TryParse(formData["ProductPrice"], out productPrice))
            {
                ProductObj.ProductPrice = double.Parse(formData["ProductPrice"] == "" ? null : formData["ProductPrice"]);
            }
                else
            {
                return View(new Products());
            }

                foreach (string file in Request.Files)
            {
                uploadFile = Request.Files[file];
            }

            //Blob container creation
            BlobManager BlobManagerObj = new BlobManager("pictures");
            string FileAbsoluteUri = BlobManagerObj.UploadFile(uploadFile);
            ProductObj.FilePath = FileAbsoluteUri.ToString();

                //Insert statement for blob container
                if (string.IsNullOrEmpty(id))
            {
                ProductObj.PartitionKey = "Product";
                ProductObj.RowKey = Guid.NewGuid().ToString();

                TableManager TableManagerObj = new TableManager("products");
                TableManagerObj.InsertEntity<Products>(ProductObj, true);
            }else
            {
                ProductObj.PartitionKey = "Product";
                ProductObj.RowKey = id;

                TableManager TableManagerObj = new TableManager("products");
                TableManagerObj.InsertEntity<Products>(ProductObj, false);
            }
            return RedirectToAction("Get");
        }

        //Get Products
        public ActionResult Get()
        {
            TableManager TableManagerObj = new TableManager("products");
            List<Products> ProductListObj = TableManagerObj.RetrieveEntity<Products>(null);
            return View(ProductListObj);
        }

        //Delete Product 
        public ActionResult Delete(string id)
        {
            //return the product to be deleted
            TableManager TableManagerObj = new TableManager("products");
            List<Products> ProductListObj = TableManagerObj.RetrieveEntity<Products>("RowKey eq '" + id + "'");
            Products ProductsObj = ProductListObj.FirstOrDefault();

            //delete the product
            TableManagerObj.DeleteEntity<Products>(ProductsObj);
            return RedirectToAction("Get");
        }


    }
}