namespace Master.Dto.Roles
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
    }

    public class RoleRequestModel
    {
        public required string Token { get; set; }
        public required RoleModel Data { get; set; }
    }

    public class RoleActiveDeactiveModel
    {
        public required string Token { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }
    }

    public class DBActionModel
    {
        public string? SSOID { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public string? Query { get; set; }
        public string? IPAddress { get; set; }
    }
}
