using AnimalSpawn.Domain.DTOs;
using AnimalSpawn.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalSpwan.Infraestructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Animal, AnimalResponseDto>();
            CreateMap<Animal, AnimalRequestDto>();
            CreateMap<AnimalRequestDto, Animal>().AfterMap((src, dest)=> {
                dest.CreateAt = DateTime.Now;
                dest.CreatedBy = 3;
                dest.Status = true;
            });
            CreateMap<AnimalResponseDto, Animal>();
        }
    }
}
