using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDeskVNext.Services;
using Microsoft.AspNet.Mvc;
using Twilio;
using Twilio.TwiML;

namespace HelpDeskVNext.Controllers
{
    [Route("api/[controller]")]
    public class SmsTicketController : Controller
    {
        private readonly ISmsService _smsService;

        public SmsTicketController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        // GET: api/ticket
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ticket/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _smsService.ProcessMessage("", "");
            return "value";
        }

        // POST api/ticket
        [HttpPost]
        public void Post(string from, string body)
        {

            _smsService.ProcessMessage(from, body);

            //string smsResponse;

            //try
            //{
            //    var host = await _usersRepository.FindByPhoneNumberAsync(from);
            //    var reservation = await _reservationsRepository.FindFirstPendingReservationByHostAsync(host.Id);

            //    var smsRequest = body;
            //    reservation.Status =
            //        smsRequest.Equals("accept", StringComparison.InvariantCultureIgnoreCase) ||
            //        smsRequest.Equals("yes", StringComparison.InvariantCultureIgnoreCase)
            //        ? ReservationStatus.Confirmed
            //        : ReservationStatus.Rejected;

            //    await _reservationsRepository.UpdateAsync(reservation);
            //    smsResponse =
            //        string.Format("You have successfully {0} the reservation", reservation.Status);
            //}
            //catch (Exception)
            //{
            //    smsResponse = "Sorry, it looks like you don't have any reservations to respond to.";
            //}

            //return TwiML(Respond(smsResponse));
        }

        private static TwilioResponse Respond(string message)
        {
            var response = new TwilioResponse();
            response.Message(message);

            return response;
        }

        // PUT api/ticket/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/ticket/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
