using System;

namespace DoIt.Domain.Common
{
    public class AuditableEntity:Entity
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public string? LastModifiedBy { get; set; }
        
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DateDeleted { get; private set; }
        public string? DeletedBy { get; private set; }

        public void Delete(string userName)
        {
            IsDeleted = true;
            DeletedBy = userName;
            DateDeleted = DateTimeOffset.Now;
        }
    }
}