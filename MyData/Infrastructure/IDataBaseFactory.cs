using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Infrastructure
{
    public interface IDatabaseFactory : IDisposable 
    {
        MyContext Get();
    } 
}
