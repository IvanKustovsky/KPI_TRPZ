
namespace DAL.Entities
{
    public class Sanatorium
    {
        public int SanatoriumID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Voucher> Vouchers { get; set; }
    }
}
