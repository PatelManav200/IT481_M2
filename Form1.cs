using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT481_Patel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllocConsole();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\v11.0;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        List<String> data = new List<String>();
        string numberofCustomers;
        private  void populateData()
        {
            try
            {
                string query = "SELECT [ShipName] FROM [dbo].[Orders]";
                string query2 = "SELECT COUNT(*) as 'Number Of Customers' FROM [dbo].[Orders]";
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                SqlCommand command2 = new SqlCommand(query2, conn);
                SqlDataReader reader = command.ExecuteReader();
                SqlDataReader reader2 = command2.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(reader["ShipName"].ToString());
                }
                reader.Close();
                while (reader2.Read())
                {
                    numberofCustomers =  reader2["Number Of Customers"].ToString();
                }
                conn.Close();

                reader2.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            populateData();
            numOfCustomers.Text = numOfCustomers.Text + numberofCustomers;
            foreach(var item in data)
            {
                listView1.Items.Add(item);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
