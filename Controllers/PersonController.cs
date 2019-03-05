using System.Collections.Generic;
using dotnet_core_webapi_sample.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_webapi_sample.Controller {

    [Route ("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase {

        [HttpGet]
        public IEnumerable<Person> GetAll () {

            List<Person> personsOne = new List<Person> ();
            personsOne.Add (new Person { Id = 1, Name = "ABC", Address = "Dhaka" });
            personsOne.Add (new Person { Id = 2, Name = "BCD", Address = "Mirpur" });

            // alternative of above
            List<Person> personsTwo = new List<Person> {
                new Person {
                Id = 1, Name = "Sadhan Sarker", Address = "Dhaka"
                },
                new Person { Id = 2, Name = "Ripon Sarker", Address = "Mirpur" }
            };

            return personsOne;
            //return personsTwo;
        }

    }
}