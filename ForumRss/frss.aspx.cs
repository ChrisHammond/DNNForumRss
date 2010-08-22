using System;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Portals;
using System.Collections;
using System.Text;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Data;

namespace com.christoc.dnn.ForumRss
{
    public partial class Frss : PageBase
    {

        private static PortalSettings GetPortalSettings(int portalId)
        {
            ArrayList portalAliases = (new PortalAliasController()).GetPortalAliasArrayByPortalID(portalId);

            if (portalAliases.Count > 0)
                return new PortalSettings(-1, (PortalAliasInfo)portalAliases[0]);
            return new PortalSettings(-1, portalId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/xml";
            Response.ContentEncoding = Encoding.UTF8;
//todo: hard coded PortalId
            var ps = GetPortalSettings(0);
            var sw = new StringWriter(CultureInfo.InvariantCulture);
            var wr = new XmlTextWriter(sw);

            wr.WriteStartElement("rss");
            wr.WriteAttributeString("version", "2.0");
            wr.WriteAttributeString("xmlns:wfw", "http://wellformedweb.org/CommentAPI/");
            wr.WriteAttributeString("xmlns:slash", "http://purl.org/rss/1.0/modules/slash/");
            wr.WriteAttributeString("xmlns:dc", "http://purl.org/dc/elements/1.1/");
            wr.WriteAttributeString("xmlns:trackback", "http://madskills.com/public/xml/rss/module/trackback/");

            wr.WriteStartElement("channel");
            wr.WriteElementString("title", ps.PortalName);
            if (ps.PortalAlias.HTTPAlias.IndexOf("http://", StringComparison.OrdinalIgnoreCase) == -1)
            {
                wr.WriteElementString("link", "http://" + ps.PortalAlias.HTTPAlias);
            }
            else
            {
                wr.WriteElementString("link", ps.PortalAlias.HTTPAlias);
            }
            wr.WriteElementString("description", "RSS Feed for " + ps.PortalName);
            wr.WriteElementString("ttl", "120");


            //todo:hard coded portalid and forumid
            var dv = Data.DataProvider.Instance().GetForumPosts(2, 0).DefaultView;

            for (int i = 0; i < dv.Count; i++)
            {
                //DataRow r = dt.Rows[i];
                DataRow r = dv[i].Row;
                wr.WriteStartElement("item");

                string title = r["Subject"].ToString();
                    string description = r["Body"].ToString();
                    string threadId = r["ThreadId"].ToString();
                    string guid = r["threadId"].ToString();
                    var startDate = (DateTime)r["CreatedDate"];
                  
                    string author = r["DisplayName"].ToString();

                wr.WriteElementString("title", title);

                wr.WriteElementString("link", "http://" + ps.PortalAlias.HTTPAlias + "/Forums/forumid/2/threadid/" + threadId + "/scope/posts.aspx");

                wr.WriteElementString("description", Server.HtmlDecode(description));

                wr.WriteElementString("dc:creator", author);

                wr.WriteElementString("pubDate", startDate.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture));

                wr.WriteStartElement("guid");

                wr.WriteAttributeString("isPermaLink", "false");

                wr.WriteString(guid);

                wr.WriteEndElement();

                wr.WriteEndElement();
            }

            wr.WriteEndElement();
            wr.WriteEndElement();
            Response.Write(sw.ToString());

        }
    }
}
