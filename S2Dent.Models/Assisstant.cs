namespace S2Dent.Models
{
    using System;
    using System.Collections.Generic;

    public class Assisstant : ApplicationUser
    {
        public Assisstant()
            : base()
        {
            Id = Guid.NewGuid().ToString();
            DoctorsAssistants = new HashSet<DoctorAssisstant>();
        }
    }
}
