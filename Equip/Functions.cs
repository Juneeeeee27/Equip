using System;
using System.Data;
using System.Data.SqlClient;

namespace Equip
{
    class Functions
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private string ConStr;

        // Constructor to initialize the connection string and set up the connection
        public Functions()
        {
            ConStr = @"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True";
            con = new SqlConnection(ConStr);
        }

        // Method to execute a query that modifies data (INSERT, UPDATE, DELETE)
        public int SetData(string query)
        {
            int affectedRows = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (cmd = new SqlCommand(query, con))
                {
                    affectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle the error or log it as needed
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return affectedRows;
        }

        // Method to execute a query that retrieves data (SELECT)
        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // Handle the error or log it as needed
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return dt;
        }
    }
}
