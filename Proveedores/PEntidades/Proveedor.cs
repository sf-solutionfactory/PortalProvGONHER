//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PEntidades
{
    public class Proveedor
    {
        private string liftnr;
        private string title_medi;
        private string name1;
        private string name2;
        private string sort1;
        private string street;
        private string house_num1;
        private string post_code1;
        private string addr1_data;
        private string country;
        private string namecountry;
        private string region;
        private string nameregion;
        private string po_box;
        private string post_code2;
        private string langu;
        private string tel_number;
        private string fax_number;
        private string extension1;
        private string extension2;

        public Proveedor()
        {
            this.liftnr = "";
            this.title_medi = "";
            this.name1 = "";
            this.name2 = "";
            this.sort1 = "";
            this.street = "";
            this.house_num1 = "";
            this.post_code1 = "";
            this.addr1_data = "";
            this.country = "";
            this.namecountry = "";
            this.region = "";
            this.nameregion = "";
            this.po_box = "";
            this.post_code2 = "";
            this.langu = "";
            this.tel_number = "";
            this.fax_number = "";
            this.extension1 = "";
            this.extension2 = "";
        }

        public Proveedor(string liftnr,string	title_medi,string	name1,string	name2,string	sort1,string	street,string	house_num1,string	post_code1,string	addr1_data,string	country,string	namecountry,string	region,string	nameregion,	string	po_box,string	post_code2,string	langu,string	tel_number,string	fax_number,string	extension1,string	extension2)
        {
            this.liftnr = liftnr;
            this.title_medi = title_medi;
            this.name1 = name1;
            this.name2 = name2;
            this.sort1 = sort1;
            this.street = street;
            this.house_num1 = house_num1;
            this.post_code1 = post_code1;
            this.addr1_data = addr1_data;
            this.country = country;
            this.namecountry = namecountry;
            this.region = region;
            this.nameregion = nameregion;
            this.po_box = po_box;
            this.post_code2 = post_code2;
            this.langu = langu;
            this.tel_number = tel_number;
            this.fax_number = fax_number;
            this.extension1 = extension1;
            this.extension2 = extension2;
        }

        public string Liftnr { get { return liftnr; } set { liftnr = value; } }
        public string TITLE_MEDI { set { this.title_medi = value; } get { return title_medi; } }
        public string NAME1 { set { this.name1 = value; } get { return name1; } }
        public string NAME2 { set { this.name2 = value; } get { return name2; } }
        public string SORT1 { set { this.sort1 = value; } get { return sort1; } }
        public string STREET { set { this.street = value; } get { return street; } }
        public string HOUSE_NUM1 { set { this.house_num1 = value; } get { return house_num1; } }
        public string POST_CODE1 { set { this.post_code1 = value; } get { return post_code1; } }
        public string ADDR1_DATA { set { this.addr1_data = value; } get { return addr1_data; } }
        public string COUNTRY { set { this.country = value; } get { return country; } }
        public string NAMECOUNTRY { set { this.namecountry = value; } get { return namecountry; } }
        public string REGION { set { this.region = value; } get { return region; } }
        public string NAMEREGION { set { this.nameregion = value; } get { return nameregion; } }
        public string PO_BOX { set { this.po_box = value; } get { return po_box; } }
        public string POST_CODE2 { set { this.post_code2 = value; } get { return post_code2; } }
        public string LANGU { set { this.langu = value; } get { return langu; } }
        public string TEL_NUMBER { set { this.tel_number = value; } get { return tel_number; } }
        public string FAX_NUMBER { set { this.fax_number = value; } get { return fax_number; } }
        public string EXTENSION1 { set { this.extension1 = value; } get { return extension1; } }
        public string EXTENSION2 { set { this.extension2 = value; } get { return extension2; } }
    }
}