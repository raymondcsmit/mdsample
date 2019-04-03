using Microsoft.EntityFrameworkCore;
using System;

namespace Madyan.Data.Context
{
    public interface IDbContext : IDisposable
    {

    }
    public interface IDDBDBContext : IDbContext
    {

    }
    public interface IExceptionDBContext : IDbContext
    {

    }
   
}
