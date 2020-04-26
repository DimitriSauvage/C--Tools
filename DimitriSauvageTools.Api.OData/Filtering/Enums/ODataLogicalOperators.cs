using System.ComponentModel;

namespace DimitriSauvageTools.Api.OData.Filtering.Enums
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
