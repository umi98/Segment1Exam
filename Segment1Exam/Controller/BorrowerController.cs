using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Controller;

internal class BorrowerController
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";
    static SqlConnection connection;

    public static void BorrowerView()
    {
        bool endSection = false;
        bool iid;
        int book_id, fine, staff_id;
        string borr_id;
        DateTime borr_from, borr_to, return_date;
        Console.Clear();
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel Borrower");
            Console.WriteLine("1. Lihat semua");
            Console.WriteLine("2. New borrowing");
            Console.WriteLine("3. Return book");
            Console.WriteLine("4. Edit");
            Console.WriteLine("5. Hapus");
            Console.WriteLine("6. Kembali ke menu utama");
            Console.Write("Pilih opsi: ");
            string opsi = Console.ReadLine();

            switch (opsi)
            {
                case "1":
                    BorrowerList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Add new borrowing\n");
                    Console.Write("Borrower ID: ");
                    borr_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    book_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Borrowed From: ");
                    borr_from = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Borrowed To: ");
                    borr_to = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Issued By: ");
                    staff_id = Convert.ToInt32(Console.ReadLine());
                    BorrowerNew(borr_id, book_id, borr_from, borr_to, staff_id);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Book Returning\n");
                    Console.Write("Borrower ID: ");
                    borr_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    book_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Borrowed From: ");
                    borr_from = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Borrowed To: ");
                    borr_to = Convert.ToDateTime(Console.ReadLine());
                    // method to get record
                    Console.Write("Return Date: ");
                    return_date = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Fine: ");
                    fine = Convert.ToInt32(Console.ReadLine());
                    BorrowerReturn(borr_id, book_id, borr_from, return_date, fine);
                    Console.ReadKey();
                    break;
                case "4":
                    /*Console.WriteLine("Edit borrower\n");
                    Console.Write("Id: ");
                    iid = int.TryParse(Console.ReadLine(), out id);
                    if (GetBorrowerById(id))
                    {
                        Console.Write("Name: ");
                        name = Console.ReadLine();
                        Console.Write("Phone: ");
                        phone = Console.ReadLine();
                        Console.Write("Address: ");
                        address = Console.ReadLine();
                        Console.Write("Borrow ID: ");
                        borrow_id = Console.ReadLine();
                        BorrowerEdit(id, name, phone, address, borrow_id);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
                    }
                    Console.ReadKey();*/
                    break;
                case "5":
                    Console.Clear();
                    Console.Write("Borrower ID: ");
                    borr_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    book_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Borrowed From: ");
                    borr_from = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Borrowed To: ");
                    borr_to = Convert.ToDateTime(Console.ReadLine());
                    if (GetBorrowerById(borr_id, book_id, borr_from))
                    {
                        BorrowerDelete(borr_id, book_id, borr_from);
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

    // Get all borrow
    public static void BorrowerList()
    {
        connection = new SqlConnection(ConnectionString);

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM borrow";

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            Console.WriteLine("Daftar borrow");
            while (reader.Read())
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Borrower ID: " + reader[0]);
                Console.WriteLine("Book ID: " + reader[1]);
                Console.WriteLine("Borrowed from: " + reader[2]);
                Console.WriteLine("Borrowed to: " + reader[3]);
                if (reader[3] == null)
                {
                    Console.WriteLine("STILL IN BORROWING PERIOD");
                }
                else
                {
                    Console.WriteLine("Return date: " + reader[4]);
                    Console.WriteLine("Fine: " + reader[5]);
                    Console.WriteLine("Issued by: " + reader[6]);
                }
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

    // Get borrow by ID
    public static void BorrowerById(string borr_id, int book_id, DateTime borr_from)
    {
        connection = new SqlConnection(ConnectionString);

        // Melakukan SQL query untuk mencari id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM borrow" +
            " WHERE borrowed_to = @borr_id AND book_id = @book_id AND borrowed_from = @borr_from";
        // Tambah parameter
        command.Parameters.Add("@borr_id", SqlDbType.VarChar);
        command.Parameters["@borr_id"].Value = borr_id;
        command.Parameters.Add("@book_id", SqlDbType.Int);
        command.Parameters["@book_id"].Value = book_id;
        command.Parameters.Add("@borr_from", SqlDbType.Date);
        command.Parameters["@borr_from"].Value = borr_from;

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        // Tampilkan data jika ditemukan dan respon jika tidak ada data
        if (reader.HasRows)
        {
            Console.WriteLine("Hasil");
            while (reader.Read())
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Borrower ID: " + reader[0]);
                Console.WriteLine("Book ID: " + reader[1]);
                Console.WriteLine("Borrowed from: " + reader[2]);
                Console.WriteLine("Borrowed to: " + reader[3]);
                if (reader[3] == null)
                {
                    Console.WriteLine("STILL IN BORROWING PERIOD");
                }
                else
                {
                    Console.WriteLine("Return date: " + reader[4]);
                    Console.WriteLine("Fine: " + reader[5]);
                    Console.WriteLine("Issued by: " + reader[6]);
                }
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


    // Check if borrow exist
    public static bool GetBorrowerById(string borr_id, int book_id, DateTime borr_from)
    {
        connection = new SqlConnection(ConnectionString);

        // Query untuk mencari berdasarkan id
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM borrow where borrower_id = @id," +
            " book_id = @book_id," +
            " and borrowed_from = @borr_from";
        command.Parameters.Add("@borr_id", SqlDbType.VarChar);
        command.Parameters["@borr_id"].Value = borr_id;
        command.Parameters.Add("@book_id", SqlDbType.Int);
        command.Parameters["@book_id"].Value = book_id;
        command.Parameters.Add("@borr_from", SqlDbType.Date);
        command.Parameters["@borr_from"].Value = borr_from;

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

    // Add new borrow
    public static void BorrowerNew(string borr_id, int book_id, DateTime borr_from, DateTime borr_to, int staff_id)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        // Melakukan penerapan rollback jika terjadi kesalahan
        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO borrow VALUES" +
                " (@borr_id, @book_id, @borr_from, @borr_to, @staff_id)";
            command.Transaction = transaction;

            command.Parameters.Add("@borr_id", SqlDbType.VarChar);
            command.Parameters["@borr_id"].Value = borr_id;
            command.Parameters.Add("@book_id", SqlDbType.Int);
            command.Parameters["@book_id"].Value = book_id;
            command.Parameters.Add("@borr_from", SqlDbType.Date);
            command.Parameters["@borr_from"].Value = borr_from;
            command.Parameters.Add("@borr_to", SqlDbType.Date);
            command.Parameters["@borr_to"].Value = borr_to;
            command.Parameters.Add("@staff_id", SqlDbType.Int);
            command.Parameters["@staff_id"].Value = staff_id;

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

    // Edit existing borrow
    public static void BorrowerReturn(string borr_id, int book_id, DateTime borr_from, DateTime return_date, int fine)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        // Menerapkan operasi untuk rollback jika terjadi kesalahan
        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE borrow SET return_date = @return_date, fine = @fine" +
                " WHERE borrowed_to = @borr_id AND book_id = @book_id AND borrowed_from = @borr_from";
            command.Transaction = transaction;

            command.Parameters.Add("@borr_id", SqlDbType.VarChar);
            command.Parameters["@borr_id"].Value = borr_id;
            command.Parameters.Add("@book_id", SqlDbType.Int);
            command.Parameters["@book_id"].Value = book_id;
            command.Parameters.Add("@borr_from", SqlDbType.Date);
            command.Parameters["@borr_from"].Value = borr_from;
            command.Parameters.Add("@return_date", SqlDbType.Date);
            command.Parameters["@return_date"].Value = return_date;
            command.Parameters.Add("@fine", SqlDbType.Int);
            command.Parameters["@fine"].Value = fine;

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

    // Delete borrow
    public static void BorrowerDelete(string borr_id, int book_id, DateTime borr_from)
    {
        connection = new SqlConnection(ConnectionString);

        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM borrow" +
                " WHERE borrowed_to = @borr_id AND book_id = @book_id AND borrowed_from = @borr_from";
            command.Transaction = transaction;

            command.Parameters.Add("@borr_id", SqlDbType.VarChar);
            command.Parameters["@borr_id"].Value = borr_id;
            command.Parameters.Add("@book_id", SqlDbType.Int);
            command.Parameters["@book_id"].Value = book_id;
            command.Parameters.Add("@borr_from", SqlDbType.Date);
            command.Parameters["@borr_from"].Value = borr_from;

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
