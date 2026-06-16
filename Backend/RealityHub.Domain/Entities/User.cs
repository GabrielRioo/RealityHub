using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        // ID do usuário no Supabase Auth (UUID do Supabase)
        public Guid SupabaseUserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public bool IsBanned { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }
    }
}
