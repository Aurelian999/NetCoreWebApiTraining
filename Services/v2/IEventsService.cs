using StudentsAPI.Models.v2;
using System.Collections.Generic;

namespace StudentsAPI.Services.v2
{
    public interface IEventsService
    {
        IEnumerable<Event> Get();
        void Add(Event eventEntity);
    }
}
