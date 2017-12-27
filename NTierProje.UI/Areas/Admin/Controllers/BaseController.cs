using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            _orderDetailService = new OrderDetailService();
            _orderService = new OrderService();
            _productService = new ProductService();
        }

        private OrderService _orderService;

        public OrderService OrderService
        {
            get { return _orderService; }
            set { _orderService = value; }
        }

        private ProductService _productService;

        public ProductService ProductService
        {
            get { return _productService; }
            set { _productService = value; }
        }
        private OrderDetailService _orderDetailService;

        public OrderDetailService OrderDetailService
        {
            get { return _orderDetailService; }
            set { _orderDetailService = value; }
        }


    }
}