﻿using MediatR;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Repositories;
using Domain.Entities;
using Domain.Common;
using Application.Use_Cases.Listings.Commands;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateListingCommandHandler : IRequestHandler<UpdateListingCommand, Result<string>>
    {
        private readonly IListingRepository repository;
        private readonly IMapper mapper;

        public UpdateListingCommandHandler(IListingRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
        {
            UpdateListingCommandValidator validator = new UpdateListingCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result<string>.Failure(validationResult.ToString());
            }
            var listing = mapper.Map<Listing>(request);
            var result = await repository.UpdateAsync(listing);
            if (result.IsSuccess)
            {
                return Result<string>.Success("Listing updated successfully");
            }
            else
            {
                return Result<string>.Failure(result.ErrorMessage);
            }
        }
    }
}
