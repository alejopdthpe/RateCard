using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RateCard.ApiCore.Mappers
{
    public abstract class EntityMapper
    {
        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is string)
                return (string)val;

            return "";
        }

        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            int val = Convert.ToInt32(dic[attName]);
            if (dic.ContainsKey(attName))
                return val;

            return -1;
        }

        protected double GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            double val = Convert.ToDouble(dic[attName]);
            if (dic.ContainsKey(attName))
                return val;

            return -1;
        }

        protected decimal GetDecimalValue(Dictionary<string, object> dic, string attName)
        {
            decimal val = Convert.ToDecimal(dic[attName]);
            if (dic.ContainsKey(attName))
                return val;

            return -1;
        }

        protected DateTime GetDateValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is DateTime)
                return (DateTime)dic[attName];

            return DateTime.Now;
        }
        protected byte[] GetByteArrayValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is byte[])
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, val);
                    return ms.ToArray();
                }

            }
            else
            {
                return new byte[0];
            }

        }
    }
}
