﻿using FileSystemServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    [Serializable]
    public class CreateRequest: BaseRequest
    {
        public FileSystemElement Element { get; set; }
    }
}
