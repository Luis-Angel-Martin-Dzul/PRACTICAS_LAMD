using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Interfaces;
using AnimalSpwan.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalSpwan.Infraestructure.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalSpawnContext _context;
        public AnimalRepository(AnimalSpawnContext context)
        {
            _context = context; 
        }

        public async Task AddAnimal(Animal animal)
        {
            _context.Animal.Add(animal);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAnimal(int id)
        {
            var animal = await GetAnimal(id);
            _context.Animal.Remove(animal);
            var rowsDelete = await _context.SaveChangesAsync();
            return rowsDelete > 0;
        }

        public async Task<Animal> GetAnimal(int id)
        {
            var animal = await _context.Animal.FirstOrDefaultAsync(animal => animal.Id == id);
            return animal;
        }

        public async Task<IEnumerable<Animal>> GetAnimals()
        {
            var animals = await _context.Animal.ToListAsync();
            return animals;
        }

        public async Task<bool> UpdateAnimal(Animal animal)
        {
            var source = await GetAnimal(animal.Id);
            source.GenusId = animal.GenusId;
            source.FamilyId = animal.FamilyId;
            source.Description = animal.Description;
            source.EstimatedAge = animal.EstimatedAge;
            source.Gender = animal.Gender;
            source.Height = animal.Height;
            source.Name = animal.Name;
            source.Photo = animal.Photo;
            source.SpeciesId = animal.SpeciesId;
            source.UpdateAt = DateTime.Now;
            source.UpdatedBy = 5;

            var rowsUpdate = await _context.SaveChangesAsync();
            return rowsUpdate > 0;
        }
    }
}
