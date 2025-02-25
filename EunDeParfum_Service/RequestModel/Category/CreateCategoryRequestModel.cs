using System;
using System.ComponentModel.DataAnnotations;

namespace EunDeParfum_Service.RequestModel.Category
{
    public class CreateCategoryRequestModel
    {
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(255, ErrorMessage = "Category name cannot exceed 255 characters")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
    }
}
