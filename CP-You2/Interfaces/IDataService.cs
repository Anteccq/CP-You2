using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CP_You2.Interfaces
{
    public interface IDataService
    {
        Task SaveAsync();
        Task LoadAsync();
    }
}