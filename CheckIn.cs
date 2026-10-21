using System;

namespace EduHub.Ch01.Domain
{
    public class CheckIn
    {
        public Guid Id { get; private set; }
        public Guid RegistrationId { get; private set; }
        public DateTime CheckedInAt { get; private set; }
        public string Method { get; private set; }

        private CheckIn(Guid registrationId, string method)
        {
            Id = Guid.NewGuid();
            RegistrationId = registrationId;
            CheckedInAt = DateTime.Now;
            Method = method;
        }

        public static CheckIn Create(Registration registration, string method = CheckInMethod.Manual)
        {
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));

            if (registration.Status != RegistrationStatus.Confirmed)
                throw new InvalidOperationException("Chỉ phiếu đã Confirmed mới được check-in.");

            return new CheckIn(registration.Id, method);
        }
    }
}