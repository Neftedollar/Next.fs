[<NextFs.NextFsEntry("app/sitemap.js", Default="sitemap")>]
module App.Sitemap

open NextFs

let sitemap () =
    [|
        MetadataRoute.SitemapEntry.create [
            MetadataRoute.SitemapEntry.url "https://example.com"
            MetadataRoute.SitemapEntry.changeFrequency MetadataRoute.SitemapChangeFrequency.Weekly
            MetadataRoute.SitemapEntry.priority 1.0
        ]
        MetadataRoute.SitemapEntry.create [
            MetadataRoute.SitemapEntry.url "https://example.com/about"
            MetadataRoute.SitemapEntry.changeFrequency MetadataRoute.SitemapChangeFrequency.Monthly
            MetadataRoute.SitemapEntry.priority 0.8
        ]
    |]
