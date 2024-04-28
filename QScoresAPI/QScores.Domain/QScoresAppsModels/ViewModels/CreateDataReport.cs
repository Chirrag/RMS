using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QScores.Domain.QScoresAppsModels.ViewModels
{
    public class CreateDataReport
    {
        public int UserID { get; set; }
        public int RecID { get; set; }
        public string? ReportName { get; set; }

        public string? RptType { get; set; }

        public string? Studydates { get; set; }

        public string? ScalePoints { get; set; }

        public string? Targets { get; set; }

        public string? DemoCodes { get; set; }

        public string? Miscellaneous { get; set; }

        public string? Application { get; set; }

        public DateTime Datetime { get; set; }

        public string?   DBaseName { get; set; }
    }
}
