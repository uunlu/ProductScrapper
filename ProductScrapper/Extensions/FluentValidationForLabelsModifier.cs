using FluentValidation;
using FluentValidation.Validators;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags.Reflection;

namespace ProductScrapper.Extensions
{
    public class FluentValidationForLabelsModifier : IElementModifier
    {
        private IValidator _validator;

        public bool Matches(ElementRequest token)
        {
            Type modelValidatorType = typeof(IValidator<>).MakeGenericType(token.Accessor.OwnerType);
            _validator = (IValidator)ServiceLocator.Current.GetInstance(modelValidatorType);
            return _validator != null;
        }

        public void Modify(ElementRequest request)
        {
            var descriptor = _validator.CreateDescriptor();
            var propertyValidators = descriptor.GetValidatorsForMember(request.Accessor.Name);
            if (propertyValidators.OfType<NotEmptyValidator>().Any())
                request.CurrentTag.Text(request.CurrentTag.Text() + "*");
        }
    }

}