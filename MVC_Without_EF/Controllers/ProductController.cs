using MVC_Without_EF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Without_EF.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"
                    Data Source=localhost\SQLEXPRESS01; 
                    Initial Catalog=ProductsDb; 
                    Integrated Security=True;";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable ();
            using ( SqlConnection con = new SqlConnection (connectionString) )
            {
                con.Open ();
                string query = "SELECT * FROM Products";
                SqlDataAdapter sda = new SqlDataAdapter (query, con);
                sda.Fill ( dt );
            }
            return View(dt);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View( new ProductModel() );
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            using ( SqlConnection con = new SqlConnection (connectionString) )
            {
                con.Open ();
                string query = "INSERT INTO Products VALUES (@ProductName,@Price,@Quantity)";
                SqlCommand cmd = new SqlCommand (query, con);
                cmd.Parameters.AddWithValue ("@ProductName", model.ProductName);
                cmd.Parameters.AddWithValue ( "@Price", model.Price );
                cmd.Parameters.AddWithValue ( "@Quantity", model.Quantity );
                cmd.ExecuteNonQuery ();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel model = new ProductModel ();
            DataTable dt = new DataTable ();
            using ( SqlConnection con = new SqlConnection (connectionString) )
            {
                con.Open ();
                string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                SqlDataAdapter sda = new SqlDataAdapter (query, con);
                sda.SelectCommand.Parameters.AddWithValue ( "@ProductID", id );
                sda.Fill ( dt );
            }
            if ( dt.Rows.Count == 1 )
            {
                model.ProductID = Convert.ToInt32 ( dt.Rows[0][0].ToString () );
                model.ProductName = dt.Rows[0][1].ToString ();
                model.Price = Convert.ToDecimal ( dt.Rows[0][2].ToString () );
                model.Quantity = Convert.ToInt32 ( dt.Rows[0][3].ToString () );
                return View ( model );
            }
            else
            {
                return RedirectToAction ( "Index" );
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit (ProductModel model)
        {
            using ( SqlConnection con = new SqlConnection ( connectionString ) )
            {
                con.Open ();
                string query = "UPDATE Products SET ProductName = @ProductName," +
                    "Price = @Price," +
                    "Quantity = @Quantity" +
                    " WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand ( query, con );
                cmd.Parameters.AddWithValue ( "@ProductID", model.ProductID );
                cmd.Parameters.AddWithValue ( "@ProductName", model.ProductName );
                cmd.Parameters.AddWithValue ( "@Price", model.Price );
                cmd.Parameters.AddWithValue ( "@Quantity", model.Quantity );
                cmd.ExecuteNonQuery ();
            }
            return RedirectToAction ( "Index" );
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductModel model = new ProductModel ();
            DataTable dt = new DataTable ();
            using ( SqlConnection con = new SqlConnection ( connectionString ) )
            {
                con.Open ();
                string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                SqlDataAdapter sda = new SqlDataAdapter ( query, con );
                sda.SelectCommand.Parameters.AddWithValue ( "@ProductID", id );
                sda.Fill ( dt );
            }
            if ( dt.Rows.Count == 1 )
            {
                model.ProductID = Convert.ToInt32 ( dt.Rows[0][0].ToString () );
                model.ProductName = dt.Rows[0][1].ToString ();
                model.Price = Convert.ToDecimal ( dt.Rows[0][2].ToString () );
                model.Quantity = Convert.ToInt32 ( dt.Rows[0][3].ToString () );
                return View ( model );
            }
            else
            {
                return RedirectToAction ( "Index" );
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using ( SqlConnection con = new SqlConnection ( connectionString ) )
            {
                con.Open ();
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand ( query, con );
                cmd.Parameters.AddWithValue ( "@ProductID", id );
                cmd.ExecuteNonQuery ();
            }
            return RedirectToAction ( "Index" );
        }
    }
}
