using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Interfaces;
using AnimalSpwan.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
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
            var current = await GetAnimal(id);
            _context.Animal.Remove(current);
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
            var current = await GetAnimal(animal.Id);
            current.GenusId = animal.GenusId;
            current.FamilyId = animal.FamilyId;
            current.Description = animal.Description;
            current.EstimatedAge = animal.EstimatedAge;
            current.Gender = animal.Gender;
            current.Height = animal.Height;
            current.Name = animal.Name;
            current.Photo = animal.Photo;
            current.SpeciesId = animal.SpeciesId;
            var rowsUpdate = await _context.SaveChangesAsync();
            return rowsUpdate > 0;
        }
    }
}
