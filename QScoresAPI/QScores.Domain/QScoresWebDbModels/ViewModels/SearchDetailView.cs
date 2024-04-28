using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QScores.Domain.QScoresWebDbModels.ViewModels
{
    public class SearchDetailView
    {
        public string? Name { get; set; }

        public int Code { get; set; }

        public string? Study { get; set; }

        public string? StudyType { get; set; }

        public int Rank { get; set; }
    }
}
