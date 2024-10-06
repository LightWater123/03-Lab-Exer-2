using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using _03LabExer2;

namespace _03LabExer2
{

    public partial class frmAddProduct : Form
    {
        // step 5 - adding variabless
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;

        private BindingSource showProductList;
        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                {
                    throw new StringFormatException("Product Name must contain only letters.");
                }
                return name;
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Product Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                txtProductName.Text = "";
            }
        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]+$"))
                {
                    throw new NumberFormatException("Quantity must be a valid integer.");
                }
                return Convert.ToInt32(qty);
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; 
            }
            finally
            {
                txtQuantity.Text = "";
            }
        }

        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException("Selling Price must be a valid currency amount.");
                }
                return Convert.ToDouble(price);
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Invalid Selling Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0.0;
            }
            finally
            {
                txtSellPrice.Text = "";
            }
        }

        public bool ValidateMfg(DateTimePicker dtPickerMfgDate, DateTimePicker dtPickerExpDate)
        {
            if (dtPickerExpDate.Value.CompareTo(dtPickerMfgDate.Value) < 0)
            {
                MessageBox.Show("The manufacturing date is greater than the expiration date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  
            }
            return true;  
        }
        
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // step 6 - create an array
            string[] ListOfProductCategory = new string[]
            {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
            };
            // step 7 - foreach loopto display array items in the comboBox
            foreach (string productList in ListOfProductCategory)
            {
                cbCategory.Items.Add(productList);
            }
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSellPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtPickerExpDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtPickerMfgDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTxtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridViewProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // step 10
        private void btnAddProduct_Click(object sender, EventArgs e)
        {


            if (!ValidateMfg(dtPickerMfgDate, dtPickerExpDate))
            {
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                return;
            }            

            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;

            

            // reset comboBox
            cbCategory.SelectedIndex = -1;
        }

        // step 12 - custom exceptions
        public class StringFormatException : Exception
        {
            public StringFormatException(string message) : base(message) { }
        }

        public class NumberFormatException : Exception
        {
            public NumberFormatException(string message) : base(message) { }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string message) : base(message) { }
        }

    }
}
