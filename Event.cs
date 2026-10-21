using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHub.Ch01.Domain
{
    public class Event
    {
        public Guid Id { get; private set; }
        public Guid ClubId { get; private set; } // Khoá ngoại liên kết với Club[cite: 42, 65]
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public int Capacity { get; private set; }
        public bool IsOnline { get; private set; }

        private Event(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
        {
            Id = Guid.NewGuid();
            ClubId = clubId;
            Title = title;
            Date = date;
            Capacity = capacity;
            IsOnline = isOnline;
        }

        public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline = false)
        {
            if (clubId == Guid.Empty)
                throw new ArgumentException("Mã câu lạc bộ không hợp lệ.", nameof(clubId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Tiêu đề sự kiện không được trống.", nameof(title));

            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa phải lớn hơn 0."); //[cite: 47]

            return new Event(clubId, title.Trim(), date, capacity, isOnline);
        }

        // Kiểm tra sức chứa[cite: 47]
        public bool HasCapacity(int registeredCount) => registeredCount < Capacity;
    }
}
