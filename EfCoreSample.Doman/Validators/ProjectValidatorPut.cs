﻿using EfCoreSample.Doman.DTO;
using FluentValidation;

namespace EfCoreSample.Doman.Validators
{
    public class ProjectValidatorPut:ProjectDtoValidatorBase<ProjectPutDto>
    {
        public ProjectValidatorPut()
        {
            RuleFor(project => project.Status).IsInEnum().NotNull().WithMessage("Project status can be " +
                 "either Pending, InProgress, Completed or Cancelled. Plz type in your status. ");
            RuleFor(project => project.Id).GreaterThan(0);

        }
    }
}
