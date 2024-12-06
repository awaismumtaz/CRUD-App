namespace CRUD_App;

public class Concert
{
    public int Id { get; set; }
    public string Venue { get; set; }
    public string Performer { get; set; }
    public int Capacity { get; set; }
    public DateTime SpecificTime { get; set; }

    public Concert(int id, string venue, string performer, int capacity, DateTime specifictime)
    {
        Id = id;
        Venue = venue;
        Performer = performer;
        Capacity = capacity;
        SpecificTime = specifictime;
    }
    public override string ToString()
    {
        return $"ID: {Id}, Venue: {Venue}, Performer: {Performer}, Capacity: {Capacity}, Time: {SpecificTime:yyyy-MM-dd hh:mm tt}";
    }

   
}