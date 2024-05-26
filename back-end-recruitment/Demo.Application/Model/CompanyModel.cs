using Demo.Application.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Model
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeCount { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        public long? ImageId { get; set; }
        public long? PreviewImageId { get; set; }
        public string? PreviewImgUrl { get; set; }
        public string? ImgUrl { get; set; }
        public bool Deleted { get; set; }
        public string? Slug { get; set; }
    }
}
