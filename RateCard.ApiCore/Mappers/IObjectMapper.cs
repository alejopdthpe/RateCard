using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.ApiCore.Mappers
{
    internal interface IObjectMapper
    {
        List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows);
        BaseEntity BuildObject(Dictionary<string, object> row);
    }
}
