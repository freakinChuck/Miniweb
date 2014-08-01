using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinismuriWeb._Global
{
    [Serializable]
    public class NavigationItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<NavigationItem> Children { get; set; }

        public static IEnumerable<NavigationItem> FromSitemapNode(SiteMapNode node)
        {
            if (node.Description.ToLower() == "dynamic")
            {
                var storage = Storage.GenericPageStorage.LoadStorage();
                var items = storage.DataSet.Pages
                    .Where(p => p.Published && string.IsNullOrEmpty(p.ParentId))
                    .OrderBy(r => r.Ordnungszahl)
                    .Select(r => new NavigationItem()
                    {
                        Title = r.Title,
                        Url = string.Format("/Pages/Generic/Default.aspx?id={0}", r.Id),
                        Children = storage.DataSet.Pages
                                        .Where(p => p.Published && p.ParentId == r.Id)
                                        .OrderBy(p => p.Ordnungszahl)
                                        .Select(s => 
                                            new NavigationItem
                                            {
                                                Title = s.Title,
                                                Url = string.Format("/Pages/Generic/Default.aspx?id={0}", s.Id),
                                            }
                                        ).ToList()
                    });
                return items;
            }
            else
            {
                NavigationItem item = new NavigationItem { Title = node.Title, Url = node.Url };
                item.Children = node.ChildNodes.Cast<SiteMapNode>().SelectMany(n => NavigationItem.FromSitemapNode(n)).ToList();
                return new[] { item };
            }
        }
    }
}