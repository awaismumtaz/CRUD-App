// See https://aka.ms/new-console-template for more information

using CRUD_App;

static

internal class Program
{
    static List<Concert> concerts = new List<Concert>();
    public static void Main(string[] args)
    {
        
        var running = true;
        while (running)
        {
            Console.WriteLine("\nConcerts Management App");
            Console.WriteLine("1. Add a Concert");
            Console.WriteLine("2. List All Concerts");
            Console.WriteLine("3. Update a Concert");
            Console.WriteLine("4. Delete a Concert");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddConcert();
                    break;
                case "2":
                    ListConcerts();
                    break;
                case "3":
                    UpdateConcert();
                    break;
                case "4":
                    DeleteConcert();
                    break;
                case "5":
                    SaveConcerts();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

            static void AddConcert()
            {
                Console.Write("Enter Venue: ");
                string venue = Console.ReadLine();
        
                Console.Write("Enter Performer: ");
                string performer = Console.ReadLine();
        
                Console.Write("Enter Capacity: ");
                int capacity;
                while (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0)
                {
                    Console.WriteLine("Invalid input. Try again. Enter positive number");
                }
        
                Console.Write("Enter Date: ");
                DateTime specificTime;
                while (!DateTime.TryParse(Console.ReadLine(), out specificTime))
                {
                    Console.WriteLine("Invalid input.use (format: yyyy-MM-dd hh:mm tt): 2025-01-01 00:00 PM ");
                }
                int newId = concerts.Count > 0 ? concerts.Max(c => c.Id) + 1 : 1;

                concerts.Add(new Concert(newId, venue, performer, capacity, specificTime));
                Console.WriteLine("Concert added successfully!");
            }

            
            // List all concerts
            static void ListConcerts()
            {
                if (concerts.Count == 0)
                {
                    Console.WriteLine("No concerts available.");
                    return;
                }

                Console.WriteLine("\nList of Concerts:");
                foreach (var concert in concerts)
                {
                    Console.WriteLine(concert);
                }
            }

            // Update a concert
            static void UpdateConcert()
            {
                Console.Write("Enter the ID of the concert to update: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id) || !concerts.Any(c => c.Id == id))
                {
                    Console.Write("Concert not found. Enter a valid ID: ");
                }

                Concert concert = concerts.First(c => c.Id == id);

                Console.Write($"Enter new Venue (current: {concert.Venue}): ");
                string newVenue = Console.ReadLine();
                concert.Venue = string.IsNullOrEmpty(newVenue) ? concert.Venue : newVenue;

                Console.Write($"Enter new Performer (current: {concert.Performer}): ");
                string newPerformer = Console.ReadLine();
                concert.Performer = string.IsNullOrEmpty(newPerformer) ? concert.Performer : newPerformer;

                Console.Write($"Enter new Capacity (current: {concert.Capacity}): ");
                int newCapacity;
                if (int.TryParse(Console.ReadLine(), out newCapacity) && newCapacity > 0)
                {
                    concert.Capacity = newCapacity;
                }

                Console.Write($"Enter new Specific Time (current: {concert.SpecificTime:yyyy-MM-dd hh:mm tt}): ");
                DateTime newSpecificTime;
                if (DateTime.TryParse(Console.ReadLine(), out newSpecificTime))
                {
                    concert.SpecificTime = newSpecificTime;
                }

                Console.WriteLine("Concert updated successfully!");
            }

            static void DeleteConcert()
            {
                Console.WriteLine("\nDelete a Concert");
            }

            static void SaveConcerts()
            {
                Console.WriteLine("\nSave all Concerts");
            }
    
        }
    }
}


