
using Madyan.Repo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Madyan.Data
{
    //public class Header
    //{
    //    public string TransactionId { get; set; }
    //    public DateTime TransactionTime { get; set; }
    //}

    public class Basic : IEntityBase
    {

        public long BasicID { get; set; }
        public string PasNumber { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SexCode { get; set; }
        public string HomeTelephoneNumber { get; set; }

        public Nullable<long> FkPatientID { get; set; }
        public Patient FkPatient { get; set; }
    }

    public class NextOfKin : IEntityBase
    {
        public long NextOfKinID { get; set; }
        public string NokName { get; set; }
        public string NokRelationshipCode { get; set; }
        public string NokAddressLine1 { get; set; }
        public string NokAddressLine2 { get; set; }
        public string NokAddressLine3 { get; set; }
        public string NokAddressLine4 { get; set; }
        public string NokPostcode { get; set; }
        public Nullable<long> FkPatientID { get; set; }
        public Patient FkPatient { get; set; }
    }

    public class GpDetail : IEntityBase
    {
        public long GpDetailID { get; set; }
        public string GpCode { get; set; }
        public string GpSurname { get; set; }
        public string GpInitials { get; set; }
        public string GpPhone { get; set; }
        public Nullable<long> FkPatientID { get; set; }
        public Patient FkPatient { get; set; }
    }

    public class Patient : IEntityBase
    {
        public long PatientID { get; set; }
        //public long FkBasicID { get; set; }
        //public long FkNextOfKinID { get; set; }
        //public Nullable<long> FkGpDetailsID { get; set; }
        public Basic FkBasic { get; set; }
        public NextOfKin FkNextOfKin { get; set; }
        public virtual ICollection<GpDetail> GpDetailList { get; set; }

    }
    

}
