using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Segment1Exam.Repository;

namespace Segment1Exam.Controller;

internal class StaffController
{
    StaffRepository sr = new StaffRepository();

    // Get all staff
    public void StaffList()
    {
        DataTable dt = sr.Select();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("ID: " + dt.Rows[i][0]);
                Console.WriteLine("Name: " + dt.Rows[i][1]);
                Console.WriteLine("Is_administrator: " + dt.Rows[i][2]);
                Console.WriteLine("Role: " + dt.Rows[i][3]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("No Data");
        }
    }

    public void StaffList2()
    {
        List<StaffModel> staffl = new List<StaffModel>();
        staffl = sr.Select2();

        if (staffl.Count > 0)
        {
            foreach (StaffModel bs in staffl)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("ID: " + bs.Id);
                Console.WriteLine("Name: " + bs.Name);
                Console.WriteLine("Is_administrator: " + bs.Is_admin);
                Console.WriteLine("Role: " + bs.Role);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("No Data");
        }
    }

    // Get staff by ID
    public void StaffById(StaffModel s)
    {
        DataTable dt = sr.SelectById(s);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("ID: " + dt.Rows[i][0]);
                Console.WriteLine("Name: " + dt.Rows[i][1]);
                Console.WriteLine("Is_administrator: " + dt.Rows[i][2]);
                Console.WriteLine("Role: " + dt.Rows[i][3]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("No Data");
        }
    }

    // Check if staff exist
    public bool CheckStaff(StaffModel s)
    {
        bool result = sr.CheckStaff(s);
        return result;
    }

    // Add new staff
    public void StaffNew(StaffModel s)
    {
        bool result = sr.Insert(s);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
    }

    // Edit existing staff
    public void StaffEdit(StaffModel s)
    {
        bool result = sr.Edit(s);
        if (result)
        {
            Console.WriteLine("Data berhasil diubah");
        }
        else
        {
            Console.WriteLine("Data gagal diubah");
        }
    }

    // Delete staff
    public void StaffDelete(StaffModel s)
    {
        bool result = sr.Delete(s);
        if (result)
        {
            Console.WriteLine("Data berhasil dihapus");
        }
        else
        {
            Console.WriteLine("Data gagal dihapus");
        }
    }

}
