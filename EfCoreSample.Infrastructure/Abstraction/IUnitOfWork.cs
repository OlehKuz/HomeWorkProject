using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreSample.Infrastructure.Abstraction
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
