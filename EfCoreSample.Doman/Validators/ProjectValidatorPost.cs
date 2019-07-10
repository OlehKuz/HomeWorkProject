using System;
using System.Collections.Generic;
using System.Text;
using EfCoreSample.Doman.DTO;
using FluentValidation;

namespace EfCoreSample.Doman.Validators
{
    public class ProjectValidatorPost : ProjectDtoValidatorBase<ProjectPostDto> 
    {
        public ProjectValidatorPost()
        {
            RuleFor(project => project.Status).IsInEnum().NotNull().WithMessage("Project status can be " +
                 "either Pending, InProgress, Completed or Cancelled. Plz type in your status. ");

        }
    }
}

