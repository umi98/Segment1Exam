using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Controller;

internal class StaffController
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";
    static SqlConnection connection;

    // Ini bagian CRUD tabel staff
    public static void StaffView()
    {
        bool endSection = false;
        int id;
        bool iid, admin;
        string name, role;
        Console.Clear();
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel staff");
            Console.WriteLine("1. Lihat semua");
            Console.WriteLine("2. Lihat berdasar id");
            Console.WriteLine("3. Tambah");
            Console.WriteLine("4. Edit");
            Console.WriteLine("5. Hapus");
            Console.WriteLine("6. Kembali ke menu utama");
            Console.Write("Pilih opsi: ");
            string opsi = Console.ReadLine();

            switch (opsi)
            {
                case "1":
                    StaffList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Get staff by ID\n");
                    Console.Write("ID: ");
                    iid = int.TryParse(Console.ReadLine(), out id);
                    StaffById(id);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Add new Staff\n");
                    Console.Write("Name: ");
                    name = Console.ReadLine();
                    Console.Write("Admin (1:Yes, 0:No): ");
                    admin = Convert.ToBoolean(Console.ReadLine());
                    Console.Write("Role: ");
                    role = Console.ReadLine();
                    StaffNew(name, admin, role);
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Edit staff\n");
                    Console.Write("ID: ");
                    iid = int.TryParse(Console.ReadLine(), out id);
                    if (GetStaffById(id))
                    {
                        Console.Write("Name: ");
                        name = Console.ReadLine();
                        Console.Write("Admin (1:Yes, 0:No): ");
                        admin = Convert.ToBoolean(Console.ReadLine());
                        Console.Write("Role: ");
                        role = Console.ReadLine();
                        StaffEdit(id, name, admin, role);
                    }
                    else
                    {
                        Console.WriteLine("ID not available, check the list.");
                    }
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Delete a staff\n");
                    Console.Write("ID: ");
                    iid = int.TryParse(Console.ReadLine(), out id);
                    if (GetStaffById(id))
                    {
                        StaffDelete(id);
                    }
                    else
                    {
                        Console.WriteLine("ID not available, check the list.");
                    }
                    Console.ReadKey();
                    break;
                case "6":
                    endSection = true;
                    break;
                default:
                    break;
            }
            Console.Clear();
        }
    }

    // Get all staff
    public static void StaffList()
    {
        connection = new SqlConnection(ConnectionString);

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM staff";

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            Console.WriteLine("List of Staff");
            while (reader.Read())
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("ID: " + reader[0]);
                Console.WriteLine("Name: " + reader[1]);
                Console.WriteLine("Administrator: " + reader[2]);
                Console.WriteLine("Role: " + reader[3]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("No Data");
        }
        Console.ReadKey();
        reader.Close();
        connection.Close();
    }

    // Get staff by ID
    public static void StaffById(int id)
    {
        connection = new SqlConnection(ConnectionString);

        // Melakukan SQL query untuk mencari id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM staff where id = @id";
        // Tambah parameter
        command.Parameters.Add("@id", SqlDbType.Int);
        command.Parameters["@id"].Value = id;

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        // Tampilkan data jika ditemukan dan respon jika tidak ada data
        if (reader.HasRows)
        {
            Console.WriteLine("Result");
            while (reader.Read())
            {
                Console.WriteLine("ID: " + reader[0]);
                Console.WriteLine("Name: " + reader[1]);
                Console.WriteLine("Admin stat: " + reader[2]);
                Console.WriteLine("Role: " + reader[3]);
            }
        }
        else
        {
            Console.WriteLine("Data not found!");
        }
        Console.ReadKey();
        reader.Close();
        connection.Close();
    }

    // Check if staff exist
    public static bool GetStaffById(int id)
    {
        connection = new SqlConnection(ConnectionString);

        // Query untuk mencari berdasarkan id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM staff where id = @id";
        command.Parameters.Add("@id", SqlDbType.Int);
        command.Parameters["@id"].Value = id;

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        // Jika ditemukan kembalikan nilai true, jika tidak kembalikan nilai false
        if (reader.HasRows)
        {
            reader.Close();
            connection.Close();
            return true;
        }
        else
        {
            reader.Close();
            connection.Close();
            return false;
        }
    }

    // Add new staff
    public static void StaffNew(string name, bool admin, string role)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        // Melakukan penerapan rollback jika terjadi kesalahan
        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO region VALUES" +
                " (@name, @admin, @role)";
            command.Transaction = transaction;

            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = name;
            command.Parameters.Add("@admin", SqlDbType.Bit);
            command.Parameters["@admin"].Value = admin;
            command.Parameters.Add("@role", SqlDbType.VarChar);
            command.Parameters["@role"].Value = role;

            int result = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (result > 0)
            {
                Console.WriteLine("Success to add data");
            }
            else
            {
                Console.WriteLine("Failed!");
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            try
            {
                transaction.Rollback();
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
            }
        }
        Console.ReadKey();
    }

    // Edit existing staff
    public static void StaffEdit(int id, string name, bool admin, string role)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        // Menerapkan operasi untuk rollback jika terjadi kesalahan
        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE staff" +
                " SET name = @name, admin = @admin, role = @role" +
                " WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id;
            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = name;
            command.Parameters.Add("@admin", SqlDbType.Bit);
            command.Parameters["@admin"].Value = admin;
            command.Parameters.Add("@role", SqlDbType.VarChar);
            command.Parameters["@role"].Value = role;

            int result = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (result > 0)
            {
                Console.WriteLine("Data has edited");
            }
            else
            {
                Console.WriteLine("Failed to edit data");
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            try
            {
                transaction.Rollback();
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
            }
        }
    }

    // Delete staff
    public static void StaffDelete(int id)
    {
        connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM staff WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id;

            int result = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (result > 0)
            {
                Console.WriteLine("Data deleted");
            }
            else
            {
                Console.WriteLine("Failed to delete data");
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            try
            {
                transaction.Rollback();
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
            }
        }
    }

}
