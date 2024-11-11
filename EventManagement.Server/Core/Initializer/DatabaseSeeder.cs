using EventManagement.Server.Core.Entities;
using EventManagementApp.Server.Core.Interfaces;
using MongoDB.Driver;

namespace EventManagementApp.Server.Core.Initializer
{
    public class DatabaseSeeder
    {
        private readonly IMongoCollectionFactory _collectionFactory;

        public DatabaseSeeder(IMongoCollectionFactory collectionFactory)
        {
            _collectionFactory = collectionFactory;
        }

        public void SeedDb()
        {
            SeedUsers();
            SeedVenues();
            SeedEvents();
            SeedReservations();
        }

        private void SeedUsers()
        {
            var usersCollection = _collectionFactory.GetCollection<User>("Users");

            if (usersCollection.CountDocuments(FilterDefinition<User>.Empty) == 0)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Tocan Alexandru",
                    Email = "alexandru.tocan@isa.utm.md",
                    PhoneNumber = "079452001"
                };

                usersCollection.InsertOne(user);
            }
        }

        private void SeedVenues()
        {
            var venuesCollection = _collectionFactory.GetCollection<Venue>("Venues");

            if (venuesCollection.CountDocuments(FilterDefinition<Venue>.Empty) == 0)
            {
                var venues = new List<Venue>
                {
                    new Venue { Id = Guid.NewGuid(), Name = "Palatul Național", Address = "Strada Pușkin 21, Chișinău", Capacity = 1000 },
                    new Venue { Id = Guid.NewGuid(), Name = "Filarmonica Națională", Address = "Strada Mitropolit Varlaam 78, Chișinău", Capacity = 600 },
                    new Venue { Id = Guid.NewGuid(), Name = "Arena Chișinău", Address = "Strada Chișinău-Orhei 12, Chișinău", Capacity = 4500 }
                };

                venuesCollection.InsertMany(venues);
            }
        }

        private void SeedEvents()
        {
            var eventsCollection = _collectionFactory.GetCollection<Event>("Events");

            if (eventsCollection.CountDocuments(FilterDefinition<Event>.Empty) == 0)
            {
                var events = new List<Event>
                {
                    new Event
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tech Conference 2024",
                        Description = "A conference about the latest in tech.",
                        StartDate = DateTime.Now.AddMonths(1),
                        EndDate = DateTime.Now.AddMonths(1).AddDays(1),
                        VenueId = GetVenueIdByName("Filarmonica Națională"),
                        Capacity = 500,
                        AvailableSeats = 500
                    },
                    new Event
                    {
                        Id = Guid.NewGuid(),
                        Name = "Music Festival",
                        Description = "A day full of music and fun.",
                        StartDate = DateTime.Now.AddMonths(2),
                        EndDate = DateTime.Now.AddMonths(2).AddDays(2),
                        VenueId = GetVenueIdByName("Arena Chișinău"),
                        Capacity = 1000,
                        AvailableSeats = 1000
                    },
                    new Event
                    {
                        Id = Guid.NewGuid(),
                        Name = "Art Exhibition",
                        Description = "Exhibition of modern art.",
                        StartDate = DateTime.Now.AddMonths(3),
                        EndDate = DateTime.Now.AddMonths(3).AddDays(1),
                        VenueId = GetVenueIdByName("Palatul Național"),
                        Capacity = 300,
                        AvailableSeats = 300
                    }
                };

                eventsCollection.InsertMany(events);
            }
        }

        private void SeedReservations()
        {
            var reservationsCollection = _collectionFactory.GetCollection<Reservation>("Reservations");

            if (reservationsCollection.CountDocuments(FilterDefinition<Reservation>.Empty) == 0)
            {
                var reservations = new List<Reservation>
                {
                    new Reservation
                    {
                        Id = Guid.NewGuid(),
                        EventId = GetEventIdByName("Tech Conference 2024"),
                        UserId = GetUserIdByEmail("alexandru.tocan@isa.utm.md"),
                        ReservationDate = DateTime.Now,
                        NumberOfSeats = 2
                    },
                    new Reservation
                    {
                        Id = Guid.NewGuid(),
                        EventId = GetEventIdByName("Music Festival"),
                        UserId = GetUserIdByEmail("alexandru.tocan@isa.utm.md"),
                        ReservationDate = DateTime.Now,
                        NumberOfSeats = 4
                    }
                };

                reservationsCollection.InsertMany(reservations);
            }
        }

        private Guid GetVenueIdByName(string name)
        {
            var venueCollection = _collectionFactory.GetCollection<Venue>("Venues");
            var venue = venueCollection.Find(v => v.Name == name).FirstOrDefault();
            return venue?.Id ?? Guid.Empty;
        }

        private Guid GetEventIdByName(string name)
        {
            var eventCollection = _collectionFactory.GetCollection<Event>("Events");
            var eventItem = eventCollection.Find(e => e.Name == name).FirstOrDefault();
            return eventItem?.Id ?? Guid.Empty;
        }

        private Guid GetUserIdByEmail(string email)
        {
            var userCollection = _collectionFactory.GetCollection<User>("Users");
            var user = userCollection.Find(u => u.Email == email).FirstOrDefault();
            return user?.Id ?? Guid.Empty;
        }
    }
}
