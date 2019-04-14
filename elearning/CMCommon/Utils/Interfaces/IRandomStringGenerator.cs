
namespace CMCommon.Utils.Interfaces
{
    public interface IRandomStringGenerator
    {
        string GenerateRandomString(int strLength);

        string GenerateRandomFromString(int strLength, string mystr);
    }
}
