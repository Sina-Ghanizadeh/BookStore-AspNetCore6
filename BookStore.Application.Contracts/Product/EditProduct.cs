namespace BookStore.Application.Contracts.Product
{
    public class EditProduct : CreateProduct
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

    }
}
