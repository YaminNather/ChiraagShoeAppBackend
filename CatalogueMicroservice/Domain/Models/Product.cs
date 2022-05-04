namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models
{
    public class Product : IEntity<Product>
    {
        public Product(
            string Id, 
            string Name,
            string Seller,
            string Description, 
            Category? Category,
            float InitialPrice, 
            DateTime CreatedAt,
            DateTime ModifiedAt, 
            string MainImage, 
            List<string>? Images,
            bool IsAvailable
        )
        {
            this.Id = Id;
            this.Name = Name;
            this.Seller = Seller;
            this.Description = Description;
            this.Category = Category;
            this.InitialPrice = InitialPrice;
            this.CreatedAt = CreatedAt;
            this.ModifiedAt = ModifiedAt;
            this.MainImage = MainImage;
            this.Images = (Images != null) ? new List<string>(Images) : null;
            this.IsAvailable = IsAvailable;
        }

        public bool Duplicate()
        {
            AddProductOptions addProductOptions = new AddProductOptions(
                Name: productToClone.Name,
                Seller: request.Seller,
                Description: productToClone.Description,
                InitialPrice: request.InitialPrice,
                MainImage: productToClone.MainImage,
                Images: productToClone.Images
            );

            Add
        }

        public bool IsSame(Product other) => Id == other.Id;

        public string Id { get; }

        public string Name { get; }

        public string Seller { get; }
        
        public string Description { get; }

        public Category? Category { get; }
        
        public float InitialPrice { get; }

        public DateTime CreatedAt { get; }
        
        public DateTime ModifiedAt { get; }

        public string MainImage { get; }

        public List<string>? Images { get; }

        public bool IsAvailable { get; }
    }
}