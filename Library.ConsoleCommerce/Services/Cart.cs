using Library.ConsoleCommerce.Models;
using Newtonsoft.Json;
namespace Library.ConsoleCommerce.Services
{
    internal class Cart
    {
        const double SALES_TAX = 0.07;
        private Inventory inventory;
        private List<Product> productsList;
        public List<Product> Products {   get { return productsList; }    }
        public Cart(Inventory _inventory)
        {
            productsList = new List<Product>();
            inventory = _inventory;
        }

        //assumes valid id and quantity. adds an item from inv to productslist 
        public void AddItem(int itemId, int amtAdded)
        {
            var item = inventory.Products.FirstOrDefault(p => p.Id == itemId);
            if (item == null)
                return;

            bool alreadyExists = false;
            // see if the item was already added
            foreach (Product prod in productsList) {
                if (prod.Id == item.Id)
                {
                    alreadyExists = true;
                    prod.Quantity += amtAdded;
                }
            }

            // if not, create a copy of the product with the specified quantity
            if (!alreadyExists)
            {
                productsList.Add(new Product(item.Name, item.Description, item.Price, amtAdded, item.Id));
                item.Quantity -= amtAdded;
            }
        }
        
        // assumes valid id, removes item from productslist, adds back to inv
        public void RemoveItem(int itemId)
        {
            var returnItem = productsList.FirstOrDefault(p => p.Id == itemId);
            var stock = inventory.Products.FirstOrDefault(p => p.Id == itemId);

            // null catching
            if (returnItem == null)
                return;
            if (stock == null) {
                inventory.Products.Add(returnItem);
                return;
            }

            stock.Quantity += returnItem.Quantity;
            productsList.Remove(returnItem);
        }

        public void Display()
        {
            foreach(var item in productsList)
                Console.WriteLine(item);
        }

        public void CheckOut()
        {
            var total = productsList.Sum(p => p.TotalPrice);
            Console.WriteLine($"Subtotal: ${total}");
            var tax = total * SALES_TAX;
            Console.WriteLine($"Tax: ${tax}");

            Console.WriteLine($"Total: ${total + tax}\n\tThank you for shopping with us.");
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(productsList);
            File.WriteAllText("CartData.json", json);
        }
        public void Load()
        {
            var json = File.ReadAllText("CartData.json");
            productsList = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
        }

        // search via name
        public void SearchName(string search)
        {
            foreach(Product product in productsList)
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
      
        // search via description
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

        // empty cart
        public void ClearCart()
        {
            Console.WriteLine("Emptying cart...");


            if (productsList.Count > 0) {
                foreach (Product prod in productsList)
                    RemoveItem(prod.Id);
            }

            Console.WriteLine("Cart emptied.");
        }

        // not actually used lol
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
