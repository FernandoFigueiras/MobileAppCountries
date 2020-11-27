using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppCountries.Common.Entities
{
    public class CommentEntries
    {
        public string Title { get; set; }

        public string BlogText { get; set; }
        
        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string Country { get; set; }

    }
}
