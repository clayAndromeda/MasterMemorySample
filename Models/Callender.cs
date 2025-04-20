using MasterMemory;
using MessagePack;

namespace MasterMemorySamples.Models
{
    [MemoryTable("calender"), MessagePackObject(true)]
    public class Calender 
    {
        [PrimaryKey]
        public int Id { get; set; }

        public required string Date { get; set; }

        public required string Title { get; set; }
    }
}
