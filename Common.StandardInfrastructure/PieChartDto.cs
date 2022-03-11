using System.Collections.Generic;

namespace Common.StandardInfrastructure
{
    public class PieChartDto
    {
        public List<string> NamesFl { get; set; } = new List<string>();
        public List<string> NamesSl { get; set; } = new List<string>();
        public List<string> Values { get; set; } = new List<string>();
        public List<string> Colors { get; set; } = new List<string>();


    }

}
