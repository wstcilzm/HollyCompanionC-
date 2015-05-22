using System;
using System.Collections.Generic;
using System.Text;

namespace HollyCompanion
{

    public class ResponseData<T>
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public string SessionID
        {
            get;
            set;
        }

        public string CommandName
        {
            get;
            set;
        }

        public T ResponseValue
        {
            get;
            set;
        }
    }
    
}
