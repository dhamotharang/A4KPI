﻿using AutoMapper;
using A4KPI.Data;
using A4KPI.DTO;
using A4KPI.Models;
using A4KPI.Services.Base;

namespace A4KPI.Services
{
    public interface IAccountTypeService: IServiceBase<AccountType, AccountTypeDto>
    {
    }
    public class AccountTypeService : ServiceBase<AccountType, AccountTypeDto>, IAccountTypeService
    {
        private readonly IRepositoryBase<AccountType> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;

        public AccountTypeService(
            IRepositoryBase<AccountType> repo,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            MapperConfiguration configMapper
            )
            : base(repo, unitOfWork, mapper, configMapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }
    }
}
