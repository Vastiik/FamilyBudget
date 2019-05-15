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
    public partial class Autorization : Form
    {
       
        public Autorization()
        {
            InitializeComponent();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            string query = "SELECT ID FROM Family WHERE Naame LIKE '%" + textBox1.Text + "%' AND Pasword LIKE'%" + textBox2.Text + "%';";
            OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader reader = command.ExecuteReader();
                Form1 fr1 = new Form1();
                if(reader.Read())
                
                {
                    fr1.UserId = Convert.ToInt32(reader[0].ToString());
                    
                    reader.Close();
                    myConnection.Close();
                    
                    if (fr1.UserId != 0)
                    {
                        this.Hide();
                        fr1.Show();
                    }
                }
                else MessageBox.Show("Wrong login or pasword");
         
        }

     
    }
}
