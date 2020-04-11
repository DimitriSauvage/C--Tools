using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tools.Api.OData.Filtering.Enums
{
    [Description("Available logical operators for OData requests")]
    public enum ODataLogicalOperators
    {
        Eq,
        Ne,
        Gt,
        Ge,
        Lt,
        Le,
        And,
        Or,
        Not,
        StartsWith
    }
}
