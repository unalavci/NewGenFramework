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
    public class KnowledgeTestManager : ManagerBase, IKnowledgeTestService
    {
        private IKnowledgeTestDal _knowledgeTestDal;

        public KnowledgeTestManager(IKnowledgeTestDal knowledgeTestDal)
        {
            _knowledgeTestDal = knowledgeTestDal;
        }
        
         // [LogAspect(typeof(DatabaseLogger))]
        [CacheAspect(typeof(MemoryCacheManager))]
        // [PerformanceCounterAspect(1)]      
        public List<KnowledgeTest> GetAll()
        {
            return _knowledgeTestDal.GetList();
        }

        public KnowledgeTest GetById(int knowledgeTestId)
        {
            return _knowledgeTestDal.Get(u => u.KnowledgeTestId == knowledgeTestId);
        }      

        //[FluentValidationAspect(typeof(KnowledgeTestValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public KnowledgeTest Add(KnowledgeTest knowledgeTest)
        {
            return _knowledgeTestDal.Add(knowledgeTest);
        }
        //[FluentValidationAspect(typeof(KnowledgeTestValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(KnowledgeTest knowledgeTest)
        {
              _knowledgeTestDal.Update(knowledgeTest);
        }

        public void Delete(KnowledgeTest knowledgeTest)
        {
            _knowledgeTestDal.Delete(knowledgeTest);
        }    

        public List<KnowledgeTest> GetByKnowledgeTest(int knowledgeTestId)
        {
            return _knowledgeTestDal.GetList(filter: t => t.KnowledgeTestId == knowledgeTestId).ToList();
        }
    }
}
