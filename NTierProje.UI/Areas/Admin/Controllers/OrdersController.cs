﻿using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Areas.Admin.Models;
using NTierProje.UI.Attributes;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    [CustomAuthorize(Role.Admin)]
    public class OrdersController : Controller
    {
        OrderService _orderService;
        OrderDetailService _orderDetailsService;
        ProductService _productService;
        public OrdersController()
        {
            _productService = new ProductService();
            _orderDetailsService = new OrderDetailService();
            _orderService = new OrderService();
        }

        [HttpGet]
        public ActionResult List(int page=1)
        {
            //Daha onaylanmamış tüm siparişleri listele
            List<Orders> model = _orderService.ListPendingOrders();
            return View(model.ToPagedList(page,10));
        }

        //Onaylanmamış sipariş sayısını ana sayfada listeler
        public JsonResult OrderCount()
        {
            int count = _orderService.PendingOrderCount();

            return Json(count,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid Id)
        {
            List<OrderDetails> model = _orderDetailsService.GetDefault(x => x.Orders.Id == Id);

            return View(model);
        }

        public RedirectResult ConfirmOrder(Guid Id)
        {
            Orders order = new Orders();
            order = _orderService.GetById(Id);

            order.Confirmed = true;
            _orderService.Update(order);
            return Redirect("~/Admin/Orders/List");
        }
        public RedirectResult RejectOrder(Guid Id)
        {
            Orders order = _orderService.GetById(Id);
            
            foreach (var item in order.OrderDetails)
            {
                Product p = _productService.GetById(item.Product.Id);
                p.UnitsInStock += Convert.ToInt16(item.Product.Quantity);
                p.Status = NTier.Core.Entity.Enum.Status.Deleted;
                _productService.Update(p);
            }
            
            order.Confirmed = false;
            order.Status = NTier.Core.Entity.Enum.Status.Deleted;
            _orderService.Update(order);
            return Redirect("~/Admin/Orders/List");

        }

    }
}