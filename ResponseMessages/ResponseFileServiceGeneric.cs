using FileSystemServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ResponseMessages
{
    [Serializable]
    [XmlInclude(typeof(ResponseFileService<FileSystemElement>))]
    public class ResponseFileService<T> : ResponseFileService
    {       
        public T Data { get; set; }
    }
}
