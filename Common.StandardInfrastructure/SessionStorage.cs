using System;
using Microsoft.AspNetCore.Http;

namespace Common.StandardInfrastructure
{
    public class SessionStorage : ISessionStorage
    {
        private readonly HttpContext _context;
        public SessionStorage()
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            _context = httpContextAccessor.HttpContext;
        }
        // the _context will be null when the deviceLogs service start to sync the EmployeeAttedanceLogs table
        public Guid UserId => _context != null ? Guid.Parse(_context.User.FindFirst(t => t.Type == "UserId")?.Value ?? Guid.Empty.ToString()) : Guid.Empty;
        public bool IsSuperAdmin => bool.Parse(_context.User.FindFirst(t => t.Type == "IsSuperAdmin")?.Value ?? "false");
        public string PrimaryLanguage => (_context?.User?.FindFirst(t => t.Type == "PrimaryLanguage")?.Value ?? "").Trim().ToLower();
        public string SecondaryLanguage => (_context?.User?.FindFirst(t => t.Type == "SecondaryLanguage")?.Value ?? "").Trim().ToLower();
        public string Token => _context.Request.Headers["Authorization"];
        public string SystemLang => _context.Request.Headers["Lang"];
    }

    public interface ISessionStorage
    {
        Guid UserId { get; }
        bool IsSuperAdmin { get; }
        string Token { get; }
        string PrimaryLanguage { get; }
        string SecondaryLanguage { get; }
        string SystemLang { get; }
    }
}
