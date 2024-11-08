﻿using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetPaginatedListingsQuery: IRequest<Result<IEnumerable<ListingDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
