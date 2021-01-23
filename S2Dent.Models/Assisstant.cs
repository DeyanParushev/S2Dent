namespace S2Dent.Models
{
    using System;
    using System.Collections.Generic;

    public class Assisstant : ApplicationUser
    {
        public Assisstant()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DoctorsAssistants = new HashSet<DoctorAssisstant>();
        }
    }
}
