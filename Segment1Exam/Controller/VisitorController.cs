using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Controller;

internal class VisitorController
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";
    static SqlConnection connection;

    // Ini bagian CRUD tabel visitor
    public static void VisitorView()
    {
        bool endSection = false;
        bool iid;
        int id;
        string name, phone, address, borrow_id;
        Console.Clear();
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel Visitor");
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
                    VisitorList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Cari visitor berdasar id\n");
                    Console.Write("Id: ");
                    iid = int.TryParse(Console.ReadLine(), out id);
                    VisitorById(id);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Add new visitor\n");
                    Console.Write("Name: ");
                    name = Console.ReadLine();
                    Console.Write("Phone: ");
                    phone = Console.ReadLine();
                    Console.Write("Address: ");
                    address = Console.ReadLine();
                    Console.Write("Borrow ID: ");
                    borrow_id = Console.ReadLine();
                    VisitorNew(name, phone, address, borrow_id);
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Edit visitor\n");
                    Console.Write("Id: ");
                    // id pada tabel region adalah integer, perlu dicek jika tipe data yg dimasukkan benar
                    iid = int.TryParse(Console.ReadLine(), out id);
                    if (GetVisitorById(id))
                    {
                        Console.Write("Name: ");
                        name = Console.ReadLine();
                        Console.Write("Phone: ");
                        phone = Console.ReadLine();
                        Console.Write("Address: ");
                        address = Console.ReadLine();
                        Console.Write("Borrow ID: ");
                        borrow_id = Console.ReadLine();
                        VisitorEdit(id, name, phone, address, borrow_id);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
                    }
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Delete a visitor\n");
                    Console.Write("Id: ");
                    // id pada tabel region adalah integer, perlu dicek jika tipe data yg dimasukkan benar
                    iid = int.TryParse(Console.ReadLine(), out id);
                    if (GetVisitorById(id))
                    {
                        VisitorDelete(id);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
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

    // Get all visitor
    public static void VisitorList()
    {
        connection = new SqlConnection(ConnectionString);

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM visitor";

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            Console.WriteLine("Daftar visitor");
            while (reader.Read())
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Id: " + reader[0]);
                Console.WriteLine("Name: " + reader[1]);
                Console.WriteLine("Phone: " + reader[2]);
                Console.WriteLine("Address: " + reader[3]);
                Console.WriteLine("Borrow ID: " + reader[4]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("Tidak ada data");
        }
        Console.ReadKey();
        reader.Close();
        connection.Close();
    }

    // Get visitor by ID
    public static void VisitorById(int id)
    {
        connection = new SqlConnection(ConnectionString);

        // Melakukan SQL query untuk mencari id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM visitor where id = @id";
        // Tambah parameter
        command.Parameters.Add("@id", SqlDbType.Int);
        command.Parameters["@id"].Value = id;

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        // Tampilkan data jika ditemukan dan respon jika tidak ada data
        if (reader.HasRows)
        {
            Console.WriteLine("Hasil");
            while (reader.Read())
            {
                Console.WriteLine("Id: " + reader[0]);
                Console.WriteLine("Name: " + reader[1]);
                Console.WriteLine("Phone: " + reader[2]);
                Console.WriteLine("Address: " + reader[3]);
                Console.WriteLine("Borrow ID: " + reader[4]);
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

    // Check if visitor exist
    public static bool GetVisitorById(int id)
    {
        connection = new SqlConnection(ConnectionString);

        // Query untuk mencari berdasarkan id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM visitor where id = @id";
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

    // Add new visitor
    public static void VisitorNew(string name, string phone, string address, string borrow_id)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        // Melakukan penerapan rollback jika terjadi kesalahan
        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO visitor VALUES (@name, @phone, @address, @borrow_id)";
            command.Transaction = transaction;

            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = name;
            command.Parameters.Add("@phone", SqlDbType.VarChar);
            command.Parameters["@phone"].Value = phone;
            command.Parameters.Add("@address", SqlDbType.VarChar);
            command.Parameters["@address"].Value = address;
            command.Parameters.Add("@borrow_id", SqlDbType.VarChar);
            command.Parameters["@borrow_id"].Value = borrow_id;

            int result = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (result > 0)
            {
                Console.WriteLine("Data berhasil ditambah");
            }
            else
            {
                Console.WriteLine("Data gagal ditambah");
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

    // Edit existing visitor
    public static void VisitorEdit(int id, string name, string phone, string address, string borrow_id)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        // Menerapkan operasi untuk rollback jika terjadi kesalahan
        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE visitor SET name = @name, phone = @phone, address = @address, borrow_id = @borrow_id WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id;
            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = name;
            command.Parameters.Add("@phone", SqlDbType.Bit);
            command.Parameters["@phone"].Value = phone;
            command.Parameters.Add("@address", SqlDbType.VarChar);
            command.Parameters["@address"].Value = address;
            command.Parameters.Add("@borrow_id", SqlDbType.VarChar);
            command.Parameters["@borrow_id"].Value = borrow_id;

            int result = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (result > 0)
            {
                Console.WriteLine("Data berhasil diubah");
            }
            else
            {
                Console.WriteLine("Data gagal diubah, pastikan id berupa angka");
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

    // Delete visitor
    public static void VisitorDelete(int id)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM visitor WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id;

            int result = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (result > 0)
            {
                Console.WriteLine("Data berhasil dihapus");
            }
            else
            {
                Console.WriteLine("Data gagal dihapus, pastikan id berupa angka");
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
