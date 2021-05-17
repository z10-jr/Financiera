using System.Web;
using System.Web.Mvc;

namespace Financiera
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new PersimosMVC.Filters.VerificaSession());
        }
    }
}
