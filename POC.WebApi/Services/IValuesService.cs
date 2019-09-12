using POC.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.WebApi.Services
{
   public  interface IValuesService
    {
        IEnumerable<ValuesItem> GetAllItems();
        ValuesItem Add(ValuesItem newItem);
        ValuesItem GetById(int id);
        void Remove(int id);
    }

}
