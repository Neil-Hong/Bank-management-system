using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass1
{
    class Account
    {
        private int _accountNumber;
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _phoneNumber;
        private string _email;
        private double _accountBalance;

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string Address { get => _address; set => _address = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Email { get => _email; set => _email = value; }
        public int AccountNumber { get => _accountNumber; set => _accountNumber = value; }
        public double AccountBalance { get => _accountBalance; set => _accountBalance = value; }

        public double AccountWithdraw(double money)
        {
            if (money > 0)
            {
                if (money <= _accountBalance)
                {
                    _accountBalance -= money;
                    return _accountBalance;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public double AccountDesposit(double money)
        {
            if (money > 0)
            {
                _accountBalance += money;
                return _accountBalance;
            }
            else
            {
                return -1;
            }
        }

        public void SetPhoneNumber(string phone)
        {
            if (phone.Length > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(phone), "phone length should no more than 10 characters...");
            }
            //PhoneNumber = _phoneNumber;
        }

    }
}
