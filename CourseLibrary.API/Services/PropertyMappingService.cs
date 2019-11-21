using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _authorPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" }) },
                { "MainCategory", new PropertyMappingValue(new List<string>() { "MainCategory" }) },
                { "Age", new PropertyMappingValue(new List<string>() { "DateOfBirth" }) },
                { "Name", new PropertyMappingValue(new List<string>() { "FirstName", "LastName" }) },
            };

        private IList<IPropertyMapping> _propertyMapping = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMapping.Add(new PropertyMapping<AuthorDto, Author>(_authorPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            var matchingMapping = _propertyMapping
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                $"for <{typeof(TSource)}, {typeof(TDestination)}");
        }

    }
}
