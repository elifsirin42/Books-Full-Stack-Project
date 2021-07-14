﻿using AutoMapper;
using Books.Business.DataTransferObjects;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Business.Extensions
{
    public static class Converters
    {
        public static List<PublisherListResponse> ConvertToListResponse(this List<Publisher> publishers, IMapper mapper)
        {
            //var result = new List<PublisherListResponse>();

            //publishers.ForEach(p => result.Add(new PublisherListResponse
            //{
            //    Id = p.Id,
            //    Name = p.Name
            //}));

            //return result;

            return mapper.Map<List<PublisherListResponse>>(publishers);
        }
        public static Publisher ConvertToPublisher(this AddNewPublisherRequest request, IMapper mapper)
        {
            return mapper.Map<Publisher>(request);
        }
        public static PublisherListResponse ConvertFromEntity(this Publisher request, IMapper mapper)
        {
            return mapper.Map<PublisherListResponse>(request);
        }
        public static Publisher ConvertToEntity(this EditPublisherRequest request, IMapper mapper)
        {
            return mapper.Map<Publisher>(request);
        }
    }
}