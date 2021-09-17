using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;

namespace TrueHome.Provider.Contracts
{
    public interface IPropertyProvider
    {
        Task<List<Property>> GetAsync();
    }
}
