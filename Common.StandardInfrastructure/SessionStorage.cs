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

        public Guid EmployeeId => Guid.Parse(_context.User.FindFirst(t => t.Type == "EmployeeId")?.Value ?? Guid.Empty.ToString());
        public bool IsSuperAdmin => bool.Parse(_context.User.FindFirst(t => t.Type == "IsSuperAdmin")?.Value ?? "false");
        public bool IsMobile => bool.Parse(_context.User.FindFirst(t => t.Type == "IsMobile")?.Value ?? "false");
        public Guid OrganizationId => _context != null ? _context.User.FindFirst(t => t.Type == "OrganizationId")?.Value.Trim() == "" ? Guid.Empty : Guid.Parse(_context.User.FindFirst(t => t.Type == "OrganizationId")?.Value ?? Guid.Empty.ToString()) : Guid.Empty;
        public string PrimaryLanguage => (_context?.User?.FindFirst(t => t.Type == "PrimaryLanguage")?.Value ?? "").Trim().ToLower();
        public string SecondaryLanguage => (_context?.User?.FindFirst(t => t.Type == "SecondaryLanguage")?.Value ?? "").Trim().ToLower();
        public bool IsArabic => string.IsNullOrWhiteSpace(SecondaryLanguage) ? PrimaryLanguage.Contains("ar") : _context.Request.Headers["Lang"] == "ar-EG";
        public bool IsFl => string.IsNullOrWhiteSpace(SecondaryLanguage) || (PrimaryLanguage.Contains("ar") && IsArabic) || (!PrimaryLanguage.Contains("ar") && !IsArabic);
        public string Token => _context.Request.Headers["Authorization"];
        public string SystemLang => _context.Request.Headers["Lang"];
        public Guid? LocationId => Guid.Parse(_context.User.FindFirst(t => t.Type == "LocationId")?.Value ?? Guid.Empty.ToString());
    }

    public interface ISessionStorage
    {
        Guid UserId { get; }
        Guid OrganizationId { get; }
        Guid EmployeeId { get; }
        bool IsSuperAdmin { get; }
        bool IsMobile { get; }
        string Token { get; }
        string PrimaryLanguage { get; }
        string SecondaryLanguage { get; }
        string SystemLang { get; }
        Guid? LocationId { get; }
    }
}
