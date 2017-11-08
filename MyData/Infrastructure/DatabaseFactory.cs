using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private MyContext dataContext = null;
        public MyContext Get()
        {
            return dataContext ?? (dataContext = new MyContext()); // tassna3 contexte
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose(); // nfassa5 contexte
        }
    }
}
