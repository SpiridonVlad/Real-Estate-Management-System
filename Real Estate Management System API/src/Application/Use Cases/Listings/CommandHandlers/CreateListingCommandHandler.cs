﻿using Application.Use_Cases.Commands;
using Application.Use_Cases.Listings.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;


namespace Application.Use_Cases.CommandHandlers
{
    public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, Result<Guid>>
    {
        private readonly IListingRepository repository;
        private readonly IMapper mapper;

        public CreateListingCommandHandler(IListingRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateListingCommand request, CancellationToken cancellationToken)
        {
            CreateListingCommandValidator validator = new CreateListingCommandValidator();
            var validatorResult = validator.Validate(request);
            if(!validatorResult.IsValid)
            {
                return Result<Guid>.Failure(validatorResult.ToString());
            }
            var listing = mapper.Map<Listing>(request);
            var result = await repository.AddAsync(listing);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
}
