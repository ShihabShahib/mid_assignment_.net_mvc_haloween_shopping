using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ch24ShoppingCartMVC.Models;

namespace Ch24ShoppingCartMVC.Models
{
    public class CartModel
    {
        //Data Access methods 
        private List<ProductViewModel> GetCartFromDataStore()
        {
            List<ProductViewModel> cart;
            object objCart = HttpContext.Current.Session["cart"];
            //Convert objCart to List<ProductViewModel>
            cart = (List<ProductViewModel>)objCart;
            if (cart == null)
            {   //Create the object cart
                objCart = new List<ProductViewModel>();
                //Assign cart to the Session object cart
                cart = (List<ProductViewModel>)objCart;
            }
            return cart;
        }
        private ProductViewModel GetSelectedProduct(string id)
        {   //Create an OrderModel object called order
            OrderModel order = new OrderModel();
            //Call the method GetSelectedProduct of the class OrderModel. Return the object returned by the call.
            return order.GetSelectedProduct(id);
        }
        public CartViewModel GetCart(string id = "")
        {
            CartViewModel model = new CartViewModel();
            //Call the method GetCartFromDataStore
            model.Cart = GetCartFromDataStore();
            if (!string.IsNullOrEmpty(id))
                //Called the method GetSelectedProduct with parameter id and assign the return object to the AddedProduct
                model.AddedProduct = GetSelectedProduct(id);
            return model;
        }
        private void AddItemToDataStore(CartViewModel model)
        {   //Add the AddedProduct to the cart
            model.Cart.Add(model.AddedProduct);
        }
        public void AddToCart(CartViewModel model)
        {
            if (model.AddedProduct.ProductID != null)
            {
                //Get the product id of the added product
                var productID = model.AddedProduct.ProductID;
                //Find the product in the car that matches the id using lambda expression.
                var inCart = GetCartFromDataStore().Where(p => p.ProductID.Equals(productID)).FirstOrDefault();
                if (inCart == null)
                    //Call the method AddItemToDataStore
                    AddItemToDataStore(model);
                else
                    //Increase the Quantity by the quantity of the added product
                    inCart.Quantity += model.AddedProduct.Quantity;
            }
        }


    }
}