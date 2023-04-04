using Segment1Exam.Controller;
using Segment1Exam.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        bool endApp = false;
        while (!endApp)
        {
            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Pengunjung");
            Console.WriteLine("3. Buku");
            Console.WriteLine("4. Peminjaman");
            Console.WriteLine("5. Keluar");
            Console.Write("Pilih opsi: ");
            string opsi = Console.ReadLine();

            switch (opsi)
            {
                case "1":
                    StaffView.Views();
                    break;
                case "2":
                    VisitorView.Views();
                    break;
                case "3":
                    BookView.Views();
                    break;
                case "4":
                    BorrowerView.Views();
                    break;
                case "5":
                    endApp = true;
                    break;
                default:
                    break;
            }
        }
    }
}