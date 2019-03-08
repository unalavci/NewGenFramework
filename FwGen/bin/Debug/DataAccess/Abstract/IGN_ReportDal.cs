﻿
using NewGenFramework.Core.DataAccess;
using NewGenFramework.Entities.Concrete;
namespace NewGenFramework.DataAccess.Abstract
{
    //Neden IEntityRepository direk kullanmıyoruz da IProductDal diye bir şeyi araya atıyoruz sorusuna 
    //Nesneye özgü metodlar geliştirilebilir.
    public interface IGN_ReportDal:IEntityRepository<GN_Report>
    {
        //for Ex:
        //List<GN_ReportDetail> GetGN_ReportDetails();
    }
}