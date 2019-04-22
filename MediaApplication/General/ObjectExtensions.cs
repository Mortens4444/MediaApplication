namespace MediaApplication.General
{
    public static class ObjectExtensions
    {
        public static bool HasMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }

        public static void CallMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            var method = type.GetMethod(methodName);
            if (method != null)
            {
                method.Invoke(objectToCheck, null);
            }
        }
    }
}
