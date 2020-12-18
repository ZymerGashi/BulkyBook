﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        ICategoryRepository Category { get; }
        
        ISP_CAll SP_Call { get; }

    }
}
