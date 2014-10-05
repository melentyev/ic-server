﻿using System.Linq;
using Events.Abstract;
using Events.Models;
using System.Threading.Tasks;

namespace Events.Concrete
{
    public class EFEventsRepository :  Events.Concrete.EFRepository<Event>, IEventsRepository
    {
        public EFEventsRepository() : base ("EventId") {}
    }
}