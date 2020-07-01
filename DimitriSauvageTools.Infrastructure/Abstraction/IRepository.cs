using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using DimitriSauvageTools.Domain.Abstractions;
using DimitriSauvageTools.Exceptions;

namespace DimitriSauvageTools.Infrastructure.Abstraction
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : class, IEntity
    {
    }

    public interface IRepository<TEntity, TId>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Obtient la liste de tous les <typeparamref name="TEntity"/>
        /// </summary>
        /// <returns></returns>
        ICollection<TEntity> List();

        /// <summary>
        /// Obtient la liste de tous les <typeparamref name="TEntity"/>
        /// </summary>
        /// <returns></returns>
        Task<ICollection<TEntity>> ListAsync();

        /// <summary>
        /// Obtient un <typeparamref name="TEntity"/> depuis son id
        /// </summary>
        /// <param name="id">Identifiant unique</param>
        /// <returns></returns>
        TEntity GetById(TId id);

        /// <summary>
        /// Obtient un <typeparamref name="TEntity"/> depuis son id
        /// </summary>
        /// <param name="id">Identifiant unique</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TId id);

        /// <summary>
        /// Effectue un select de type <typeparamref name="TEntity"/>, la clause where sera passée en paramètre grâce à un prédicat
        /// </summary>
        /// <param name="predicate">Prédicat de sélection</param>
        /// <returns></returns>
        ICollection<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate,
            ICollection<string> includes = null, bool noTracking = false);

        /// <summary>
        /// Effectue un select de type <typeparamref name="TEntity"/>, la clause where sera passée en paramètre grâce à un prédicat
        /// </summary>
        /// <param name="predicate">Prédicat de sélection</param>
        /// <param name="includes">Liaisons à inclure dans l'objet retourné</param>
        /// <param name="noTracking">Recharge obligatoirement les données de la BDD et ne prend pas en compte les données cache</param>
        /// <returns></returns>
        Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            ICollection<string> includes = null, bool noTracking = false);

        /// <summary>
        /// Effectue un comptage des éléments de type <typeparamref name="TEntity"/>, la clause where sera passée en paramètre grâce à un prédicat
        /// </summary>
        /// <param name="predicate">Prédicat de sélection</param>
        /// <returns>Le nombre d'éléments</returns>
        int CountBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Effectue un comptage des éléments de type <typeparamref name="TEntity"/>, la clause where sera passée en paramètre grâce à un prédicat
        /// </summary>
        /// <param name="predicate">Prédicat de sélection</param>
        /// <returns>Le nombre d'éléments</returns>
        Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Ajout un objet transiant de type <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="entity">Objet transiant</param>
        void Add(TEntity entity);

        /// <summary>
        /// Ajout un objet transiant de type <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="entity">Objet transiant</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Attache une entité de type <typeparamref name="TEntity"/> au contexte actuel
        /// </summary>
        /// <param name="entity"></param>
        void Attach(TEntity entity);

        /// <summary>
        /// Attache une collection d'entités de type <typeparamref name="TEntity"/> au contexte actuel
        /// </summary>
        /// <param name="entities"></param>
        void Attach(IEnumerable<TEntity> entities);

        /// <summary>
        /// Supprime un objet de type <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="entity"></param>
        int Delete(TEntity entity);

        /// <summary>
        /// Supprime un objet de type <typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="entity"></param>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// Marque l'objet de type <typeparamref name="TEntity"/> commme étant modifié
        /// </summary>
        /// <param name="entity"></param>
        void Edit(TEntity entity);

        /// <summary>
        /// Envoi en base de données les modifications
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Envoi en base de données les modifications
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Génère un objet requêtable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Obtient une <see cref="TEntity"/> depuis les propriétés qui composent sa clé unique
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<TEntity> GetByUniqueKeyAsync(TEntity obj);

        /// <summary>
        /// Obtient une <see cref="TEntity"/> depuis les propriétés qui composent sa clé unique
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        TEntity GetByUniqueKey(TEntity obj);

        /// <summary>
        /// Force le rechargement de l'entité passée en paramètre
        /// </summary>
        /// <param name="entity">entité à recharger</param>
        Task ReloadAsync(TEntity entity);

        /// <summary>
        /// Force le rechargement des entités passées en paramètre
        /// </summary>
        /// <param name="entities">entités à recharger</param>
        Task ReloadAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Démarre une nouvelle transaction de base de données
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// Démarre une nouvelle transaction de base de données de manière asynchrone
        /// </summary>
        /// <returns>Transaction</returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Objet</param>
        Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessAction">Méthode à éxecuter en cas de succès</param>
        Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Action<TEntity> onSuccessAction);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Action<AppException> onErrorAction);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessAction">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        Task TransactionalExecutionAsync(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Action<TEntity> onSuccessAction,
            Action<AppException> onErrorAction);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessFunc">Méthode à éxecuter en cas de succès</param>
        Task<TOut> TransactionalExecutionAsync<TOut>(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Func<TEntity, TOut> onSuccessFunc);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="obj">Paramètres</param>
        /// <param name="onSuccessFunc">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        Task<TOut> TransactionalExecutionAsync<TOut>(Action<TEntity, IDbContextTransaction> action, TEntity obj,
            Func<TEntity, TOut> onSuccessFunc, Action<AppException> onErrorAction);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de transaction
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessAction">Méthode à éxecuter en cas de succès</param>
        Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id,
            Action<TId> onSuccessAction);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id,
            Action<AppException> onErrorAction);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessAction">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        Task TransactionalExecutionAsync(Action<TId, IDbContextTransaction> action, TId id, Action<TId> onSuccessAction,
            Action<AppException> onErrorAction);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessFunc">Méthode à éxecuter en cas de succès</param>
        Task<TOut> TransactionalExecutionAsync<TOut>(Action<TId, IDbContextTransaction> action, TId id,
            Func<TId, TOut> onSuccessFunc);

        /// <summary>
        /// Méthode d'execution d'un traitement Entity Framework dans un nouveau contexte de type "Unit of Work" (transactionnel)
        /// </summary>
        /// <param name="action">Méthode à executer</param>
        /// <param name="id">Id</param>
        /// <param name="onSuccessFunc">Méthode à executer en cas de succès</param>
        /// <param name="onErrorAction">Méthode à executer en cas d'erreur</param>
        Task<TOut> TransactionalExecutionAsync<TOut>(Action<TId, IDbContextTransaction> action, TId id,
            Func<TId, TOut> onSuccessFunc, Action<AppException> onErrorAction);
    }
}