using Newtonsoft.Json.Linq;

namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
    public static class YCRecaptchaGoogle
    {
        public static bool Validar(ref JCHNETReCaptchaGoogleDto post, ref string mensaje)
        {
            if (post.ReCaptchaClaveSitioWeb != null && post.ReCaptchaClaveComGoogle != null)
            {
                post.EncodedResponse = YCConversions.NothingToString(post.EncodedResponse);
                if ((YCConversions.NothingToString(post.ValorReCaptcha) != post.EncodedResponse.Length.ToString()) || post.EncodedResponse.Length == 0)
                {
                    if (!ReCaptchaGoogleReply(post, ref mensaje))
                    {
                        post.ValorReCaptcha = "0";
                        return false;
                    }
                    else
                    {
                        post.ReCaptchaCont = 0;
                        post.ValorReCaptcha = post.EncodedResponse.Length.ToString();
                    }
                }
                else
                {
                    post.ReCaptchaCont += 1;
                }
            }
            if (post.ReCaptchaCont >= 10)
            {
                post.ReCaptchaCont = 0;
                post.ValorReCaptcha = null;
                return false;
            }
            return true;
        }

        private static bool ReCaptchaGoogleReply(JCHNETReCaptchaGoogleDto post, ref string mensaje)
        {
            try
            {
                bool Respuesta = false;
                if (post.ReCaptchaClaveSitioWeb == null | post.ReCaptchaClaveComGoogle == null)
                    return true;
                if (YCConversions.NothingToString(post.EncodedResponse) == "")
                {
                    mensaje = "Comprueba que eres Humano: Validar Captcha.".ToUpper();
                    return false;
                }
                System.Net.WebClient webClient = new System.Net.WebClient();
                string GoogleReply = null;
                try
                {
                    GoogleReply = webClient.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", post.ReCaptchaClaveComGoogle, post.EncodedResponse));
                }
                catch (System.Exception)
                {
                }
                JObject ser = JObject.Parse(GoogleReply);
                List<JToken> data = ser.Children().ToList();
                foreach (JProperty item in data)
                {
                    item.CreateReader();
                    switch (item.Name)
                    {
                        case "success":
                            {
                                Respuesta = Convert.ToBoolean(item.Value);
                                break;
                            }

                        case "error-codes":
                            {
                                foreach (JValue i in item)
                                {
                                    if (i.Value.ToString() == "missing-input-secret")
                                        mensaje = "El parámetro secreto no se encuentra.";
                                    else if (i.Value.ToString() == "invalid-input-secret")
                                        mensaje = "El parámetro secreto es inválida o mal formado.";
                                    else if (i.Value.ToString() == "missing-input-response")
                                        mensaje = "El parámetro de la respuesta no se encuentra.";
                                    else if (i.Value.ToString() == "invalid-input-response")
                                        mensaje = "El parámetro de respuesta es no válido o incorrecto.";
                                    else
                                        mensaje = "Captcha Inválido.";
                                }

                                break;
                            }
                    }
                }
                mensaje = "";
                if (!Respuesta)
                    mensaje = "Comprueba que eres humano: Captcha Inválido.".ToUpper();
                return Respuesta;
            }
            catch (System.Exception)
            {
                mensaje = "Comprueba que eres humano: reCaptcha Inválido.".ToUpper();
                return false;
            }
        }
    }

    public sealed class JCHNETReCaptchaGoogleDto
    {
        public string ValorReCaptcha { get; set; }
        public int ReCaptchaCont { get; set; }
        public string EncodedResponse { get; set; }
        public string ReCaptchaClaveSitioWeb { get; set; }
        public string ReCaptchaClaveComGoogle { get; set; }
    }
}
