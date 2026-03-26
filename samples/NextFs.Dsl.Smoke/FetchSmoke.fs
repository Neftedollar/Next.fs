module NextFs.Dsl.Smoke.FetchSmoke

open NextFs
open NextFs.Dsl

let fetchNoStore () =
    async {
        let! response =
            fetch "https://example.com/api/posts" {
                noStore
            }
        ignore response.ok
    }

let fetchWithRevalidate () =
    async {
        let! response =
            fetch "https://example.com/api/posts" {
                forceCache
                revalidate 900
                tags [ "posts"; "homepage" ]
            }
        ignore response.status
    }

let fetchNeverCache () =
    async {
        let! (response: ServerFetchResponse) =
            fetch "https://example.com/api/data" {
                revalidateNever
                tag "live-data"
            }
        ignore response.headers
    }

let fetchForever () =
    async {
        let! response =
            fetch "https://example.com/api/static" {
                revalidateForever
            }
        ignore response.ok
    }
