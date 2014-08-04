using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb._Global
{
    public partial class NavigationWebUserControl : System.Web.UI.UserControl
    {
        public string CurrentUrl
        {
            get { return Request.Url.PathAndQuery.ToLower(); }
        }

        private List<NavigationItem> siteMapNavigationNodes
        {
            get
            {
                return ViewState["SiteMapNavigationNodes"] as List<NavigationItem>;
            }
            set
            {
                ViewState["SiteMapNavigationNodes"] = value;
            }
        }
        public List<NavigationItem> SiteMapNavigationNodes
        {
            get
            {
                if (siteMapNavigationNodes == null)
                {
                    List<NavigationItem> nodes = SiteMap.RootNode.ChildNodes.Cast<SiteMapNode>().Where(n => n.Description != "NoDisplay").SelectMany(n => NavigationItem.FromSitemapNode(n)).ToList();
                    var node = SiteMap.RootNode;
                    //node..ChildNodes = new SiteMapNodeCollection();
                    nodes.Insert(0, NavigationItem.FromSitemapNode(node).First());
                    siteMapNavigationNodes =  nodes;

                    AdjustNavigationWithDynamicValue();

                }
                return siteMapNavigationNodes;
            }
        }

        private void AdjustNavigationWithDynamicValue()
        {
            foreach (var item in SiteMapNavigationNodes)
            {
                if (item.Url.Contains("Pages/Bildergalerie/Default.aspx"))
                {
                    AddBildergalerieItems(item);
                }
            }
        }
        private void AddBildergalerieItems(NavigationItem node)
        {
            var storage = Storage.DataStorage.LoadStorage();
            if (!Directory.Exists(Server.MapPath("~/UploadData/Bildergalerie")))
            {
                Directory.CreateDirectory(Server.MapPath("~/UploadData/Bildergalerie"));
            }
            foreach (var item in new System.IO.DirectoryInfo(Server.MapPath("~/UploadData/Bildergalerie")).GetDirectories().OrderByDescending(d => d.CreationTime))
            {
                if (storage.DataSet.FreigegebeneGalerien.Any(g => g.Name.ToLower() == item.Name.ToLower()))
                {
                    node.Children.Add(
                        new NavigationItem()
                        { 
                            Title = item.Name,
                            Url = string.Format("/Pages/Bildergalerie/Default.aspx?Galerie={0}", item.Name)
                        });    
                }
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (var node in SiteMapNavigationNodes)
            {
                AddCell(node);                    
            }

            AdjustLastAndFirstClass();

        }

        private void AdjustLastAndFirstClass()
        {
            if (SiteMapNavigationNodes[0].Url.ToLower() != CurrentUrl.ToLower() && (SiteMapNavigationNodes[0].Children.All(s => s.Url.ToLower() != CurrentUrl.ToLower()) || SiteMapNavigationNodes[0].Url.ToLower() == SiteMap.RootNode.Url.ToLower()))
            {
                ((Panel)mainNavigationTableRow.Cells[0].Controls[0]).CssClass = "NavigationItemPanel FirstNavigationItemPanel";                
            }
            else
            {
                ((Panel)mainNavigationTableRow.Cells[0].Controls[0]).CssClass = "NavigationItemPanelSelected FirstNavigationItemPanelSelected";
            }
            mainNavigationTableRow.Cells[0].Width = new Unit(67, UnitType.Pixel);
            ((Panel)mainNavigationTableRow.Cells[0].Controls[0]).Width = new Unit(67, UnitType.Pixel);
            //mainNavigationTableRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            if (SiteMapNavigationNodes[SiteMapNavigationNodes.Count - 1].Url.ToLower() != CurrentUrl.ToLower() && (SiteMapNavigationNodes[SiteMapNavigationNodes.Count - 1].Children.All(s => s.Url.ToLower() != CurrentUrl.ToLower()) || SiteMapNavigationNodes[SiteMapNavigationNodes.Count - 1].Url.ToLower() == SiteMap.RootNode.Url.ToLower()))
            {
                ((Panel)mainNavigationTableRow.Cells[mainNavigationTableRow.Cells.Count - 1].Controls[0]).CssClass = "NavigationItemPanel LastNavigationItemPanel";
            }
            else
            {
                ((Panel)mainNavigationTableRow.Cells[mainNavigationTableRow.Cells.Count - 1].Controls[0]).CssClass = "NavigationItemPanelSelected LastNavigationItemPanelSelected";
            }
            mainNavigationTableRow.Cells[mainNavigationTableRow.Cells.Count - 1].Width = new Unit(64, UnitType.Pixel);
            ((Panel)mainNavigationTableRow.Cells[mainNavigationTableRow.Cells.Count - 1].Controls[0]).Width = new Unit(64, UnitType.Pixel);
            for (int i = 1; i < mainNavigationTableRow.Cells.Count - 1; i++)
            {
                mainNavigationTableRow.Cells[i].Width = new Unit(100 / (mainNavigationTableRow.Cells.Count - 2), UnitType.Percentage);
            }
        }

        private void AddCell(NavigationItem node)
        {
            Panel panel = new Panel();
            panel.ID = Guid.NewGuid().ToString();
            //panel.BackColor = System.Drawing.Color.Red;
            if (node.Url.ToLower() != CurrentUrl.ToLower() && (node.Children.All(s => s.Url.ToLower() != CurrentUrl.ToLower()) || node.Url.ToLower() == SiteMap.RootNode.Url.ToLower()))
            {
                panel.CssClass = "NavigationItemPanel";
            }
            else
            {
                panel.CssClass = "NavigationItemPanelSelected";
            }
            
            panel.HorizontalAlign = HorizontalAlign.Center;

            HyperLink link = new HyperLink();
            link.Text = node.Title;
            link.NavigateUrl = node.Url;
            panel.Controls.Add(link);
            
            var childnodes = node.Children;
            if (node.Title == SiteMap.RootNode.Title)
            {
                childnodes = new List<NavigationItem>();                
            }

            if (childnodes.Count > 0)
            {
                AjaxControlToolkit.HoverMenuExtender menuExtender = new AjaxControlToolkit.HoverMenuExtender();
                menuExtender.TargetControlID = panel.ID;
                Panel hoverPanel = new Panel();
                hoverPanel.ID = Guid.NewGuid().ToString();
                hoverPanel.BackColor = System.Drawing.Color.FromArgb(55, 55, 55);
                hoverPanel.Style[HtmlTextWriterStyle.Padding] = "10px 30px 10px 10px";
                menuExtender.PopupControlID = hoverPanel.ID;
                menuExtender.PopupPosition = AjaxControlToolkit.HoverMenuPopupPosition.Bottom;
                foreach (var child in childnodes)
                {
                    HyperLink childLink = new HyperLink();
                    childLink.Text = child.Title;
                    childLink.NavigateUrl = child.Url;
                    childLink.Style[HtmlTextWriterStyle.MarginRight] = "30px;";
                    childLink.Style[HtmlTextWriterStyle.TextDecoration] = "none";
                    childLink.Style[HtmlTextWriterStyle.Color] = "#FFFFFF";
                    childLink.Style[HtmlTextWriterStyle.MarginBottom] = "5px";
                    hoverPanel.Controls.Add(childLink);
                    hoverPanel.Controls.Add(new Literal() { Text = "<br/>" });
                }
                navigationExtraStuffPlaceholder.Controls.Add(hoverPanel);
                navigationExtraStuffPlaceholder.Controls.Add(menuExtender);
                
            }

            var cell = new TableCell();
            cell.Attributes["onclick"] = string.Format("window.location = '{0}'", node.Url);
            cell.Controls.Add(panel);
            mainNavigationTableRow.Cells.Add(cell);
        }
    }
}