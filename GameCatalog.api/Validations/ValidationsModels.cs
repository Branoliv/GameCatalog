using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameCatalog.api.Validations
{
    public class ValidationsModels
    {
        public ValidationsModels(ModelStateDictionary errors)
        {
            Errors = GetErrors(errors);
        }

        public IEnumerable<ErrorModel> Errors { get; set; }
        private IEnumerable<ErrorModel> GetErrors(ModelStateDictionary keyValues)
        {
            var ls = new List<ErrorModel>();

            foreach (var item in keyValues)
            {
                var ch = item.Key.ToString();
                var er = item.Value.Errors.Select(s => s.ErrorMessage);

                var list = new List<string>();

                foreach (var erro in er)
                {
                    list.Add(erro);
                }

                ls.Add(new ErrorModel(ch, list));
            }

            return ls;
        }
        
        public struct ErrorModel
        {
            public ErrorModel(string key, List<string> messages)
            {
                Key = key;
                Messages = messages;
            }

            public string Key { get; private set; }
            public IEnumerable<string> Messages { get; private set; }
        }
    }
}
