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
    public class BrandManager : ManagerBase, IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<Brand> GetAll()
        {
            return _brandDal.GetList();
        }

        public Brand GetById(int brandId)
        {
            return _brandDal.Get(u => u.BrandId == brandId);
        }      

        //[FluentValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Brand Add(Brand brand)
        {
            return _brandDal.Add(brand);
        }
        //[FluentValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(Brand brand)
        {
              _brandDal.Update(brand);
        }

        public void Delete(Brand brand)
        {
            _brandDal.Delete(brand);
        }    

        public List<Brand> GetByBrand(int brandId)
        {
            return _brandDal.GetList(filter: t => t.BrandId == brandId).ToList();
        }
    }
}