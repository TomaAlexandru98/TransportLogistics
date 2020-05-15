using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class RoutesHistory:DataEntity
    {
        public ICollection<Route> Routes { get; private set; }
    }
}
