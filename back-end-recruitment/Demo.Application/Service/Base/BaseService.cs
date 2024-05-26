using AutoMapper;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;

namespace Demo.Application.Service.Base
{
    public abstract class BaseService
    {
        protected IMapper _mapper;

        protected BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected string GenerateSlug(string input)
        {
            string slug = RemoveDiacritics(input);

            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "", RegexOptions.IgnoreCase);
            slug = slug.ToLower().Replace(" ", "-");

            slug = Regex.Replace(slug, @"-+", "-");

            slug = slug.Trim('-');

            return slug;
        }
        protected string RemoveDiacritics(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(normalizedString[i]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
