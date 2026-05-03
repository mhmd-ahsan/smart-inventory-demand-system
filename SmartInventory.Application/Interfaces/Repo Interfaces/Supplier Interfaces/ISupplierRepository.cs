using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Repo_Interfaces
{
    public interface  ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsyn(int id);
        Task AddSupplierAsync(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void RemoveSupplier(Supplier supplier);
        Task<bool> SaveChangesAsync();

    }
}
