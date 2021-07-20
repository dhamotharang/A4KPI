﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using A4KPI.Constants;
using A4KPI.Data;
using A4KPI.DTO;
using A4KPI.Helpers;
using A4KPI.Models;
using A4KPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace A4KPI.Services
{
    public interface ICommentService: IServiceBase<Comment, CommentDto>
    {
        Task<List<CommentDto>> GetAllByObjectiveId(int objectiveId);
        Task<CommentDto> GetFisrtByAccountId(int accountId, int periodTypeId, int period, string scoreType);
        Task<object> GetFunctionalLeaderCommentByAccountId(int accountId, int periodTypeId, int period);
        Task<object> GetL1CommentByAccountId(int accountId, int periodTypeId, int period);
        Task<object> GetGHRCommentByAccountId(int accountId, int periodTypeId, int period);
        Task<object> GetL0SelfEvaluationCommentByAccountId(int accountId, int periodTypeId, int period);
        Task<object> GetL1SelfEvaluationCommentByAccountId(int accountId, int periodTypeId, int period);
        Task<object> GetL2SelfEvaluationCommentByAccountId(int accountId, int periodTypeId, int period);

    }
    public class CommentService : ServiceBase<Comment, CommentDto>, ICommentService
    {
        private OperationResult operationResult;

        private readonly IRepositoryBase<Comment> _repo;
        private readonly IRepositoryBase<Account> _repoAccount;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MapperConfiguration _configMapper;
        public CommentService(
            IRepositoryBase<Comment> repo, 
            IRepositoryBase<Account> repoAccount,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            MapperConfiguration configMapper
            )
            : base(repo, unitOfWork, mapper,  configMapper)
        {
            _repo = repo;
            _repoAccount = repoAccount;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configMapper = configMapper;
        }
       
        public async Task<CommentDto> GetFisrtByAccountId(int accountId, int periodTypeId, int period, string scoreType)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            int scoreBy = JWTExtensions.GetDecodeTokenById(accessToken);

            return await _repo.FindAll(x =>
                                        x.ScoreType == scoreType
                                        && x.PeriodTypeId == periodTypeId
                                        && x.CreatedTime.Year == DateTime.Today.Year
                                        && x.Period == period
                                        && x.CommentTypeId == CommentType.Comment
                                        && accountId == x.AccountId
                                        && scoreBy == x.CreatedBy
                                        && x.AccountId != scoreBy)
                                    .ProjectTo<CommentDto>(_configMapper)
                                    .FirstOrDefaultAsync();
        }
        public async Task<List<CommentDto>> GetAllByObjectiveId(int objectiveId)
        {
            var currrentQuarter = (DateTime.Now.Month + 2) / 3;
            return await _repo.FindAll(x => x.Period == currrentQuarter).ProjectTo<CommentDto>(_configMapper).ToListAsync();
        }
        /// <summary>
        /// Chỉnh sửa thành vừa cập nhật vừa thêm mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<OperationResult> AddAsync(CommentDto model)
        {
            if (model.Id > 0)
            {
                var item = await _repo.FindAll(x => x.Id == model.Id && x.CreatedBy == model.CreatedBy).AsNoTracking().FirstOrDefaultAsync();
                item.Content = model.Content;
                _repo.Update(item);
            }
            else
            {
                var itemList = _mapper.Map<Comment>(model);
                _repo.Add(itemList);
            }
            try
            {
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        public  async Task<object> GetFunctionalLeaderCommentByAccountId(int accountId, int periodTypeId, int period)
        {
            var l1 = await _repoAccount.FindAll(x => x.Id == accountId).FirstOrDefaultAsync();

            if (l1 == null) return new CommentDto();
            return await _repo.FindAll(x =>
                                        x.ScoreType == ScoreType.FunctionalLeader
                                        && x.CommentTypeId == CommentType.Comment
                                        && x.PeriodTypeId == periodTypeId
                                        && x.CreatedTime.Year == DateTime.Today.Year
                                        && x.Period == period
                                        && x.CreatedBy == l1.Leader
                                        && accountId == x.AccountId
                                        && x.AccountId != x.CreatedBy)
                                    .ProjectTo<CommentDto>(_configMapper)
                                    .FirstOrDefaultAsync();
        }
       
        public async Task<object> GetGHRCommentByAccountId(int accountId, int periodTypeId, int period)
        {
            return await _repo.FindAll(x =>
                                       x.ScoreType == ScoreType.GHR
                                       && x.CommentTypeId == CommentType.Comment
                                       && x.PeriodTypeId == periodTypeId
                                       && x.CreatedTime.Year == DateTime.Today.Year
                                       && x.Period == period
                                       && accountId == x.AccountId
                                       && x.AccountId != x.CreatedBy)
                                   .ProjectTo<CommentDto>(_configMapper)
                                   .FirstOrDefaultAsync();
        }

        public async Task<object> GetL1CommentByAccountId(int accountId, int periodTypeId, int period)
        {
            var l1 = await _repoAccount.FindAll(x => x.Id == accountId).FirstOrDefaultAsync();

            if (l1 == null) return new CommentDto();
            return await _repo.FindAll(x =>
                                       x.PeriodTypeId == periodTypeId
                                       && x.CreatedTime.Year == DateTime.Today.Year
                                       && x.Period == period
                                       && x.ScoreType == ScoreType.L1
                                       && x.CommentTypeId == CommentType.Comment
                                       && accountId == x.AccountId
                                       && x.CreatedBy == l1.Manager)
                                   .ProjectTo<CommentDto>(_configMapper)
                                   .FirstOrDefaultAsync();
        }

        public async Task<object> GetL0SelfEvaluationCommentByAccountId(int accountId, int periodTypeId, int period)
        {
            var l0 = await _repoAccount.FindAll(x => x.Id == accountId).FirstOrDefaultAsync();

            if (l0 == null) return new CommentDto();
            return await _repo.FindAll(x =>
                                       x.PeriodTypeId == periodTypeId
                                       && x.CreatedTime.Year == DateTime.Today.Year
                                       && x.Period == period
                                       && x.ScoreType == ScoreType.L0
                                       && x.CommentTypeId == CommentType.SelfEvaluation
                                       && accountId == x.AccountId
                                       && x.CreatedBy == accountId)
                                   .ProjectTo<CommentDto>(_configMapper)
                                   .FirstOrDefaultAsync();
        }
        public async Task<object> GetL1SelfEvaluationCommentByAccountId(int accountId, int periodTypeId, int period)
        {
            var l0 = await _repoAccount.FindAll(x => x.Id == accountId).FirstOrDefaultAsync();

            if (l0 == null) return new CommentDto();
            return await _repo.FindAll(x =>
                                       x.PeriodTypeId == periodTypeId
                                       && x.CreatedTime.Year == DateTime.Today.Year
                                       && x.Period == period
                                       && x.ScoreType == ScoreType.L0
                                       && x.CommentTypeId == CommentType.SelfEvaluation
                                       && accountId == x.AccountId
                                       && x.CreatedBy == accountId)
                                   .ProjectTo<CommentDto>(_configMapper)
                                   .FirstOrDefaultAsync();
        }
        public async Task<object> GetL2SelfEvaluationCommentByAccountId(int accountId, int periodTypeId, int period)
        {
            var l0 = await _repoAccount.FindAll(x => x.Id == accountId).FirstOrDefaultAsync();

            if (l0 == null) return new CommentDto();
            return await _repo.FindAll(x =>
                                       x.PeriodTypeId == periodTypeId
                                       && x.CreatedTime.Year == DateTime.Today.Year
                                       && x.Period == period
                                       && x.ScoreType == ScoreType.L0
                                       && x.CommentTypeId == CommentType.SelfEvaluation
                                       && accountId == x.AccountId
                                       && x.CreatedBy == accountId)
                                   .ProjectTo<CommentDto>(_configMapper)
                                   .FirstOrDefaultAsync();
        }
    }
}
