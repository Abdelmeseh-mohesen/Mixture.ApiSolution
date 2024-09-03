namespace Mixture.Core.Dto
{
    public class PumpReadingDto
    {
        public string PumpId { get; set; } // رقم المضخة
        public double Weight { get; set; } // الوزن
        public int FeedTypeId { get; set; } // رقم العلف
    }
}
