using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nestor.Tools.Infrastructure.EntityFramework.Exceptions;
using Tools.Domain.Abstractions;
using Tools.Domain.DataAnnotations;
using Tools.Exceptions;
using Tools.Helpers;
using Tools.Infrastructure.Abstraction;
using Tools.Infrastructure.EntityFramework.Exceptions;

namespace Tools.Infrastructure.EntityFramework.Abstractions
{
    public abstract class DbRepository<TEntity> : DbRepository<TEntity, long>
        where TEntity : class, IEntityWithId<long>
    {
        public DbRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    /// <summary>
    /// Repository de base pour Entity Framework
    /// </summary>
    /// <typeparam name="TContext">Contexte Entity Framework</typeparam>
    /// <typeparam name="TEntity">Type de l'entité concernée</typeparam>
    /// <typeparam name="TId">Type d'identifiant</typeparam>
    public abstract class DbRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntityWithId<TId>
    {
        #region Fields

        /// <summary>
        /// Affecte ou obtient le contexte
        /// </summary>
        public DbContext Context { get; set; }

        #endregion

        #region Constructor

        protected DbRepository(DbContext context)
        {
            this.Context = context;
        }

        #endregion

        #region Methods

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// Attache l'entité passée en paramètre au contexte courant
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }

        /// <summary>
        /// Attache la collection d'entitées passée en paramètre au contexte courant
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AttachRange(entities);
        }

