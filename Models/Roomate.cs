using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Models
{
    // C# representation of the Roommate table
    public class Roommate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RentPortion { get; set; }
        public DateTime MovedInDate { get; set; }
        public Room Room { get; set; }

        public Roommate(string firstName, string lastName, int rentPortion, DateTime movedInDate, Room roomId)
        {
            FirstName = firstName;
            LastName = lastName;
            RentPortion = rentPortion;
            MovedInDate = movedInDate;
            Room = roomId;
        }

        public Roommate()
        {

        }

        public string Details
        {
            get
            {
                return $"{FirstName} {LastName} moved in on {MovedInDate} and will pay {RentPortion}% of rent";
            }
        }

    }
}