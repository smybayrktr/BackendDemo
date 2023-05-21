using System;
using System.ComponentModel.DataAnnotations;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess;
using DataAccess.EntityFramework;
using Entities;

namespace Business
{
	public class UserOperationClaimManager: IUserOperationClaimService
	{
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;


        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal,
            IOperationClaimService operationClaimService, IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
        }

        
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(
                IsUserExist(userOperationClaim.UserId),
                IsOperationSetAvailable(userOperationClaim),
                IsOperationSetExistForAdd(userOperationClaim));
            if (result != null)
            {
                return result;
            }
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            var result = _userOperationClaimDal.GetAll();
            return new SuccessDataResult<List<UserOperationClaim>>(result, Messages.Listed);
        }

        public IDataResult<UserOperationClaim> GetById(int Id)
        {
            var result = _userOperationClaimDal.Get(p=>p.Id == Id);
            return new SuccessDataResult<UserOperationClaim>(result, Messages.Listed);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(
                IsUserExist(userOperationClaim.UserId),
                IsOperationSetAvailable(userOperationClaim),
                IsOperationSetExistForUpdate(userOperationClaim));
            if (result != null)
            {
                return result;
            }
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.Updated);
        }

        private IResult IsOperationSetAvailable(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(p=>p.UserId == userOperationClaim.UserId
                                                &&
                                                p.OperationClaimId == userOperationClaim.OperationClaimId);

            if (result != null)
            {
                return new ErrorResult(Messages.UserAndClaimAvailable);
            }
            return new SuccessResult();
        }

        public IResult IsOperationSetExistForAdd(UserOperationClaim userOperationClaim)
        {
            var result =_userOperationClaimDal.Get(p => p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
            if (result != null)
            {
                return new ErrorResult(Messages.OperationClaimNotExist);
            }
            return new SuccessResult();
        }

        private IResult IsOperationSetExistForUpdate(UserOperationClaim userOperationClaim)
        {
            var currentUserOperationClaim =  _userOperationClaimDal.Get(p => p.Id == userOperationClaim.Id);
            if (currentUserOperationClaim.UserId != userOperationClaim.UserId || currentUserOperationClaim.OperationClaimId != userOperationClaim.OperationClaimId)
            {
                var result = _userOperationClaimDal.Get(p => p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
                if (result != null)
                {
                    return new ErrorResult(Messages.OperationClaimNotExist);
                }
            }
            return new SuccessResult();
        }
        private IResult IsUserExist(int userId)
        {
            var result = _userService.GetById(userId).Data;

            if (result == null)
            {
                return new ErrorResult(Messages.UserNotExist);
            }
            return new SuccessResult();
        }
    }
}