        public int Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Context.SaveChanges();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return await Context.SaveChangesAsync();
        }

        public void Edit(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public ICollection<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            ICollection<string> includes = null, bool noTracking = false)
        {
            IQueryable<TEntity> context = Context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    context = context.Include(include);
                }
            }

            if (noTracking)
            {
                return await context.Where(predicate).AsNoTracking().ToListAsync();
            }
            else
            {
                return await context.Where(predicate).ToListAsync();
            }
        }

        public TEntity GetById(TId id)
        {
            return Context.Set<TEntity>().Where(entity => entity.Id.Equals(id)).SingleOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await Context.Set<TEntity>().Where(entity => entity.Id.Equals(id)).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Obtient une <see cref="TEntity"/> depuis les propriétés qui composent sa clé unique
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByUniqueKeyAsync(TEntity obj)
        {
            var uniqueByAttribute =
                typeof(TEntity).GetCustomAttributes(typeof(UniqueByAttribute), true).SingleOrDefault() as
                    UniqueByAttribute;

            if (uniqueByAttribute == null)
                throw new NoUniqueByAttributeForTypeException<TEntity>();

            if (!uniqueByAttribute.PropertyNames.Any())
                throw new UniqueByAttributeIsEmptyException<TEntity>();

            var uniqueKeyProperties =
                AttributeHelper.GetValueAndPropertyNameDictionary(obj, uniqueByAttribute.PropertyNames);

            // Composition du critère de requête
            var query = Query();
            foreach (var uniqueKeyProperty in uniqueKeyProperties)
            {
                var prop = typeof(TEntity).GetProperty(uniqueKeyProperty.Key);
                if (prop.PropertyType.IsSubclassOf(typeof(Entity)))
                    throw new CantExecuteAutoUniqueKeyQueryOnClassPropertyException(uniqueKeyProperty.Key);
                else
                {
                    ParameterExpression pe = Expression.Parameter(typeof(TEntity));

                    // Le type est un type valeur
                    if (uniqueKeyProperty.Value == null)
                    {
                        // Cas de la valeur nulle
                        Expression left = Expression.Property(pe, uniqueKeyProperty.Key);
                        Expression right = Expression.Constant(null, typeof(object));

                        Expression expression = Expression.Equal(left, right);
                    }
                    else
                    {
                        Expression left = Expression.Property(pe, uniqueKeyProperty.Key);
                        Expression right = Expression.Constant(uniqueKeyProperty.Value);

                        Expression expression = Expression.Equal(left, right);
                    }
                }
            }

            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Obtient une <see cref="TEntity"/> depuis les propriétés qui composent sa clé unique
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TEntity GetByUniqueKey(TEntity obj)
        {
            return GetByUniqueKeyAsync(obj).Result;
        }

        public ICollection<TEntity> List()
        {
            return Context.Set<TEntity>().ToList();
        }

        public async Task<ICollection<TEntity>> ListAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
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

        /// <summary>
        /// Force le rechargement de l'entité passée en paramètre
        /// </summary>
        /// <param name="entity">entité à recharger</param>
        public async Task ReloadAsync(TEntity entity)
        {
            await Context.Entry<TEntity>(entity).ReloadAsync();
        }

        /// <summary>
        /// Force le rechargement des entités passées en paramètre
        /// </summary>
        /// <param name="entities">entités à recharger</param>
        public async Task ReloadAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                await ReloadAsync(item);
            }
        }


        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Objet</param>
        public async Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj)
        {
            await TransactionalExecutionAsync(action, obj, onSuccessAction: null, onErrorAction: null);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessAction">Méthode à éxecuter en cas de succès</param>
        public async Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Action<TEntity> onSuccessAction)
        {
            await TransactionalExecutionAsync(action, obj, onSuccessAction: onSuccessAction, onErrorAction: null);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        public async Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Action<AppException> onErrorAction)
        {
            await TransactionalExecutionAsync(action, obj, onSuccessAction: null, onErrorAction: onErrorAction);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessAction">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        public async Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Action<TEntity> onSuccessAction, Action<AppException> onErrorAction)
        {
            // Local function
            Func<TEntity> Func(TEntity entity)
            {
                onSuccessAction?.Invoke(entity);
                return null;
            }

            await TransactionalExecutionAsync<object>(action, obj, onSuccessFunc: Func, onErrorAction: onErrorAction);
        }


        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessFunc">Méthode à éxecuter en cas de succès</param>
        public async Task<TOut> TransactionalExecutionAsync<TOut>(Action<TEntity, IDbContextTransaction> action,
            TEntity obj,
            Func<TEntity, TOut> onSuccessFunc)
        {
            return await this.TransactionalExecutionAsync(action, obj, onSuccessFunc: onSuccessFunc,
                onErrorAction: null);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessFunc">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        public async Task<TOut> TransactionalExecutionAsync<TOut>(Action<TEntity, IDbContextTransaction> action,
            TEntity obj,
            Func<TEntity, TOut> onSuccessFunc, Action<AppException> onErrorAction)
        {
            using var transaction = BeginTransaction();
            TOut result = default;
            try
            {
                result = await Task.Run(() =>
                {
                    action(obj, transaction);
                    transaction.Commit();
                    return onSuccessFunc != null ? onSuccessFunc.Invoke(obj) : default;
                });
            }
            catch (Exception e)
            {
                transaction.Rollback();
                onErrorAction?.Invoke(new EntityFrameworkException(e, obj));
            }

            return result;
        }


        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        public async Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id)
        {
            await TransactionalExecutionAsync(action, id, onSuccessAction: null, onErrorAction: null);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessAction">Méthode à éxecuter en cas de succès</param>
        public async Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id,
            Action<TId> onSuccessAction)
        {
            await TransactionalExecutionAsync(action, id, onSuccessAction: onSuccessAction, onErrorAction: null);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        public async Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id,
            Action<AppException> onErrorAction)
        {
            await TransactionalExecutionAsync(action, id, onSuccessAction: null, onErrorAction: onErrorAction);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessAction">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        public async Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id,
            Action<TId> onSuccessAction, Action<AppException> onErrorAction)
        {
            // Local function
            Func<TId> Func(TId entity)
            {
                onSuccessAction?.Invoke(entity);
                return null;
            }

            await TransactionalExecutionAsync<object>(action, id, onSuccessFunc: Func, onErrorAction: onErrorAction);
        }


        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessFunc">Méthode à éxecuter en cas de succès</param>
        public async Task<TOut> TransactionalExecutionAsync<TOut>(Action<TId, IDbContextTransaction> action,
            TId id,
            Func<TId, TOut> onSuccessFunc)
        {
            return await this.TransactionalExecutionAsync(action, id, onSuccessFunc: onSuccessFunc,
                onErrorAction: null);
        }

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessFunc">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        public async Task<TOut> TransactionalExecutionAsync<TOut>(Action<TId, IDbContextTransaction> action,
            TId id,
            Func<TId, TOut> onSuccessFunc, Action<AppException> onErrorAction)
        {
            using var transaction = BeginTransaction();
            TOut result = default;
            try
            {
                result = await Task.Run(() =>
                {
                    action(id, transaction);
                    transaction.Commit();
                    return onSuccessFunc != null ? onSuccessFunc.Invoke(id) : default;
                });
            }
            catch (Exception e)
            {
                transaction.Rollback();
                onErrorAction?.Invoke(new EntityFrameworkException(e, id));
            }

            return result;
        }


        #endregion
    }
}