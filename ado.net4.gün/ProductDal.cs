using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ado.net4.gün
{
    class ProductDal
    { SqlConnection _connection = new SqlConnection(
            @"server=(localdb)\mssqllocaldb;initial catalog=ETrd;integrated security=true");
        //@ string olarak kabul et demek (\ ile)
        public List<Product> GetAll()
        {   ConnectionControl();
            SqlCommand command = new SqlCommand("Select* from Products", _connection);
            SqlDataReader reader = command.ExecuteReader(); //komutun çalışması için yazılır.
            List<Product> products = new List<Product>();
            // amacımız readerdeki ürünleri listeye atmak
            while (reader.Read())
            {  Product product = new Product
                {   Id = Convert.ToInt32(reader["Id"]),
                    //gelen data, burda obje türünde olduğu için int çeviremiyor.
                    Name = reader["Name"].ToString(),
                    UnitPrice = Convert.ToInt32(reader["UnitPrice"]),
                    StockAmount = Convert.ToInt32(reader["StockAmount"])  };
                //ürünü oluşturduk,kullanmak için listeye ekleyelim.
            products.Add(product);  }
            reader.Close();
            _connection.Close();
            return products;  }
           private void ConnectionControl()
           {  if (_connection.State == ConnectionState.Closed)//-->bağlantı kapalıysa aç
            {  _connection.Open(); } }
            public DataTable GetAll2()
            {  SqlConnection connection = new SqlConnection(
                @"server=(localdb)\mssqllocaldb;initial catalog=ETrd;integrated security=true");
            if (connection.State == ConnectionState.Closed)//-->bağlantı kapalıysa aç
            { connection.Open(); }
            SqlCommand command = new SqlCommand("Select* from Products", connection);
            SqlDataReader reader = command.ExecuteReader(); //komutun çalışması için yazılır.
            DataTable dataTable = new DataTable();
            dataTable.Load(reader); //--> IDataReader SqlDataReader ın base dir.
            reader.Close(); connection.Close(); return dataTable;  }
        public void Add(Product product)
        {   ConnectionControl();
            SqlCommand command = new SqlCommand(
                "Insert into Products values(@name,@unitprice,@stockAmount)", _connection);
            //parametre varsa:
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitprice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery();
            _connection.Close(); }
        public void Update(Product product)
        {   ConnectionControl();
            SqlCommand command = new SqlCommand(
                "Update Products set Name=@name,UnitPrice=@unitprice,StockAmount=@stockAmount where Id=@id" , _connection);
            //parametre varsa:
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitprice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();
            _connection.Close(); }
        public void Delete(int id)
        {   ConnectionControl();
            SqlCommand command = new SqlCommand(
                "Delete from Products where Id=@id", _connection);
            //parametre varsa:
            command.Parameters.AddWithValue("@id",id);
            command.ExecuteNonQuery();
            _connection.Close();  }
    }
}
