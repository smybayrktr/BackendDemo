using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess;
using Entities;

namespace Business
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(OperationClaimValidator))]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public async Task<IResult> Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(await IsNameExistForAdd(operationClaim.Name));
            if (result != null)
            {
                return result;
            }

            await _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(OperationClaimValidator))]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public async Task<IResult> Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(await IsNameExistForUpdate(operationClaim));
            if (result != null)
            {
                return result;
            }

            await _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredAspect()]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public async Task<IResult> Delete(OperationClaim operationClaim)
        {
            await _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.Deleted);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<OperationClaim>>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(await _operationClaimDal.GetAll());
        }

        public async Task<IDataResult<OperationClaim>> GetById(int id)
        {
            var result = await _operationClaimDal.Get(p => p.Id == id);
            return new SuccessDataResult<OperationClaim>(result);
        }

        public async Task<OperationClaim> GetByIdForUserService(int id)
        {
            var result = await _operationClaimDal.Get(p => p.Id == id);
            return result;
        }

        private async Task<IResult> IsNameExistForAdd(string name)
        {
            var result = await _operationClaimDal.Get(p => p.Name == name);
            if (result != null)
            {
                return new ErrorResult(Messages.NameExist);
            }
            return new SuccessResult();
        }

        private async Task<IResult> IsNameExistForUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim = await _operationClaimDal.Get(p => p.Id == operationClaim.Id);
            if (currentOperationClaim.Name != operationClaim.Name)
            {
                var result = await _operationClaimDal.Get(p => p.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(Messages.NameExist);
                }
            }
            return new SuccessResult();
        }
    }
}

