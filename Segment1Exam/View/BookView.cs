using Segment1Exam.Controller;
using Segment1Exam.Model;
using System;

namespace Segment1Exam.View;

internal class BookView
{
    BookModel bm = new BookModel();
    BookController bc = new BookController();

    // Ini bagian CRUD tabel book

    public void View()
    {
        bool endSection = false;
        Console.Clear();
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel Book");
            Console.WriteLine("1. Lihat semua");
            Console.WriteLine("2. Lihat berdasar id");
            Console.WriteLine("3. Tambah");
            Console.WriteLine("4. Edit");
            Console.WriteLine("5. Kembali ke menu utama");
            Console.Write("Pilih opsi: ");
            string opsi = Console.ReadLine();

            switch (opsi)
            {
                case "1":
                    bc.BookList2();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Get book by ID\n");
                    Console.Write("ID: ");
                    bm.Id = Convert.ToInt32(Console.ReadLine());
                    bc.BookById(bm);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Add new Book\n");
                    Console.Write("Title: ");
                    bm.Title = Console.ReadLine();
                    Console.Write("Author: ");
                    bm.Author = Console.ReadLine();
                    Console.Write("No. of actual copies: ");
                    bm.No_Of_Copies_Actual = Convert.ToInt32(Console.ReadLine());
                    Console.Write("No. of current copies: ");
                    bm.No_Of_Copies_Current = Convert.ToInt32(Console.ReadLine());
                    bc.BookNew(bm);
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Edit Book\n");
                    Console.Write("Id: ");

                    bm.Id = Convert.ToInt32(Console.ReadLine());
                    if (bc.CheckBook(bm))
                    {
                        Console.Write("Title: ");
                        bm.Title = Console.ReadLine();
                        Console.Write("Author: ");
                        bm.Author = Console.ReadLine();
                        Console.Write("No. of actual copies: ");
                        bm.No_Of_Copies_Actual = Convert.ToInt32(Console.ReadLine());
                        Console.Write("No. of current copies: ");
                        bm.No_Of_Copies_Current = Convert.ToInt32(Console.ReadLine());
                        bc.BookEdit(bm);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
                    }
                    Console.ReadKey();
                    break;
                /*case "5":
                    Console.Clear();
                    Console.WriteLine("Delete a Book\n");
                    Console.Write("ID: ");
                    bm.Id = Convert.ToInt32(Console.ReadLine());
                    if (bc.CheckBook(bm))
                    {
                        bc.BookDelete(bm);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
                    }
                    Console.ReadKey();
                    break;*/
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
        BookView bv = new BookView();
        bv.View();
    }
}
