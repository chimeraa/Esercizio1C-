using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace VehicleRental
{
    public class Vehicle
    {
        public string Plate { get; set; }
        public string Model { get; set; }
        public decimal DailyRate { get; set; }
        public List<Rental> Rentals { get; set; }

        public Vehicle(string plate, string model, decimal dailyRate)
        {
            Plate = plate;
            Model = model;
            DailyRate = dailyRate;
            Rentals = new List<Rental>();
        }
    }

    public class Rental
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfDays { get; set; }
        public decimal Cost { get; set; }
        public string CustomerID { get; set; }
        public Vehicle Vehicle { get; set; }
        public Customer Customer { get; set; }

        public Rental(int id, DateTime startDate, int numberOfDays, string customerID, Vehicle vehicle, Customer customer)
        {
            ID = id;
            StartDate = startDate;
            NumberOfDays = numberOfDays;
            CustomerID = customerID;
            Vehicle = vehicle;
            Customer = customer;
            Cost = numberOfDays * vehicle.DailyRate;
        }
    }

    public class Customer
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Rental> Rentals { get; set; }

        public Customer(string id, string firstName, string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Rentals = new List<Rental>();
        }
    }

    public class RentalManager
    {
        private List<Customer> customers;
        private List<Vehicle> vehicles;
        private string connectionString;

        public RentalManager(string connectionString)
        {
            this.connectionString = connectionString;
            customers = new List<Customer>();
            vehicles = new List<Vehicle>();
        }

        public void RegisterCustomer(string CustomerID, string firstName, string lastName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO clienti (codice_fiscale, nome, cognome) VALUES (@id, @firstName, @lastName)", connection);
                command.Parameters.AddWithValue("@id", CustomerID);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.ExecuteNonQuery();
            }
        }
        public Rental GetRentals(int id)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT noleggi.*, veicoli.*, clienti.* FROM noleggi INNER JOIN veicoli ON noleggi.targa = veicoli.targa INNER JOIN clienti ON noleggi.codice_fiscale = clienti.codice_fiscale WHERE noleggi.id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        using (SqlDataReader reader = command.ExecuteReader())
        {
           
                int rentalID = (int)reader["id"];
                DateTime startDate = (DateTime)reader["data_inizio"];
                int numberOfDays = (int)reader["numero_giorni"];
                string customerID = (string)reader["codice_fiscale"];
                string plate = (string)reader["targa"];
                string model = (string)reader["modello"];
                decimal dailyRate = (decimal)reader["tariffa_giornaliera"];
                string firstName = (string)reader["nome"];
                string lastName = (string)reader["cognome"];

                Vehicle vehicle = new Vehicle(plate, model, dailyRate);
                Customer customer = new Customer(customerID, firstName, lastName);
                Rental rental = new Rental(rentalID, startDate, numberOfDays, customerID, vehicle, customer);
                return rental;
        }
    }

 

         void AddRental(DateTime startDate, int numberOfDays, string idInput, string plate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        SqlCommand command = new SqlCommand("INSERT INTO noleggi (data_inizio, numero_giorni, codice_fiscale, targa) VALUES (@startDate, @numberOfDays, @customerID, @plate)", connection);
        command.Parameters.AddWithValue("@startDate", startDate);
        command.Parameters.AddWithValue("@numberOfDays", numberOfDays);
        command.Parameters.AddWithValue("@customerID", idInput);
        command.Parameters.AddWithValue("@plate", plate);
        command.ExecuteNonQuery();
    }
        }
    }
internal decimal GetTotalRentalCost(string customerID)
{
    decimal totalCost = 0;

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT SUM(costo) FROM noleggi WHERE codice_fiscale = @customerID", connection);
        command.Parameters.AddWithValue("@customerID", customerID);

        totalCost = (decimal)command.ExecuteScalar();
    }

    return totalCost;
}

        internal void AddRental(DateTime startDate, int numberOfDays, string idInput, string plateInput)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        SqlCommand command = new SqlCommand("INSERT INTO noleggi (data_inizio, numero_giorni, codice_fiscale_cliente, targa) VALUES (@startDate, @numberOfDays, @customerID, @plate)", connection);
        command.Parameters.AddWithValue("@startDate", startDate);
        command.Parameters.AddWithValue("@numberOfDays", numberOfDays);
        command.Parameters.AddWithValue("@customerID", idInput);
        command.Parameters.AddWithValue("@plate", plateInput);
        command.ExecuteNonQuery();
    }
        }
        }
    }
