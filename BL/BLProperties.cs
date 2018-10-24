using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IndecommPOC.Models;
using System.Net;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace IndecommPOC.BL
{
    public sealed class BLProperties : IBLProperties
    {
        private GenericUnitOfWork unitOfWork = new GenericUnitOfWork();
        ParsingData parsingData = null;
        private static readonly BLProperties instanceBLProeprties = new BLProperties();
        private BLProperties()
        { }

        public static BLProperties GetInstance
        {

            get { return instanceBLProeprties; }


        }
        public List<ViewModelProperties> GetPropertiesfromService()
        {

            try
            {
                List<ViewModelProperties> lstProperties = new List<ViewModelProperties>();
                string serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"].ToString();

                if (!string.IsNullOrEmpty(serviceUrl))
                {
                    var output = SubmitRequestToService(serviceUrl);

                    parsingData = JsonConvert.DeserializeObject<ParsingData>(output);

                    foreach (var property in parsingData.Properties)
                    {
                        ViewModelProperties properties = new ViewModelProperties();


                        properties.PropertyId = property.Id;
                        if (property.Physical != null)
                        {
                            properties.YearBuilt = Convert.ToInt32(property.Physical.YearBuilt);
                        }
                        if (property.Financial != null)
                        {
                            properties.ListPrice = Convert.ToInt32(property.Financial.ListPrice);
                            properties.MonthlyRent = Convert.ToInt32(property.Financial.MonthlyRent);
                            properties.GrossYield = Convert.ToString((properties.MonthlyRent * 12) / properties.ListPrice) + "%";
                        }

                        if (property.Address != null)
                        {
                            properties.Address1 = property.Address.Address1 ?? string.Empty;
                            properties.Address2 = property.Address.Address2 ?? string.Empty;
                            properties.City = property.Address.City ?? string.Empty;
                            properties.Country = property.Address.Country ?? string.Empty;
                            properties.District = property.Address.District ?? string.Empty;
                            properties.State = property.Address.State ?? string.Empty;
                            properties.Zip = Convert.ToInt32(property.Address.Zip);
                            properties.ZipPlus4 = Convert.ToInt32(property.Address.ZipPlus4);
                        }

                        lstProperties.Add(properties);
                    }
                }

                return lstProperties; ;


            }

            catch (Exception)
            {
                throw;
            }

        }

        public void UpdateProperty(ViewModelProperties requestProperty)
        {

            try
            {
                GenericRepository<Property> _property = unitOfWork.PropertyRepository;
                Property property = toFillProperty(requestProperty);
                Property _propertyToUpdate = _property.GetFirstorDefault(requestProperty.PropertyId);                

                if (_propertyToUpdate != null)
                {
                    _propertyToUpdate.propertyid = requestProperty.PropertyId;
                    _propertyToUpdate.yearbuilt = requestProperty.YearBuilt;
                    _propertyToUpdate.listprice = requestProperty.ListPrice;
                    _propertyToUpdate.monthlyrent = requestProperty.MonthlyRent;
                    _propertyToUpdate.grossyield = requestProperty.GrossYield.Replace("%","");
                    _propertyToUpdate.address1 = requestProperty.Address1;
                    _propertyToUpdate.address2 = requestProperty.Address2;
                    _propertyToUpdate.city = requestProperty.City;
                    _propertyToUpdate.country = requestProperty.Country;
                    _propertyToUpdate.district = requestProperty.District;
                    _propertyToUpdate.state = requestProperty.State;
                    _propertyToUpdate.zip = requestProperty.Zip;
                    _propertyToUpdate.zipplus4 = requestProperty.ZipPlus4;
                    _property.Update(_propertyToUpdate);
                }
                else
                {
                    _property.Add(property);
                }

                unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }


        }
        public void SaveProperty(ViewModelProperties requestProperty)
        {

            try
            {
                GenericRepository<Property> _property = unitOfWork.PropertyRepository;
                Property toSaveProperty = toFillProperty(requestProperty);
                if (requestProperty != null)
                {
                    _property.Add(toSaveProperty);
                    unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void DeleteProperty(ViewModelProperties requestProperty)
        {
            try
            {
                GenericRepository<Property> _property = unitOfWork.PropertyRepository;
                Property _propertyToUpdate = _property.GetFirstorDefault(requestProperty.PropertyId);
                if (_propertyToUpdate != null)
                {

                    _property.Delete(_propertyToUpdate);
                }
                else
                {
                    _property.Add(_propertyToUpdate);
                }

                unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }



        }

        public Property toFillProperty(ViewModelProperties requestviewModle)
        {
            Property property = new Property
            {
                propertyid = requestviewModle.PropertyId,
                yearbuilt = requestviewModle.YearBuilt,
                listprice = requestviewModle.ListPrice,
                monthlyrent = requestviewModle.MonthlyRent,
                grossyield = requestviewModle.GrossYield.Replace("%","").Trim(),
                address1 = requestviewModle.Address1,
                address2 = requestviewModle.Address2,
                city = requestviewModle.City,
                country = requestviewModle.Country,
                district = requestviewModle.District,
                state = requestviewModle.State,
                zip = requestviewModle.Zip,
                zipplus4 = requestviewModle.ZipPlus4
            };

            return property;

        }

        public string SubmitRequestToService(string url)
        {
            string responseText = "";
            var httpwebrequest = (HttpWebRequest)WebRequest.Create(url);
            httpwebrequest.Method = "GET";
            HttpWebResponse httpresponse = null;
            httpresponse = (HttpWebResponse)httpwebrequest.GetResponse();
            using (var reader = new System.IO.StreamReader(httpresponse.GetResponseStream(), true))
            {
                responseText = reader.ReadToEnd();
            }
            String pattern = @"[\r\t\n]";
            String replaceValue = String.Empty;

            string output = Regex.Replace(responseText, pattern, replaceValue);

            return output.Trim();
        }
    }
}