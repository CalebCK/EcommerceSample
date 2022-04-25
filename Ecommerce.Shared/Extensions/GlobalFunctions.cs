using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ecommerce.Shared.Extensions
{
    public static class GlobalFunctions
    {

        public static string GetModelValidationErrors(List<ValidationResult> results)
        {
            string error = "";

            foreach (var item in results)
            {
                error = error + $"{item.ErrorMessage};" + Environment.NewLine;
            }

            return error;
        }

        public static KeyValuePair<bool, List<ValidationResult>> IsModelValid(object model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, validationResults, true);

            return new KeyValuePair<bool, List<ValidationResult>>(isValid, validationResults);
        }

        /// <summary>
        /// IEnumerable extension method that returns the index of the current item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self?.Select((item, index) => (item, index)) ?? new List<(T, int)>();

        public static T[] ConcatArrays<T>(params T[][] p)
        {
            var position = 0;
            var outputArray = new T[p.Sum(a => a.Length)];
            foreach (var curr in p)
            {
                Array.Copy(curr, 0, outputArray, position, curr.Length);
                position += curr.Length;
            }
            return outputArray;
        }

        public static IEnumerable<string> GetSentenceWords(string sentence)
        {
            //get punctuations available in text
            var punctuation = sentence.Where(char.IsPunctuation).Distinct().ToArray();

            //split the words in highlight and trim out punctuations
            var words = sentence.Split().Select(x => x.ToLower().Trim(punctuation));

            return words;
        }
    }
}
