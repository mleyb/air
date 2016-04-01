using System;

namespace BlueZero.Air.Data
{
    public interface IRandomDataGenerator
    {
        string GenerateBase64(int lengthInBytes);
        byte[] GenerateBytes(int lengthInBytes);
        string GenerateString(int lengthInChars);
    }
}
