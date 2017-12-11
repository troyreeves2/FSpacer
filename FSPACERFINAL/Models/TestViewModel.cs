using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSPACERFINAL.Data;

namespace FSPACERFINAL.Models
{
    public class TestViewModel
    {
        public TestViewModel() {

        }

        public ApplicationDbContext db;

        public IEnumerable<SpacerCard> SpacerCards { get; set; }


        public double HorizontalSpacerLength { get; set; }

        public double VerticalSpacerLength { get; set; }


        public TestViewModel(ApplicationDbContext db) {

            this.db = db;
            SpacerCards = db.SpacerCards.ToList();

        }
    }
}
