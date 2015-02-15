using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Govision.Model
{
    public class HistoryData
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Tag_Type { get; set; }
        public string Tag_id { get; set; }
    }

    public class HistoryList : List<HistoryData> //for storing HistoryList class items with type of list 
    {

    }
    

        
}


