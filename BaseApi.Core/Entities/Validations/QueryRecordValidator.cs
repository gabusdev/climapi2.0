using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climapi.Core.Entities.Validations
{
    internal class QueryRecordValidator : AbstractValidator<QueryRecord>
    {
        public QueryRecordValidator()
        {
            RuleFor(record => record.Query).NotEmpty().MinimumLength(2);
            RuleFor(record => record.Time).GreaterThan(DateTime.Now.AddMinutes(-5)).LessThan(DateTime.Now);
        }
    }
}
