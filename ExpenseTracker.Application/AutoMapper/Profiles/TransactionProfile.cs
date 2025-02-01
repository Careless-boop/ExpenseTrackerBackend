using AutoMapper;
using ExpenseTracker.Application.DTOs.Transaction;
using ExpenseTracker.Application.Mediator.Transactions.Commands;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.AutoMapper.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ReverseMap();

            CreateMap<Transaction, CreateTransactionCommand>()
                .ReverseMap();

            CreateMap<Transaction, CreateTransactionDto>()
                .ReverseMap();
        }
    }
}
