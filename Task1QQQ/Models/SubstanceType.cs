﻿namespace Task1QQQ.Models
{
    public class SubstanceType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }
}
