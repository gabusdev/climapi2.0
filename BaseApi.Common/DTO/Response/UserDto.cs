﻿namespace Climapi.Common.DTO.Response
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public List<string>? Roles { get; set; }
    }
}
