using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemsAPI.Model;

namespace ItemsAPI.Repositories
{
    public interface IItemRepository
    {
        Task<Items[]> GetAllItemsAsync();

        Task<Items> Create(Items item);

        Task<bool> Update(Items item);

        Items GetItembyId(int itemId);

        Task<bool> Delete(int ItemId);

        Task<Items> MaxPrice(string ItemName);
    }
}
