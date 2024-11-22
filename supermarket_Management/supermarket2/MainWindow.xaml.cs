using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace supermarket2
{
    public partial class MainWindow : Window
    {

        SupermarketEntities1 SupermarketEntities = new SupermarketEntities1();
        Product selectedproduct = new Product();
        Supplier selectedsupplier = new Supplier();
        Customer selectedcustomer = new Customer();
        Cart selectedcart = new Cart();
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
            Loadsuppliers();
            LoadCustomers();
            LoadProductsForComboBox();
            LoadCustomersForComboBox();
            LoadObservableCollection();
            LoadSalesSummary();
        }

        private void LoadProducts()
        {
            dataGridProducts.ItemsSource = SupermarketEntities.Products.ToList();
        }

        public void clearpro()
        {
            txtProductName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            Suppliers.Clear();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                ProductName = txtProductName.Text,
                Quantity = int.Parse(txtQuantity.Text),
                Price = decimal.Parse(txtPrice.Text),
                SupplierID =int.Parse(Suppliers.Text)
            };
            SupermarketEntities.Products.Add(product);
            SupermarketEntities.SaveChanges();
            LoadProducts();
            clearpro();
            LoadProductsForComboBox();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            selectedproduct.ProductName = txtProductName.Text;
            selectedproduct.Quantity = int.Parse(txtQuantity.Text);
            selectedproduct.Price = decimal.Parse(txtPrice.Text);
            selectedproduct.SupplierID = int.Parse(Suppliers.Text);
            SupermarketEntities.SaveChanges();
            LoadProducts();
            clearpro();
            LoadProductsForComboBox();
        }
        private void dataGridProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridProducts.SelectedItem is Product selectdproduct)
            {
                selectedproduct= selectdproduct;
                txtProductName.Text = selectedproduct.ProductName;
                txtQuantity.Text =selectedproduct.Quantity.ToString();
                txtPrice.Text = selectedproduct.Price.ToString();
                Suppliers.Text = selectedproduct.SupplierID.ToString();
            }
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            SupermarketEntities.Products.Remove(selectedproduct);
            SupermarketEntities.SaveChanges();
            LoadProducts();
            clearpro();
            LoadProductsForComboBox();
        }

        // --------------------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------------------- //

        private void Loadsuppliers()
        {
            dataGridsuppliers.ItemsSource = SupermarketEntities.Suppliers.ToList();
        }

        public void clearsup()
        {
            SupplierName.Clear();
            Phone.Clear();
            email.Clear();
            ContactPerson.Clear();
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            Supplier supplier = new Supplier
            {
                SupplierName = SupplierName.Text,
                Phone = Phone.Text,
                Email =email.Text,
                ContactPerson =ContactPerson.Text
            };
            SupermarketEntities.Suppliers.Add(supplier);
            SupermarketEntities.SaveChanges();
            Loadsuppliers();
            clearsup();
        }

        private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            selectedsupplier.SupplierName = SupplierName.Text;
            selectedsupplier.Phone = Phone.Text;
            selectedsupplier.Email = email.Text;
            selectedsupplier.ContactPerson = ContactPerson.Text;
            SupermarketEntities.SaveChanges();
            Loadsuppliers();
            clearsup();
        }
        private void dataGridsuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridsuppliers.SelectedItem is Supplier selectedSupplier)
            {
                selectedsupplier = selectedSupplier;
                SupplierName.Text = selectedsupplier.SupplierName;
                Phone.Text = selectedsupplier.Phone;
                email.Text = selectedsupplier.Email;
                ContactPerson.Text = selectedsupplier.ContactPerson;
            }
        }

        private void deleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            SupermarketEntities.Suppliers.Remove(selectedsupplier);
            SupermarketEntities.SaveChanges();
            Loadsuppliers();
            clearsup();
        }
        // --------------------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------------------- //

        private void LoadCustomers()
        {
            dataGridcustomers.ItemsSource = SupermarketEntities.Customers.ToList();
        }

        public void clearcus()
        {
            txtCustomerName.Clear();
            txtEmail.Clear();
            Phone1.Clear();
        }
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer
            {
                CustomerName = txtCustomerName.Text,
                Email = txtEmail.Text,
                Phone =Phone1.Text
            };
            SupermarketEntities.Customers.Add(customer);
            SupermarketEntities.SaveChanges();
            LoadCustomers();
            clearcus();
            LoadCustomersForComboBox();
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            selectedcustomer.CustomerName = txtCustomerName.Text;
            selectedcustomer.Email = txtEmail.Text;
            selectedcustomer.Phone = Phone1.Text;
            SupermarketEntities.SaveChanges();
            LoadCustomers();
            clearcus();
            LoadCustomersForComboBox();
        }

        private void dataGridcustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridcustomers.SelectedItem is Customer selectedcustomer2)
            {
                selectedcustomer  =selectedcustomer2;
                txtCustomerName.Text = selectedcustomer.CustomerName;
                txtEmail.Text = selectedcustomer.Email;
                Phone1.Text = selectedcustomer.Phone;
            }
        }

        private void deleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            SupermarketEntities.Customers.Remove(selectedcustomer);
            SupermarketEntities.SaveChanges();
            LoadCustomers();
            clearcus();
            LoadCustomersForComboBox();
        }


        // --------------------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------------------- //
        // --------------------------------------------------------------------------------------- //


        public ObservableCollection<Cart> cartItems = new ObservableCollection<Cart>(); 
        private void LoadObservableCollection()
        {
            dataGridCartItems.ItemsSource = cartItems;
        }
        private void LoadCustomersForComboBox()
        {
            cmbCustomerName.ItemsSource = SupermarketEntities.Customers.ToList();
            cmbCustomerName.DisplayMemberPath = "CustomerName";
            cmbCustomerName.SelectedValuePath = "CustomerID";
        }

        private void LoadProductsForComboBox()
        {
            cmbProduct.ItemsSource = SupermarketEntities.Products.ToList();
            cmbProduct.DisplayMemberPath = "ProductName";
            cmbProduct.SelectedValuePath = "ProductID";
        }
        private void LoadCart()
        {
            cmbProduct.SelectedItem = null;
            txtCartQuantity.Clear();
        }

        private void LoadCart_after_tranzaction()
        {
            cmbCustomerName.SelectedItem=null;
            cmbProduct.SelectedItem = null;
            txtCartQuantity.Clear();
        }

        private void LoadCartItemsFromDatabase()
        {
            cartItems.Clear();
            var itemsFromDb = SupermarketEntities.Carts.Include("Product").ToList();
            foreach (var item in itemsFromDb)
            {
                cartItems.Add(item);
            }
            UpdateTotalAmount();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomerName.SelectedItem is Customer selectedCustomer &&
                cmbProduct.SelectedItem is Product selectedProduct &&
                int.TryParse(txtCartQuantity.Text, out int quantity))
            {
                decimal price = selectedProduct.Price;
                Cart newItem = new Cart
                {
                    CustomerID = selectedCustomer.CustomerID,
                    ProductID = selectedProduct.ProductID,
                    Quantity = quantity,
                    Price = price,
                    DateAdded = DateTime.Now
                };
                SupermarketEntities.Carts.Add(newItem);
                SupermarketEntities.SaveChanges();
                LoadCartItemsFromDatabase();
                LoadCart();
            }
            else
            {
                MessageBox.Show("Please select a product and enter a valid quantity.");
            }
        }
        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            SupermarketEntities.Carts.RemoveRange(SupermarketEntities.Carts);
            SupermarketEntities.SaveChanges();
            cartItems.Clear();
            UpdateTotalAmount();
            MessageBox.Show("All items have been cleared from the cart.");
        }

        private void UpdateTotalAmount()
        {
            decimal totalAmount = cartItems.Sum(item => item.Price * item.Quantity);
            txtTotalAmount.Text = $"Total: {totalAmount:C}";
        }

        private void CompleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            foreach (var cartItem in cartItems)
            {
                var product = SupermarketEntities.Products.SingleOrDefault(p => p.ProductID == cartItem.ProductID);
                if (product != null)
                {
                    if (product.Quantity >= cartItem.Quantity)
                    {
                        product.Quantity -= cartItem.Quantity;
                    }
                    else
                    {
                        MessageBox.Show($"Insufficient quantity for {product.ProductName}. Available: {product.Quantity}");
                        return;
                    }
                }
            }
            foreach (var cartItem in cartItems)
            {
                var sale = new Sale
                {
                    CustomerID = cartItem.CustomerID,
                    TotalAmount = cartItem.Price * cartItem.Quantity,
                    SaleDate = DateTime.Now
                };
                SupermarketEntities.Sales.Add(sale);
            }

            SupermarketEntities.SaveChanges();
            cartItems.Clear();
            SupermarketEntities.Carts.RemoveRange(SupermarketEntities.Carts);
            SupermarketEntities.SaveChanges();
            UpdateTotalAmount();
            LoadSalesSummary();
            LoadCart_after_tranzaction();
            MessageBox.Show("Transaction completed successfully and stock updated.");
        }

        private void LoadSalesSummary()
        {
            var salesData = (from sale in SupermarketEntities.Sales
                select new
                {
                    SaleID = sale.SaleID,
                    CustomerID = sale.CustomerID,
                    SaleDate =sale.SaleDate,
                    TotalAmount = sale.TotalAmount
                }).ToList();

            dataGridSalesSummary.ItemsSource = salesData;

            decimal totalSales = salesData.Sum(s => s.TotalAmount);
            lblTotalSales.Content = $"Total Sales: {totalSales:C}";
        }

    }

}
