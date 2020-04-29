using System;

namespace cd.Domain.Infrastructure
{
    public static class GuidExtensionMethods
    {
        /// <summary>
        /// LDAP passes GUIDs as SByte Arrays.
        /// </summary>
        /// <param name="sbytes"></param>
        /// <returns></returns>
        public static Guid SByteArrayToGuid(this sbyte[] sbytes)
        {
            byte[] unsigned = new byte[sbytes.Length];
            Buffer.BlockCopy(sbytes, 0, unsigned, 0, sbytes.Length);

            return new Guid(unsigned);
        }
    }
}