namespace Interpreter
{
    public enum ValueType
    {
        String,
        Double,
        Bool,
        Int,
        Null, //Used for  InternalType in ValueHelper in Values, except Arrays
        Array
    }
}
