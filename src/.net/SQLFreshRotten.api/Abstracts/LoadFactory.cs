﻿using SQLFreshRotten.api.Interfaces;

namespace SQLFreshRotten.api.Abstracts
{
    public abstract class LoadFactory
    {
        public abstract ILoad<T> AddItemRange<T>() where T : class;
    }
}
