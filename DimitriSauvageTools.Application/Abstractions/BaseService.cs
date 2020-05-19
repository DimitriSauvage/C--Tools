using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DimitriSauvageTools.Domain.Abstractions;
using DimitriSauvageTools.Infrastructure.Abstraction;

namespace DimitriSauvageTools.Application.Abstractions
{
    public abstract class BaseService<TEntity, TRepository, TDto> : BaseService<TEntity, TRepository, TDto, Guid>
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity, Guid>
        where TDto : BaseDTO
    {
        #region Constructor

        protected BaseService(TRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        #endregion
    }

    public abstract class BaseService<TEntity, TRepository, TDto, TId> : BaseService
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity, TId>
        where TDto : BaseDTO
    {
        #region Fields

        /// <summary>
        /// Data manager
        /// </summary>
        protected TRepository Repository { get; }

        /// <summary>
        /// Mapper
        /// </summary>
        protected IMapper Mapper { get; }

        #endregion

        #region Constructor

        protected BaseService(TRepository repository, IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all <see cref="TEntity"/>
        /// </summary>
        /// <returns>All <see cref="TEntity"/></returns>
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var elements = await this.Repository.ListAsync();
            return elements.Select(x => this.Mapper.Map<TDto>(x));
        }

        /// <summary>
        /// Get the <see cref="TEntity"/> by his identifier
        /// </summary>
        /// <param name="id">Identifier to search</param>
        /// <returns><see cref="TDto"/> of the <see cref="TEntity"/></returns>
        public async Task<TDto> GetByIdAsync(TId id)
        {
            return this.Mapper.Map<TDto>(await this.Repository.GetByIdAsync(id));
        }

        #endregion
    }

    public abstract class BaseService
    {
    }
}