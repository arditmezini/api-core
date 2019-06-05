using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreApi.Models.Dto
{
    public class CountriesDto
    {
        public int Id { get; set; }
        public string Iso { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public int NumCode { get; set; }
        public int PhoneCode { get; set; }
    }
}
