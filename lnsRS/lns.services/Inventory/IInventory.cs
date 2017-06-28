using System;

namespace lns.services.Inventory
{
    public interface IInventory
    {
        int Id { get; set; }
        string Name_en { get; set; }
        string Description_en { get; set; }
        string Name_ru { get; set; }
        string Description_ru { get; set; }
        decimal? Price { get; set; }
        Boolean IsActive { get; set; }
        byte[] Image { get; set; }
        string ImageName { get; set; }
    }
}
