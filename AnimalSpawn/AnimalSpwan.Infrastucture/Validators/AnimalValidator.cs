using AnimalSpawn.Domain.DTOs;
using FluentValidation;
using System;

namespace AnimalSpwan.Infraestructure.Validators
{
    public class AnimalValidator : AbstractValidator<AnimalRequestDto>
    {
        public AnimalValidator()
        {
            RuleFor(animal => animal.Name)
                .NotNull()
                .Length(3, 50);
            RuleFor(animal => animal.CaptureDate)
                .LessThan(DateTime.Now);
            RuleFor(animal => animal.CaptureCondition)
                .NotNull()
                .Length(4, 200);
        }
    }
}
