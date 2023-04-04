using Segment1Exam.Controller;
using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.View;

internal class BorrowerView
{
    BorrowerModel bm = new BorrowerModel();
    BorrowerController bc = new BorrowerController();
    public void View()
    {
        bool endSection = false;
        string y, m, d, date, format;
        DateTime inputtedDate;
        CultureInfo provider = CultureInfo.InvariantCulture;
        Console.Clear();

        format = "yyyy-MM-dd";
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel Borrower");
            Console.WriteLine("1. Lihat semua");
            Console.WriteLine("2. New borrowing");
            Console.WriteLine("3. Return book");
            Console.WriteLine("4. Hapus");
            Console.WriteLine("5. Kembali ke menu utama");
            Console.Write("Pilih opsi: ");
            string opsi = Console.ReadLine();

            switch (opsi)
            {
                case "1":
                    bc.BorrowerList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Add new borrowing\n");
                    Console.Write("Borrower ID: ");
                    bm.Borrower_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    bm.Book_id = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Borrowed From: ");
                    Console.Write("Enter a year: ");
                    y = Console.ReadLine();
                    Console.Write("Enter a month: ");
                    m = Console.ReadLine();
                    Console.Write("Enter a day: ");
                    d = Console.ReadLine();
                    date = y + "-" + m + "-" + d;
                    bm.Borrowed_from = Convert.ToDateTime(date);

                    Console.WriteLine("Borrowed To: ");
                    Console.Write("Enter a year: ");
                    y = Console.ReadLine();
                    Console.Write("Enter a month: ");
                    m = Console.ReadLine();
                    Console.Write("Enter a day: ");
                    d = Console.ReadLine();
                    date = y + "-" + m + "-" + d;
                    bm.Borrowed_to = Convert.ToDateTime(date);
                    Console.Write("Issued By: ");
                    bm.Issued_by = Convert.ToInt32(Console.ReadLine());
                    bc.BorrowerNew(bm);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Book Returning\n");
                    Console.Write("Borrower ID: ");
                    bm.Borrower_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    bm.Book_id = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Borrowed From: ");
                    Console.Write("Enter a year: ");
                    y = Console.ReadLine();
                    Console.Write("Enter a month: ");
                    m = Console.ReadLine();
                    Console.Write("Enter a day: ");
                    d = Console.ReadLine();
                    date = y + "-" + m + "-" + d;
                    bm.Borrowed_from = Convert.ToDateTime(date);

                    if (bc.CheckBorrower(bm))
                    {
                        Console.WriteLine("Return Date: ");
                        Console.Write("Enter a year: ");
                        y = Console.ReadLine();
                        Console.Write("Enter a month: ");
                        m = Console.ReadLine();
                        Console.Write("Enter a day: ");
                        d = Console.ReadLine();
                        date = y + "-" + m + "-" + d;
                        bm.Return_date = Convert.ToDateTime(date);
                        Console.Write("Fine: ");
                        bm.Fine = Convert.ToInt32(Console.ReadLine());
                        bc.BorrowerReturn(bm);
                    }
                    else
                    {
                        Console.WriteLine("Combined keys not available, check the list.");
                    }
                    Console.ReadKey();
                    break;
                
                case "4":
                    Console.Clear();
                    Console.WriteLine("Delete borrower\n");
                    Console.Write("Borrower ID: ");
                    bm.Borrower_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    bm.Book_id = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Borrowed From: ");
                    Console.Write("Enter a year: ");
                    y = Console.ReadLine();
                    Console.Write("Enter a month: ");
                    m = Console.ReadLine();
                    Console.Write("Enter a day: ");
                    d = Console.ReadLine();
                    date = y + "-" + m + "-" + d;
                    bm.Borrowed_from = Convert.ToDateTime(date);

                    if (bc.CheckBorrower(bm)) {
                        bc.BorrowerDelete(bm);
                    }
                    else
                    {
                        Console.WriteLine("Combined keys not available, check the list.");
                    }
                    Console.ReadKey();
                    break;
                case "5":
                    endSection = true;
                    break;
                default:
                    break;
            }
            Console.Clear();
        }
    }

    public static void Views()
    {
        BorrowerView bv = new BorrowerView();
        bv.View();
    }
}
