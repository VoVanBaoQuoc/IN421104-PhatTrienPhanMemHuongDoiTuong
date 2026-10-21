using System;

namespace EduHub.Ch01.Domain
{
    public class Registration
    {
        public Guid Id { get; private set; }
        public Guid MemberId { get; private set; }
        public Guid EventId { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public RegistrationStatus Status { get; private set; }

        private Registration(Guid memberId, Guid eventId)
        {
            Id = Guid.NewGuid();
            MemberId = memberId;
            EventId = eventId;
            RegisteredAt = DateTime.Now;
            Status = RegistrationStatus.Pending;
        }

        public static Registration Create(Guid memberId, Guid eventId)
        {
            if (memberId == Guid.Empty)
                throw new ArgumentException("Mã thành viên không hợp lệ.", nameof(memberId));

            if (eventId == Guid.Empty)
                throw new ArgumentException("Mã sự kiện không hợp lệ.", nameof(eventId));

            return new Registration(memberId, eventId);
        }

        public void Confirm()
        {
            if (Status != RegistrationStatus.Pending)
                throw new InvalidOperationException($"Chỉ có thể xác nhận phiếu đang Pending. Hiện tại: {Status}");

            Status = RegistrationStatus.Confirmed;
        }

        public void Cancel()
        {
            Status = RegistrationStatus.Cancelled;
        }
    }
}