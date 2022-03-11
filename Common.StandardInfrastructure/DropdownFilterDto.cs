namespace Common.StandardInfrastructure
{
   public class DropdownFilterDto
    {
        public int? Limit { get; set; } = 10;
        public int? Offset { get; set; } = 1;
        public string SearchText { get; set; }
    }
}
