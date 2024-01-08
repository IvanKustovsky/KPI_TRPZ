using AutoMapper;
using BLL.Dtos;
using BLL.Service.Interfaces;
using CCL.Security;
using CCL.Security.Identity;
using DAL.Entities;
using DAL.UnitOfWork;

namespace BLL.Service.Impl
{
    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _database;
        private readonly int pageSize = 10;

        public VoucherService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        public IEnumerable<VoucherDTO> GetVouchersByVoucherType(VoucherType voucherType, int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();

            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var projectVouchers = _database.Vouchers.Find(v => v.VoucherType == voucherType, pageNumber, pageSize);
            //перетворення об'єктів
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Voucher, VoucherDTO>()).CreateMapper();
            var voucherDto = mapper.Map<IEnumerable<Voucher>, List<VoucherDTO>>(projectVouchers);

            return voucherDto;
        }
    }
}
