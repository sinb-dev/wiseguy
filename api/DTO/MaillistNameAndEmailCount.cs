using System.Collections.Generic;
namespace wiseguy
{
    public class MaillistNameAndEmailCount 
    {
        public class row {
            public string name {get;set;}
            public int count {get;set;}
        }
        List<row> list = new List<row>();
        public void Add(string name, int count) {
            list.Add( new row{ name=name, count=count });
        }
        public List<row> GetList() {
            return list;
        }
    }
}