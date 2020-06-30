using FluentValidation;
using System.Linq;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public static class BaseValidator
    {
        public static IRuleBuilderOptions<T, TProperty> PropertyNotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().WithMessage("{PropertyName} cannot be blank.");
        }

        public static IRuleBuilderOptions<T, int> PropertyInclusiveBetween<T>(this IRuleBuilder<T, int> ruleBuilder, int from, int to)
        {
            return ruleBuilder.InclusiveBetween(from, to).WithMessage("{PropertyName} should be between" + $" {from} and {to}");
        }

        public static IRuleBuilderOptions<T, string> PropertyLength<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
        {
            return ruleBuilder.Length(min, max).WithMessage("{PropertyName} length should be between" + $" {min} and {max} chars");
        }

        public static IRuleBuilderOptions<T, string> PropertyAllLetter<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsAllLetter).WithMessage("{PropertyName} should be all letters.");
        }

        public static IRuleBuilderOptions<T, string> PropertyEmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.EmailAddress().WithMessage("{PropertyName} is not in valid format.");
        }

        private static bool IsAllLetter(string property)
        {
            return property.All(char.IsLetter);
        }
    }
}