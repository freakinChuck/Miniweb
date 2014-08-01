using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        public int ZugriffeGesammt { get; set; }
        public int ZugriffeMonat { get; set; }
        public int ZugriffeWoche { get; set; }
        public int ZugriffeTag { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var statistikStorage = Storage.StatisticStorage.LoadStorage();
            ZugriffeGesammt = statistikStorage.DataSet.HitCount.Count(x => true);
            ZugriffeMonat = statistikStorage.DataSet.HitCount.Count(x => x.Datum > Time.Now.AddMonths(-1));
            ZugriffeWoche = statistikStorage.DataSet.HitCount.Count(x => x.Datum > Time.Now.AddDays(-7));
            ZugriffeTag = statistikStorage.DataSet.HitCount.Count(x => x.Datum > Time.Now.AddDays(-1));

            var storage = Storage.DataStorage.LoadStorage();
            var user = storage.DataSet.Benutzer.First(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower());
            blogLi.Visible = user.Blogschreiber;
            bildergalerieLi.Visible = user.BildergalerieFreigeber || user.BildergalerieUploader;
            gaestebuchLi.Visible = user.Gaestebuchmoderator;
            linksLi.Visible = user.Linkadmin;
            termineLi.Visible = user.Terminadmin;
            videoLi.Visible = user.Videoadministrator;
            seitenadminDiv.Visible = user.Seitenadmin;
            eventanmeldungLi.Visible = user.Eventadmin;


            var pageStorage = Storage.GenericPageStorage.LoadStorage();
            var items = pageStorage.DataSet.Pages
                .Where(p => string.IsNullOrEmpty(p.ParentId))
                .OrderBy(r => r.Ordnungszahl)
                .Select(r => new
                {
                    Title = r.Title,
                    Url = string.Format("/Pages/Generic/Default.aspx?id={0}", r.Id),
                    Published = r.Published,
                    Id = r.Id,
                    Ordnungszahl = r.Ordnungszahl,
                    User = r.Aenderungsbenutzer,
                    Date = r.Aenderungsdatum,
                    Children = pageStorage.DataSet.Pages
                                    .Where(p => p.ParentId == r.Id)
                                    .OrderBy(p => p.Ordnungszahl)
                                    .Select(s =>
                                        new 
                                        {
                                            Title = s.Title,
                                            Url = string.Format("/Pages/Generic/Default.aspx?id={0}", s.Id),
                                            Published = s.Published,
                                            Id = s.Id,
                                            User = s.Aenderungsbenutzer,
                                            Date = s.Aenderungsdatum,
                                            Ordnungszahl = s.Ordnungszahl,
                                        }
                                    ).ToList()
                }).ToList();

            if (!IsPostBack)
            {
                genericNavigationRepeater.DataSource = items.SelectMany(x =>
                {
                    List<object> data = new List<object>();
                    data.Add(new
                    {
                        Title = x.Title,
                        Url = x.Url,
                        Id = x.Id,
                        Ordnungszahl = x.Ordnungszahl,
                        Published = x.Published,
                        ParentPublished = true,
                        User = x.User,
                        Date = x.Date,
                        IsChild = false,
                        IsFirst = items.IndexOf(x) == 0,
                        IsLast = items.IndexOf(x) >= items.Count - 1,
                    });

                    foreach (var subitem in x.Children)
                    {
                        data.Add(new
                        {
                            Title = subitem.Title,
                            Url = subitem.Url,
                            Id = subitem.Id,
                            Ordnungszahl = subitem.Ordnungszahl,
                            Published = subitem.Published,
                            ParentPublished = x.Published,
                            User = subitem.User,
                            Date = subitem.Date,
                            IsChild = true,
                            IsFirst = x.Children.IndexOf(subitem) == 0,
                            IsLast = x.Children.IndexOf(subitem) >= x.Children.Count - 1,
                        });
                    }
                    return data;
                });
                genericNavigationRepeater.DataBind();
            }
        }

        protected void freigabeEntziehenLinkButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var pageStorage = Storage.GenericPageStorage.LoadStorage();
            pageStorage.DataSet.Pages.Where(p => p.Id == id).ToList().ForEach(r => r.Published = false);
            pageStorage.Save();
            Response.Redirect("Default.aspx");
        }

        protected void freigebenLinkButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var pageStorage = Storage.GenericPageStorage.LoadStorage();
            pageStorage.DataSet.Pages.Where(p => p.Id == id).ToList().ForEach(r => r.Published = true);
            pageStorage.Save();
            Response.Redirect("Default.aspx");
        }

        protected void neuerChildEintragLinkButoon_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(string.Format("Generic/Default.aspx?parent={0}", e.CommandArgument));
        }

        protected void loeschenLinkButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var storage = Storage.GenericPageStorage.LoadStorage();
            var row = storage.DataSet.Pages.Single(x => x.Id == id);


            storage.DataSet.Pages.RemovePagesRow(row);

            storage.Save();
            Response.Redirect("Default.aspx");
        }

        protected void onceDownImageButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            var storage = Storage.GenericPageStorage.LoadStorage();
            var row = storage.DataSet.Pages.Single(x => x.Id == id);

            var items = storage.DataSet.Pages.Where(s => row.ParentId == s.ParentId).ToList();

            var other = items.OrderBy(x => x.Ordnungszahl).Where(x => x.Ordnungszahl > row.Ordnungszahl).First();
            int tmp = row.Ordnungszahl;
            row.Ordnungszahl = other.Ordnungszahl;
            other.Ordnungszahl = tmp;

            storage.Save();
            Response.Redirect("Default.aspx");

        }

        protected void onceUpImageButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var storage = Storage.GenericPageStorage.LoadStorage();
            var row = storage.DataSet.Pages.Single(x => x.Id == id);

            var items = storage.DataSet.Pages.Where(s => row.ParentId == s.ParentId).ToList();

            var other = items.OrderByDescending(x => x.Ordnungszahl).Where(x => x.Ordnungszahl < row.Ordnungszahl).First();
            int tmp = row.Ordnungszahl;
            row.Ordnungszahl = other.Ordnungszahl;
            other.Ordnungszahl = tmp;

            storage.Save();
            Response.Redirect("Default.aspx");

        }
    }
}