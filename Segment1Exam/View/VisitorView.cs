using Segment1Exam.Controller;
using Segment1Exam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.View;

internal class VisitorView
{
    VisitorModel vm = new VisitorModel();
    VisitorController vc = new VisitorController();
    // Ini bagian CRUD tabel visitor
    public void View()
    {
        bool endSection = false;
        Console.Clear();
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel Visitor");
            Console.WriteLine("1. Lihat semua");
            Console.WriteLine("2. Lihat berdasar id");
            Console.WriteLine("3. Tambah");
            Console.WriteLine("4. Edit");
            Console.WriteLine("5. Hapus");
            Console.WriteLine("6. Kembali ke menu utama");
            Console.Write("Pilih opsi: ");
            string opsi = Console.ReadLine();

            switch (opsi)
            {
                case "1":
                    vc.VisitorList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Cari visitor berdasar id\n");
                    Console.Write("Id: ");
                    vm.Id = Convert.ToInt32(Console.ReadLine());
                    vc.VisitorById(vm);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Add new visitor\n");
                    Console.Write("Name: ");
                    vm.Name = Console.ReadLine();
                    Console.Write("Phone: ");
                    vm.Phone = Console.ReadLine();
                    Console.Write("Address: ");
                    vm.Address = Console.ReadLine();
                    Console.Write("Borrow ID: ");
                    vm.Borrower_Id = Console.ReadLine();
                    vc.VisitorNew(vm);
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Edit visitor\n");
                    Console.Write("Id: ");
                    vm.Id = Convert.ToInt32(Console.ReadLine());
                    if (vc.CheckVisitor(vm))
                    {
                        Console.Write("Name: ");
                        vm.Name = Console.ReadLine();
                        Console.Write("Phone: ");
                        vm.Phone = Console.ReadLine();
                        Console.Write("Address: ");
                        vm.Address = Console.ReadLine();
                        Console.Write("Borrow ID: ");
                        vm.Borrower_Id = Console.ReadLine();
                        vc.VisitorEdit(vm);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
                    }
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Delete a visitor\n");
                    Console.Write("Id: ");
                    vm.Id = Convert.ToInt32(Console.ReadLine());
                    if (vc.CheckVisitor(vm))
                    {
                        vc.VisitorDelete(vm);
                    }
                    else
                    {
                        Console.WriteLine("Id tidak tersedia, harap lihat kembali daftar");
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
