using sqlapp.Models;
using System.Data.SqlClient;


namespace sqlapp.Services
{
    public class ProductService
    {
        private readonly string db_source = "appserver300000.database.windows.net";
        private readonly string db_user = "sqladmin";
        private readonly string db_password = "Password#1";
        private readonly string db_database = "appdb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> _products = new List<Product>();
            string statement = "SELECT * FROM Products";

            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _products.Add(product);
                }
            }

            conn.Close();
            return _products;
        }
    }
}
