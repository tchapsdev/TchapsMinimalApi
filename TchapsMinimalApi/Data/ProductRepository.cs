namespace TchapsMinimalApi.Data
{
    public class ProductRepository
    {
        private readonly ProductDatabase _productDatabase;
        public ProductRepository(ProductDatabase productDatabase)
        {
            _productDatabase = productDatabase;
        }

        public List<Model.Product> GetProducts() => _productDatabase.Products.ToList();

        public Model.Product? GetProduct(int id) => _productDatabase.Products.FirstOrDefault(p => p.Id == id);

        public void AddProduct(Model.Product product)
        {
            _productDatabase.Products.Add(product);
            _productDatabase.SaveChanges();
        }

        public void UpdateProduct(Model.Product product)
        {
            var existingProduct = GetProduct(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.Category = product.Category;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.IsAvailable = product.IsAvailable;
            }
            _productDatabase.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                _productDatabase.Products.Remove(product);
                _productDatabase.SaveChanges();
            }
        }

    }
}
