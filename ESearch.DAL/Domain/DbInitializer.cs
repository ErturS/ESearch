using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.DAL.Domain
{
    class DbInitializer : MigrateDatabaseToLatestVersion<ESearchContext, Configuration>
    {
    }

}
