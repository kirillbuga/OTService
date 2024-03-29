using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using OTAnswerService.Extensions;
using OTAnswerService.Infrastructure;

namespace OTAnswerService.DataAccess
{
    public class EfRepository : IRepository
    {
        private readonly EfDataContext context = new EfDataContext();

        public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] eagerlyLoadedProperties) where T : class, IIdentifiable
        {
            return eagerlyLoadedProperties.Aggregate(
                (DbQuery<T>)context.Set<T>(),
                (current, property) => current.Include(property.GetPath()));
        }

        public T Get<T>(int id, params Expression<Func<T, object>>[] eagerlyLoadedProperties) where T : class, IIdentifiable
        {
            var set = context.Set<T>();

            if (!eagerlyLoadedProperties.Any())
            {
                return set.Find(id);
            }

            return eagerlyLoadedProperties.Aggregate(
                (DbQuery<T>)set,
                (current, property) => current.Include(property.GetPath())).First(x => x.Id == id);
        }

        public void Save<T>(T instance) where T : class, IIdentifiable
        {
            if (instance.Id == default(int))
            {
                context.Set<T>().Add(instance);
            }
            else
            {
                context.Entry(instance).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void Delete<T>(T instance) where T : class, IIdentifiable
        {
            context.Set<T>().Remove(instance);
            context.SaveChanges();
        }
    }
}