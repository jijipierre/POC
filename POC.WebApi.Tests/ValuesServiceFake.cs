using POC.WebApi.Models;
using POC.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POC.WebApi.Tests
{
    public class ValuesServiceFake : IValuesService
    {
        private readonly List<ValuesItem> _valeurs;

        public ValuesServiceFake()
        {
            _valeurs = new List<ValuesItem>()
            {
                new ValuesItem() { Id = 1,
                                   Value = "Value1" },
                new ValuesItem() { Id = 2,
                                   Value = "Value2" },
                new ValuesItem() { Id = 3,
                                   Value = "Value3" }
            };
        }

        public IEnumerable<ValuesItem> GetAllItems()
        {
            return _valeurs;
        }

        public ValuesItem Add(ValuesItem newItem)
        {
            newItem.Id = _valeurs.LastOrDefault().Id + 1;
            _valeurs.Add(newItem);
            return newItem;
        }

        public ValuesItem GetById(int id)
        {
            return _valeurs.Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void Remove(int id)
        {
            var existing = _valeurs.First(a => a.Id == id);
            _valeurs.Remove(existing);
        }
    }
}
