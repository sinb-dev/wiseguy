#This file builds upon the development image to produce a production image.
# sudo docker run --rm -d --name pwaptest -v "$(pwd)"/certs:/certs -e TLS_KEY=/certs/domain.key -e TLS_CRT=/certs/domain.crt -p 8080:8080 localhost:443/pwaptest
FROM wiseguy_pwa
WORKDIR /app

RUN npm run build
RUN npm install -g http-server

ENV TLS_KEY="/certs/domain.key"
ENV TLS_CRT="/certs/domain.crt"

EXPOSE 8080

CMD http-server dist --ssl --cert $TLS_CRT --key $TLS_KEY
