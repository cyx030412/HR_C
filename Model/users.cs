﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class users
    {
        public int u_id { get; set; }

        public string u_name { get; set; }

        public string u_true_name { get; set; }

        public string u_password { get; set; }

        //用户身份

        public string RolesName { get; set; }
    }
}
