using AutoMapper;
using DAL.Data;
using Infrastructure.EFCore;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Services.GenericService
{
    public abstract class GenericService<TEntity, FEntity, UEntity, IEntity> : IGenericService<TEntity, FEntity, UEntity, IEntity>
        where TEntity : class
        where FEntity : class
        where UEntity : class
        where IEntity : class
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public FEntity Find(int id)
        {
            return _mapper.Map<FEntity>(_repository.GetByID(id));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Update(UEntity dtoToUpdate)
        {
            _repository.Update(_mapper.Map<TEntity>(dtoToUpdate));
        }

        public void Insert(IEntity dtoToInsert)
        {
            _repository.Insert(_mapper.Map<TEntity>(dtoToInsert));
        }
    }
}
