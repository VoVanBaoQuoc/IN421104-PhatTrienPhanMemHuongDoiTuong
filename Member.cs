using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHub.Ch01.Domain
{
    public class Member
    {
        // Tất cả thuộc tính dùng private set để bảo vệ dữ liệu[cite: 43]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string StudentCode { get; private set; }
        public DateTime JoinedAt { get; private set; }
        public MemberStatus Status { get; private set; }

        // Constructor ở mức private, chỉ cho phép class tự gọi[cite: 44]
        private Member(string name, string email, string studentCode)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            StudentCode = studentCode;
            JoinedAt = DateTime.Now;
            Status = MemberStatus.Active;
        }

        // Factory method đóng vai trò "nhà máy" tạo đối tượng[cite: 44]
        public static Member Create(string name, string email, string studentCode)
        {
            // Validation: Kiểm tra dữ liệu đầu vào[cite: 45]
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên không được bỏ trống.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("Email không hợp lệ.");

            if (string.IsNullOrWhiteSpace(studentCode))
                throw new ArgumentException("Mã số sinh viên không được bỏ trống.");

            return new Member(name.Trim(), email.Trim().ToLowerInvariant(), studentCode.Trim());
        }

        public bool IsActive() => Status == MemberStatus.Active;

        public void Deactivate()
        {
            Status = MemberStatus.Inactive;
        }
    }
}
