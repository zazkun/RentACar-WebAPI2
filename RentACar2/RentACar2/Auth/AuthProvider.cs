using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using RentACar2.Auth;
using Microsoft.Owin.Security;

namespace RentACar2.OAuth
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuthAuthorizationServerProvider sınıfının client erişimine izin verebilmek için ilgili ValidateClientAuthentication metotunu override ediyoruz.
            context.Validated();
        }

        // OAuthAuthorizationServerProvider sınıfının kaynak erişimine izin verebilmek için ilgili GrantResourceOwnerCredentials metotunu override ediyoruz.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // CORS ayarlarını set ediyoruz. -- Cross Domain yazım dan konu ile alakalı detaylı bilgi alabilirsiniz.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //validation işlemlerini ve kontrollerini bu kısımda yapıyoruz , örnek olması için sabit değerler verildi ,
            //bu kısmı db den okuyacak şekilde bir yapı kurgulanabilir.
            var uyeServis = new UyeService();
            var uye = uyeServis.UyeOturumAc(context.UserName.Equals, context.Password);

            List<string> uyeYetkileri = new List<string>();


            if (uye != null)
            {
                string yetki = "";
                if (uye.uyeAdmin == 1)
                {
                    yetki = "Admin";
                }
                else
                {
                    yetki = "Uye";
                }
                uyeYetkileri.Add(yetki);

                //eğer başarılı ise ClaimsIdentity (Kimlik oluşturuyoruz)

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));//Identity özelliklerini ekliyoruz.
                identity.AddClaim(new Claim(ClaimTypes.Role, yetki));
                identity.AddClaim(new Claim(ClaimTypes.PrimarySid, uye.uyeId.ToString()));

                AuthenticationProperties prop = new AuthenticationProperties(new Dictionary<string, string>

                {
                    {"uyeId",uye.uyeId.ToString()},
                    {"uyeKAdi",uye.kullaniciAdi},
                    {"uyeYetkileri",Newtonsoft.Json.JsonConvert.SerializeObject(uyeYetkileri)}


                });

                AuthenticationTicket ticket = new AuthenticationTicket(identity, prop);


                context.Validated(identity);//Doğrulanmış olan kimliği context e ekliyoruz.
            }
            else
            {
                //eğer hata var ise bir hata mesajı gönderiyoruz. hata ve açıklaması şeklinde.
                context.SetError("Oturum Hatası", "Kullanıcı adı ve şifre hatalıdır");
            }
        }
    }
}