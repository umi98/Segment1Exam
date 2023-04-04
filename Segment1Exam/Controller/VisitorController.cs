using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Segment1Exam.Repository;
using Segment1Exam.Model;

namespace Segment1Exam.Controller;

internal class VisitorController
{
    VisitorRepository vr = new VisitorRepository();

    // Get all visitor
    public void VisitorList()
    {
        DataTable dt = vr.Select();

        if (dt.Rows.Count > 0)
        {
            Console.WriteLine("Daftar visitor");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Id: " + dt.Rows[i][0]);
                Console.WriteLine("Name: " + dt.Rows[i][1]);
                Console.WriteLine("Phone: " + dt.Rows[i][2]);
                Console.WriteLine("Address: " + dt.Rows[i][3]);
                Console.WriteLine("Borrow ID: " + dt.Rows[i][4]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("Tidak ada data");
        }
    }

    public void VisitorList2()
    {
        List<VisitorModel> visitorl = new List<VisitorModel>();
        visitorl = vr.Select2();

        if (visitorl.Count > 0)
        {
            foreach (VisitorModel v in visitorl)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Id: " + v.Id);
                Console.WriteLine("Name: " + v.Name);
                Console.WriteLine("Phone: " + v.Phone);
                Console.WriteLine("Address: " + v.Address);
                Console.WriteLine("Borrow ID: " + v.Borrower_Id);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("Tidak ada data");
        }
        Console.ReadKey();
    }


    // Get visitor by ID
    public void VisitorById(VisitorModel v)
    {
        DataTable dt = vr.SelectById(v);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Id: " + dt.Rows[i][0]);
                Console.WriteLine("Name: " + dt.Rows[i][1]);
                Console.WriteLine("Phone: " + dt.Rows[i][2]);
                Console.WriteLine("Address: " + dt.Rows[i][3]);
                Console.WriteLine("Borrow ID: " + dt.Rows[i][4]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("Tidak ada data");
        }        
    }

    // Check if visitor exist
    public bool CheckVisitor(VisitorModel v)
    {
        bool result = vr.CheckVisitor(v);
        return result;
    }

    // Add new visitor
    public void VisitorNew(VisitorModel v)
    {
        bool result = vr.Insert(v);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
    }

    // Edit existing visitor
    public void VisitorEdit(VisitorModel v)
    {
        bool result = vr.Edit(v);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
    }

    // Delete visitor
    public void VisitorDelete(VisitorModel v)
    {
        bool result = vr.Delete(v);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
    }

}
