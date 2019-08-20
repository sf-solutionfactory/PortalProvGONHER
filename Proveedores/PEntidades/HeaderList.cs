using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEntidades
{
    public class HeaderList
    {
        

        public HeaderList() { }

        public HeaderList(string header) {
            this.header = header;
        }

        string header;

        public string Header {
            set {
                this.header = value;
            }
            get {
                return header;
            }
                
        }
    }
}