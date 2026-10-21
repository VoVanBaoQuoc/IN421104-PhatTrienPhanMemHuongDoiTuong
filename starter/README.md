# EduHub Chuong 2: Tai Cau Truc Ma Nguon Voi SOLID Va Design Patterns

Hoc phan: IN4211 - Phat trien phan mem huong doi tuong
Giang vien: ThS. Nguyen Tuan Kiet
Sinh vien: Vo Van Bao Quoc
Ma so sinh vien: 0024419350
Lop: DHCNTT24E-IT

---

## 1. Muc tieu de tai
Du an thuc hien tai cau truc (refactoring) lop ClubManager tu mot lop qua tai trach nhiem (God Class) sang kien truc phan tach ro rang, dap ung day du cac nguyen ly SOLID va 4 mau thiet ke GoF cot loi.

---

## 2. Tien do va ket qua thuc hien cac nhiem vu

### Nhiem vu 1: Phan tich vi pham SOLID (Da hoan thanh)
Da hoan thanh tai lieu phan tich chi tiet tai tap tin docs/solid-analysis.md, chi ro 4 vi pham co ban trong lop ClubManager:
- Vi pham SRP: Phuong thuc CreateClub dong thoi thuc hien kiem tra du lieu, luu tru, ghi tap tin va gui thong bao.
- Vi pham OCP: Su dung chuoi if/else de phan loai email gui thong bao va gan cung dinh dang Console trong xuat bao cao.
- Vi pham DIP: Phuong thuc nghiep vu goi truc tiep cac ham he thong File.AppendAllText va Console.WriteLine thay vi thong qua abstraction.
- Vi pham ISP: Lop ClubManager tap trung moi phuong thuc quan ly toan he thong vao mot noi duy nhat.

### Nhiem vu 2: Tai cau truc lop va phu thuoc qua Interface (Da hoan thanh)
He thong duoc phan tach thanh cac lop va interface doc lap:
- Lop dich vu (Services): ClubService, EventService, ReportService di kem cac giao dien IClubService, IEventService, IReportService.
- Lop luu tru (Repositories): IClubRepository, IMemberRepository, IEventRepository cung cac ban cai dat trong bo nho InMemoryClubRepository, InMemoryMemberRepository, InMemoryEventRepository.
- Lop ghi nhat ky (Logging): IAppLogger di kem ConsoleLogger va FileLogger.
- Diem lap ghep (Composition Root): File Program.cs la noi duy nhat su dung tu khoa new de khoi tao va truyen phu thuoc qua ham khoi tao (Constructor Injection).

### Nhiem vu 3: Cai dat 4 mau thiet ke (Da hoan thanh)
1. Factory Pattern:
   - Muc dich: Khoi tao kenh thong bao phu hop theo tham so kenh truyen vao.
   - Thanh phan: INotificationService, INotificationServiceFactory, NotificationServiceFactory, EmailNotificationService, SmsNotificationService, SlackNotificationService.
2. Decorator Pattern:
   - Muc dich: Bo sung hanh vi ghi nhat ky thao tac luu tru ma khong can can thiep sua ma nguon cua kho du lieu goc va khong dung ke thua.
   - Thanh phan: LoggingClubRepository boc ben ngoai IClubRepository.
3. Strategy Pattern:
   - Muc dich: Cho phep thay doi dinh dang xuat bao cao linh hoat luc chuong trinh dang chay.
   - Thanh phan: IReportStrategy, ConsoleReportStrategy, CsvReportStrategy.
4. Observer Pattern:
   - Muc dich: Tu dong thong bao cho cac ben quan tam khi mot su kien moi duoc tao ra.
   - Thanh phan: IEventObserver, EventService, LogNotifier, EmailNotifier.

### Nhiem vu 4: Kiem thu tu dong DomainTests voi xUnit (Chua thuc hien)
- Tinh trang: Chua thuc hien.
- Ke hoach: Se bo bo sung 6 unit test theo mo hinh Arrange - Act - Assert trong du an Ch02.Refactor.Tests de kiem thu logic cua lop Domain (Club, Event) va kiem tra cac truong hop bien, ngoai le nghiep vu.

### Nhiem vu 5: Quy trinh Git va Pull Request (Chua thuc hien)
- Tinh trang: Chua thuc hien.
- Ke hoach: Se tach nho cac lan commit theo chuan Conventional Commits, thuc hien rebase va mo Pull Request hoan chinh voi mo ta day du.

---

## 3. Huong dan bien dich va chay chuong trinh

### Bien dich giai phap:
dotnet build starter/Ch02.Refactor.sln

### Chay ung dung mau:
dotnet run --project starter/Ch02.Refactor