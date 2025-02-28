﻿using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table for storing categories of products
    /// </summary>
    public partial class ProductCategory
    {
        public uint Id { get; set; }
        public uint? ProductId { get; set; }
        public uint? CategoryId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Category? Category { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
