using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QScores.Domain.QScoresAppsModels.ViewModels
{
    public class ViewReport
    {
        public int RecID { get; set; }

        public string? ReportName { get; set; }

        public DateTime Datetime { get; set; }
    }
}
