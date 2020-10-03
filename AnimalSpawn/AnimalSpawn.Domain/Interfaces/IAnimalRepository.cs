using AnimalSpawn.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalSpawn.Domain.Interfaces
{
    public interface IAnimalRepository
    {
        public Task<IEnumerable<Animal>> GetAnimals();
        public Task<Animal> GetAnimal(int id);
        public Task AddAnimal(Animal animal);
        public Task<bool> UpdateAnimal(Animal animal);
        public Task<bool> DeleteAnimal(int id);
    }
}
