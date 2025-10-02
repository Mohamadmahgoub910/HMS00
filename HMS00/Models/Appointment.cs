namespace HMS00.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        // Relations
        public int UserId { get; set; }
        public User User { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }

}