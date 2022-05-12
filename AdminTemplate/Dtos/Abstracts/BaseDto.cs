namespace AdminTemplate.Dtos.Abstracts
{
    public abstract class BaseDto<T> where T : IEquatable<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
