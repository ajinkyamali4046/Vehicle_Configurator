namespace v_conf_dn.Dto
{
    public class AlternateComponentResponse
    {
        public long Id { get; set; }
        public double DeltaPrice { get; set; }
        public ModelResponseDto ModId { get; set; }
        public ComponentResponseDto CompId { get; set; }
        public ComponentResponseDto AltCompId { get; set; }
    }

    public class ModelResponseDto
    {
        public long Id { get; set; }
        public string ImagePath { get; set; }
        public ManufacturerResponseDto Manufacturer { get; set; }
        public int MinQty { get; set; }
        public string ModName { get; set; }
        public decimal Price { get; set; }
        public int SafetyRating { get; set; }
        public SegmentResponseDto Segment { get; set; }
    }

    public class ComponentResponseDto
    {
        public long Id { get; set; }
        public string CompName { get; set; }
    }

    public class ManufacturerResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class SegmentResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
