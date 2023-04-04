using Segment1Exam.Model;
using Segment1Exam.Repository;
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
    BorrowerRepository br = new BorrowerRepository();
        
    // Get all borrow
    public void BorrowerList()
    {
        DataTable dt = br.Select();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Borrower ID: " + dt.Rows[i][0]);
                Console.WriteLine("Book ID: " + dt.Rows[i][1]);
                Console.WriteLine("Borrowed from: " + dt.Rows[i][2]);
                Console.WriteLine("Borrowed to: " + dt.Rows[i][3]);
                if (dt.Rows[i][3] == null)
                {
                    Console.WriteLine("STILL IN BORROWING PERIOD");
                }
                else
                {
                    Console.WriteLine("Return date: " + dt.Rows[i][4]);
                    Console.WriteLine("Fine: " + dt.Rows[i][5]);
                    Console.WriteLine("Issued by: " + dt.Rows[i][6]);
                }
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("Tidak ada data");
        }
    }

    // Get borrow by ID
    public void BorrowerById(BorrowerModel bm)
    {
        DataTable dt = br.SelectById(bm);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Borrower ID: " + dt.Rows[i][0]);
                Console.WriteLine("Book ID: " + dt.Rows[i][1]);
                Console.WriteLine("Borrowed from: " + dt.Rows[i][2]);
                Console.WriteLine("Borrowed to: " + dt.Rows[i][3]);
                if (dt.Rows[i][3] == null)
                {
                    Console.WriteLine("STILL IN BORROWING PERIOD");
                }
                else
                {
                    Console.WriteLine("Return date: " + dt.Rows[i][4]);
                    Console.WriteLine("Fine: " + dt.Rows[i][5]);
                    Console.WriteLine("Issued by: " + dt.Rows[i][6]);
                }
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("Tidak ada data");
        }
    }

    // Check if borrow exist
    public bool CheckBorrower(BorrowerModel bm)
    {
        bool result = br.CheckBorrower(bm);
        return result;
    }

    // Add new borrow
    public void BorrowerNew(BorrowerModel bm)
    {
        bool result = br.BorrowerNew(bm);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
    }

    // Edit existing borrow
    public void BorrowerReturn(BorrowerModel bm)
    {
        bool result = br.BorrowerReturn(bm);
        if (result)
        {
            Console.WriteLine("Data berhasil diubah");
        }
        else
        {
            Console.WriteLine("Data gagal diubah");
        }
    }

    // Delete borrow
    public void BorrowerDelete(BorrowerModel bm)
    {
        bool result = br.BorrowerDelete(bm);
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
