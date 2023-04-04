using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segment1Exam.Model;

internal class BorrowerModel
{
    public string Borrower_id { get; set; }
	public int Book_id { get; set; }
    public DateTime Borrowed_from { get; set; }
    public DateTime Borrowed_to { get; set; }
    public DateTime Return_date { get; set; }
    public int Fine { get; set; }
    public int Issued_by { get; set; }
}
