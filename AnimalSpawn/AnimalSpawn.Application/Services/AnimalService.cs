using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSpawn.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _Max_Register_Day = 45;
        public AnimalService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task AddAnimal(Animal animal)
        {
            Expression<Func<Animal, bool>> exprAnimal = item => item.Name == animal.Name;
            var animals = await _unitOfWork.AnimalRepository.FindByCondition(exprAnimal);

            if (animals.Any(item => item.Name == animal.Name))
            {
                throw new Exception("This animal name already exist");
            }
            if (animal?.EstimatedAge != 0 && (animal.Weight <= 0 || animal.Height <= 0)) 
            {
                throw new Exception("The height and weight should be greated than zero");
            }
            var older = DateTime.Now - (animal?.CaptureDate?.Date ?? DateTime.Now);
            if (older.TotalDays > _Max_Register_Day)
            {
                throw new Exception("The animals capture date is older than 45 days");
            }
            if (animal.RfidTag != null)
            {
                Expression<Func<RfidTag, bool>> exprTag = item => item.Tag == animal.RfidTag.Tag;
                var tags = await _unitOfWork.RfidTagRepository.FindByCondition(exprTag);

                if (tags.Any())
                {
                    throw new Exception("This animal tag rfid already exist...");
                }
            }


            await _unitOfWork.AnimalRepository.Add(animal);
        }


        public async Task DeleteAnimal(int id)
        {
            await _unitOfWork.AnimalRepository.Delete(id);
        }

        public async Task<IEnumerable<Animal>> GetAnimals()
        {
            return await _unitOfWork.AnimalRepository.GetAll();
        }

        public async Task<Animal> GetAnimal(int id)
        {
            return await _unitOfWork.AnimalRepository.GetById(id);
        }

        public async Task UpdateAnimal(Animal animal)
        {
            await _unitOfWork.AnimalRepository.Update(animal);
        }
    }
}
