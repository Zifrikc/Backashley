using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MVCCRUD.Models;

namespace MVCCRUD.Data
{
    public class databaseContext
    {
        private readonly string _connectionString;

        public databaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
        }

        public List<Worker> GetWorkers()
        {
            var workers = new List<Worker>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, fullName, lastName, typeDocument, numberDocument, birthDate, entryDate, numberChildren FROM Workers";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var worker = new Worker
                            {
                                id = reader.GetInt32(0),
                                fullName = reader.GetString(1),
                                lastName = reader.GetString(2),
                                typeDocument = reader.GetString(3),
                                numberDocument = reader.GetString(4),
                                birthDate = reader.GetDateTime(5),
                                entryDate = reader.GetDateTime(6),
                                numberChildren = reader.GetInt32(7)
                            };
                            workers.Add(worker);
                        }
                    }
                }
            }

            return workers;
        }

        public Worker GetWorker(int id)
        {
            Worker worker = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, FullName, LastName, TypeDocument, NumberDocument, BirthDate, EntryDate, NumberChildren FROM Workers WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            worker = new Worker
                            {
                                id = reader.GetInt32(0),
                                fullName = reader.GetString(1),
                                lastName = reader.GetString(2),
                                typeDocument = reader.GetString(3),
                                numberDocument = reader.GetString(4),
                                birthDate = reader.GetDateTime(5),
                                entryDate = reader.GetDateTime(6),
                                numberChildren = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }

            return worker;
        }

        public void CreateWorker(Worker worker)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Workers (FullName, LastName, TypeDocument, NumberDocument, BirthDate, EntryDate, NumberChildren) VALUES (@FullName, @LastName, @TypeDocument, @NumberDocument, @BirthDate, @EntryDate, @NumberChildren)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", worker.fullName);
                    command.Parameters.AddWithValue("@LastName", worker.lastName);
                    command.Parameters.AddWithValue("@TypeDocument", worker.typeDocument);
                    command.Parameters.AddWithValue("@NumberDocument", worker.numberDocument);
                    command.Parameters.AddWithValue("@BirthDate", worker.birthDate);
                    command.Parameters.AddWithValue("@EntryDate", worker.entryDate);
                    command.Parameters.AddWithValue("@NumberChildren", worker.numberChildren);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateWorker(int id, Worker worker)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Workers SET FullName = @FullName, LastName = @LastName, TypeDocument = @TypeDocument, NumberDocument = @NumberDocument, BirthDate = @BirthDate, EntryDate = @EntryDate, NumberChildren = @NumberChildren WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@FullName", worker.fullName);
                    command.Parameters.AddWithValue("@LastName", worker.lastName);
                    command.Parameters.AddWithValue("@TypeDocument", worker.typeDocument);
                    command.Parameters.AddWithValue("@NumberDocument", worker.numberDocument);
                    command.Parameters.AddWithValue("@BirthDate", worker.birthDate);
                    command.Parameters.AddWithValue("@EntryDate", worker.entryDate);
                    command.Parameters.AddWithValue("@NumberChildren", worker.numberChildren);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteWorker(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Workers WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
