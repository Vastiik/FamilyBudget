using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FamilyBudget
{
    //class Budget
    //{
    //    public int id;
    //    public String motherboard;
    //    public String op;
    //    public String type_op;
    //    public int hard_disk_volume;
    //    public String video_card;
    //}
    class Budget
{
	//закриті поля класу для збереження даних про ім’я, прізвище, номер телефону та номер 
	// у записнику

	string productName;
	string type;
	double price;
	DateTime daate;
	int id;
	// відкриті члени класу


	public Budget(){} // порожній конструктор

	// конструктор ініціалізаціі полів об’єктів класу
	public Budget(string _productName, string _type, double _price, DateTime _daate, int _id)
	{
		productName = _productName;
		type = _type;
		price = _price;
		daate = _daate;
		if(_id>0){
			id=_id;
		} 
		else throw new Exception("Wrong id");  
// обробка помилки при спробі ввести код запису, менший рівний 0		
	}
	
	public string ProductName
    {
        get{return productName;}
        set{productName = value;}
    }

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    public double Price
    {
        get { return price; }
        set { price = value; }
    }

    public DateTime Daate
    {
        get { return daate; }
        set { daate = value; }
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

}

}