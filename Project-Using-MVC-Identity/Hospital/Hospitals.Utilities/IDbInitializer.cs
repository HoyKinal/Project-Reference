using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitals.Utilities
{
    public interface IDbInitializer
    {
        Task InitializeAsync(); //void Initialize();  
    }
}
