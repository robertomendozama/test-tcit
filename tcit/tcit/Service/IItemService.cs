using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcit.Models;

namespace tcit.Repository
{
    public interface IItemService
    {
        List<Item> GetAllItems(string nombre = null);
        bool AddItem(Item item); 
        bool DeleteItem(int id);
    }
}
