[<NextFs.NextFsEntry("app/robots.js", Default="robots")>]
module App.Robots

open NextFs

let robots () =
    MetadataRoute.Robots.create [
        MetadataRoute.Robots.rulesMany [
            MetadataRoute.RobotsRule.create [
                MetadataRoute.RobotsRule.userAgent "*"
                MetadataRoute.RobotsRule.allow "/"
                MetadataRoute.RobotsRule.disallow "/api/"
            ]
        ]
        MetadataRoute.Robots.sitemap "https://example.com/sitemap.xml"
    ]
