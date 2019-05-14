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
    public partial class Form1 : Form
    {

        private int memberId;
        private double budget;
        private double remainder;
      //--------------------------------------------------------------------
        // метод виведення на екран даних про контакти
		public void getProducts(string query){
            
           // текст запроса
            
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();
            int i = 0;
            // в цикле построчно читаем ответ от БД
            dataGridView1.Rows.Clear();
            while (reader.Read())
            { 
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = reader[0].ToString();
                dataGridView1.Rows[i].Cells[1].Value = reader[1].ToString();
                dataGridView1.Rows[i].Cells[2].Value = reader[2].ToString();
                dataGridView1.Rows[i].Cells[3].Value = reader[3].ToString();
                dataGridView1.Rows[i].Cells[4].Value = reader[4].ToString();
                dataGridView1.Rows[i].Cells[5].Value = reader[5].ToString();
                i++;
            }
            // закрываем OleDbDataReader
            reader.Close();
           
		}
        void progres()
        {
            remainder = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                remainder = remainder + Convert.ToInt32(dataGridView1[3, i].Value);
            }
            progressBar1.Value = 0;
            progressBar1.Increment(Convert.ToInt32(remainder));
        }

////метод передачі запиту до БД на виконання
//        void execute(String^ query){
//            connect->Open();
//            System::Data::OleDb::OleDbCommand^ command = gcnew System::Data::OleDb::OleDbCommand(query,connect);
//            command->ExecuteNonQuery();
//            connect->Close();
//        }

//// метод повернення результатів виконання запиту
//        void getQuerypostach(String^ query){		
//            connect->Open();
//            System::Data::OleDb::OleDbCommand^ command = gcnew System::Data::OleDb::OleDbCommand(query,connect);
//            System::Data::OleDb::OleDbDataReader^ oledbRead=command->ExecuteReader();
//            group->Clear();
//            while(oledbRead->Read()){
//              group->Add(gcnew postach(
//                System::Convert::ToString(oledbRead["Nazva_Postachalnuka"]),
//                System::Convert::ToString(oledbRead["Telephone"]),
//                System::Convert::ToString(oledbRead["Adressa"]),
//                System::Convert::ToString(oledbRead["Mail"]),
//                System::Convert::ToInt32(oledbRead["id"])
//              ));
//            }
//            oledbRead->Close();
//            connect->Close();
//            this->updateTable();
//        }
		
//// метод оновлення таблиці dataGridView
//        void updateTable(){
//            int i=0;
//            dataGridView1->Rows->Clear();
//            for each(postach^ postach in group){
//                this->dataGridView1->Rows->Add();
//                this->dataGridView1->Rows[i]->Cells[0]->Value=postach->getId();
//                this->dataGridView1->Rows[i]->Cells[1]->Value=postach->getName();
//                this->dataGridView1->Rows[i]->Cells[2]->Value=postach->getTelephon();
//                this->dataGridView1->Rows[i]->Cells[3]->Value=postach->getAdress();
//                this->dataGridView1->Rows[i]->Cells[4]->Value=postach->getMail();
//                ++i;
//            }
//        }
        //--------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            ProductsEdit settingsForm = new ProductsEdit();

            // Show the settings form
            settingsForm.Show();
            //Form1_FormClosing();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {

            getProducts("SELECT [Products.Id], [Product_name], [Type],[Price] , [Daate], [Naame] FROM [Products],[Family] WHERE ([Member_id] = [Family.ID])");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string num_book = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
            int index = dataGridView1.CurrentRow.Index;
            Edit edit = new Edit();
            edit.textBox1.Text = Convert.ToString(dataGridView1[1, index].Value);
            edit.textBox2.Text = Convert.ToString(dataGridView1[2, index].Value);
            edit.textBox3.Text = Convert.ToString(dataGridView1[3, index].Value);
            edit.textBox4.Text = Convert.ToString(dataGridView1[5, index].Value);
            edit.textBox5.Text = Convert.ToString(dataGridView1[0, index].Value);
            edit.Show();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String query = "DELETE FROM Products WHERE [id]=" + Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
            OleDbCommand command = new OleDbCommand(query, myConnection);
                if(command.ExecuteNonQuery()>0){
                   MessageBox.Show("Deleted!");
                }

                getProducts("SELECT [Products.Id], [Product_name], [Type],[Price] , [Daate], [Naame] FROM [Products],[Family] WHERE ([Member_id] = [Family.ID])");        
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            getProducts("SELECT [Products.Id], [Product_name], [Type],[Price] , [Daate], [Naame] FROM [Products],[Family] WHERE ([Member_id] = [Family.ID])");
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            getProducts("SELECT [Products.Id], [Product_name], [Type],[Price] , [Daate], [Naame] FROM [Products],[Family] WHERE ([Member_id] = [Family.ID]) AND Type LIKE '%" + comboBox1.Text + "%'");
           
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            String query = "SELECT Type FROM Products";
            OleDbCommand command = new OleDbCommand(query, myConnection);

            comboBox1.Items.Clear();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(Convert.ToString(reader[0].ToString()));
            }
            //connect->Close();
            //this->getData();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            getProducts("SELECT * FROM Products WHERE Daate BETWEEN '%" + dateTimePicker1.Value + "%' AND '%" + dateTimePicker2.Value + "%';");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "SELECT ID FROM Family WHERE Naame LIKE '%" + comboBox2.Text + "%'";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();
            memberId = Convert.ToInt32(reader[0]);
            getProducts("SELECT [Products.Id], [Product_name], [Type],[Price] , [Daate], [Naame] FROM [Products],[Family] WHERE ([Member_id] = [Family.ID]) AND Member_id LIKE '%" + memberId + "%'");
               
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            String query = "SELECT Naame FROM Family";
            OleDbCommand command = new OleDbCommand(query, myConnection);

            comboBox2.Items.Clear();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(Convert.ToString(reader[0].ToString()));
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            budget = Convert.ToDouble(textBox1.Text);
            progressBar1.Maximum = Convert.ToInt32(budget);
            progres();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Show();
            OleDbCommand command = new OleDbCommand("SELECT Type, SUM(Price) FROM Products GROUP BY Type", myConnection);         
            OleDbDataReader reader = command.ExecuteReader();
            int i = 0;
            double totalSum=0;
            dataGridView2.Rows.Clear();
            while (reader.Read())
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = reader[0].ToString();
                dataGridView2.Rows[i].Cells[1].Value = reader[1].ToString();
                totalSum += Convert.ToDouble(reader[1]);
                i++;
            }
            // закрываем OleDbDataReader
            reader.Close();
            label2.Text = "Total sum\n" + totalSum;
            if (budget == 0)
            {
                MessageBox.Show("Enter budget");
            }
            else label3.Text = "Remainder\n" + (budget - totalSum);
            
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        

       
    }
}
