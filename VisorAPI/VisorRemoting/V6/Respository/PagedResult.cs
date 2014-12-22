﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V6.Respository
{
    public class PagedResult<TEntity>
    {
        IEnumerable<TEntity> _items;
        int _totalCount;

        public PagedResult(IEnumerable<TEntity> items, int totalCount)
        {
            _items = items;
            _totalCount = totalCount;
        }

        public IEnumerable<TEntity> Items { get { return _items; } }
        public int TotalCount { get { return _totalCount; } }
    }
}