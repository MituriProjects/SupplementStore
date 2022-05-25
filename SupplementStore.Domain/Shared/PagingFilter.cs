using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SupplementStore.Domain.Shared {

    public class PagingFilter<TEntity> : IManyFilter<TEntity>
        where TEntity : Entity {

        int Skip { get; }

        int Take { get; }

        IManyFilter<TEntity> PreFilter { get; }

        Expression<Func<TEntity, object>> OrderByDescendingKeySelector { get; }

        public PagingFilter(int skip, int take) {

            Skip = skip;
            Take = take;
        }

        public PagingFilter(int skip, int take, IManyFilter<TEntity> preFilter)
            : this(skip, take) {

            PreFilter = preFilter;
        }

        public PagingFilter(int skip, int take, Expression<Func<TEntity, object>> orderByDescendingKeySelector)
            : this(skip, take) {

            OrderByDescendingKeySelector = orderByDescendingKeySelector;
        }

        public IEnumerable<TEntity> Process(IQueryable<TEntity> entities) {

            var results = entities;

            if (PreFilter != null)
                results = (IQueryable<TEntity>)PreFilter.Process(results);

            if (OrderByDescendingKeySelector != null)
                results = results.OrderByDescending(OrderByDescendingKeySelector);

            return results
                .Skip(Skip)
                .Take(Take);
        }
    }
}
