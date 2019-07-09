using EfCoreSample.Doman.Abstraction;

namespace EfCoreSample.Doman
{
    public class Address:IEntity<long>
    {
        public long Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public long EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
