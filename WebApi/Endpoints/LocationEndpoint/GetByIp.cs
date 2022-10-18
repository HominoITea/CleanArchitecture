using Ardalis.ApiEndpoints;
using AutoMapper;
using Core.Entities;
using Core.Entities.DTOs;
using Core.Entities.Models;
using Core.Interfaces;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repository;

namespace WebApi.Endpoints.LocationEndpoint
{
    public class GetByIp : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<GetByIpLocationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Location> _geoDataRepository;

        public GetByIp(IAsyncRepository<Location> geoDataRepository, IMapper mapper, BinaryRepository<Location> dataRepository)
        {
            _geoDataRepository = geoDataRepository;
            _mapper = mapper;
        }


        [HttpGet("/loc")]
        [SwaggerOperation(
            Summary = "Creates a new Author",
            Description = "Creates a new Author",
            OperationId = "Author.Create",
            Tags = new[] { "AuthorEndpoint" })]
        public override async Task<ActionResult<GetByIpLocationResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            _geoDataRepository.GetIpRange();

            //var data = _geoDataRepository.GetData<Location>();
            //var header = await _geoDataRepository.GetHeaderAsync();
            //var ipRange = await _geoDataRepository.GetIpRangeAsync();


            //var response = new GetByIpLocationResponse
            //{
            //    IpLocations = new IpLocationDto[data.IpRanges.Length]
            //};
            //for (int i =0; i< data.IpRanges.Length; ++i)
            //{
            //    response.IpLocations[i] = new IpLocationDto
            //    {
            //        IpFrom = data.IpRanges[i].IpFrom,
            //        IpTo = data.IpRanges[i].IpTo,
            //        LocationIndex = data.IpRanges[i].LocationIndex
            //    };
            //} 

            return Ok();//response);
        }

    }
}
