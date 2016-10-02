//using FluentValidation;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProductScrapper.AppServices.Decorators
//{
//    public class ValidatorHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
//   where TRequest : IRequest<TResponse>
//   where TResponse : IResponse
//    {
//        protected readonly IRequestHandler<TRequest, TResponse> _inner;
//        protected readonly IEnumerable<IValidator<TRequest>> _validators;

//        public ValidatorHandler(
//            IRootRequestHandler<TRequest, TResponse> inner,
//            IEnumerable<IValidator<TRequest>> validators)
//        {
//            _inner = inner;
//            _validators = validators;
//        }

//        public async Task<TResponse> Handle(IRequest<TResponse> request)
//        {
//            var validationFailures = Validate(request);
//            if (validationFailures.Any())
//            {
//                var errors = validationFailures
//                    .Select(x => new ResultErrorMessage(x.PropertyName, x.ErrorMessage, x.AttemptedValue));
//                var result = new Result<TResponse>
//                {
//                    Exception = new ResultInvalidException
//                    {
//                        Errors = errors
//                    }
//                };
//                return await Task.FromResult(result);
//            }

//            try
//            {
//                return await _inner.Handle(request);
//            }
//            catch (Exception ex)
//            {
                
//            }
//        }

//        private IEnumerable<ValidationFailure> Validate(IRequest<TResponse> request)
//        {
//            var context = new ValidationContext(request);
//            var failures = _validators
//                .Select(validator => validator.Validate(context))
//                .SelectMany(result => result.Errors);
//            return failures;
//        }
//    }
//}
