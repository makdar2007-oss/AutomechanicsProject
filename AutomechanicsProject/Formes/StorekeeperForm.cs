using AutomechanicsProject.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class StorekeeperForm : Form
    {
        private DateBase db;
        public static string UserRole { get; set; }
        public StorekeeperForm()
        {
            InitializeComponent();
            db = new DateBase();
        }
        private void StorekeeperForm_Load(object sender, EventArgs e)
        {
            if (Program.CurrentUser != null)
            {
                toolStripTextBox2.Text = $"Кладовщик: {Program.CurrentUser.FullName}";
            }
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                var query = from p in db.Products
                            join c in db.Categories on p.CategoryId equals c.Id
                            select new
                            {
                                p.Id,
                                p.Article,
                                p.Name,
                                CategoryName = c.Name,
                                p.Unit,
                                p.Price,
                                p.Balance
                            };

                dataGridViewStore.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке товаров: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripTextBox1_Click(object sender, EventArgs e)
        {
            var shipmentForm = new CreateShipment();
            shipmentForm.ShowDialog();
            LoadProducts(); 
        }
    
    }
}
