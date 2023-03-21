using System;

namespace Blog.Shared
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public int Expiration { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
