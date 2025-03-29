using System;
using System.Collections.Generic;
using System.Text;

#if NETCOREAPP
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace FSNetwork
{
    public static class HttpContext
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor m_httpContextAccessor;
        private static IWebHostEnvironment m_env;
        private static IApplicationBuilder m_app;

        public static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IApplicationBuilder app)
        {
            m_httpContextAccessor = httpContextAccessor;
            m_env = env;
            m_app = app;
        }

        public static void SetEnvironment(IWebHostEnvironment env)
        {
            m_env = env;
        }

        public static void SetApplication(IApplicationBuilder app)
        {
            m_app = app;
        }

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                if (m_httpContextAccessor == null) 
                    return null;
                else
                    return m_httpContextAccessor.HttpContext;
            }
        }

        public static IWebHostEnvironment Environment
        {
            get
            {
                return m_env;
            }
        }

        public static IApplicationBuilder Application
        {
            get
            {
                return m_app;
            }
        }
    }
}

#endif
