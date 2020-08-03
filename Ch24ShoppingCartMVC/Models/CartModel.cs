﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ch24ShoppingCartMVC.Models;

namespace Ch24ShoppingCartMVC.Models
{
    public class CartModel
    { 
        private List<ProductViewModel> GetCartFromDataStore()
        {
            List<ProductViewModel> cart;
            object objCart = HttpContext.Current.Session["cart"];
            //Convert objCart to List<ProductViewModel>
            cart = (List<ProductViewModel>)objCart;
            if (cart == null)
            {
                //Create the object cart
                HttpContext.Current.Session["cart"] = new List<ProductViewModel>();
                //Assign cart to the Session object cart
                cart = (List<ProductViewModel>)HttpContext.Current.Session["cart"];
            }
            return cart;
        }//close GetCartFromDataStore()


        private ProductViewModel GetSelectedProduct(string id)
        {
            //Create an OrderModel object called order
            OrderModel order = new OrderModel();
            //Call the method GetSelectedProduct of the class OrderModel.
            return order.GetSelectedProduct(id);
        }


        public CartViewModel GetCart(string id = "")
        {
            CartViewModel model = new CartViewModel();
            //Call the method GetCartFromDataStore
            model.Cart = GetCartFromDataStore();
            if (!string.IsNullOrEmpty(id))
                //Called the method GetSelectedProduct with parameter id and assign the return object 
                model.AddedProduct = GetSelectedProduct(id);
            return model;
        }
        private void AddItemToDataStore(CartViewModel model)
        {
            //Add the AddedProduct to the cart
            model.Cart.Add(model.AddedProduct);
        }//close AddItemToDataStore(...)


        public void AddToCart(CartViewModel model)
        {
            if (model.AddedProduct.ProductID != null)
            {
                //Get the product id of the added product
                string id = model.AddedProduct.ProductID;
                //Find the product in the cart that matches the id using lambda expression.
                ProductViewModel inCart = model.Cart.Where(p => p.ProductID == id).FirstOrDefault();
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