namespace VehicleTransponder.Utilities
{
    public static class ParsingUtility
    {
        public static int GetIntFromString(string stringValue)
        {
            int parsedValue = 0;
            if (int.TryParse(stringValue, out parsedValue))
            {
                return parsedValue;
            }
            else
            {
                throw new ArgumentException($"{stringValue} is not a valid integer");
            }
        }
    }
}
