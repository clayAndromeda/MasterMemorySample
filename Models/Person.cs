using MasterMemory;
using MessagePack;
using System;

namespace MasterMemorySamples.Models
{
    [MemoryTable("person"), MessagePackObject(true)]
    public class Person
    {
        [PrimaryKey]
        public int Id { get; set; }

        public required string Name { get; set; }

        public int Age { get; set; }

        public required string Email { get; set; }

        public DateTime RegisterDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Age: {Age}, Email: {Email}, RegisterDate: {RegisterDate}";
        }
    }
}
