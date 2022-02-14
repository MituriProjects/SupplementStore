﻿using Microsoft.EntityFrameworkCore;
using SupplementStore.Domain;
using System;
using System.Linq;
using System.Reflection;

namespace SupplementStore.Infrastructure {

    public class Document<TEntity> : IDocument<TEntity>
        where TEntity : Entity {

        DbSet<TEntity> Entities { get; }

        public Document(ApplicationDbContext dbContext) {

            Entities = (DbSet<TEntity>)dbContext
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .First(p => p.PropertyType.GenericTypeArguments[0] == typeof(TEntity))
                .GetValue(dbContext);
        }

        public IQueryable<TEntity> All => Entities;

        public void Add(TEntity entity) {

            Entities.Add(entity);
        }

        public void Delete(Guid id) {

            var entity = Entities.FirstOrDefault(e => e.Id.Equals(id));

            if (entity != null) {

                Entities.Remove(entity);
            }
        }
    }
}
