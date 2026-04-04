using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Portal
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public long? ParentId { get; set; }
        public string IdPath { get; set; } = null!;
        public string SlugPath { get; set; } = null!;
        public int Level { get; set; }
        public int SortOrder { get; set; }
    }

    public class UpdateCategoryDto
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class CategoryResponseDto
    {
        public long CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public long? ParentId { get; set; }
        public string SlugPath { get; set; } = null!;
        public int Level { get; set; }
        public bool IsActive { get; set; }
    }


}
