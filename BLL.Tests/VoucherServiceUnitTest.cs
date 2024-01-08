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
        // Тест, що перевіряє, чи виникає виняток ArgumentNullException, 
        // коли конструктор VoucherService отримує null в якості параметра IUnitOfWork.
        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            IUnitOfWork nullUnitOfWork = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => new VoucherService(nullUnitOfWork));
        }

        // Тест, що перевіряє, чи виникає виняток MethodAccessException, 
        // коли спроба отримати ваучери викликається менеджером. (Manager) - не має доступу.
        [Fact]
        public void GetVouchersByVoucherType_UserIsManager_ThrowMethodAccessException()
        {
            User user = new Manager(1, "Іван", "Кустовський", GenderType.Male);
            SecurityContext.SetUser(user);

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            IVoucherService voucherService = new VoucherService(mockUnitOfWork.Object);

            // Assert
            Assert.Throws<MethodAccessException>(() => voucherService.GetVouchersByVoucherType(VoucherType.WellnessVacation, 0));
        }

        // Тест, що перевіряє, чи правильно відбувається відображення ваучерів з репозиторію в об'єкти VoucherDTO.
        [Fact]
        public void GetVouchersByVoucherType_VoucherFromDAL_CorrectMappingToVoucherDTO()
        {
            User user = new Admin(1, "Іван", "Кустовський", GenderType.Male);
            SecurityContext.SetUser(user);

            var VoucherService = GetVoucherService();

            // Act  Беремо перший отриманий VoucherDTO 
            var actualVoucherDto = VoucherService.GetVouchersByVoucherType(VoucherType.WellnessVacation, 0).First();

            // Assert Порівнюємо значення властивостей об'єкту Voucher, що передається зі сховища із значеннями об'єкту VoucherDTO
            Assert.True(actualVoucherDto.VoucherId == 1 &&
                        actualVoucherDto.StartVoucher == "11.11.2023 0:00:00" &&
                        actualVoucherDto.EndVoucher == "11.12.2023 0:00:00" &&
                        actualVoucherDto.VoucherType == "WellnessVacation");
        }

        // Витягує інстанцію IVoucherService з вказаного середовища для тестування.
        private IVoucherService GetVoucherService()
        {
            // Створюємо імітований контекст робочої одиниці
            var mockContext = new Mock<IUnitOfWork>();
            var expectedVoucher = new Voucher()
            {
                VoucherId = 1,
                StartVoucher = new DateTime(2023, 11, 11),
                EndVoucher = new DateTime(2023, 12, 11),
                VoucherType = VoucherType.WellnessVacation
            };

            // Створюємо імітований репозиторій ваучерів
            var mockDbSet = new Mock<IVoucherRepository>();

            // Встановлюємо, що поверненням з репозиторію буде один ваучер (expectedVoucher).
            mockDbSet.Setup(p => p.Find(It.IsAny<Func<Voucher, bool>>(), It.IsAny<int>(), It.IsAny<int>())).
                Returns(new List<Voucher>() { expectedVoucher });

            // Встановлюємо, що при звертанні до властивості Vouchers має бути повернуто mockDbSet.Object
            mockContext.Setup(context => context.Vouchers).Returns(mockDbSet.Object);

            // Створюємо інстанцію сервісу ваучерів з імітованим контекстом одиниці роботи
            IVoucherService VoucherService = new VoucherService(mockContext.Object);

            return VoucherService;
        }
    }
}