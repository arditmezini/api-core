using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspNetCoreApi.Models.Dto
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BookCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookDto> Books { get; set; }
    }
}
