using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using FamilyBudget;

namespace FamilyBudget
{
    public partial class ProductsEdit : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=FamilyBudget.accdb;Persist Security Info=False;";
        private OleDbConnection myConnection;
        public ProductsEdit()
        {
            InitializeComponent();
            // создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);

            // открываем соединение с БД
            
        }
        private void insert(string name, string type,double price, int member){
            myConnection.Open();
				String query="INSERT INTO Products([Product_name], [Type], [Price], [Daate], [Member_id])"
					+"VALUES('"+name+"','"+type+"','"+price+"','"+DateTime.Now+"',"+member+");";
				OleDbCommand command = new OleDbCommand(query, myConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            //OleDbDataReader reader = command.ExecuteReader();
				
				if(command.ExecuteNonQuery()>0)
				{
					MessageBox.Show("Inserted!");
				}
                myConnection.Close();
			 }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox[] tbs = { textBox1, textBox2, textBox3, textBox4,  textBox5, textBox6, textBox7,  textBox8, textBox9, textBox10,  textBox11, textBox12, textBox13,  textBox14, textBox15 };
            ComboBox[] cmb = { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5 };
            int   j = 0, c = 0;
            //while (tbs[i].Text.Length != 0 && cmb[c].Text.Length != 0 &&
            //       tbs[i + 2].Text.Length != 0 && tbs[i + 3].Text.Length != 0)
            //{

            //    insert(tbs[j].Text, cmb[c].Text,
            //   Convert.ToDouble(tbs[j + 2].Text), Convert.ToInt32(tbs[j + 3].Text));
            //    i += 4;
            //    j += 4;
            //    c += 1;
            //}
            for (int i = 0; i < 15; i += 3)
            {
                if (tbs[i].Text.Length != 0 && cmb[c].Text.Length != 0 &&
                       tbs[i + 1].Text.Length != 0 && tbs[i + 2].Text.Length != 0)
                {

                    insert(tbs[j].Text, cmb[c].Text,
                   Convert.ToDouble(tbs[j + 1].Text), Convert.ToInt32(tbs[j + 2].Text));
                    
                }
                j += 3;
                c += 1;
            }
            for (int i = 0; i < 15; i++)
                tbs[i].Clear();
        }

        private void ProductsEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fr = new Form1();
            fr.Update();
        }

    }
}
