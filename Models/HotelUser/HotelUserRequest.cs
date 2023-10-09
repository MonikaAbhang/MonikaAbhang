using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagment.Models.HotelUser
{
    public class HotelUserRequest
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string Code { get; set; }

        public string PostalCode { get; set; }
        public string PushToken { get; set; }

        //public string Code { get; set; }

        public string ipaddress { get; set; }
        public string browser { get; set; }
        public string browserversion { get; set; }
        public string osdetails { get; set; }
        public string location { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string questionid { get; set; }
        public string answer { get; set; }
        public string userid { get; set; }
        public string locale { get; set; }


    }
}
