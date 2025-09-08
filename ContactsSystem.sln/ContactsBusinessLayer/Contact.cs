using System;
using System.Data;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using ContactsBusinessLayer;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public enum enMode { AddNewMode, UpdateMode};
        public enMode Mode = enMode.AddNewMode;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int CountryID { get; set; }
        
        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.ImagePath = "";
            this.CountryID = -1;

            Mode = enMode.AddNewMode;
        }

        private clsContact(int ID, string FirstName, string LastName, string Email, string Phonne, string Address, 
            DateTime DateOfBirth, string ImagePath, int CountryID)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phonne;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.ImagePath = ImagePath;
            this.CountryID = CountryID;

            Mode = enMode.UpdateMode;
        }

        private bool _AddNewContact()
        {
            this.ID = clsContactData.AddNewContact(this.FirstName, this.LastName, this.Email, this.Phone,
                this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);

            return (this.ID != -1);
        }
        public static clsContact Find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;

            if (clsContactData.GetContactInfoByID(ID, ref FirstName, ref LastName, ref Email,
            ref Phone, ref Address, ref DateOfBirth, ref ImagePath, ref CountryID))
            {
                return new clsContact(ID, FirstName, LastName, Email, Phone, Address,
            DateOfBirth, ImagePath, CountryID);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateContact()
        {
            return clsContactData.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone,
                this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
        }

        public bool Save()

        {
            switch (Mode)
            {
                case enMode.AddNewMode:
                    if(_AddNewContact())
                    {
                        Mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateMode:
                    return _UpdateContact();
            }
            return false;
        }

        public static bool DeleteContact(int ID)
        {
            return clsContactData.DeleteContact(ID);
        }

        public static DataTable GetAllContact()
        {
            return clsContactData.GetAllContacts();
        }

        public static bool IsContactExist(int ID)
        {
            return clsContactData.IsContactExist(ID);
        }
    }

}

