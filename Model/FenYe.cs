using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FenYe<T>
    {
        //当前页
        public int currentPage { get; set; }
        //总页数
        public int Totalpage { get; set; }
        //当前页数据
        public IEnumerable<T> CList { get; set; }
        //共多少条数据
        public int Totalnumber { get; set; }

        //每页大小
        public int pageSize { get; set; }
    }
}
