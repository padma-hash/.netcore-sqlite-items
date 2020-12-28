using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsAPI.Model;
using ItemsAPI.Repositories;

namespace apiTests
{
    class FakeService : IItemRepository
    {
        private readonly List<Items> _Items;
        public FakeService()
        {
            _Items = new List<Items>()
            {
                new Items { ItemId = 1,  ItemName = "Item1", Price = 100},
                new Items { ItemId = 2, ItemName = "Item2", Price = 100 },   
                new Items { ItemId =3, ItemName = "Item2", Price = 200 }
            };
        }
        public  Task<Items[]> GetAllItemsAsync()
        {
            return Task.FromResult(_Items.ToArray());
        }

        public async Task<Items> Create(Items newItem)
        {
 
            _Items.Add(newItem);
            return await Task.FromResult(newItem);
        }

    public Task<bool> Update(Items item)
        {
            bool result;
            var existing = _Items.FirstOrDefault(a => a.ItemId == item.ItemId);

            if (existing == null)
                result = false;
            else
            {

                result = true;
            }

            return Task.FromResult(result);
        }

        public Items GetItembyId(int itemId)
        {
            return _Items
                .FirstOrDefault(a => a.ItemId == itemId);
        }

        public Task<bool> Delete(int ItemId)
        {
            bool result;
            var existing = _Items.FirstOrDefault(a => a.ItemId == ItemId);
           
           if (existing == null) 
               result = false;
           else
           {
               result = _Items.Remove(existing);
            }

            return Task.FromResult(result);
        }

        public async Task<Items> MaxPrice(string itemName)
        {
            var query =
                from p in _Items
                    .OrderByDescending(p => p.Price)
                where p.ItemName == itemName
                select p;

            if (query.Any())
                return await Task.FromResult(query.First());
            else
                return null;
        }
    }
}
