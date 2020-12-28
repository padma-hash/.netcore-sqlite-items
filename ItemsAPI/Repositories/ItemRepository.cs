using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemsAPI.DAL;
using ItemsAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ItemsAPI.Repositories
{
    public class ItemRepository : IItemRepository
    {

        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context)
        {
            
            _context = context;
        }
        public async  Task<Items[]> GetAllItemsAsync()
        {
           // IQueryable<Items> query = _context.Item;

            var query =
                from p in _context.Item
               
                select p;
            return await query.ToArrayAsync();
        }

        public async Task<Items> Create(Items item)
        {
            var existingItem = GetItembyId(item.ItemId);
            if (existingItem == null)
            {
                _context.Item.Add(item);
            

            var numberOfItemsCreated = await _context.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                return item;
            }

        return null;
    }

        public async Task<bool> Update(Items item)
        {
            var success = false;

            var existingItem = GetItembyId(item.ItemId);

            if (existingItem != null)
            {
                existingItem.ItemName = item.ItemName;
                existingItem.Price = item.Price;


                _context.Item.Attach(existingItem);

                var numberOfItemsUpdated = await _context.SaveChangesAsync();

                if (numberOfItemsUpdated == 1)
                    success = true;
            }

            return success;
        }

        public Items GetItembyId(int itemId)
        {
            //var result = _context.Item
              //  .FirstOrDefault(x => x.ItemId == itemId);
            var result =
                from p in _context.Item
                where (p.ItemId == itemId)
                select p;

            return result.FirstOrDefault();
        }

        public async Task<bool> Delete(int ItemId)
        {
            var success = false;

            var existingItem = GetItembyId(ItemId);

            if (existingItem != null)
            {
                _context.Item.Remove(existingItem);

                var numberOfItemsDeleted = await _context.SaveChangesAsync();

                if (numberOfItemsDeleted == 1)
                    success = true;
            }

            return success;
        }

        public async Task<Items> MaxPrice(string itemName)
        {

            
           var query = 
                from p in _context.Item
                    .OrderByDescending(p => p.Price)
                where p.ItemName == itemName
                 select p;

          if (query.Any())
              return await  Task.FromResult(query.First());
          else
          return null;
        }
    }
}
