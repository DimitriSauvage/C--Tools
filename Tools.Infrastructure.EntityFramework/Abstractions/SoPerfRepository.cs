using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tools.Domain.Abstractions;
using Tools.Infrastructure.EntityFramework;
using Tools.Infrastructure.EntityFramework.Exceptions;
using Tools.Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Tools.Helpers;
using Tools.Domain.DataAnnotations;

namespace Tools.Infrastructure.EntityFramework.Abstractions
{
    public abstract class SoPerfRepository<TEntity> : SoPerfRepository<TEntity, long>
        where TEntity : class, IEntityWithId<long>
    {
        public SoPerfRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }

    /// <summary>
    /// Repository de base pour Entity Framework
    /// </summary>
    /// <typeparam name="TContext">Contexte Entity Framework</typeparam>
    /// <typeparam name="TEntity">Type de l'entité concernée</typeparam>
    /// <typeparam name="TId">Type d'identifiant</typeparam>
    public abstract class SoPerfRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntityWithId<TId>
    {
        /// <summary>
        /// Affecte ou obtient le contexte
        /// </summary>
        public DbContext Context { get; set; }

        //public SoPerfRepository(IDesignTimeDbContextFactory<DbContext> factory, params string[] args)
        //{
        //    this.Context = factory.CreateDbContext(args) as TContext;
        //}

        public SoPerfRepository(DbContext context)
        {
            this.Context = context;
        }

        #region Methods

        public TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Attache l'entité passée en paramètre au contexte courant
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }

        /// <summary>
        /// Attache la collection d'entitées passée en paramètre au contexte courant
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Attach(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AttachRange(entities);
        }

        public virtual int Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Context.SaveChanges();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return await Context.SaveChangesAsync();
        }
        public virtual void Edit(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual ICollection<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual TEntity GetById(TId id)
        {
            return Context.Set<TEntity>().Where(entity => entity.Id.Equals(id)).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await Context.Set<TEntity>().Where(entity => entity.Id.Equals(id)).SingleOrDefaultAsync();
        }

        ///// <summary>
        ///// Obtient un objet de la session nHibernate à partir de sa clé unique
        ///// </summary>
        ///// <typeparam name="T">Type de l'objet à requêter</typeparam>
        ///// <param name="obj">objet à tester</param>
        ///// <returns></returns>
        //public TEntity GetByUniqueKey(TEntity obj)
        //{
        //    var uniqueKeyProperties = AttributeHelper.GetValueAndPropertyNameDictionary(obj, typeof(UniqueKeyAttribute));
        //    if (!uniqueKeyProperties.Any())
        //        throw new AttributeUndefinedException(typeof(UniqueKeyAttribute), typeof(TEntity));

        //    // Composition du critère de requête
        //    var query = Query();
        //    foreach (var uniqueKeyProperty in uniqueKeyProperties)
        //    {
        //        var prop = typeof(TEntity).GetProperty(uniqueKeyProperty.Key);
        //        if (prop.PropertyType.IsSubclassOf(typeof(Entity)))
        //            throw new CantExecuteAutoUniqueKeyQueryOnClassPropertyException(uniqueKeyProperty.Key);
        //        else
        //        {
        //            ParameterExpression pe = Expression.Parameter(typeof(TEntity));

        //            // Le type est un type valeur
        //            if (uniqueKeyProperty.Value == null)
        //            {
        //                // Cas de la valeur nulle
        //                Expression left = Expression.Property(pe, uniqueKeyProperty.Key);
        //                Expression right = Expression.Constant(null, typeof(object));

        //                Expression expression = Expression.Equal(left, right);

        //            }
        //            else
        //            {
        //                Expression left = Expression.Property(pe, uniqueKeyProperty.Key);
        //                Expression right = Expression.Constant(uniqueKeyProperty.Value);

        //                Expression expression = Expression.Equal(left, right);
        //            }
        //        }
        //    }

        //    return query.SingleOrDefault();
        //}

        /// <summary>
        /// Obtient une <see cref="TEntity"/> depuis les propriétés qui composent sa clé unique
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract Task<TEntity> GetByUniqueKeyAsync(TEntity obj);

        /// <summary>
        /// Obtient une <see cref="TEntity"/> depuis les propriétés qui composent sa clé unique
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual TEntity GetByUniqueKey(TEntity obj)
        {
            return GetByUniqueKeyAsync(obj).Result;
        }

        public virtual ICollection<TEntity> List()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual async Task<ICollection<TEntity>> ListAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="parameter">Paramètres</param>
        public void Execute<T>(Action<T> action, T parameter)
        {
            Execute<T>(action, parameter);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="parameter">Paramètres</param>
        /// <param name="onSuccessAction">Méthode à éxecuter en cas de succès</param>
        /// <param name="clearSession">Indique la session nHibernate doit être effacée après le traitement</param>
        public void Execute<T>(Action<T> action, T parameter, Action<T> onSuccessAction)
        {
            Execute<T>(action, parameter, onSuccessAction, null);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="parameter">Paramètres</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        public void Execute<T>(Action<T> action, T parameter, Action<EntityFrameworkException> onErrorAction)
        {
            Execute<T>(action, parameter, onErrorAction);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="parameter">Paramètres</param>
        /// <param name="onSuccessAction">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        /// <param name="clearSession">Indique la session nHibernate doit être nettoyée après le traitement</param>
        public void Execute<T>(Action<T> action, T parameter, Action<T> onSuccessAction, Action<EntityFrameworkException> onErrorAction)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    action(parameter);
                    transaction.Commit();

                    onSuccessAction?.Invoke(parameter);
                }
                catch (System.Exception e)
                {
                    transaction.Rollback();

                    onErrorAction?.Invoke(new EntityFrameworkException(e, parameter));
                }
            }
        }

        /// <summary>
        /// Effectue un comptage des éléments de type <typeparamref name="TEntity"/>, la clause where sera passée en paramètre grâce à un prédicat
        /// </summary>
        /// <param name="predicate">Prédicat de sélection</param>
        /// <returns>Le nombre d'éléments</returns>
        public int CountBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).Count();
        }

        public Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).CountAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>().AsQueryable();
        }
        #endregion
    }
}
