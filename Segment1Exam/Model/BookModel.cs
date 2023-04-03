using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Model;

internal class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int No_Of_Copies_Actual { get; set; }
    public int No_Of_Copies_Current { get; set; }
}
