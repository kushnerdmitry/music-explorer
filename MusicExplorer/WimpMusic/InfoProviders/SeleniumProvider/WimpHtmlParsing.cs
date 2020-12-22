using System;
using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using MusicExplorer.Old.WimpMusic.Model;

namespace MusicExplorer.Old.WimpMusic.InfoProviders.SeleniumProvider {
    //TODO AngleSharp?
    public static class WimpHtmlParsing {
        private const string TracksTableBodySelector =
            "table.js-track-list.track-list > tbody[data-test-id='tracklist']";

        private static HtmlNode GetDocumentNode(string html) {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode;
        }

        public static IEnumerable<ShortBandInfo> BandsInfoFromSearchResults(string html) {
            return GetDocumentNode(html)
                .QuerySelectorAll("div.js-grid__item--artist.js-grid__item.grid__item--artist.grid__item")
                .Select(x => {
                    var relativeLinkNode = x.QuerySelector("a.js-grid__link.grid__interactions.js-grid__interactions");

                    var id = relativeLinkNode.GetAttributeValue("data-id", null) ??
                             throw new ArgumentNullException("id");
                    var relativeLink = relativeLinkNode.GetAttributeValue("href", null) ??
                                       throw new ArgumentNullException("link");
                    var absoluteLink = new Uri(WimpUris.MainLink, relativeLink);

                    var name = x.QuerySelector("span.grid__name--artist").InnerText;
                    var imageLink = x.QuerySelector("img.js-grid__image--artist").GetAttributeValue("src", null) ??
                                    throw new ArgumentNullException("src");

                    return new ShortBandInfo(id, name, absoluteLink, imageLink);
                });
        }

        public static DetailedBandInfo BandInfoFromDetailsPage(string html, ShortBandInfo shortInfo) {
            var documentNode = GetDocumentNode(html);

            var imageStr = documentNode.QuerySelector("div.artist-header__bg > img.artist-header__bg__img")
                .GetAttributeValue("src", null);
            var imageUrl = imageStr == null ? null : new Uri(imageStr);

            var topTracks = GetTracks(documentNode.QuerySelector(TracksTableBodySelector), true);
            var albums =
                GetAlbums(documentNode.QuerySelector("div.albumCollection__albums.js-grid"));

            return new DetailedBandInfo(shortInfo, imageUrl, albums, topTracks);
        }

        private static TrackInfo[] GetTracks(HtmlNode tracksTableBody, bool fetchAlbum = false) {
            return tracksTableBody
                .QuerySelectorAll("tr.js-tracklist__draggable.js-tracklist__row.ui-draggable.ui-draggable-handle")
                .Select(x => {
                    var numberString = x.QuerySelector("td.track-list__number").InnerText;
                    var number = Int32.Parse(numberString);

                    var title = x.QuerySelector("td.track-list__title > a").GetAttributeValue("title", null) ??
                                throw new ArgumentNullException("title");
                    var artist =
                        x.QuerySelector("td.track-list__artist[data-bind=artist_display] > a")
                            .GetAttributeValue("title", null) ?? throw new ArgumentNullException("title");

                    string albumName = null;
                    Uri absoluteAlbumLink = null;

                    if (fetchAlbum) {
                        var albumA = x.QuerySelector("td.track-list__artist[data-bind=album_display] > a");
                        albumName = albumA.GetAttributeValue("title", null) ?? throw new ArgumentNullException("title");
                        var relativeLink = albumA.GetAttributeValue("href", null) ??
                                           throw new ArgumentNullException("href");
                        absoluteAlbumLink = new Uri(WimpUris.MainLink, relativeLink);
                    }

                    var duration = x.QuerySelector("td.track-list__time[data-bind=duration_display]").InnerText;

                    return new TrackInfo(number, title, artist, albumName, absoluteAlbumLink, duration);
                })
                .ToArray();
        }

