using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Data;

namespace CustomerCart.Models
{
    public class SoldContext
    {
        private readonly MySqlConnection _mySqlConnection;


        public SoldContext(string connectionString)
        {
            _mySqlConnection = new MySqlConnection(connectionString);
        }


        //Create an Sold//
        public bool InsertSold(SoldModel sold)
        {
            try
            {
                _mySqlConnection.Open();
                MySqlCommand sqlcommand = new MySqlCommand(

                    @"INSERT INTO sold_item (SoldItemID, ItemName, ItemBrand, QuantitySold, Price, DateSold)
                        VALUES(@SoldItemID, @ItemName, @ItemBrand, @QuantitySold, @Price, @DateSold)", _mySqlConnection);
                sqlcommand.Parameters.AddWithValue("@SoldItemID", sold.SoldItemID);
                sqlcommand.Parameters.AddWithValue("@ItemName", sold.ItemName);
                sqlcommand.Parameters.AddWithValue("@ItemBrand", sold.ItemBrand);
                sqlcommand.Parameters.AddWithValue("@QuantitySold", sold.QuantitySold);
                sqlcommand.Parameters.AddWithValue("@Price", sold.Price);
                sqlcommand.Parameters.AddWithValue("@DateSold", sold.DateSold);

                int rowsAffected = sqlcommand.ExecuteNonQuery();
                _mySqlConnection.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //**************************************************************UPDATE********************************************************//

        // Get sold item from solditemID by ID
        public SoldModel GetSoldById(int soldId)
        {
            SoldModel sold = null;
            _mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM sold_item WHERE SoldItemID = @SoldItemID", _mySqlConnection);
            command.Parameters.AddWithValue("SoldItemID", soldId);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    sold = new SoldModel
                    {
                        SoldItemID = reader.GetInt32("SoldItemID"),
                        ItemName = reader.GetString("ItemName"),
                        ItemBrand = reader.GetString("ItemBrand"),
                        QuantitySold = reader.GetInt32("QuantitySold"),
                        Price = reader.GetInt32("Price"),
                        DateSold = reader.GetDateTime("DateSold")
                    };
                }
            }
            return sold;
        }


        // Update Sold
        public bool UpdateSold(SoldModel updatedSold)
        {
            try
            {
                _mySqlConnection.Open();
                MySqlCommand command = new MySqlCommand(
                    @"UPDATE sold_item SET ItemName = @ItemName, ItemBrand = @ItemBrand, QuantitySold = @QuantitySold, Price = @Price, DateSold = @DateSold WHERE SoldItemID = @SoldItemID", _mySqlConnection);
                command.Parameters.AddWithValue("@SoldItemID", updatedSold.SoldItemID);
                command.Parameters.AddWithValue("@ItemName", updatedSold.ItemName);
                command.Parameters.AddWithValue("ItemBrand", updatedSold.ItemBrand);
                command.Parameters.AddWithValue("QuantitySold", updatedSold.QuantitySold);
                command.Parameters.AddWithValue("Price", updatedSold.Price);
                command.Parameters.AddWithValue("DateSold", updatedSold.DateSold);

                int rowsAffected = command.ExecuteNonQuery();
                _mySqlConnection.Close();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Sold: {ex.Message}");
                return false;
            }
        }


        public bool TestConnection()
        {
            try
            {
                _mySqlConnection.Open();
                _mySqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}