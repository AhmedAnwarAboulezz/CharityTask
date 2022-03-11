namespace Common.StandardInfrastructure.CommonDto
{
    public class DynamicFilterDto<T> 
    {
        public T Name { get; set; }
        public int IsContain { get; set; }
    }
}
