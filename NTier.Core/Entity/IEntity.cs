﻿namespace NTier.Core.Entity
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}