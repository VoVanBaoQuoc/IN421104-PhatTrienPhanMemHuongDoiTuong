# Bao Cao Phan Tich Vi Pham Nguyen Ly SOLID Trong Lop ClubManager

## Gioi thieu
Tai lieu nay ghi nhan ket qua phan tich cac loi vi pham nguyen ly thiet ke SOLID trong lop ClubManager (thuoc du an khoi dau EduHub Chuong 2). Lop ClubManager duoc thiet ke theo dang "God Class" (lop om dom qua nhieu trach nhiem), gay kho khan cho viec bao tri, mo rong va viet kiem thu tu dong.

---

## 1. Vi pham nguyen ly Don trach nhiem (Single Responsibility Principle - SRP)

- Vi tri ma nguon: Phuong thuc CreateClub (dong 35 - 64) va toan bo cau truc lop ClubManager.
- Hien trang vi pham:
  Phuong thuc CreateClub dong thoi thuc hien 4 nhiem vu hoan toan khac nhau:
  1. Kiem tra tinh hop le cua du lieu dau vao (ten cau lac bo khong rong, do dai khong qua 100 ky tu).
  2. Tao moi va luu tru thuc the Club vao danh sach noi bo (_clubs.Add).
  3. Ghi nhat ky hoat dong truc tiep ra tap tin van ban (File.AppendAllText).
  4. Phan tich dinh dang email de gui thong bao ra man hinh dong lenh.
- Hau qua ky thuat:
  Lop co qua nhieu ly do de bi thay doi. Khi co bat ky yeu cau nao thay doi (doi quy tac kiem tra ten, doi noi luu tru du lieu, doi cau truc tap tin log, hoac doi co che gui thong bao), nha phat trien deu phai can thiep va chinh sua cung mot lop nay, lam tang nguy co sinh loi cheo.
- Huong khac phuc:
  - Tach logic kiem tra tinh hop le ve chinh lop thuc the Domain (phuong thuc tinh Club.Create).
  - Tach tac vu luu tru sang giao dien kho du lieu IClubRepository.
  - Tach tac vu ghi log sang giao dien IAppLogger.
  - Tach tac vu gui thong bao sang giao dien INotificationService.
  - Tao lop dich vu ClubService dong vai tro dieu phoi luong nghiep vu.

---

## 2. Vi pham nguyen ly Dong/Mo (Open/Closed Principle - OCP)

- Vi tri ma nguon: Khoi lenh kiem tra email trong CreateClub (dong 52 - 64) va phuong thuc PrintClubReport (dong 100 - 116).
- Hien trang vi pham:
  - Trong CreateClub: Su dung chuoi cau lenh if/else re nhanh de kiem tra chuoi "@gmail.com", "@yahoo.com" nham xac dinh kenh gui tin. Khi he thong can bo sung kenh thong bao moi (nhu SMS, Slack, Zalo), bat buoc phai mo ma nguon cua CreateClub ra de viet them nhanh else-if.
  - Trong PrintClubReport: Dinh dang bao cao bi gan cung vao viec xuat truc tiep ra man hinh Console. Khi can xuat bao cao ra dinh dang CSV, JSON hoac HTML, phuong thuc nay buoc phai bi sua doi.
- Hau qua ky thuat:
  Vi pham tieu chi "Mo cho mo rong, dong cho sua doi". Moi lan them tinh nang moi deu phai sua ma nguon hien co, buoc phai bien dich lai toan bo va co nguy co pha vo cac chuc nang dang chay on dinh.
- Huong khac phuc:
  - Ap dung mau thiet ke Factory: Dinh nghia giao dien INotificationService va lop NotificationServiceFactory de khoi tao doi tuong thong bao tuong ung voi tung kenh dua tren tham so truyen vao ma khong can can thiep logic cua ClubService.
  - Ap dung mau thiet ke Strategy: Dinh nghia giao dien IReportStrategy cung cac chien luoc cu the nhu ConsoleReportStrategy, CsvReportStrategy de thay doi linh hoat dinh dang bao cao khi chuong trinh dang chay.

---

## 3. Vi pham nguyen ly Nghich dao phu thuoc (Dependency Inversion Principle - DIP)

- Vi tri ma nguon: Cac khai bao truong du lieu (dong 30 - 32) va viec goi I/O truc tiep (dong 48, dong 104).
- Hien trang vi pham:
  - Lop ClubManager phu thuoc truc tiep vao cac cai dat cu the cap thap: khoi tao danh sach bo nho noi bo (new List<Club>(), new List<Member>()), goi truc tiep ham he thong File.AppendAllText va Console.WriteLine ngay giua logic nghiep vu.
  - Khong thong qua bat ky tang truu tuong (interface hoac abstract class) nao.
- Hau qua ky thuat:
  - Ma nguon bi troi chat vao he dieu hanh cuc bo (tap tin vat ly va man hinh console).
  - Khong the viet Unit Test doc lap cho nghiep vu do khong the thay the ban ghi that bang doi tuong gia lap (Mock/Stub/InMemory).
  - Khong the chuyen doi co che luu tru sang co so du lieu quan he (nhu PostgreSQL, SQL Server) neu khong viet lai toan bo lop.
- Huong khac phuc:
  - Lop cap cao chi phu thuoc vao abstraction: IClubRepository, IMemberRepository, IEventRepository, IAppLogger, INotificationService.
  - Nhan toan bo cac phu thuoc tren thong qua ham khoi tao (Constructor Injection).
  - Chi khoi tao va lap ghep cac lop cu the tai diem khoi dau duy nhat cua chuong trinh (Composition Root trong Program.cs).

---

## 4. Vi pham nguyen ly Phan tach giao dien (Interface Segregation Principle - ISP)

- Vi tri ma nguon: Toan bo danh sach phuong thuc cong khai cua lop ClubManager.
- Hien trang vi pham:
  Lop ClubManager gom toan bo cac phuong thuc cua nhieu phan he khac nhau vao mot noi: quan ly thong tin cau lac bo, quan ly thong tin thanh vien, quan ly to chuc su kien, dang ky tham gia, diem danh va ket xuat bao cao thong ke.
- Hau qua ky thuat:
  Cac thanh phan goi (client) chi co nhu cau su dung mot chuc nang nho (vi du: may Kiosk chi can goi chuc nang diem danh CheckIn) lai bi ep buoc phai phu thuoc va nhin thay toan bo cac phuong thuc quan ly con lai.
- Huong khac phuc:
  - Phan ra giao dien thanh cac interface nho gon, doc lap va tap trung theo tung nhom nghiep vu:
    - IClubService: Chi phuc vu cac thao tac quan ly cau lac bo.
    - IEventService: Chi phuc vu viec tao su kien, dang ky va diem danh.
    - IReportService: Chi phuc vu viec ket xuat bao cao.
    - Cac kho du lieu phan tach theo tung thuc the: IClubRepository, IMemberRepository, IEventRepository.