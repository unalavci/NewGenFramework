﻿using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Core.Utilities.Mappings;
using NonFactors.Mvc.Grid;
using NewGenFramework.Business.Abstract;
using NewGenFramework.Entities.ComplexTypes;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.MvcWebUi.GenericVM;
using NewGenFramework.MvcWebUi.Filters;
using NewGenFramework.MvcWebUi.Infrastructure;
using NewGenFramework.MvcWebUi.Models.ViewModels;

namespace NewGenFramework.MvcWebUi.Controllers
{
    [AuthorizationFilter]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IQueryableRepository<Customer> _queryableRepository;
        private readonly IQueryableRepository<vwRP_StockCount> _totalRowsRepository;
        public CustomerController(ICustomerService customerService, IQueryableRepository<Customer> queryableRepository, IQueryableRepository<vwRP_StockCount> totalRowsRepository)
        {
            _customerService = customerService;
            _queryableRepository = queryableRepository;
            _totalRowsRepository = totalRowsRepository;
        }
        // GET: List
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult CustomerIndex(Int32? page, Int32? rows)
        {
            IGrid<Customer> col = new Grid<Customer>(_queryableRepository.Table.OrderByDescending(x => x.CustomerId).Skip((page - 1 ?? 0) * (rows ?? 10)).Take(rows ?? 10));
            col.Query = new NameValueCollection(Request.QueryString);

            if (col.Query != null)
            {
                col = new Grid<Customer>(_queryableRepository.Table.OrderByDescending(x => x.CustomerId));
            }
            col.Columns.Add(x => "<a class=' fas fa-edit btn btn-warning btn-sm' title='Güncelle' href='Edit/" + x.CustomerId + "'> </a>" +
                                 "<a class='actions fas fa-trash-alt btn btn-danger btn-sm' title='Sil' href='Delete/" + x.CustomerId + "'> </a>")
                .Encoded(false).Titled("işlemler").Filterable(false);
            //Görüntülenecek kolonları buraya yazacaksanız
            col.Columns.Add(x => x.CustomerId).Titled("CustomerId").MultiFilterable(true);            

            col.Pager = new GridPager<Customer>(col);
            col.Processors.Add(col.Pager);
            col.Pager.RowsPerPage = 10;
            col.EmptyText = "Gösterilecek Kayıt Yok :(";
            foreach (IGridColumn column in col.Columns)
            {
                column.IsFilterable = true;
                column.IsSortable = true;
            }
            var total = _totalRowsRepository.Table.Where(x => x.TableName == "Customers").Select(x => x.TableRows).First();
            ViewBag.totalRows = Convert.ToInt32(total);
            return View(col);
        }
         // GET: Create
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Create()
        {
            var empty = new CustomerViewModel();
            var data = empty.ToVM();
            return View(data);
        }
        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification("Kayıt Eklenemedi!");
                return RedirectToAction("Create");
            }
            _customerService.Add(new Customer
            {
                //TODO:Alanlar buraya yazılacak
                //Örn:BrandName = brand.BrandName,
                
            });
            SuccessNotification("Kayıt Eklendi.");
            return RedirectToAction("CustomerIndex");
        }
        // GET: Edit
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Edit(int id)
        {
            var data = AutoMapperHelper.MapToSameViewModel<Customer, CustomerViewModel>(_customerService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Edit
        [HttpPost]
        public ActionResult Edit( Customer customer)
        {
            try
            {                
                _customerService.Update(new  Customer
                {
                 //TODO:Alanlar buraya yazılacak Id alanı en altta olacak unutmayın!!!
                 //Örn:BrandName = brand.BrandName,
                 //BrandId = brand.BrandId
                  CustomerId = customer.CustomerId
                });
                SuccessNotification("Kayıt Güncellendi");
                return RedirectToAction("CustomerIndex");
            }
            catch
            {
                return View();
            }
        }
         // GET: Delete
        [SecuredOperation(Roles = "SystemAdmin")]
        public ActionResult Delete(int id, Customer customer)
        {
            var data = AutoMapperHelper.MapToSameViewModel<Customer, CustomerViewModel>(_customerService.GetById(id));
            return View(data.ToVM());
        }
        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {            
                _customerService.Delete(_customerService.GetById(id));
                SuccessNotification("Kayıt Silindi");
                return RedirectToAction("CustomerIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}