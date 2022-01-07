using System.Text;

namespace MusicSite.Server.Options
{
    public class MyJwtOptions
    {
        public string Secret { get; set; }

        public double HoursExpires { get; set; }

        public byte[] GetSecretInBytes()
        {
            return Encoding.ASCII.GetBytes(Secret);
        }
    }
}
