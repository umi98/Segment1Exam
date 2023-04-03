using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Controller;

internal class BookController
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";
    static SqlConnection connection;

    // Ini bagian CRUD tabel book

    public static void BookView()
    {
        bool endSection = false;
        int id, no_act, no_curr;
        bool iid;
        string title, author;
        Console.Clear();
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel Book");
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
                    BookList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Get book by ID\n");
                    Console.Write("ID: ");
                    iid = int.TryParse(Console.ReadLine(), out id);
                    BookById(id);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Add new Book\n");
                    Console.Write("Title: ");
                    title = Console.ReadLine();
                    Console.Write("Author: ");
                    author = Console.ReadLine();
                    Console.Write("No. of actual copies: ");
                    no_act = Convert.ToInt32(Console.ReadLine());
                    Console.Write("No. of current copies: ");
                    no_curr = Convert.ToInt32(Console.ReadLine());
                    BookNew(title, author, no_act, no_curr);
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Edit Book\n");
                    Console.Write("Id: ");
                    
                    iid = int.TryParse(Console.ReadLine(), out id);
                    if (GetBookById(id))
                    {
                        Console.Write("Title: ");
                        title = Console.ReadLine();
                        Console.Write("Author: ");
                        author = Console.ReadLine();
                        Console.Write("No. of actual copies: ");
                        no_act = Convert.ToInt32(Console.ReadLine());
                        Console.Write("No. of current copies: ");
                        no_curr = Convert.ToInt32(Console.ReadLine());
                        BookEdit(id, title, author, no_act, no_curr);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
                    }
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Delete a Book\n");
                    Console.Write("ID: ");
                    iid = int.TryParse(Console.ReadLine(), out id);
                    if (GetBookById(id))
                    {
                        BookDelete(id);
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

    // Get all Book
    public static void BookList()
    {
        connection = new SqlConnection(ConnectionString);

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM book";

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            Console.WriteLine("List of Book");
            while (reader.Read())
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("ID: " + reader[0]);
                Console.WriteLine("Title: " + reader[1]);
                Console.WriteLine("Author: " + reader[2]);
                Console.WriteLine("No. of actual copies: " + reader[3]);
                Console.WriteLine("No. of current copies: " + reader[4]);
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

    // Get Book by ID
    public static void BookById(int id)
    {
        connection = new SqlConnection(ConnectionString);

        // Melakukan SQL query untuk mencari id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM book where id = @id";
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
                Console.WriteLine("Id: " + reader[0]);
                Console.WriteLine("Title: " + reader[1]);
                Console.WriteLine("Author: " + reader[2]);
                Console.WriteLine("No. of actual copies: " + reader[3]);
                Console.WriteLine("No. of current copies: " + reader[4]);
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

    // Check if Book exist
    public static bool GetBookById(int id)
    {
        connection = new SqlConnection(ConnectionString);

        // Query untuk mencari berdasarkan id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM book where id = @id";
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

    // Add new Book
    public static void BookNew(string title, string author, int no_act, int no_curr)
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
                " (@title, @author, @no_act, @no_curr)";
            command.Transaction = transaction;

            command.Parameters.Add("@title", SqlDbType.VarChar);
            command.Parameters["@title"].Value = title;
            command.Parameters.Add("@author", SqlDbType.Bit);
            command.Parameters["@author"].Value = author;
            command.Parameters.Add("@no_act", SqlDbType.Int);
            command.Parameters["@no_act"].Value = no_act;
            command.Parameters.Add("@no_curr", SqlDbType.Int);
            command.Parameters["@no_curr"].Value = no_curr;

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

    // Edit existing Book
    public static void BookEdit(int id, string title, string author, int no_act, int no_curr)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        // Menerapkan operasi untuk rollback jika terjadi kesalahan
        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Book" +
                " SET title = @title, author = @author, no_of_copies_actual = @no_act," +
                " no_of_copies_current = @no_curr" +
                " WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id;
            command.Parameters.Add("@title", SqlDbType.VarChar);
            command.Parameters["@title"].Value = title;
            command.Parameters.Add("@author", SqlDbType.Bit);
            command.Parameters["@author"].Value = author;
            command.Parameters.Add("@no_act", SqlDbType.Int);
            command.Parameters["@no_act"].Value = no_act;
            command.Parameters.Add("@no_curr", SqlDbType.Int);
            command.Parameters["@no_curr"].Value = no_curr;

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

    // Delete book
    public static void BookDelete(int id)
    {
        connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM book WHERE id = @id";
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
