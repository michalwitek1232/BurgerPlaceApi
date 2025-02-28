﻿using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    public partial class OrderProduct
    {
        public uint Id { get; set; }
        public uint OrderId { get; set; }
        public uint? ProductId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Order Order { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
