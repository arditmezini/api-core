using System;

namespace AspNetCoreApi.Models
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
