using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace AzureSearchStarter.Models
{
    public class SuperHeroModel
    {
        public string SuperHeroModelID { get; set; }

        [DisplayName("Super Hero Name")]
        public string SuperHeroName { get; set; }

        [DisplayName("Real Name")]
        public string RealName { get; set; }

        [DisplayName("Powers & Abilities")]
        public string PowersAndAbilities { get; set; }
    }
}