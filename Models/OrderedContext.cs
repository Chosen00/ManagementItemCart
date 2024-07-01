using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Data;

namespace CustomerCart.Models
{
    public class OrderedContext
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly string _connectionString;



        public OrderedContext(string connectionString)
        {
            _mySqlConnection = new MySqlConnection(connectionString);
            _connectionString = connectionString;
        }


        //Create an Ordered//
        public bool InsertOrdered(OrderedModel ordered)
        {
            try
            {
                _mySqlConnection.Open();
                MySqlCommand sqlcommand = new MySqlCommand(

                    @"INSERT INTO ordered_item (OrderedItemId, ItemName, ItemBrand, QuantityOrdered, Price, DateOrdered)
                        VALUES(@OrderedItemId, @ItemName, @ItemBrand, @QuantityOrdered, @Price, @DateOrdered)", _mySqlConnection);
                sqlcommand.Parameters.AddWithValue("@OrderedItemId", ordered.OrderedItemId);
                sqlcommand.Parameters.AddWithValue("@ItemName", ordered.ItemName);
                sqlcommand.Parameters.AddWithValue("@ItemBrand", ordered.ItemBrand);
                sqlcommand.Parameters.AddWithValue("@QuantityOrdered", ordered.QuantityOrdered);
                sqlcommand.Parameters.AddWithValue("@Price", ordered.Price);
                sqlcommand.Parameters.AddWithValue("@DateOrdered", ordered.DateOrdered);
               
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
      
        // Get client from booktable by ID
        public OrderedModel GetOrderedById(int orderedId)
        {
            OrderedModel ordered = null;
            _mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM ordered_item WHERE OrderedItemId = @OrderedItemId", _mySqlConnection);
            command.Parameters.AddWithValue("OrderedItemId", orderedId);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ordered = new OrderedModel
                    {
                        OrderedItemId = reader.GetInt32("OrderedItemId"),
                        ItemName = reader.GetString("ItemName"),
                        ItemBrand = reader.GetString("ItemBrand"),
                        QuantityOrdered = reader.GetInt32("QuantityOrdered"),
                        Price = reader.GetInt32("Price"),
                        DateOrdered = reader.GetDateTime("DateOrdered")
                    };
                }
            }
            return ordered;
        }


        // Update Ordered Method
        public bool UpdateOrdered(OrderedModel updatedOrdered)
        {
            try
            {
                _mySqlConnection.Open();
                MySqlCommand command = new MySqlCommand(
                    @"UPDATE ordered_item SET ItemName = @ItemName, ItemBrand = @ItemBrand, QuantityOrdered = @QuantityOrdered, Price = @Price, DateOrdered = @DateOrdered WHERE OrderedItemId = @OrderedItemId", _mySqlConnection);
                command.Parameters.AddWithValue("@OrderedItemId", updatedOrdered.OrderedItemId);
                command.Parameters.AddWithValue("@ItemName", updatedOrdered.ItemName);
                command.Parameters.AddWithValue("ItemBrand", updatedOrdered.ItemBrand);
                command.Parameters.AddWithValue("QuantityOrdered", updatedOrdered.QuantityOrdered);
                command.Parameters.AddWithValue("Price", updatedOrdered.Price);
                command.Parameters.AddWithValue("DateOrdered", updatedOrdered.DateOrdered);

                int rowsAffected = command.ExecuteNonQuery();
                _mySqlConnection.Close();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Ordered: {ex.Message}");
                return false;
            }
        }



        //********************************************************************************DELETE****************************************************************************//
        public bool DeleteOrdered(int id)
        {
            try
            {
                _mySqlConnection.Open();
                MySqlCommand sqlcommand = new MySqlCommand("DELETE FROM ordered_item WHERE OrderedItemId = @OrderedItemId", _mySqlConnection);
                sqlcommand.Parameters.AddWithValue("@OrderedItemId", id);
                int rowAffected = sqlcommand.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting ordered: {ex.Message}");
                return false;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }

        public bool DeleteSold(int id)
        {
            try
            {
                _mySqlConnection.Open();
                MySqlCommand sqlcommand = new MySqlCommand("DELETE FROM sold_item WHERE SoldItemID = @SoldItemID", _mySqlConnection);
                sqlcommand.Parameters.AddWithValue("@SoldItemID", id);
                int rowAffected = sqlcommand.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting sold: {ex.Message}");
                return false;
            }
            finally
            {
                _mySqlConnection.Close();
            }
        }

        // Get Ordered table data
        public List<OrderedModel> GetOrderedList()
        {
            List<OrderedModel> orderedList = new List<OrderedModel>();
            _mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand(
                "SELECT * FROM ordered_item", _mySqlConnection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    orderedList.Add(new OrderedModel
                    {
                        OrderedItemId = reader.GetInt32("OrderedItemId"),
                        ItemName = reader.GetString("ItemName"),
                        ItemBrand = reader.GetString("ItemBrand"),
                        QuantityOrdered = reader.GetInt32("QuantityOrdered"),
                        Price = reader.GetInt32("Price"),
                        DateOrdered = reader.GetDateTime("DateOrdered")

                    });
                }
            }
            _mySqlConnection.Close();
            return orderedList;
        }

        // Get Sold table data
        public List<SoldModel> GetSoldList()
        {
            List<SoldModel> soldList = new List<SoldModel>();
            _mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand(
                "SELECT * FROM sold_item", _mySqlConnection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    soldList.Add(new SoldModel
                    {
                        SoldItemID = reader.GetInt32("SoldItemID"),
                        ItemName = reader.GetString("ItemName"),
                        ItemBrand = reader.GetString("ItemBrand"),
                        QuantitySold = reader.GetInt32("QuantitySold"),
                        Price = reader.GetInt32("Price"),
                        DateSold = reader.GetDateTime("DateSold")

                    });
                }
            }
            _mySqlConnection.Close();
            return soldList;
        }

