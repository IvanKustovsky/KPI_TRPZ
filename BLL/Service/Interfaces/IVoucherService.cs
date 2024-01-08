using BLL.Dtos;
using DAL.Entities;

namespace BLL.Service.Interfaces
{
    public interface IVoucherService
    {
        IEnumerable<VoucherDTO> GetVouchersByVoucherType(VoucherType voucherType, int page);
    }
}
