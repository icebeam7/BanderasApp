using System;

using System.Collections.Generic;

namespace BanderasApp.Models
{
    public class Pais
    {
        public int GeonameId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class InfoPaises
    {
        public List<Pais> Geonames { get; set; }
    }
}
