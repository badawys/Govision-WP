using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Govision.Model
{
    public class AlbumData
    {
        public string src { get; set; }
        public string id { get; set; }
        public string total { get; set; }
    }

    public class AlbumPhotosList : List<AlbumData>
    {

    }
}
