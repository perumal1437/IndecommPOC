using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace IndecommPOC.Models
{
    public class ViewModelProperties
    {
        public int PropertyId { get; set; }

        public int? YearBuilt { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public int? ListPrice { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public int? MonthlyRent { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string GrossYield { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address1 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address2 { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Country { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string District { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State { get; set; }

        public int? Zip { get; set; }

        public int? ZipPlus4 { get; set; }



    }



    public class ParsingData
    {
        
        public List<Properties> Properties { get; set; }
    }


    public class Properties
    {


        public int Id { get; set; }
        public string AccountId { get; set; }
        public Address Address { get; set; }
        public Physical Physical { get; set; }
        public Financial Financial { get; set; }

    }

    public class Address
    {

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ZipPlus4 { get; set; }
    }

    public class Physical
    {
        public string YearBuilt { get; set; }
    }

    public class Financial
    {
       public string ListPrice { get; set; }
        public string MonthlyRent { get; set; }
    }



}