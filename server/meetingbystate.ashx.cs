﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingbystate
    /// </summary>
    public class meetingbystate : IHttpHandler
    {
        private meetingDataContext mDb = new meetingDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            var m = from meetings in mDb.T_meetings
                    orderby meetings.m_state
                    select meetings;
            var json =  JsonConvert.SerializeObject(m.Take(9));
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}