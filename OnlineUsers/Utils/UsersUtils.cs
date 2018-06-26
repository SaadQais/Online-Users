using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineUsers.Utils
{

    public class UsersUtils
    {
        public static List<UserInfo> AllUsers { get; set; }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public DateTime LastActive { get; set; }
    }
}
