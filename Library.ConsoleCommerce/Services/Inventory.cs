using Library.ConsoleCommerce.Models;
using Newtonsoft.Json;
namespace Library.ConsoleCommerce.Services
{
    internal class Inventory
    {
        private List<Product> productsList;
        public List<Product> Products { 
            get 
            { 
                return productsList; 
            } 
        }
        public Inventory() {
            productsList = new List<Product>();
        }
        // sets up id
        private int NextId
        {
            get
            {
                if (!productsList.Any())
                {
                    return 1;
                }

                return productsList.Select(t => t.Id).Max() + 1;
            }
        }
        
        // add a product passed in
        public void AddProduct(Product _prod) {
            _prod.Id = NextId;
            productsList.Add(_prod);
        }

        // add stock to a product
        public void AddStock(int _id, int _amount) {
            foreach (var item in productsList)
            {
                if (item.Id == _id)
                {
                    item.Quantity += _amount;
                    return;
                }
            }
        }

        // create a product and add it
        public void CreateProduct()
        {
            Console.WriteLine("Enter the product name -> ");
            var _name = Console.ReadLine() ?? "def name";
            Console.WriteLine("Enter the product description -> ");
            var _description = Console.ReadLine() ?? "def descr";
            Console.WriteLine("Enter the product price -> ");
            var _price = double.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Enter the product quantity -> ");
            var _quantity = int.Parse(Console.ReadLine() ?? "0");

            AddProduct(new Product (_name, _description, _price, _quantity));  
        }

        public void Display()
        {
            foreach (var item in productsList)
            {
                Console.WriteLine($"{item}");
            }
        }

        // edit a product
        public void Edit(int _id, string _name, string _description)
        {
            var editProduct = productsList.FirstOrDefault(p => p.Id == _id);
            
            if (editProduct == null)
                return;

            editProduct.Name = _name;
            editProduct.Description = _description;

        }

        // search by name
        public void SearchName(string search)
        {
            foreach (Product product in productsList)
            {
                if (product.Name == search)
                {
                    Console.WriteLine($"Found item! -> {product}");
                    return;
                }
            }

            Console.WriteLine("Item not found.");
            return;
        }
        // search by description
        public void SearchDescription(string search)
        {
            foreach (Product product in productsList)
            {
                if (product.Description == search)
                {
                    Console.WriteLine($"Found item! -> {product}");
                    return;
                }
            }

            Console.WriteLine("Item not found.");
            return;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(productsList);
            File.WriteAllText("InvData.json", json);
        }
        public void Load()
        {
            var json = File.ReadAllText("InvData.json");
            productsList = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
        }
        public override string ToString()
        {
            string temp = "";

            foreach (Product prod in productsList)
            {
                temp += prod.ToString();
                temp += '\n';
            }

            return temp;
        }

    }
}
