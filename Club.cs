using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHub.Ch01.Domain
{
    public class Club
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Đóng gói danh sách thành viên, chỉ cho đọc từ bên ngoài[cite: 26, 65]
        private readonly List<Member> _members = new List<Member>();
        public IReadOnlyList<Member> Members => _members.AsReadOnly();

        private Club(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedAt = DateTime.Now;
        }

        public static Club Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên câu lạc bộ không được trống.");

            return new Club(name.Trim(), description?.Trim());
        }

        // Hành vi thêm thành viên[cite: 46]
        public void AddMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            // Chặn thêm trùng lặp thành viên[cite: 46]
            if (_members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException($"Thành viên {member.Name} đã tồn tại trong CLB.");

            _members.Add(member);
        }

        public int MemberCount() => _members.Count;
    }
}
