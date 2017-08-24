using SQLite;
using System;

namespace GroupGift.Models
{
    public class Gift2
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set;}
        public DateTime Date { get; set; }
        public bool IsFunded { get; set; }
        public bool IsComplete { get; set; }
        public bool IsArchived { get; set; }
    }
}
