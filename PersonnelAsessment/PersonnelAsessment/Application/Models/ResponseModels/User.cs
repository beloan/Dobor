﻿namespace Application.Models.ResponseModels
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool IsActivated { get; set; }
    }
}
