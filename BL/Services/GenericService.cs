﻿using AutoMapper;
using Infrastructure.Repository;

namespace BL.Services
{
    public abstract class GenericService<TEntity, FEntity, UEntity>
        where TEntity : class
        where FEntity : class
        where UEntity : class
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private IRepository<TEntity> repository;

        public GenericService(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public FEntity find(int id)
        {
            return mapper.Map<FEntity>(repository.GetByID(id));
        }

        public void delete(int id)
        {
            repository.Delete(id);
        }

        public void update(UEntity dtoToUpdate)
        {
            repository.Update(mapper.Map<TEntity>(dtoToUpdate));
        }
    }
}
