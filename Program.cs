using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_2
{
    internal class Program
    {
        public static Connect conn = new Connect();
        public static void GetAllData()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM `ember`";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            MySqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            do
            {
                var ember = new
                {
                    Id = dr.GetInt32(0),
                    UserName = dr.GetString(1),
                    Email = dr.GetInt32(2),
                    Password = dr.GetInt32(3),
                    RegistrationTime = dr.GetDateTime(4),
                    UpdatedTime = dr.GetDateTime(5)
                };

                Console.WriteLine($"Felhasználói adatok: {ember.UserName},{ember.Email},{ember.Password}, {ember.RegistrationTime}");
            }
            while (dr.Read());



            dr.Close();



            conn.Connection.Close();
        }

        public static void AddNewUser(string username, string email, string password)
        {
            try
            {

                conn.Connection.Open();

                string sql = $"INSERT INTO `ember`(`UserName`, `Email`, `Password`) VALUES ('{username}','{email}','{password}')";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
                cmd.ExecuteNonQuery();

                conn.Connection.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        public static void DeleteUser(int id)
        {
            conn.Connection.Open();

            string sql = $"DELETE FROM `ember` WHERE `Id` = {id};";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }

        public static void UpdateUser(int id, string name, string email, string password)
        {
            conn.Connection.Open();

            string sql = $"UPDATE `ember` SET `UserName`='{name}',`Email`= '{email}',`Password`= '{password}' WHERE `ID` = {id};";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }

        static void Main(string[] args)
        {

            try
            {
                Console.Write("Kérem adja meg a felhasználói azonosítót: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Kérem az új nevet: ");
                string username = Console.ReadLine();
                Console.Write("Kérem az új emailt: ");
                string email = Console.ReadLine();
                Console.Write("Kérem az új jelszót: ");
                string password = Console.ReadLine();

                UpdateUser(id, username, email, password);

                Console.WriteLine("Sikeres frissítés!");
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }


            try
            {
                Console.Write("Kérem a felhasználói azonosítót: ");
                int azon = int.Parse(Console.ReadLine());
                DeleteUser(azon);
                Console.Write("Sikeres Törlés");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //GetAllData();
            try
            {
                Console.Write("Kérem a felhasználó nevét: ");
                string name = Console.ReadLine();
                Console.Write("Kérem a felhasználó emailjét: ");
                string height = Console.ReadLine();
                Console.Write("Kérem a felhasználó jelszavát: ");
                string weight = Console.ReadLine();

                AddNewUser(name, height, weight);
                Console.WriteLine("Sikeres Regisztráció!");
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
