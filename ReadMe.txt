# Generate a self-signed certificate and private key
openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout key.pem -out cert.pem -subj "/CN=apis.local.dev"

# Create a PKCS12 file (dev.pfx) that contains the private key and certificate
openssl pkcs12 -export -out dev.pfx -inkey key.pem -in cert.pem -passout pass:123456






openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -sha256 -days 365



#############  nginx.conf #############

worker_processes 1;

events { worker_connections 1024; }

http {

    sendfile on;

    upstream author-api {
        server author-api:5002;
    }

    upstream post-api {
        server post-api:6002;
    }

    # server {
    #     listen 80;
    #     server_name localhost;

    #     return 301 https://$server_name$request_uri;
    # }

    server {
        listen 443 ssl;
        server_name localhost;

        ssl_certificate     /etc/ssl/certs/localhost.crt;
        ssl_certificate_key /etc/ssl/certs/localhost.key;

        location /author-api {
            proxy_pass http://author-api;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_ssl_verify off;
        }

        location /post-api {
            proxy_pass http://post-api;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $remote_addr;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_ssl_verify off;
        }


        # location /author-api {
        #     proxy_pass         http://author-api;
        #     proxy_redirect     off;
        #     proxy_http_version 1.1;
        #     proxy_cache_bypass $http_upgrade;
        #     proxy_set_header   Upgrade $http_upgrade;
        #     proxy_set_header   Connection keep-alive;
        #     proxy_set_header   Host $host;
        #     proxy_set_header   X-Real-IP $remote_addr;
        #     proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        #     proxy_set_header   X-Forwarded-Proto $scheme;
        #     proxy_set_header   X-Forwarded-Host $server_name;
        #     proxy_ssl_verify off;
        # }

        # location /post-api {
        #     proxy_pass         http://post-api;
        #     proxy_redirect     off;
        #     proxy_http_version 1.1;
        #     proxy_cache_bypass $http_upgrade;
        #     proxy_set_header   Upgrade $http_upgrade;
        #     proxy_set_header   Connection keep-alive;
        #     proxy_set_header   Host $host;
        #     proxy_set_header   X-Real-IP $remote_addr;
        #     proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        #     proxy_set_header   X-Forwarded-Proto $scheme;
        #     proxy_set_header   X-Forwarded-Host $server_name;
        #     proxy_ssl_verify off;
        # }
    }
}