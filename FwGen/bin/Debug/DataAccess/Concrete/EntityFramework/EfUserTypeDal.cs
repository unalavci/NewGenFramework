﻿using NewGenFramework.Core.DataAccess.EntityFramework;
using NewGenFramework.DataAccess.Abstract;
using NewGenFramework.DataAccess.Concrete.Context;
using NewGenFramework.Entities.Concrete;

namespace NewGenFramework.DataAccess.Concrete.EntityFramework
{
		 public class EfUserTypeDal:EfEntityRepositoryBase<UserType,NewGenFrameworkContext>,IUserTypeDal
		{
		}
}
