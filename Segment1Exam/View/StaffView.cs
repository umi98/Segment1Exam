using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Segment1Exam.Controller;
using Segment1Exam.Model;
using Segment1Exam.Repository;

namespace Segment1Exam.View;

internal class StaffView
{
    StaffModel sm = new StaffModel();
    StaffController sc = new StaffController();
    public void View()
    {
        bool endSection = false;
        string id;
        Console.Clear();
        while (!endSection)
        {
            Console.WriteLine("Bagian tabel staff");
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
                    sc.StaffList();
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Get staff by ID\n");
                    Console.Write("ID: ");
                    id = Console.ReadLine();
                    sm.Id = Convert.ToInt32(id);
                    sc.StaffById(sm);
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Add new Staff\n");
                    Console.Write("Name: ");
                    sm.Name = Console.ReadLine();
                    Console.Write("Admin (1:Yes, 0:No): ");
                    sm.Admin = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Role: ");
                    sm.Role = Console.ReadLine();
                    sc.StaffNew(sm);
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Edit staff\n");
                    Console.Write("ID: ");
                    sm.Id = Convert.ToInt32(Console.ReadLine());
                    if (sc.CheckStaff(sm))
                    {
                        Console.Write("Name: ");
                        sm.Name = Console.ReadLine();
                        Console.Write("Admin (1:Yes, 0:No): ");
                        sm.Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Role: ");
                        sm.Role = Console.ReadLine();
                        sc.StaffEdit(sm);
                    }
                    else
                    {
                        Console.WriteLine("ID not available, check the list.");
                    }
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Delete a staff\n");
                    Console.Write("ID: ");
                    sm.Id = Convert.ToInt32(Console.ReadLine());
                    if (sc.CheckStaff(sm))
                    {
                        sc.StaffDelete(sm);
                    }
                    else
                    {
                        Console.WriteLine("ID not available, check the list.");
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
        StaffView sv = new StaffView();
        sv.View();
    }
}
