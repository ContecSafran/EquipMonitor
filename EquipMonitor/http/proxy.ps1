$port = 3000
$listener = New-Object System.Net.HttpListener
$listener.Prefixes.Add("http://localhost:$port/")
$listener.Start()
Write-Host "Proxy server running: http://localhost:$port"

while ($true) {
    $ctx = $listener.GetContext()
    $req = $ctx.Request
    $res = $ctx.Response

    $res.Headers.Add("Access-Control-Allow-Origin", "*")
    $res.Headers.Add("Access-Control-Allow-Methods", "*")
    $res.Headers.Add("Access-Control-Allow-Headers", "*")

    if ($req.HttpMethod -eq "OPTIONS") {
        $res.StatusCode = 204
        $res.Close()
        continue
    }

    $targetUrl = $req.Headers["x-target-url"]
    if (-not $targetUrl) {
        $res.StatusCode = 400
        $bytes = [System.Text.Encoding]::UTF8.GetBytes("x-target-url header missing")
        $res.ContentLength64 = $bytes.Length
        $res.OutputStream.Write($bytes, 0, $bytes.Length)
        $res.Close()
        continue
    }

    try {
        $webReq = [System.Net.WebRequest]::Create($targetUrl)
        $webReq.Method = $req.HttpMethod

        foreach ($key in $req.Headers.AllKeys) {
            if ($key -eq "x-target-url" -or $key -eq "Host") { continue }
            try { $webReq.Headers[$key] = $req.Headers[$key] } catch {}
        }

        if ($req.HasEntityBody) {
            $reqStream = $webReq.GetRequestStream()
            $req.InputStream.CopyTo($reqStream)
            $reqStream.Close()
        }

        $webRes = $webReq.GetResponse()
        $res.StatusCode = [int]$webRes.StatusCode
        $webRes.GetResponseStream().CopyTo($res.OutputStream)
        $webRes.Close()
    } catch [System.Net.WebException] {
        $ex = $_.Exception
        if ($ex.Response) {
            $res.StatusCode = [int]$ex.Response.StatusCode
            $ex.Response.GetResponseStream().CopyTo($res.OutputStream)
            $ex.Response.Close()
        } else {
            $res.StatusCode = 502
            $bytes = [System.Text.Encoding]::UTF8.GetBytes("Proxy error: $($ex.Message)")
            $res.ContentLength64 = $bytes.Length
            $res.OutputStream.Write($bytes, 0, $bytes.Length)
        }
    }

    $res.Close()
}
