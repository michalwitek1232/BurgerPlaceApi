﻿using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table to store products in set
    /// </summary>
    public partial class SetProduct
    {
        public uint Id { get; set; }
        public uint? SetId { get; set; }
        public uint? ProductId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Product? Product { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Set? Set { get; set; }
    }
}
