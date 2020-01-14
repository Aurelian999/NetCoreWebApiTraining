using StudentsAPI.Models.v2;
using System.Collections.Generic;

namespace StudentsAPI.Services.v2
{
    public class EventsService : IEventsService
    {
        private readonly List<Event> events;

        public EventsService()
        {
            events = new List<Event>();
        }

        public void Add(Event eventEntity)
        {
            lock (events)
            {
                events.Add(eventEntity);
            }
        }

        public IEnumerable<Event> Get()
        {
            return events;
        }
    }
}
