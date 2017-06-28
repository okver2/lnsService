using System;

namespace lns.services.Inventory
{
    public class Inventory : IInventory
    {
        public int Id { get; set; }
        public string Name_en { get; set; }
        public string Description_en { get; set; }
        public string Name_ru { get; set; }
        public string Description_ru { get; set; }
        public decimal? Price { get; set; }
        public Boolean IsActive { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
    }
}