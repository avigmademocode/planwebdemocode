using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using MyProject.Models;

namespace MyProject.Data
{

    public class users_citiesData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[users_citiesSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static List<cities> List()
        {
            List<cities> citiesList = new List<cities>();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            String selectProcedure = "[users_citiesSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                cities cities = new cities();
                while (reader.Read())
                {
                    cities = new cities();
                    cities.Name = System.Convert.ToString(reader["Name"]);
                    cities.CountryId = Convert.ToInt32(reader["CountryId"]);
                    citiesList.Add(cities);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return citiesList;
            }
            finally
            {
                connection.Close();
            }
            return citiesList;
        }

    }

    public class users_countriesData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[users_countriesSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static List<countries> List()
        {
            List<countries> countriesList = new List<countries>();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            String selectProcedure = "[users_countriesSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                countries countries = new countries();
                while (reader.Read())
                {
                    countries = new countries();
                    countries.id = System.Convert.ToInt32(reader["id"]);
                    countries.Name = Convert.ToString(reader["Name"]);
                    countriesList.Add(countries);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return countriesList;
            }
            finally
            {
                connection.Close();
            }
            return countriesList;
        }

    }

}

 
