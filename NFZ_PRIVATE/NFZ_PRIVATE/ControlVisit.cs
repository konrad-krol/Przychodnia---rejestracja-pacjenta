using System;
using System.Collections.Generic;
using System.Text;

namespace NFZ_PRIVATE
{
    class ControlVisit
    {
        public int VisitIndex { get; set; }
        public int DoctorIndex { get; set; }
        public string Specialization { get; set; }
        public string PrivateNFZ { get; set; }
        public DateTime DateVisit { get; set; }
        public TimeSpan TimeVisit { get; set; }
        public int PatientIndex { get; set; }
    }
}
