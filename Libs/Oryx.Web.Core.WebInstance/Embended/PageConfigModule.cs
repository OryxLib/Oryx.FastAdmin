using Oryx.Web.Core.Actions;

namespace Oryx.Web.Core.WebInstance.Embended
{
    public class PageConfigModule : OryxWebModule
    {
        public PageConfigModule()
        {
            Get("/Page/Set", async ctx =>
            {
                //var db = ctx.Service<DbContext>();
                //DbTableQuery table = db.Table(ctx.Json.table);
                //if (ctx.Json.set != null)
                //{
                //    foreach (var setItme in ctx.Json.set)
                //    {

                //    }
                //}
            });
        }
    }
}
