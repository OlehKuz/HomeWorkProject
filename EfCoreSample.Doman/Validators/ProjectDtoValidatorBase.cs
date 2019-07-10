using System;
using System.Collections.Generic;
using System.Text;
using EfCoreSample.Doman.DTO;
using FluentValidation;

namespace EfCoreSample.Doman.Validators
{
    public class ProjectDtoValidatorBase<TEntity>:AbstractValidator<ProjectPutDto> 
        where TEntity: ProjectDtoBase
    {
        public ProjectDtoValidatorBase()
        {
            RuleFor(project => project.Title).NotNull().Length(1, 50);
            RuleFor(project => project.Description).Length(0, 255);
           
            RuleFor(project => project.StartTime)
                .Must(BeAValidDate).WithMessage("Start date of DateTime format is required. ") ;
            RuleFor(project => project.EndTime)
                .Must(BeAValidDate).WithMessage("End date of DateTime format is required. ")
                .GreaterThan(project=>project.StartTime).WithMessage("Project End date should be later than " +
                "its Start date. ");
            RuleFor(project => project.LastUpdated)
                .Must(BeAValidDate).WithMessage("DateTime format is required. ")
                .LessThan(project => DateTime.UtcNow).WithMessage("Last update can't be done later than right now");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
