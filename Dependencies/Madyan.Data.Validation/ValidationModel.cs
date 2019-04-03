using FluentValidation;
using System;

namespace Madyan.Data.Validation
{
    public class BasicValidator : AbstractValidator<Basic>
    {
        public BasicValidator()
        {
            //RuleFor(x => x.BasicID).NotNull();
            RuleFor(x => x.PasNumber).NotNull();//.Length(0, 10);
            RuleFor(x => x.DateOfBirth).InclusiveBetween(DateTime.Now.AddYears(-100), DateTime.Now);// ..EmailAddress();
            RuleFor(x => x.Surname).NotNull();
            
        }
    }
    public class NextOfKinValidator : AbstractValidator<NextOfKin>
    {
        public NextOfKinValidator()
        {
            //RuleFor(x => x.BasicID).NotNull();
            RuleFor(x => x.NokName).NotNull();//.Length(0, 10);
            RuleFor(x => x.NokRelationshipCode).NotNull();
            
        }
    }
    public class GpDetailValidator : AbstractValidator<GpDetail>
    {
        public GpDetailValidator()
        {
            //RuleFor(x => x.BasicID).NotNull();
            RuleFor(x => x.GpCode).NotNull();//.Length(0, 10);
            RuleFor(x => x.GpPhone).NotNull();

        }
    }
    
}
