FROM nginx:latest
COPY src/config/nginx.conf     /etc/nginx/nginx.conf
COPY src/config/certs/cert.pem /etc/ssl/certs/cert.pem
COPY src/config/certs/key.pem  /etc/ssl/certs/key.pem 