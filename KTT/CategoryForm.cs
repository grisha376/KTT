using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using KTT.Properties;

namespace KTT
{
    public partial class CategoryForm : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader Reader;

        BindingList<Products> Default = null;
        BindingList<Products> Flexible = null;


        public CategoryForm()
        {
            InitializeComponent();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private BindingList<Products> GetCategory()
        {
            BindingList<Products> list = new BindingList<Products>();
            try
            {
                using (connection = new SqlConnection(Settings.Default.KTT_IvanovoConnectionString))
                {
                    connection.Open();
                    using (command = new SqlCommand("Select * From Product", connection))
                    {
                        using (Reader = command.ExecuteReader())
                        {
                            Products products = null;
                            while (Reader.Read())
                            {
                                products = new Products(
                                    Convert.ToUInt32(Reader["ID"]),
                                    $"{Convert.ToString(Reader["Name"])}"+
                                    $"\n{Convert.ToDouble(Reader["Price"])} руб."+
                                    $"\nВ наличии {Convert.ToInt32(Reader["Quantity"])} шт."+
                                    $"\n{Convert.ToUInt32(Reader["ID_category"])} категория товара",
                                    Convert.ToDouble(Reader["Price"]),
                                    Convert.ToInt32(Reader["Quantity"]),
                                    Convert.ToUInt32(Reader["ID_category"])
                                    );
                                list.Add(products);

                            }
                        }
                    }
                }
            }
            catch (Exception Error) { MessageBox.Show(Error.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return list;
        }

        private void FillGrid()
        {
            Default = GetCategory();
            Flexible = Default;
            //
            CategoryGrid.DataSource = Default;
            CategoryGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //
            CategoryGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CategoryGrid.Columns[0].Visible = false;
            CategoryGrid.Columns[2].Visible = false;
            CategoryGrid.Columns[3].Visible = false;
            CategoryGrid.Columns[4].Visible = false;
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Flexible = Default;
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    Flexible = Default;
                    break;
                case 1:
                    Flexible = new BindingList<Products>(Default.Where(x => x.ProductID_category == 0).ToList());
                    break;
                case 2:
                    Flexible = new BindingList<Products>(Default.Where(x => x.ProductID_category == 1).ToList());
                    break;
                case 3:
                    Flexible = new BindingList<Products>(Default.Where(x => x.ProductID_category == 2).ToList());
                    break;
                case 4:
                    Flexible = new BindingList<Products>(Default.Where(x => x.ProductID_category == 3).ToList());
                    break;
                case 5:
                    Flexible = new BindingList<Products>(Default.Where(x => x.ProductID_category == 4).ToList());
                    break;
            }

            CategoryGrid.DataSource = Flexible;
        }
    }
}
