using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.Enum;
using Demo.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Service
{
    public class TransactionService: BaseService, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository) : base(mapper)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> CreateAsync(TransactionModel model)
        {
            var transaction = _mapper.Map<Transaction>(model);
            var isSuccess = await _transactionRepository.CreateAsync(transaction);
            if (isSuccess)
            {
                return true;
            }

            return false;
        }

        public async Task<DataObject<UserTransactionModel>> GetPagingAsync(BaseFilter page)
        {
            var total = 0;
            var userTransactions = await _transactionRepository.GetPagingAsync(page.PageIndex, page.PageSize, page.KeyWord);
            var userTransactionModels = _mapper.Map<List<UserTransactionModel>>(userTransactions);
            if (page.KeyWord == null)
            {
                total = await _transactionRepository.GetTotalTransaction();
            }
            else
            {
                total = await _transactionRepository.GetTotalTransactionByKeyword(page.KeyWord);
            }
            return new DataObject<UserTransactionModel>(userTransactionModels, total);
        }

        public async Task<bool> Delete(int id)
        {
            var transactoin = await _transactionRepository.GetByIdAsync(id);
            if (transactoin == null)
            {
                return false;
            }
            var isSuccessed = await _transactionRepository.DeleteAsync(transactoin);
            return isSuccessed;
        }

        public async Task<List<TransactionModel>?> GetAllAsync()
        {
            var transaction = await _transactionRepository.GetAllAsync();
            return _mapper.Map<List<TransactionModel>>(transaction);
        }

        public async Task<bool> UpdateAsync(int id, TransactionModel model)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                return false;
            }

            transaction.Fee = model.Fee;
            transaction.Message = model.Message;
            transaction.Status = (TransactionStatus)model.Status;

            var isSuccessed = await _transactionRepository.UpdateAsync(transaction);

            return isSuccessed;
        }
    }
}
