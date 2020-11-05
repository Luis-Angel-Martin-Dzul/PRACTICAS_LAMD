using AnimalSpawn.Api.Responses;
using AnimalSpawn.Domain.DTOs;
using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalSpawn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _service;
        private readonly IMapper _mapper;

        public AnimalController(IAnimalService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var animal = await _service.GetAnimals();
            var animalsDto = _mapper.Map<IEnumerable<AnimalResponseDto>>(animal);
            var response = new ApiResponse<bool>(true);
            return Ok(animalsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var animal = await _service.GetAnimal(id);
            var animalDto = _mapper.Map<IEnumerable<AnimalResponseDto>>(animal);
            //var response = new ApiResponse<AnimalResponseDto>(animalDto);
            return Ok(animalDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AnimalRequestDto animalDto)
        {
            var animal = _mapper.Map<Animal>(animalDto);
            await _service.AddAnimal(animal);
            var responseDto = _mapper.Map< AnimalRequestDto > (animal);
            return Ok(responseDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAnimal(id);
            var response = new ApiResponse<bool>(true);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, AnimalRequestDto animalDto)
        {
            var animal = _mapper.Map<Animal>(animalDto);
            animal.Id = id;
            animal.UpdateAt = DateTime.Now;
            animal.UpdateBy = 3;
            await _service.UpdateAnimal(animal);
            var response = new ApiResponse<bool>(true);
            return Ok(response);

        }
    }
}