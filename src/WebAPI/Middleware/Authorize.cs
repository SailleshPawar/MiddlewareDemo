using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{

    public class Authorize
    {
        int i = 0;
        private readonly RequestDelegate _next;
        static string strToken = "";
        string strTokVal = "";

        public Authorize(RequestDelegate next)
        {
            //public delegate
            _next = next;

        }

        
        public async Task Invoke(HttpContext _context)
        {
            string authHeader = _context.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                string authStr = _context.Request.Headers["Authorization"];

                authStr = authStr.Trim();
                if (authStr.IndexOf("Basic", 0) != 0 || string.IsNullOrEmpty(authStr))
                {
                    DenyAccess(_context);
                    return;

                }

                authStr = authStr.Trim();

                string encodedCredentials = authStr.Substring(5);
                byte[] decodedBytes;
                decodedBytes = Convert.FromBase64String(encodedCredentials);

                string s = new ASCIIEncoding().GetString(decodedBytes);
                string[] userPass = s.Split(new char[] { ':' });
                string username = userPass[0];
                string password = userPass[1];
                if (!(string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password)))
                {
                    if (username == "Saillesh" && password == "Saillesh@123")
                    {
                        AllowAccess(_context);
                    }
                    else
                    {
                        DenyAccess(_context);

                        return;
                    }
                }
                else
                {

                    DenyAccess(_context);
                    return;

                }

            }
            else
            {

                _context.Response.StatusCode = 401;
                _context.Response.Headers.Add("WWW-Authenticate", "Basic Authentication");

            }


        }


        private void DenyAccess(HttpContext app)
        {
            app.Response.StatusCode = 401;
            app.Response.WriteAsync("401 Access Denied");
            return;
        }

        private async void AllowAccess(HttpContext app)
        {
            app.Response.StatusCode = 200;
            await _next(app);

        }

    }
}
