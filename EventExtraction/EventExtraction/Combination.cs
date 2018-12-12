using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventExtraction
{
    public class Combination
    {
        public int Index { get; set; }

        public List<object> Items { get; private set; }
        
        public Combination(params object[] items)
        {
            Items = new List<object>();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public override bool Equals(object obj)
        {
            var combination = obj as Combination;
            return combination != null &&
                   combination.Items.SequenceEqual(Items);
        }

        public override int GetHashCode()
        {
            var hashcode = -604923257;
            Items.ForEach(x => hashcode ^= x.GetHashCode());
            return hashcode;
        }
    }
}
