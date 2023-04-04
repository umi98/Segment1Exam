﻿using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Segment1Exam.Repository;

namespace Segment1Exam.Controller;

internal class BookController
{
    BookRepository br = new BookRepository();
    
    public void BookList()
    {
        DataTable dt = br.Select();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("ID: " + dt.Rows[i][0]);
                Console.WriteLine("Title: " + dt.Rows[i][1]);
                Console.WriteLine("Author: " + dt.Rows[i][2]);
                Console.WriteLine("No. of Actual Copy: " + dt.Rows[i][3]);
                Console.WriteLine("No. of Current Copy: " + dt.Rows[i][4]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("No Data");
        }
        Console.ReadKey();
    }

    public void BookById(BookModel b)
    {
        DataTable dt = br.SelectById(b);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("ID: " + dt.Rows[i][0]);
                Console.WriteLine("Title: " + dt.Rows[i][1]);
                Console.WriteLine("Author: " + dt.Rows[i][2]);
                Console.WriteLine("No. of Actual Copy: " + dt.Rows[i][3]);
                Console.WriteLine("No. of Current Copy: " + dt.Rows[i][4]);
            }
            Console.WriteLine("---------------------");
        }
        else
        {
            Console.WriteLine("No Data");
        }
        Console.ReadKey();
    }

    public bool CheckBook(BookModel b)
    {
        bool result = br.CheckBook(b);
        return result;
    }

    public void BookNew(BookModel b)
    {
        bool result = br.Insert(b);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
        Console.ReadKey();
    }

    public void BookEdit(BookModel b)
    {
        bool result = br.Edit(b);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
        Console.ReadKey();
    }

    public void BookDelete(BookModel b)
    {
        bool result = br.Delete(b);
        if (result)
        {
            Console.WriteLine("Data berhasil ditambah");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }
        Console.ReadKey();
    }
}
