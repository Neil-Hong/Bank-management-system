using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass1
{
    class User
    {
        private string _userName;
        private string _passWord;
        public string UserName { get => _userName; set => _userName = value; }
        public string PassWord { get => _passWord; set => _passWord = value; }

        public override string ToString()
        {
            return string.Format("Username : " + _userName +
                                 "Password : " + _passWord);
        }
    }
}
