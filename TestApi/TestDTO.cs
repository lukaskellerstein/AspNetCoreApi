using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi
{
    public class TestDTO
    {
        //Obsahuje chyby ? Je validní ?
        public bool isOK
        {
            get
            {
                if (isException)
                {
                    return false;
                }

                if (isCustomError)
                {
                    return false;
                }

                if (!isValid)
                {
                    return false;
                }

                return true;
            }
        }


        //Data
        public Object data { get; set; }


        //Obsahuje exceptions ?
        public bool isException { get; set; } = false;
        public Guid exceptionNo { get; set; }
        public List<string> exceptions { get; set; } = new List<string>();
        public string exceptionText
        {
            get
            {
                StringBuilder res = new StringBuilder();
                res.AppendLine(exceptionNo.ToString());
                foreach (var item in exceptions)
                {
                    res.AppendLine(item);
                }

                return res.ToString();
            }
            private set { }
        }

        //Neni nejaka custom chyba ?
        public bool isCustomError { get; set; } = false;
        public string customErrorText { get; set; }

        //Je validní ?
        public bool isValid { get; set; } = true;
        public Dictionary<string, string> validations { get; set; } = new Dictionary<string, string>();







        //**************************************************************
        // PUBLIC METHODS
        //**************************************************************

        public static TestDTO Ok()
        {
            TestDTO result = new TestDTO();
            result.isException = false;
            result.isValid = true;
            return result;
        }

        public static TestDTO Data(Object data)
        {
            TestDTO result = new TestDTO();
            if (data != null)
            {
                result.isException = false;
                result.isValid = true;
                result.data = data;
            }
            return result;
        }

        //exception - neocekavana vyjimka
        public static TestDTO Exception(Guid exceptionNo, string exceptionText)
        {
            TestDTO result = new TestDTO();
            result.exceptionNo = exceptionNo;
            result.isException = true;
            result.exceptions.Add(exceptionText);
            return result;
        }

        //custom error - neboli pokud ja budu chtit vyhodit chybu z vlastniho programu
        public static TestDTO CustomError(string errorText)
        {
            TestDTO result = new TestDTO();
            result.isCustomError = true;
            result.customErrorText = errorText;
            return result;
        }

        public static TestDTO ValidationError(Dictionary<string, string> validations)
        {
            TestDTO result = new TestDTO();
            if (validations != null)
            {
                result.isValid = false;
                result.validations = validations;
            }
            else
            {
                result.isValid = true;
            }
            return result;
        }

    }
}
