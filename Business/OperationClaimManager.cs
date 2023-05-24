using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess;
using Entities;

namespace Business
{
	public class OperationClaimManager: IOperationClaimService
	{
        private readonly IOperationClaimDal _operationClaimDal;

		public OperationClaimManager(IOperationClaimDal operationClaimDal)
		{
            _operationClaimDal = operationClaimDal;
		}

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameAvailableForAdd(operationClaim.Name));
            if (result !=null)
            {
                return result;
            }
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.Added);
        }


        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameAvailableForUpdate(operationClaim));
            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.Deleted);
        }


        [SecuredAspect()]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            var result = _operationClaimDal.GetAll();
            return new SuccessDataResult<List<OperationClaim>>(result, Messages.Listed);
        }

        public IDataResult<OperationClaim> GetById(int Id)
        {
            var result = _operationClaimDal.Get(p=>p.Id == Id);
            return new SuccessDataResult<OperationClaim>(result, Messages.Listed);
        }

        private IResult IsNameAvailableForAdd(string name)
        {
            var result = _operationClaimDal.Get(p=>p.Name == name);
            if (result != null)
            {
                return new ErrorResult(Messages.NameIsNotAvailable);
            }
            return new SuccessResult(Messages.NameExist);
        }

        private IResult IsNameAvailableForUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim = _operationClaimDal.Get(p => p.Id == operationClaim.Id);
            if (currentOperationClaim.Name != operationClaim.Name)
            {
                var result = _operationClaimDal.Get(p => p.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(Messages.NameIsNotAvailable);
                }
            }
           
            return new SuccessResult(Messages.NameExist);
        }
    }
}

