using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduHub.Ch01.Domain;

namespace EduHub.Ch01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("==== HỆ THỐNG QUẢN LÝ EDUHUB ====\n");

            // Bước 1: Tạo CLB[cite: 51]
            Console.WriteLine("[1. Tạo CLB]");
            var clbTinHoc = Club.Create("CLB Tin Học", "Nơi hội tụ những người yêu công nghệ");
            Console.WriteLine($"-> Đã tạo CLB: {clbTinHoc.Name}");

            // Bước 2: Thêm 3 thành viên[cite: 51]
            Console.WriteLine("\n[2. Thêm 3 thành viên]");
            var member1 = Member.Create("Võ Văn Bảo Quốc", "quoc@dthu.edu.vn", "DH24001");
            var member2 = Member.Create("Nguyễn Văn A", "nva@dthu.edu.vn", "DH24002");
            var member3 = Member.Create("Trần Thị B", "ttb@dthu.edu.vn", "DH24003");

            clbTinHoc.AddMember(member1);
            clbTinHoc.AddMember(member2);
            clbTinHoc.AddMember(member3);
            Console.WriteLine($"-> CLB hiện có {clbTinHoc.MemberCount()} thành viên.");

            // Bước 3: Tạo sự kiện[cite: 51]
            Console.WriteLine("\n[3. Tạo sự kiện]");
            var eventDotNet = Event.Create(clbTinHoc.Id, "Workshop .NET 10 Cơ Bản", DateTime.Now.AddDays(5), 2, false);
            Console.WriteLine($"-> Đã tạo sự kiện: {eventDotNet.Title} (Sức chứa: {eventDotNet.Capacity} người)");

            // Bước 4: Đăng ký (Pending)[cite: 51]
            Console.WriteLine("\n[4. Đăng ký sự kiện]");
            var reg1 = Registration.Create(member1.Id, eventDotNet.Id);
            var reg2 = Registration.Create(member2.Id, eventDotNet.Id);
            var regList = new List<Registration> { reg1, reg2 };
            Console.WriteLine($"-> {member1.Name} vừa đăng ký, trạng thái: {reg1.Status}");

            // Bước 5: Xác nhận (Confirmed)[cite: 51]
            Console.WriteLine("\n[5. Xác nhận đăng ký]");
            reg1.Confirm();
            reg2.Confirm();
            Console.WriteLine($"-> Phiếu của {member1.Name} đã đổi sang trạng thái: {reg1.Status}");

            // Bước 6: Check-in qua QR[cite: 51]
            Console.WriteLine("\n[6. Check-in bằng mã QR]");
            var checkIn1 = CheckIn.Create(reg1, CheckInMethod.QRCode);
            Console.WriteLine($"-> {member1.Name} đã check-in thành công lúc {checkIn1.CheckedInAt:HH:mm:ss} bằng {checkIn1.Method}");

            // Bước 7: Báo cáo tham dự[cite: 51]
            Console.WriteLine("\n[7. Báo cáo tham dự]");
            Console.WriteLine($"--- BÁO CÁO SỰ KIỆN: {eventDotNet.Title} ---");
            Console.WriteLine($"- Số lượng đăng ký: {regList.Count} / Sức chứa: {eventDotNet.Capacity}");
            Console.WriteLine($"- Danh sách đã Check-in: {member1.Name} (Bằng {checkIn1.Method})");
            Console.WriteLine($"- Danh sách vắng mặt: {member2.Name}");

            // Bước 8 (Tuỳ chọn thêm): Thử vi phạm luật nghiệp vụ để xem lỗi[cite: 66, 67]
            Console.WriteLine("\n[8. Thử vi phạm quy tắc nghiệp vụ]");
            var reg3 = Registration.Create(member3.Id, eventDotNet.Id);
            reg3.Cancel(); // Phiếu này đã bị huỷ
            Console.WriteLine($"-> {member3.Name} vừa huỷ phiếu (Trạng thái: {reg3.Status})");

            try
            {
                Console.WriteLine("-> Ban tổ chức cố tình gọi Confirm() để xác nhận phiếu đã bị huỷ...");
                reg3.Confirm(); // Thử xác nhận lại phiếu đã huỷ[cite: 67]
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"-> [HỆ THỐNG BÁO LỖI]: {ex.Message}"); // Sẽ in ra lời báo lỗi và không bị sập app[cite: 67]
            }
            Console.ReadKey();
        }
    }
}