        // Get Sold and Ordered table data
        public List<SoldOrderedModel> GetSoldOrderedList()
        {
            List<SoldOrderedModel> soldOrderedList = new List<SoldOrderedModel>();
            _mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand(
                "SELECT * FROM sold_oredered_table", _mySqlConnection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    soldOrderedList.Add(new SoldOrderedModel
                    {
                        sold_item_SoldItemID = reader.GetInt32("sold_item_SoldItemID"),
                        ordered_item_OrderedItemId = reader.GetInt32("ordered_item_OrderedItemId")

                    });
                }
            }
            _mySqlConnection.Close();
            return soldOrderedList;
        }



        //Retrieve the Two Ids
        public List<int> GetSoldItemIDs()
        {
            List<int> soldItemIDs = new List<int>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT SoldItemID FROM sold_item";

                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            soldItemIDs.Add(reader.GetInt32("SoldItemID"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving SoldItemIDs: " + ex.Message);
                }
            }

            return soldItemIDs;
        }

        public List<int> GetOrderedItemIDs()
        {
            List<int> orderedItemIDs = new List<int>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT OrderedItemId FROM ordered_item";

                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderedItemIDs.Add(reader.GetInt32("OrderedItemId"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving OrderedItemIDs: " + ex.Message);
                }
            }

            return orderedItemIDs;
        }

        // Insert retrieved IDs into sold_oredered_table
        public bool InsertIntoSoldOrderedTable(List<int> soldItemIDs, List<int> orderedItemIDs)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    foreach (var soldItemID in soldItemIDs)
                    {
                        foreach (var orderedItemID in orderedItemIDs)
                        {
                            string query = "INSERT INTO sold_oredered_table (sold_item_SoldItemID, ordered_item_OrderedItemId) VALUES (@sold_item_SoldItemID, @ordered_item_OrderedItemId)";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@sold_item_SoldItemID", soldItemID);
                            command.Parameters.AddWithValue("@ordered_item_OrderedItemId", orderedItemID);
                            command.ExecuteNonQuery();
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting into sold_oredered_table: " + ex.Message);
                    return false;
                }
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