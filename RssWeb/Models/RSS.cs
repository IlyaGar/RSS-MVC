using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssWeb.Models
{
    public class RSS : IEqualityComparer<RSS>, IEquatable<RSS>
    {
        public string Headline { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int SourceId { get; set; }

        public bool Equals(RSS other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            if (this.GetType() != other.GetType())
                return false;

            if (string.Compare(this.Headline, other.Headline, StringComparison.CurrentCulture) == 0 && this.Date.Equals(other.Date))
                return true;
            else
                return false;
        }

        public bool Equals(RSS x, RSS y)
        {
            if (x == null || y == null) return false;

            return ReferenceEquals(x, y) || (x.Headline == y.Headline && x.Date == y.Date);
        }

        public int GetHashCode(RSS obj)
        {
            return obj.Headline.GetHashCode() + obj.Date.GetHashCode();
        }
    }
}
