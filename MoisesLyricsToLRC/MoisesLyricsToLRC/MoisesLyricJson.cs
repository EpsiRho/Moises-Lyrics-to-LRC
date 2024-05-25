using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoisesLyricsToLRC
{
    public class FileContent
    {
        public int id { get; set; }
        public int line_id { get; set; }
        public double start { get; set; }
        public double end { get; set; }
        public string text { get; set; }
        public string confidence { get; set; }
        public FileContent()
        {

        }
        public FileContent(Modified m)
        {
            id = m.id;
            line_id = m.line_id;
            start = m.start;
            end = m.end;
            text = m.text;
            confidence = "1";
        }
    }

    public class Modified
    {
        public int id { get; set; }
        public double end { get; set; }
        public string text { get; set; }
        public double start { get; set; }
        public int line_id { get; set; }
    }

    public class MoisesLyricJson
    {
        public List<FileContent> fileContent { get; set; }
        public List<Modified> modified { get; set; }
    }
}
