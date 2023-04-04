using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Repository;

internal class BorrowerRepository
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";

    // Get all borrow
    public DataTable Select()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM borrow";
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            connection.Open();
            adapter.Fill(dt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();
        return dt;
    }

    // Get borrow by ID
    public DataTable SelectById(BorrowerModel bm)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM borrow" +
                " WHERE borrowed_to = @borr_id AND book_id = @book_id AND borrowed_from = @borr_from";
            // Tambah parameter
            command.Parameters.Add("@borr_id", SqlDbType.VarChar);
            command.Parameters["@borr_id"].Value = bm.Borrower_id;
            command.Parameters.Add("@book_id", SqlDbType.Int);
            command.Parameters["@book_id"].Value = bm.Book_id;
            command.Parameters.Add("@borr_from", SqlDbType.Date);
            command.Parameters["@borr_from"].Value = bm.Borrowed_from;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            connection.Open();
            adapter.Fill(dt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();
        return dt;
    }


    // Check if borrow exist
    public bool CheckBorrower(BorrowerModel bm)
    {
        bool result = false;
        if (SelectById(bm).Columns.Count > 0)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    // Add new borrow
    public bool BorrowerNew(BorrowerModel bm)
    {
        bool result = false;

        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO borrow" +
                " (borrower_id, book_id, borrowed_from, borrowed_to, issued_by) VALUES" +
                " (@borr_id, @book_id, @borr_from, @borr_to, @staff_id)";
            command.Transaction = transaction;

            command.Parameters.Add("@borr_id", SqlDbType.VarChar);
            command.Parameters["@borr_id"].Value = bm.Borrower_id;
            command.Parameters.Add("@book_id", SqlDbType.Int);
            command.Parameters["@book_id"].Value = bm.Book_id;
            command.Parameters.Add("@borr_from", SqlDbType.Date);
            command.Parameters["@borr_from"].Value = bm.Borrowed_from;
            command.Parameters.Add("@borr_to", SqlDbType.Date);
            command.Parameters["@borr_to"].Value = bm.Borrowed_to;
            command.Parameters.Add("@staff_id", SqlDbType.Int);
            command.Parameters["@staff_id"].Value = bm.Issued_by;

            int row = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (row > 0)
            {
                BookRepository br = new BookRepository();
                BookModel bb = new BookModel();
                bb.Id = bm.Book_id;
                br.BorrowBook(bb);
                result = true;
            }
            else
            {
                result = false;
            }
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
        connection.Close();
        return result;
    }

    // Edit existing borrow
    public bool BorrowerReturn(BorrowerModel bm)
    {
        bool result = false;

        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE borrow SET return_date = @return_date, fine = @fine" +
                " WHERE borrowed_to = @borr_id AND book_id = @book_id AND borrowed_from = @borr_from";
            command.Transaction = transaction;

            command.Parameters.Add("@borr_id", SqlDbType.VarChar);
            command.Parameters["@borr_id"].Value = bm.Borrower_id;
            command.Parameters.Add("@book_id", SqlDbType.Int);
            command.Parameters["@book_id"].Value = bm.Book_id;
            command.Parameters.Add("@borr_from", SqlDbType.Date);
            command.Parameters["@borr_from"].Value = bm.Borrowed_from;
            command.Parameters.Add("@return_date", SqlDbType.Date);
            command.Parameters["@return_date"].Value = bm.Return_date;
            command.Parameters.Add("@fine", SqlDbType.Int);
            command.Parameters["@fine"].Value = bm.Fine;

            int row = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (row > 0)
            {
                BookRepository br = new BookRepository();
                BookModel bb = new BookModel();
                bb.Id = bm.Book_id;
                br.ReturnBook(bb);
                result = true;
            }
            else
            {
                result = false;
            }
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
        connection.Close();
        return result;
    }

    // Delete borrow
    public bool BorrowerDelete(BorrowerModel bm)
    {
        bool result = false;
        SqlConnection connection = new SqlConnection(ConnectionString);
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
            command.Parameters["@borr_id"].Value = bm.Borrower_id;
            command.Parameters.Add("@book_id", SqlDbType.Int);
            command.Parameters["@book_id"].Value = bm.Book_id;
            command.Parameters.Add("@borr_from", SqlDbType.Date);
            command.Parameters["@borr_from"].Value = bm.Borrowed_from;

            int row = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (row > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
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
        connection.Close();
        return result;
    }
}
