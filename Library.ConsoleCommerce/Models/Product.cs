namespace Library.ConsoleCommerce.Models
{
    internal class Product
    {
        //Name - the name of the product
        public string Name { get; set; }
        //Description - the description of the product
        public string Description { get; set; }
        //Price - the unit price of the product
        public double Price { get; set; }
        //Quantity - the number of units being purchased
        public int Quantity { get; set; }
        //TotalPrice - the total price of the product being purchased(i.e., Price* Quantity)
        public double TotalPrice => Price * Quantity;
        //id number
        public int Id { get; set; }


        // default constr
        public Product() {
            Name = "Name";
            Description = "Description";
            Price = 10;
            Quantity = 100;
        }
        // parameterized constr
        public Product(string name, string description, double price, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public Product(string name, string description, double price, int quantity, int id)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            Id = id;
        }

        public override string ToString()
        {
            return $"ID: {Id} - {Name}: {Description} \n\t${Price} x {Quantity} units = ${TotalPrice}";
        }
    }
}
