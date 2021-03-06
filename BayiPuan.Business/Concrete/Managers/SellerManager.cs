﻿
using System.Collections.Generic;
using System.Linq;
using BayiPuan.Business.Abstract;
using NewGenFramework.Core.Aspects.Postsharp.CacheAspects;
using NewGenFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using BayiPuan.DataAccess.Abstract;
using BayiPuan.Entities.Concrete;
using NewGenFramework.Core.CrossCuttingConcerns.Transaction;

namespace BayiPuan.Business.Concrete.Managers
{
    public class SellerManager : ManagerBase, ISellerService
    {
        private readonly ISellerDal _sellerDal;

        public SellerManager(ISellerDal sellerDal)
        {
            _sellerDal = sellerDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Seller> GetAll()
        {
            return _sellerDal.GetList();
        }

        public Seller GetById(int sellerId)
        {
            return _sellerDal.Get(u => u.SellerId == sellerId);
        }      

        //[FluentValidationAspect(typeof(SellerValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Seller Add(Seller seller)
        {
            return _sellerDal.Add(seller);
        }
        //[FluentValidationAspect(typeof(SellerValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Seller seller)
        {
              _sellerDal.Update(seller);
        }

        public void Delete(Seller seller)
        {
            _sellerDal.Delete(seller);
        }    

        public List<Seller> GetBySeller(int sellerId)
        {
            return _sellerDal.GetList(filter: t => t.SellerId == sellerId).ToList();
        }
    }
}
