

namespace DAL.Entities
{
    public class Voucher
    {
        public int VoucherId { get; set; }

        public DateTime StartVoucher { get; set; }

        public DateTime EndVoucher { get; set; }

        public int UserId { get; set; }

        public VoucherType VoucherType { get; set; }
        public StatusType StatusType { get; set; }
    }
}
