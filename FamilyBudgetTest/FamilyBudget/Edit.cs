using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FamilyBudget
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();

            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            myConnection.Open();
        }
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FamilyBudget.accdb;Persist Security Info=False;";
        private OleDbConnection myConnection;

        private void button1_Click(object sender, EventArgs e)
        {
            int num_book = Convert.ToInt32(textBox5.Text);
            String query = "UPDATE Products SET [Product_name]='" + textBox1.Text +
                        "', [Type]='" + textBox2.Text +
                        "', [Price]=" + Convert.ToDouble(textBox3.Text) +
                        " WHERE [id]=" + num_book + ";";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            if (command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Updated");
            }
          
        }

        

        private void Edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }
    }
}
