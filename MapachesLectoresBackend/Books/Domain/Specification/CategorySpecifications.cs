﻿using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Books.Domain.Specification
{
    public static class CategorySpecifications
    {
        public sealed class SearchByName(string name) 
            : BaseSpecification<Category>(entity => entity.Description.ToLower().Trim().Contains(name.ToLower().Trim()));

    }
}