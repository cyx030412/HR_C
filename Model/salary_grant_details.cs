using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class salary_grant_details
    {
        public int grd_id { get; set; }

        public string salary_grant_id { get; set; }

        public string human_id { get; set; }

        public string human_name { get; set; }

        public double bouns_sum { get; set; }

        public double sale_sum { get; set; }
        public double deduct_sum { get; set; }
        public double salary_standard_sum { get; set; }
        public double salary_paid_sum { get; set; }

    }
}
