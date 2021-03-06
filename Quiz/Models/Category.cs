﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Quiz.Models
{
    public class Category
    {
        [ScaffoldColumn(false)]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Question> Question { get; set; }
        public virtual ICollection<UserCategories> Users { get; set; }
    }
}