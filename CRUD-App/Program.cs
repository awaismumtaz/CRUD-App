// See https://aka.ms/new-console-template for more information

using CRUD_App;
internal class Program
{
    static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ConcertsData", "concerts.txt");
    static List<Concert> concerts = new List<Concert>();
    
    public static void Main(string[] args)
    {
        LoadConcerts();
        Console.WriteLine($"File path: {filePath}");
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

            // Delete a concert
            static void DeleteConcert()
            {
                Console.Write("Enter the ID of the concert to delete: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id) || !concerts.Any(c => c.Id == id))
                {
                    Console.Write("Concert not found. Enter a valid ID: ");
                }

                concerts.RemoveAll(c => c.Id == id);
                Console.WriteLine("Concert deleted successfully!");
            }

            // Save concerts to file
            static void SaveConcerts()
            {
                try
                {
                    string directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var concert in concerts)
                        {
                            writer.WriteLine($"{concert.Id}|{concert.Venue}|{concert.Performer}|{concert.Capacity}|{concert.SpecificTime:yyyy-MM-dd hh:mm tt}");
                        }
                    }
                    Console.WriteLine("Concerts saved to file.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving concerts: {ex.Message}");
                }
            }
        }
        // Load concerts from file
        static void LoadConcerts()
        {
            try
            {
                if (!File.Exists(filePath)) return; // No file to load

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        concerts.Add(new Concert(
                            int.Parse(parts[0]),
                            parts[1],
                            parts[2],
                            int.Parse(parts[3]),
                            DateTime.Parse(parts[4])
                        ));
                    }
                }

                Console.WriteLine("Concerts loaded from file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading concerts: {ex.Message}");
            }
        }

    }
        
    }


