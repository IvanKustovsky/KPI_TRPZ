using CCL.Security.Identity;
using CCL.Security;
using DAL.UnitOfWork;
using BLL.Service.Impl;
using BLL.Service.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Tests
{
    public class VoucherServiceUnitTest
    {
        // ����, �� ��������, �� ������ ������� ArgumentNullException, 
        // ���� ����������� VoucherService ������ null � ����� ��������� IUnitOfWork.
        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            IUnitOfWork nullUnitOfWork = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => new VoucherService(nullUnitOfWork));
        }

        // ����, �� ��������, �� ������ ������� MethodAccessException, 
        // ���� ������ �������� ������� ����������� ����������. (Manager) - �� �� �������.
        [Fact]
        public void GetVouchersByVoucherType_UserIsManager_ThrowMethodAccessException()
        {
            User user = new Manager(1, "����", "�����������", GenderType.Male);
            SecurityContext.SetUser(user);

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            IVoucherService voucherService = new VoucherService(mockUnitOfWork.Object);

            // Assert
            Assert.Throws<MethodAccessException>(() => voucherService.GetVouchersByVoucherType(VoucherType.WellnessVacation, 0));
        }

        // ����, �� ��������, �� ��������� ���������� ����������� ������� � ���������� � ��'���� VoucherDTO.
        [Fact]
        public void GetVouchersByVoucherType_VoucherFromDAL_CorrectMappingToVoucherDTO()
        {
            User user = new Admin(1, "����", "�����������", GenderType.Male);
            SecurityContext.SetUser(user);

            var VoucherService = GetVoucherService();

            // Act  ������ ������ ��������� VoucherDTO 
            var actualVoucherDto = VoucherService.GetVouchersByVoucherType(VoucherType.WellnessVacation, 0).First();

            // Assert ��������� �������� ������������ ��'���� Voucher, �� ���������� � ������� �� ���������� ��'���� VoucherDTO
            Assert.True(actualVoucherDto.VoucherId == 1 &&
                        actualVoucherDto.StartVoucher == "11.11.2023 0:00:00" &&
                        actualVoucherDto.EndVoucher == "11.12.2023 0:00:00" &&
                        actualVoucherDto.VoucherType == "WellnessVacation");
        }

        // ������ ��������� IVoucherService � ��������� ���������� ��� ����������.
        private IVoucherService GetVoucherService()
        {
            // ��������� ��������� �������� ������ �������
            var mockContext = new Mock<IUnitOfWork>();
            var expectedVoucher = new Voucher()
            {
                VoucherId = 1,
                StartVoucher = new DateTime(2023, 11, 11),
                EndVoucher = new DateTime(2023, 12, 11),
                VoucherType = VoucherType.WellnessVacation
            };

            // ��������� ��������� ���������� �������
            var mockDbSet = new Mock<IVoucherRepository>();

            // ������������, �� ����������� � ���������� ���� ���� ������ (expectedVoucher).
            mockDbSet.Setup(p => p.Find(It.IsAny<Func<Voucher, bool>>(), It.IsAny<int>(), It.IsAny<int>())).
                Returns(new List<Voucher>() { expectedVoucher });

            // ������������, �� ��� �������� �� ���������� Vouchers �� ���� ��������� mockDbSet.Object
            mockContext.Setup(context => context.Vouchers).Returns(mockDbSet.Object);

            // ��������� ��������� ������ ������� � ��������� ���������� ������� ������
            IVoucherService VoucherService = new VoucherService(mockContext.Object);

            return VoucherService;
        }
    }
}