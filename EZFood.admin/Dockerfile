FROM node:22.14.0 AS build

WORKDIR /app

COPY package*.json ./

RUN npm install

RUN npm install -g @angular/cli

COPY . .

RUN ng build --configuration=production

FROM nginx:latest

COPY --from=build /app/dist/mlm.client/browser /usr/share/nginx/html
COPY docker/default.conf /etc/nginx/conf.d

EXPOSE 80
