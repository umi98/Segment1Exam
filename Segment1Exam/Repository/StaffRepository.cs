using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Segment1Exam.Repository;
internal class StaffRepository
{
    static string ConnectionString = "Data Source=DESKTOP-IBME24N;Initial Catalog=db_library;Integrated Security=True;Connect Timeout=30;";

    public DataTable Select()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM staff";
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

    public List<StaffModel> Select2()
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        List<StaffModel> staffl = new List<StaffModel>();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM staff";

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                StaffModel staff = new StaffModel();
                staff.Id = Convert.ToInt32(reader[0]);
                staff.Name = Convert.ToString(reader[1]);
                staff.Is_admin = Convert.ToInt32(reader[2]);
                staff.Role = Convert.ToString(reader[3]);
                staffl.Add(staff);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();
        return staffl;
    }


    public DataTable SelectById(StaffModel s)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        DataTable dt = new DataTable();

        SqlCommand command = new SqlCommand();
        try
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM staff WHERE id = @id";
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = s.Id;

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

    public bool CheckStaff(StaffModel s)
    {
        bool result = false;
        if (SelectById(s).Columns.Count > 0)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    public bool Insert(StaffModel s)
    {
        bool result = false;

        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO staff VALUES" +
                " (@name, @admin, @role)";
            command.Transaction = transaction;

            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = s.Name;
            command.Parameters.Add("@admin", SqlDbType.Bit);
            command.Parameters["@admin"].Value = s.Is_admin;
            command.Parameters.Add("@role", SqlDbType.VarChar);
            command.Parameters["@role"].Value = s.Role;

            int rows = command.ExecuteNonQuery();
            transaction.Commit();

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

    public bool Edit(StaffModel s)
    {
        bool result = false;
        
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE staff SET name = @name, is_admin = @admin, role = @role WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = s.Id;
            command.Parameters.Add("@name", SqlDbType.VarChar);
            command.Parameters["@name"].Value = s.Name;
            command.Parameters.Add("@admin", SqlDbType.Bit);
            command.Parameters["@admin"].Value = s.Is_admin;
            command.Parameters.Add("@role", SqlDbType.VarChar);
            command.Parameters["@role"].Value = s.Role;

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

    public bool Delete(StaffModel s)
    {
        bool result = false;
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM staff WHERE id = @id";
            command.Transaction = transaction;

            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = s.Id;

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
