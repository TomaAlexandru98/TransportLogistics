using System;
using System.Collections.Generic;
using System.Text;

namespace TransportLogistics.Model
{
    public class RoutesHistory:DataEntity
    {
        public ICollection<Route> Routes  { get; private set; }
        public void AddRoute(Route route)
        {
            if(Routes == null)
            {
                Routes = new List<Route>();
            }
            Routes.Add(route);
        }
        public static RoutesHistory Create()
        {
            var routeHistoric = new RoutesHistory()
            {
                Id = Guid.NewGuid(),
                Routes = new List<Route>()

         };
            return routeHistoric;
        }
    }
}
