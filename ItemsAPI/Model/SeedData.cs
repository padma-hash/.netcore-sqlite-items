using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemsAPI.DAL;

namespace ItemsAPI.Model
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext db)
        {
            if (!db.Item.Any())
            {
                db.Item.Add(new Items()
                {
                    ItemName = "Item1",
                    Price= 300
                });

               

                db.SaveChanges();
            }
        }


    }
}
