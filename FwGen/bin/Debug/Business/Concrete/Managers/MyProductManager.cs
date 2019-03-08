﻿
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NewGenFramework.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.AuthorizationAspects;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.Aspects.Postsharp.TransactionAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using NewGenFramework.DataAccess.Abstract;
using NewGenFramework.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace NewGenFramework.Business.Concrete.Managers
{
    public class MyProductManager : ManagerBase, IMyProductService
    {
        private IMyProductDal _myProductDal;

        public MyProductManager(IMyProductDal myProductDal)
        {
            _myProductDal = myProductDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<MyProduct> GetAll()
        {
            return _myProductDal.GetList();
        }

        public MyProduct GetById(int myProductId)
        {
            return _myProductDal.Get(u => u.MyProductId == myProductId);
        }      

        //[FluentValidationAspect(typeof(MyProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public MyProduct Add(MyProduct myProduct)
        {
            return _myProductDal.Add(myProduct);
        }
        //[FluentValidationAspect(typeof(MyProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(MyProduct myProduct)
        {
              _myProductDal.Update(myProduct);
        }

        public void Delete(MyProduct myProduct)
        {
            _myProductDal.Delete(myProduct);
        }    

        public List<MyProduct> GetByMyProduct(int myProductId)
        {
            return _myProductDal.GetList(filter: t => t.MyProductId == myProductId).ToList();
        }
    }
}
