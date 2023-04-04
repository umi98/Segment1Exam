using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Segment1Exam.Repository;

internal class BookRepository
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";


    // Get all Book
    public DataTable Select()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();
        
        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM book";
            
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

    public List<BookModel> Select1()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        List<BookModel> bookl = new List<BookModel>();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM book";

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                BookModel book = new BookModel();
                book.Id = Convert.ToInt32(reader[0]);
                book.Title = Convert.ToString(reader[1]);
                book.Author = Convert.ToString(reader[2]);
                book.No_Of_Copies_Actual = Convert.ToInt32(reader[3]);
                book.No_Of_Copies_Current = Convert.ToInt32(reader[4]);
                bookl.Add(book);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();
        return bookl;
    }

    // Get Book by ID
    public DataTable SelectById(BookModel b)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();

        SqlCommand command = new SqlCommand();
        try
        {

            command.Connection = connection;
            command.CommandText = "SELECT * FROM book where id = @id";
            // Tambah parameter
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = b.Id;

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

    // Check if Book exist
    public bool CheckBook(BookModel b)
    {
        bool result = false;
        if (SelectById(b).Columns.Count > 0)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    // Add new Book
    public bool Insert(BookModel b)
    {
        bool result = false;
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO book VALUES" +
                " (@title, @author, @no_act, @no_curr)";
            command.Transaction = transaction;

            command.Parameters.Add("@title", SqlDbType.VarChar);
            command.Parameters["@title"].Value = b.Title;
            command.Parameters.Add("@author", SqlDbType.VarChar);
            command.Parameters["@author"].Value = b.Author;
            command.Parameters.Add("@no_act", SqlDbType.Int);
            command.Parameters["@no_act"].Value = b.No_Of_Copies_Actual;
            command.Parameters.Add("@no_curr", SqlDbType.Int);
            command.Parameters["@no_curr"].Value = b.No_Of_Copies_Current;

            int rows = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (rows > 0)
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

    public void BorrowBook(BookModel b)
    {
        BookModel nb = new BookModel();
        try
        {
            DataTable book = SelectById(b);
            nb.Id = Convert.ToInt32(book.Rows[0][0]);
            nb.Title = Convert.ToString(book.Rows[0][1]);
            nb.Author = Convert.ToString(book.Rows[0][2]);
            nb.No_Of_Copies_Actual = Convert.ToInt32(book.Rows[0][3]);
            nb.No_Of_Copies_Current = Convert.ToInt32(book.Rows[0][4]) - 1;
            Edit(nb);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void ReturnBook(BookModel b)
    {
        BookModel nb = new BookModel();
        try
        {
            DataTable book = SelectById(b);
            nb.Id = Convert.ToInt32(book.Rows[0][0]);
            nb.Title = Convert.ToString(book.Rows[0][1]);
            nb.Author = Convert.ToString(book.Rows[0][2]);
            nb.No_Of_Copies_Actual = Convert.ToInt32(book.Rows[0][3]);
            nb.No_Of_Copies_Current = Convert.ToInt32(book.Rows[0][4]) + 1;
            Edit(nb);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // Edit existing Book
    public bool Edit(BookModel b)
    {
        bool result = false;

        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

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
            command.Parameters["@id"].Value = b.Id;
            command.Parameters.Add("@title", SqlDbType.VarChar);
            command.Parameters["@title"].Value = b.Title;
            command.Parameters.Add("@author", SqlDbType.VarChar);
            command.Parameters["@author"].Value = b.Author;
            command.Parameters.Add("@no_act", SqlDbType.Int);
            command.Parameters["@no_act"].Value = b.No_Of_Copies_Actual;
            command.Parameters.Add("@no_curr", SqlDbType.Int);
            command.Parameters["@no_curr"].Value = b.No_Of_Copies_Current;

            int rows = command.ExecuteNonQuery();
            transaction.Commit(); // Titik data dipulihkan ketika rollback dilaksanakan.

            if (rows > 0)
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

    // Delete book
    public bool Delete(BookModel b)
    {
        bool result = false;
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM book WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = b.Id;

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
