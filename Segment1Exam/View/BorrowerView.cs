using Segment1Exam.Controller;
using Segment1Exam.Model;
using System;
using System.Collections.Generic;
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
        string y, m, d;
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
                    bc.BorrowerList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Add new borrowing\n");
                    Console.Write("Borrower ID: ");
                    bm.Borrower_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    bm.Book_id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Borrowed From: ");
                    y = Convert.ToString(Console.Read());
                    Console.Write("\tMonth: ");
                    m = Convert.ToString(Console.Read());
                    Console.Write("\tDay: ");
                    d = Convert.ToString(Console.Read());
                    bm.Borrowed_from = Convert.ToDateTime(y + m + d);

                    Console.Write("Borrowed To: ");
                    Console.Write("Year: ");
                    y = Convert.ToString(Console.Read());
                    Console.Write("\tMonth: ");
                    m = Convert.ToString(Console.Read());
                    Console.Write("\tDay: ");
                    d = Convert.ToString(Console.Read());
                    bm.Borrowed_to = Convert.ToDateTime(y + m + d);

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

                    Console.Write("Borrowed From: ");
                    Console.Write("Year: ");
                    y = Convert.ToString(Console.Read());
                    Console.Write("\tMonth: ");
                    m = Convert.ToString(Console.Read());
                    Console.Write("\tDay: ");
                    d = Convert.ToString(Console.Read());
                    bm.Borrowed_from = Convert.ToDateTime(y + m + d);

                    if (bc.CheckBorrower(bm))
                    {
                        Console.Write("Return Date: ");
                        bm.Return_date = Convert.ToDateTime(Console.ReadLine());
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
                    Console.WriteLine("Delete borrower\n");
                    Console.Write("Borrower ID: ");
                    bm.Borrower_id = Console.ReadLine();
                    Console.Write("Book ID: ");
                    bm.Book_id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Borrowed From: ");
                    Console.Write("Year: ");
                    y = Convert.ToString(Console.Read());
                    Console.Write("\tMonth: ");
                    m = Convert.ToString(Console.Read());
                    Console.Write("\tDay: ");
                    d = Convert.ToString(Console.Read());
                    bm.Borrowed_from = Convert.ToDateTime(y + m + d);

                    if (bc.CheckBorrower(bm)) {
                        bc.BorrowerDelete(bm);
                    }
                    else
                    {
                        Console.WriteLine("Combined keys not available, check the list.");
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

    public static void Views()
    {
        VisitorView vv = new VisitorView();
        vv.View();
    }
}
