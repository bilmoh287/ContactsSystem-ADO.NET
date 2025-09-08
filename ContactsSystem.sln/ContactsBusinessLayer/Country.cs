using System;
using ContactsDataAccessLayer;
using System.Data;


namespace ContactsBusinessLayer
{
    public class clsCountries
    {
        public enum enMode { AddNewMode, UpdateMode };
        public enMode Mode;

        public int ID { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        public clsCountries()
        {
            this.ID = -1;
            this.CountryName = "";
            this.Code = "";
            this.PhoneCode = "";

            Mode = enMode.AddNewMode;
        }
        private clsCountries(int ID, string CountrName, string Code, string PhoneCode)
        {
            this.ID = ID;
            this.CountryName = CountrName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;

            Mode = enMode.UpdateMode;
        }

        private bool _AddNewCountry()
        {
            int ID = clsCountryData.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);
            return (ID != -1);
        }

        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountry(this.ID, this.CountryName, this.Code, this.PhoneCode);
        }

        public static clsCountries FindCountry(int ID)
        {
            string CountryName = "";
            string Code = "";
            string PhoneCode = "";
            if (clsCountryData.FindCountryByID(ID, ref CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountries(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }

        public static clsCountries FindCountry(string CountryName)
        {
            int ID = -1;
            string Code = "";
            string PhoneCode = "";

            if (clsCountryData.FindCountryByName(CountryName, ref ID, ref Code, ref PhoneCode))
            {
                return new clsCountries(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNewMode:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateMode:
                    return _UpdateCountry();
            }
            return false;
        }

        public static bool IsCountryExists(int ID)
        {
            return clsCountryData.IsCountryExist(ID);
        }

        public static bool IsCountryExists(string CountryName)
        {
            return clsCountryData.IsCountryExist(CountryName);
        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountryData.DeleteCountry(ID);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
