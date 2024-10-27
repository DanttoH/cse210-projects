using System;
using System.Collections.Generic;

namespace Foundation2
{
    public class Product
    {
        private string name;
        private string productID;
        private double price;
        private int quantity;

        public Product(string name, string productID, double price, int quantity)
        {
            this.name = name;
            this.productID = productID;
            this.price = price;
            this.quantity = quantity;
        }

        public double GetTotalCost()
        {
            return price * quantity;
        }

        public string GetName() => name;
        public string GetProductID() => productID;
    }

    public class Address
    {
        private string street;
        private string city;
        private string stateOrProvince;
        private string country;

        public Address(string street, string city, string stateOrProvince, string country)
        {
            this.street = street;
            this.city = city;
            this.stateOrProvince = stateOrProvince;
            this.country = country;
        }

        public bool IsInUSA()
        {
            return country.ToLower() == "usa";
        }

        public string GetFullAddress()
        {
            return $"{street}\n{city}, {stateOrProvince}\n{country}";
        }
    }

    public class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public bool IsInUSA()
        {
            return address.IsInUSA();
        }

        public string GetName() => name;
        public string GetAddress() => address.GetFullAddress();
    }

    public class Order
    {
        private List<Product> products;
        private Customer customer;

        public Order(Customer customer)
        {
            this.customer = customer;
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public double CalculateTotalCost()
        {
            double totalCost = 0;
            foreach (var product in products)
            {
                totalCost += product.GetTotalCost();
            }
            totalCost += customer.IsInUSA() ? 5 : 35;
            return totalCost;
        }

        public string GetPackingLabel()
        {
            string label = "Packing Label:\n";
            foreach (var product in products)
            {
                label += $"{product.GetName()} (ID: {product.GetProductID()})\n";
            }
            return label;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Address, Customer, and Products for Order 1
            Address address1 = new Address("123 Apple St", "New York", "NY", "USA");
            Customer customer1 = new Customer("John Doe", address1);
            Product product1 = new Product("Laptop", "L001", 1200, 1);
            Product product2 = new Product("Mouse", "M001", 25, 2);
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            // Address, Customer, and Products for Order 2
            Address address2 = new Address("456 Orange Ave", "Toronto", "ON", "Canada");
            Customer customer2 = new Customer("Jane Smith", address2);
            Product product3 = new Product("Monitor", "MN001", 300, 1);
            Product product4 = new Product("Keyboard", "KB001", 50, 1);
            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);

            // Display results for Order 1
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order1.CalculateTotalCost()}\n");

            // Display results for Order 2
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order2.CalculateTotalCost()}\n");
        }
    }
}
