using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Repository;

internal class VisitorRepository
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";

    // Get all visitor
    public DataTable Select()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM visitor";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            connection.Open();

            adapter.Fill(dt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    // Get visitor by ID
    public DataTable SelectById(VisitorModel v)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM visitor where id = @id";
            // Tambah parameter
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = v.Id;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            connection.Open();
            adapter.Fill(dt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    // Check if visitor exist
    public bool CheckVisitor(VisitorModel v)
    {
        bool result = false;
        if (SelectById(v).Columns.Count > 0)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    // Add new visitor
    public bool Insert(VisitorModel v)
    {
        bool result = false;

        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();// open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO visitor VALUES" +
                " (@name, @phone, @address, @borrow_id)";
            command.Transaction = transaction;

            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = v.Name;
            command.Parameters.Add("@phone", SqlDbType.VarChar);
            command.Parameters["@phone"].Value = v.Phone;
            command.Parameters.Add("@address", SqlDbType.VarChar);
            command.Parameters["@address"].Value = v.Address;
            command.Parameters.Add("@borrow_id", SqlDbType.VarChar);
            command.Parameters["@borrow_id"].Value = v.Borrower_Id;

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
        finally
        {
            connection.Close();
        }
        return result;
    }

    // Edit existing visitor
    public bool Edit(VisitorModel v)
    {
        bool result = false;

        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE visitor" +
                " SET name = @name, phone = @phone, address = @address," +
                " borrower_id = @borrow_id" +
                " WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = v.Id;
            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = v.Name;
            command.Parameters.Add("@phone", SqlDbType.Bit);
            command.Parameters["@phone"].Value = v.Phone;
            command.Parameters.Add("@address", SqlDbType.VarChar);
            command.Parameters["@address"].Value = v.Address;
            command.Parameters.Add("@borrow_id", SqlDbType.VarChar);
            command.Parameters["@borrow_id"].Value = v.Borrower_Id;

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
        finally
        {
            connection.Close();
        }
        return result;
    }

    // Delete visitor
    public bool Delete(VisitorModel v)
    {
        bool result = false;
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction(); // open connection before use this

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM visitor WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = v.Id;

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
        finally
        {
            connection.Close();
        }
        return result;
    }
}
