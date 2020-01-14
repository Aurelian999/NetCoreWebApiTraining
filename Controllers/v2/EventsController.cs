using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models.v2;
using StudentsAPI.Services.v2;
using System.Collections.Generic;

namespace StudentsAPI.Controllers.v2
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService eventsService;

        public EventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return eventsService.Get();
        }

    }
}
