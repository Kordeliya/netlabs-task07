﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ResponseFileService<T> : ResponseFileService
    {
        public T Data { get; set; }
    }
}
