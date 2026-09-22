const http = require('http');
const https = require('https');

const PORT = 3000;

http.createServer((req, res) => {
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', '*');
  res.setHeader('Access-Control-Allow-Headers', '*');

  if (req.method === 'OPTIONS') {
    res.writeHead(204);
    res.end();
    return;
  }

  const targetUrl = req.headers['x-target-url'];
  if (!targetUrl) {
    res.writeHead(400);
    res.end('x-target-url header missing');
    return;
  }

  const parsed = new URL(targetUrl);
  const lib = parsed.protocol === 'https:' ? https : http;

  const proxyHeaders = { ...req.headers };
  delete proxyHeaders['host'];
  delete proxyHeaders['x-target-url'];

  const options = {
    hostname: parsed.hostname,
    port: parsed.port || (parsed.protocol === 'https:' ? 443 : 80),
    path: parsed.pathname + parsed.search,
    method: req.method,
    headers: proxyHeaders,
  };

  const proxyReq = lib.request(options, (proxyRes) => {
    res.writeHead(proxyRes.statusCode, proxyRes.headers);
    proxyRes.pipe(res);
  });

  proxyReq.on('error', (e) => {
    res.writeHead(502);
    res.end('Proxy error: ' + e.message);
  });

  req.pipe(proxyReq);
}).listen(PORT, () => {
  console.log(`✓ 프록시 서버 실행 중: http://localhost:${PORT}`);
  console.log(`  이제 api_tester.html 을 열고 사용하세요.`);
});
