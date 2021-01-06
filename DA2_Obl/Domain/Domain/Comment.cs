using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Document Document { get; set; }
        public User Commenter { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public Comment()
        {
            Document = null;
            Commenter = null;
            Text = "";
            Rating = 0;
        }
    }
}
