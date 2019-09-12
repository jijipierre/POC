using System;

namespace POC.WebApi.Models
{
    public interface IValuesItem
    {
        int Id { get; set; }
        string Value { get; set; }
    }
}