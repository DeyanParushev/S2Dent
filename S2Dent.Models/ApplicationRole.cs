namespace S2Dent.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
            : base(null)
        { }

        public ApplicationRole(string name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
        }
    }
}
