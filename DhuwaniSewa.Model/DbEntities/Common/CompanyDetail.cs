namespace DhuwaniSewa.Model.DbEntities
{
    public class CompanyDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AppUserId { get; set; }
        public AppUsers AppUsers { get; set; }
    }
}
