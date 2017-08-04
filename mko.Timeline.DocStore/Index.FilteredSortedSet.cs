using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Timeline.DocStore
{
    public class FilteredSortedSet : MkPrgNet.Pattern.Repository.IFilteredSortedSet<string>
    {
        long _count;
        IEnumerable<string> idset;

        internal FilteredSortedSet(IEnumerable<string> idset)
        {
            _count = idset.Count();
            this.idset = idset;
        }

        public bool Any()
        {
            return _count > 0;
        }

        public long Count()
        {
            return _count;
        }

        public IEnumerable<string> Get(int skip = 0, int take = -1)
        {
            if(skip > 0)
            {
                if(take > -1)
                {
                    return idset.Skip(skip).Take(take);
                } else
                {
                    return idset.Skip(skip);
                }
            } else
            {
                if (take > -1)
                {
                    return idset.Take(take);
                }
                else
                {
                    return idset;
                }
            }
        }
    }
}
