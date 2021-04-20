FROM node:lts-alpine

# make the 'app' folder the current working directory
WORKDIR /app

#Attempt to fix missing babel dependency
ENV PATH /app/node_modules/.bin:$PATH

# copy both 'package.json' and 'package-lock.json' (if available)
COPY package.json /app/package.json
COPY vue.config.js ./vue.config.js
COPY .env ./.env
COPY .env.production.local ./.env.production.local
COPY src src

# install project dependencies
RUN npm install

# Install Vue necessary for building the environment
RUN npm install -g @vue/cli-service


# BUILD APP FOR PRODUCTION WITH A MINIFIED VERSION - comment out if not necessary
# Build the development files (html, javascript and css. Will be placed in dist)
#RUN npm run build
# Install simple http server for serving production files
#RUN npm install -g http-server

#



# CHOOSE EITHER DEVELOPMENT OR PRODUCTION - comment out the other
# Run development server over https
CMD ["vue-cli-service","serve","--https"]

# Run production server over https

#CMD ["http-server", "dist", "--ssl", ""]

#RUN npm run build
#CMD http-server dist --ssl --cert $TLS_CRT --key $TLS_CRT