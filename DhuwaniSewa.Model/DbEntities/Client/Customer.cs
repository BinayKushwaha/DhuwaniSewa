namespace DhuwaniSewa.Model.DbEntities
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public AppUsers AppUser { get; set; }
    }
}
