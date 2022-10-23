﻿using System.Collections.Generic;
using System.Linq;

namespace TestNinja.ExternalDependencies;

public interface IUnitOfWork
{
    IQueryable<T> Query<T>();
}

public class UnitOfWork : IUnitOfWork
{
    public IQueryable<T> Query<T>()
    {
        return new List<T>().AsQueryable();
    }
}
