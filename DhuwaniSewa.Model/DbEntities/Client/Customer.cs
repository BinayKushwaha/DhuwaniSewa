namespace DhuwaniSewa.Model.DbEntities
{
    public class Customer
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public bool IsActive { get; set; }
        public AppUsers AppUsers { get; set; }
    }
}
