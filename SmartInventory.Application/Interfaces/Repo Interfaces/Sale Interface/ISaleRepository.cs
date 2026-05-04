using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Repo_Interfaces.Sale_Interface
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();

        Task<Sale?> GetSaleByIdAsync(int id);

        Task AddSaleAsync(Sale sale);

        void UpdateSale(Sale sale);

        void DeleteSale(Sale sale);

        Task<bool> SaveChangesAsync();
    }
}