        private static ShortAlbumInfo[] GetAlbums(HtmlNode albumCollection) {
            return albumCollection
                .QuerySelectorAll("div.js-grid__item--album.js-grid__item.grid__item--album.grid__item")
                .Select(x => {
                    var id = x.GetAttributeValue("data-test-id", null) ?? throw new ArgumentNullException("id");

                    var linkAs = x.QuerySelectorAll("a[data-bind-href=albumUrl]").ToArray();

                    var imageUrlA =
                        linkAs.First(la =>
                            la.HasClass("js-grid__link") &&
                            la.HasClass("grid__interactions") &&
                            la.HasClass("js-grid__interactions") &&
                            la.HasClass("ui-draggable") &&
                            la.HasClass("ui-draggable-handle")
                        );

                    var imageUrlString = imageUrlA
                                             .QuerySelector("img")
                                             .GetAttributeValue("src", null) ?? throw new ArgumentNullException("src");

                    var imageUrl = new Uri(imageUrlString);

                    var a = linkAs.First(la => la != imageUrlA);

                    var relativeLink = a.GetAttributeValue("href", null) ?? throw new ArgumentNullException("href");
                    var absoluteLink = new Uri(WimpUris.MainLink, relativeLink);

                    var title = a.QuerySelector("span").GetAttributeValue("title", null) ??
                                throw new ArgumentNullException("title");

                    //TODO artist link
                    var artist =
                        x.QuerySelector("span.grid__title--album.grid__title__artist--album.smallText")
                            .GetAttributeValue("title", null) ?? throw new ArgumentNullException("title");

                    return new ShortAlbumInfo(id, imageUrl, absoluteLink, title, artist);
                })
                .ToArray();
        }

        public static DetailedAlbumInfo AlbumInfoFromDetailsPage(string html, ShortAlbumInfo info) {
            var documentNode = GetDocumentNode(html)
                               ?? throw new ArgumentNullException("documentNode");

            var header = documentNode.QuerySelector("div.album-header__metadata.js-album-header__metadata")
                         ?? throw new ArgumentNullException("header");

            var title = header.QuerySelector("h1.js-title.album-header__title.h2")
                            .GetAttributeValue("title", null) ?? throw new ArgumentNullException("title");

            var artistHeader = header.QuerySelector("h2.album-header__artist-name.h6");
            var artistNameContainer = artistHeader.QuerySelector("a") ?? artistHeader; // a is null for 'Various Artists'

            //TODO also artist URL?
            var artist = artistNameContainer.InnerText;

            var releaseYear = header.QuerySelector("div.album-header__release-year").InnerText;

            var tracksNumberDiv = header.QuerySelector("div.album-header__number-of-tracks");

            var numberOfTracks = tracksNumberDiv.QuerySelector("span[data-bind=numberOfTracks]").InnerText;

            var totalDuration = tracksNumberDiv.QuerySelector("span[data-bind=duration]")
                .InnerText
                .TrimStart('(')
                .TrimEnd(')');

            var tracksTableBody = documentNode.QuerySelector(TracksTableBodySelector)
                                  ?? throw new ArgumentNullException("tracksTableBody");

            var tracks = GetTracks(tracksTableBody); //FIXME can return null so needs reattempts / waiting till the end

            var copyright = documentNode.QuerySelector("small.album__copyright").InnerText;

            var imageA = documentNode.QuerySelector("div.image.image--main.image--album.js-album-cover > a");

            var largeImageStr = imageA.GetAttributeValue("href", null) ?? throw new ArgumentNullException("href");
            var largeImageUrl = new Uri(largeImageStr);

            var smallImageStr = imageA.QuerySelector("img").GetAttributeValue("src", null) ??
                                throw new ArgumentNullException("src");
            var smallImageUrl = new Uri(smallImageStr);

            return new DetailedAlbumInfo(info, tracks, title, artist, releaseYear, totalDuration, copyright,
                smallImageUrl, largeImageUrl);
        }
    }
}