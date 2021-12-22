using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;
using HtmlAgilityPack;
using System.IO;
using Avalonia.Media.Imaging;
using System.Net;
using TestTask.ViewModels;

namespace TestTask.Services
{
    class PlaylistService
    {
        public HtmlDocument GetHtml(string html)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);
            return htmlDoc;
        }
        public Bitmap SetImage(HtmlDocument htmlDoc)
        {
            var albumimg = htmlDoc.DocumentNode.SelectSingleNode(@"//a[@class='main-image mfp-image']");
            string imgurl = albumimg.Attributes["href"].Value;
            using (WebClient client = new WebClient())
            {
                //client.DownloadDataAsync(new Uri(imgurl));
                    byte[] bytes = client.DownloadData(imgurl);

                    Stream stream = new MemoryStream(bytes);

                    var image = new Bitmap(stream);
                    //p.Poster = image;
                    return image;
            }
           
        }
        public List<Track> GetSongs(HtmlDocument htmlDoc)
        {
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes(@"//small[@itemprop='name']");
            var divsartist = htmlDoc.DocumentNode.SelectSingleNode(@"//a[@data-masked-href='']");
            string artist = divsartist.InnerHtml;
            List<Track> items = new List<Track>();
            
            foreach (var tag in nodes)
            {
                Track t = new Track();
                t.ArtistName = artist;
                t.TrackName = tag.InnerHtml.Trim();
                items.Add(t);
            }
            return items;
        }
        public string GetPlaylistName(HtmlDocument htmlDoc)
        {
            var playlist = htmlDoc.DocumentNode.SelectSingleNode(@"//div[@class='box title']");
            return playlist.InnerText.Trim();
        }
    }
}
