﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LucidiaIT.Interfaces
{
    public interface IDataService<T> : IDisposable where T : IIdentifiable
    {
        Task<List<T>> GetListAsync();
        Task<T> GetDataObjectAsync(int? id);
        Task<int> CreateAsync(T t);
        Task<int> EditAsync(T t);
        Task<int> DeleteAsync(T t);
        bool DataObjectExists(int id);
    }
}