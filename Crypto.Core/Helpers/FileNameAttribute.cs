using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Helpers {
    public class FileNameAttribute : Attribute {
        public FileNameAttribute() { }
        public FileNameAttribute(string filterString) {
            FilterString = filterString;
        }
        public string FilterString { get; set; } = "All Files (*.*)|*.*";
    }
}
